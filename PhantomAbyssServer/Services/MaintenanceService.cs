using Microsoft.Extensions.Configuration;

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
    }
}