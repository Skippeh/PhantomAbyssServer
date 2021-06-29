using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using PhantomAbyssServer.Models;

namespace PhantomAbyssServer.Services
{
    public class GlobalValuesService
    {
        private readonly IConfiguration configuration;

        public GlobalValuesService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public ICollection<RelicConversionReward> GetRelicConversionRewards()
        {
            return configuration.GetSection("GlobalValues").GetSection("RelicConversionRewards").Get<RelicConversionReward[]>();
        }
    }
}