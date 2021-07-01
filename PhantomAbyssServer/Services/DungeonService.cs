using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhantomAbyssServer.Database;
using PhantomAbyssServer.Database.Models;
using PhantomAbyssServer.Exceptions;

namespace PhantomAbyssServer.Services
{
    public class DungeonService
    {
        private readonly CryptoService cryptoService;
        private readonly PAContext dbContext;
        private readonly MaintenanceService maintenanceService;
        private ILogger<DungeonService> logger;

        public DungeonService(CryptoService cryptoService, PAContext dbContext, MaintenanceService maintenanceService, ILogger<DungeonService> logger)
        {
            this.cryptoService = cryptoService;
            this.dbContext = dbContext;
            this.maintenanceService = maintenanceService;
            this.logger = logger;

            Directory.CreateDirectory("LayoutData");
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
        public async Task SaveDungeonLayout(uint dungeonId,
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