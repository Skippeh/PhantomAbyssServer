using System;
using System.Collections.Generic;
using System.Linq;
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
        private GlobalValuesService globalValuesService;

        public UserService(PAContext dbContext, RandomGeneratorService randomGenerator, IConfiguration configuration, GlobalValuesService globalValuesService)
        {
            this.dbContext = dbContext;
            this.randomGenerator = randomGenerator;
            this.globalValuesService = globalValuesService;
            healthConfig = configuration.GetSection("Health");
        }

        public async Task<User> GetUserFromId(uint userId)
        {
            return await dbContext.Users
                .Include(u => u.Health)
                .Include(u => u.Currency)
                .ThenInclude(c => c.DungeonKeys)
                .Include(c => c.CurrentRoute)
                //.Include(u => u.VictoryRoutes)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserFromSteamId(string steamId)
        {
            return await dbContext.Users
                .Include(u => u.Health)
                .Include(u => u.Currency)
                .ThenInclude(c => c.DungeonKeys)
                .Include(c => c.CurrentRoute)
                //.Include(u => u.VictoryRoutes)
                .FirstOrDefaultAsync(user => user.SteamId == steamId);
        }

        public async Task<User> CreateUser(string steamId, string name, uint? userId = null)
        {
            if (userId == null && await dbContext.Users.AnyAsync(u => u.SteamId == steamId))
                throw new UserExistsAlreadyException();
            
            if (userId != null && await dbContext.Users.AnyAsync(u => u.Id == userId))
                throw new UserExistsAlreadyException();

            var user = new User
            {
                Id = userId ?? await GetUniqueUserId(),
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

        public async Task GiveCurrency(User user, uint essence, IEnumerable<DungeonKeyCurrency> dungeonKeys)
        {
            user.Currency.Essence += essence;
                
            foreach (var currency in dungeonKeys)
            {
                var dbCurrency = user.Currency.DungeonKeys.FirstOrDefault(key => key.Stage == currency.Stage);

                if (dbCurrency != null)
                    dbCurrency.NumKeys += currency.NumKeys;
            }

            await dbContext.SaveChangesAsync();
        }

        /// <summary>Resets the current route the user is running, if any</summary>
        public async Task ResetUserState(User user)
        {
            user.CurrentRoute = null;
            await dbContext.SaveChangesAsync();
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

        private async Task<uint> GetUniqueUserId()
        {
            // This is not very efficient with many users i bet, but the purpose of this server doesn't really have more than a few users at most in a normal scenario.
            // The reason we're not automatically generating an id is because it's necessary to be able to create an account with a custom id so that users can use their "live" save file without
            // editing its user id.
            List<User> users = await dbContext.Users.OrderBy(u => u.Id).ToListAsync();

            for (uint i = 1; i < globalValuesService.MaxUserId; ++i)
            {
                if (users.All(u => u.Id != i))
                    return i;
            }

            throw new InvalidOperationException("The database has reached the max number of users");
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}