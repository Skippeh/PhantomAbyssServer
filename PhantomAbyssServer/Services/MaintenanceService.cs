using System;
using Microsoft.Extensions.Configuration;
using PhantomAbyssServer.Models;

namespace PhantomAbyssServer.Services
{
    public class MaintenanceService
    {
        private readonly IConfiguration configuration;

        public MaintenanceService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public int GetServerVersion()
        {
            return configuration.GetValue<int>("ServerVersion");
        }

        public MaintenanceInfo GetMaintenanceInfo()
        {
            return new()
            {
                Mode = 1,
                ServerVersion = GetServerVersion(),
                MaintenanceTimeUtc = new DateTime(0),
                NewGamesLockedOut = false
            };
        }
    }
}