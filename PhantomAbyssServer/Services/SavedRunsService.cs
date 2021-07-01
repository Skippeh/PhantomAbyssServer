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

namespace PhantomAbyssServer.Services
{
    public class SavedRunsService
    {
        private readonly CryptoService cryptoService;
        private readonly MaintenanceService maintenanceService;
        private readonly PAContext dbContext;
        private readonly ILogger<SavedRunsService> logger;

        public SavedRunsService(CryptoService cryptoService, MaintenanceService maintenanceService, PAContext dbContext, ILogger<SavedRunsService> logger)
        {
            this.cryptoService = cryptoService;
            this.maintenanceService = maintenanceService;
            this.dbContext = dbContext;
            this.logger = logger;

            Directory.CreateDirectory("RunData");
        }
        
        /// <exception cref="DataExistsAlready">Thrown if there is already an existing run with identical run data.</exception>
        /// <exception cref="SaveFailedException">Thrown if saving the run data to disk fails.</exception>
        public async Task SaveRunData(User runner, uint dungeonId, uint routeId, uint dungeonFloorNumber, bool runSuccessful, string runData)
        {
            string hash = GetRunDataHash(runData);

            if ((await GetSavedRunFromHash(hash)) != null)
            {
                throw new DataExistsAlready("There is already a run submitted with identical run data");
            }
            
            var directoryPath = GetDirectoryPath(dungeonId, routeId, dungeonFloorNumber);
            Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, hash);

            try
            {
                await File.WriteAllTextAsync(filePath, runData, Encoding.ASCII);
            }
            catch (IOException ex)
            {
                logger.LogCritical("Could not save run data to disk: {ExceptionMessage}", ex.Message);
                throw new SaveFailedException("Could not save run data to disk", ex);
            }

            SavedRun savedRun = new()
            {
                User = runner,
                DataHash = hash,
                DungeonId = dungeonId,
                RouteId = routeId,
                DungeonFloorNumber = dungeonFloorNumber,
                RunSuccessful = runSuccessful,
                ServerVersion = maintenanceService.GetServerVersion()
            };

            dbContext.SavedRuns.Add(savedRun);
            await dbContext.SaveChangesAsync();
        }

        public string GetRunDataHash(string runData)
        {
            return cryptoService.GetMD5Hash(runData);
        }

        public Task<SavedRun> GetSavedRunFromHash(string hash)
        {
            return dbContext.SavedRuns.FirstOrDefaultAsync(run => run.DataHash == hash);
        }

        public Task<string> GetRunDataFromSavedRun(SavedRun savedRun)
        {
            string filePath = Path.Combine(GetDirectoryPath(savedRun.DungeonId, savedRun.RouteId, savedRun.DungeonFloorNumber), savedRun.DataHash);

            if (!File.Exists(filePath))
                return null;

            return File.ReadAllTextAsync(filePath, Encoding.ASCII);
        }

        private string GetDirectoryPath(uint dungeonId, uint routeId, uint dungeonFloor)
        {
            string directoryPath = $"RunData/v{maintenanceService.GetServerVersion()}-{dungeonId}-{routeId}-{dungeonFloor}/";
            return directoryPath;
        }

        public async Task<ICollection<SavedRun>> GetSavedRuns(uint dungeonId, uint routeId, uint dungeonFloorNumber)
        {
            return await dbContext.SavedRuns.Include(e => e.User).Where(run => run.DungeonId == dungeonId && run.RouteId == routeId && run.DungeonFloorNumber == dungeonFloorNumber).ToListAsync();
        }
    }
}