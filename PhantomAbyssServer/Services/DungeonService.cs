using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhantomAbyssServer.Database;
using PhantomAbyssServer.Database.Models;
using PhantomAbyssServer.Exceptions;
using PhantomAbyssServer.Extensions;

namespace PhantomAbyssServer.Services
{
    public class DungeonService
    {
        private readonly CryptoService cryptoService;
        private readonly PAContext dbContext;
        private readonly MaintenanceService maintenanceService;
        private readonly ILogger<DungeonService> logger;
        private readonly RandomGeneratorService randomService;

        public DungeonService(CryptoService cryptoService, PAContext dbContext, MaintenanceService maintenanceService, ILogger<DungeonService> logger, RandomGeneratorService randomService)
        {
            this.cryptoService = cryptoService;
            this.dbContext = dbContext;
            this.maintenanceService = maintenanceService;
            this.logger = logger;
            this.randomService = randomService;

            Directory.CreateDirectory("LayoutData");
        }

        public Task<Route> GetRouteById(uint routeId)
        {
            return dbContext.Routes
                .Include(r => r.Dungeons).ThenInclude(d => d.CompletedBy)
                .Include(r => r.Dungeons).ThenInclude(d => d.SavedRuns)
                .Include(r => r.CompletedBy)
                .Include(r => r.CurrentUser)
                .FirstOrDefaultAsync(route => route.Id == routeId);
        }

        public Task<Dungeon> GetDungeonById(uint dungeonId)
        {
            return dbContext.Dungeons.FirstOrDefaultAsync(dungeon => dungeon.Id == dungeonId);
        }

        public async Task<Route> CreateNewRoute()
        {
            var route = new Route
            {
                Dungeons = new()
                {
                    {
                        new Dungeon
                        {
                            RouteStage = 0,
                            NumFloors = 4,
                            Seed = randomService.GetRandomInteger()
                        }
                    }
                }
            };

            dbContext.Routes.Add(route);
            await dbContext.SaveChangesAsync();

            return route;
        }

        public async Task<Dungeon> AddStageToRoute(Route route, uint routeStage)
        {
            if (route.Dungeons.Any(d => d.RouteStage == routeStage))
                throw new InvalidOperationException("There is already a dungeon for this route stage");

            Dungeon dungeon = new()
            {
                NumFloors = 4,
                RouteStage = routeStage,
                Seed = randomService.GetRandomInteger()
            };
            
            route.Dungeons.Add(dungeon);
            await dbContext.SaveChangesAsync();

            return dungeon;
        }

        public async Task<Route> GetUnfinishedRoute(User user)
        {
            List<Route> unfinishedRoutes = await dbContext.Routes
                .Include(r => r.Dungeons)
                .Include(r => r.CurrentUser)
                .Where(r => r.CurrentUser == null && r.CompletedBy == null)
                .ToListAsync();
            
            // If we wanted to restrict the user from playing the same route more than once this is where we'd check if the user has played the route before.
            // We're not doing that though, at least not right now.
            return unfinishedRoutes.GetRandomItem();
        }

        public async Task StartRoute(Route route, User user)
        {
            if (route.CurrentUser?.Id != user.Id)
            {
                route.CurrentUser = user;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task FinishRoute(User user, Route route, Dungeon dungeon)
        {
            route.CompletedBy = user;
            dungeon.CompletedBy = user;
            route.CurrentUser = null;

            await dbContext.SaveChangesAsync();
        }

        public Task<DungeonLayout> GetDungeonLayout(uint dungeonId, uint floorNumber, uint version)
        {
            return dbContext.DungeonLayouts.FirstOrDefaultAsync(layout =>
                layout.DungeonId == dungeonId &&
                layout.DungeonFloorNumber == floorNumber &&
                layout.DungeonVersion == version
            );
        }

        public Task<string> GetLayoutData(DungeonLayout dungeonLayout)
        {
            string filePath = Path.Combine(GetDirectoryPath(dungeonLayout.DungeonId, dungeonLayout.DungeonFloorNumber), dungeonLayout.LayoutDataHash);

            if (!File.Exists(filePath))
                return null;

            return File.ReadAllTextAsync(filePath, Encoding.ASCII);
        }

        /// <exception cref="DataExistsAlready">Thrown if there is already existing layout data for this dungeon floor and version.</exception>
        /// <exception cref="SaveFailedException">Thrown if saving the layout data to disk fails.</exception>
        public async Task SaveDungeonLayout(
            uint dungeonId,
            uint floorNumber,
            uint version,
            string relicId,
            uint floorCount,
            uint floorType,
            uint layoutType,
            string permanentSettingsData,
            string layoutData
        )
        {
            if ((await GetDungeonLayout(dungeonId, floorNumber, version)) != null)
                throw new DataExistsAlready("There is already a dungeon layout saved for this dungeon");

            var dungeon = await GetDungeonById(dungeonId);

            if (dungeon == null)
                throw new NotImplementedException("Dungeon not found");

            var hash = GetLayoutDataHash(layoutData);

            var directoryPath = GetDirectoryPath(dungeonId, floorNumber);
            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, hash);

            try
            {
                await File.WriteAllTextAsync(filePath, layoutData, Encoding.ASCII);
            }
            catch (IOException ex)
            {
                logger.LogCritical("Could not save layout data to disk: {ExceptionMessage}", ex.Message);
                throw new SaveFailedException("Could not save layout data to disk", ex);
            }

            DungeonLayout dungeonLayout = new()
            {
                DungeonId = dungeonId,
                DungeonFloorNumber = floorNumber,
                DungeonVersion = version,
                RelicId = relicId,
                DungeonFloorCount = floorCount,
                DungeonFloorType = floorType,
                DungeonLayoutType = layoutType,
                PermanentSettingsData = permanentSettingsData,
                LayoutDataHash = hash
            };

            dungeon.RelicId = relicId;

            dbContext.DungeonLayouts.Add(dungeonLayout);
            await dbContext.SaveChangesAsync();
        }

        public string GetLayoutDataHash(string layoutData)
        {
            return cryptoService.GetMD5Hash(layoutData);
        }

        private string GetDirectoryPath(uint dungeonId, uint dungeonFloor)
        {
            string directoryPath = $"LayoutData/v{maintenanceService.GetServerVersion()}-{dungeonId}-{dungeonFloor}/";
            return directoryPath;
        }
    }
}