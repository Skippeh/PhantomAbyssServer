using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PhantomAbyssServer.Database;
using PhantomAbyssServer.JsonConverters;
using PhantomAbyssServer.Middleware;
using PhantomAbyssServer.Services;

namespace PhantomAbyssServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                var serializerOptions = options.SerializerSettings;
                serializerOptions.Converters.Add(new UserCurrencyConverter());
            });
            
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "PhantomAbyssServer", Version = "v1"}); });

            services.AddSingleton<MaintenanceService>();
            services.AddSingleton<GlobalValuesService>();
            services.AddSingleton<RandomGeneratorService>();
            services.AddSingleton<CryptoService>();
            services.AddScoped<UserService>();
            services.AddScoped<SavedRunsService>();
            services.AddScoped<DungeonService>();

            services.AddDbContext<PAContext>(options =>
            {
                options.UseSqlite(new SqliteConnectionStringBuilder
                {
                    Mode = SqliteOpenMode.ReadWriteCreate,
                    DataSource = "persistance.db",
                    Cache = SqliteCacheMode.Shared,
                    ForeignKeys = true
                }.ToString());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhantomAbyssServer v1"));

            app.UseRouting();
            app.UseAuthorization();
            
            if (env.IsDevelopment())
            {
                app.UseMiddleware<RequestLoggingMiddleware>();
            }
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            // Create database if it doesn't exist and apply pending database migrations
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PAContext>();
                context.Database.Migrate();
            }
        }
    }
}