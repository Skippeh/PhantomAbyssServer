using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PhantomAbyssServer.Database;
using PhantomAbyssServer.Database.Models;
using PhantomAbyssServer.Exceptions;

namespace PhantomAbyssServer.Services
{
    public class UserService
    {
        private readonly PAContext dbContext;
        private readonly RandomGeneratorService randomGenerator;
        private readonly IConfigurationSection healthConfig;

        public UserService(PAContext dbContext, RandomGeneratorService randomGenerator, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.randomGenerator = randomGenerator;
            healthConfig = configuration.GetSection("Health");
        }

        public async Task<User> GetUserFromId(uint userId)
        {
            return await dbContext.Users
                .Include(u => u.Health)
                .Include(u => u.Currency)
                .ThenInclude(c => c.DungeonKeys)
                //.Include(u => u.VictoryRoutes)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserFromSteamId(string steamId)
        {
            return await dbContext.Users
                .Include(u => u.Health)
                .Include(u => u.Currency)
                .ThenInclude(c => c.DungeonKeys)
                //.Include(u => u.VictoryRoutes)
                .FirstOrDefaultAsync(user => user.SteamId == steamId);
        }

        public async Task<User> CreateUser(string steamId, string name)
        {
            if (await dbContext.Users.AnyAsync(u => u.SteamId == steamId))
                throw new UserAlreadyExistsException();

            var user = new User
            {
                Name = name,
                SteamId = steamId,
                SharerId = await GetUniqueSharerId(),
                Currency = new UserCurrency
                {
                    DungeonKeys = new List<DungeonKeyCurrency>
                    {
                        new() {Stage = 0, NumKeys = 0},
                        new() {Stage = 1, NumKeys = 0},
                        new() {Stage = 2, NumKeys = 0},
                        new() {Stage = 3, NumKeys = 0}
                    }
                },
                Health = new UserHealth
                {
                    BaseHealth = healthConfig.GetValue<uint>("BaseHealth", 3),
                    BonusHealth = healthConfig.GetValue<uint>("BonusHealth", 0),
                    MaxBonusHealth = healthConfig.GetValue<uint>("MaxBonusHealth", 3)
                }
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        private async Task<string> GetUniqueSharerId()
        {
            while (true)
            {
                string sharerId = randomGenerator.GenerateSharerId();

                if (await dbContext.Users.AllAsync(e => e.SharerId != sharerId))
                    return sharerId;
            }
        }
    }
}