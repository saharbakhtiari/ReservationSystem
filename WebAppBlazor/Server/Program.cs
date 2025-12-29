using AspNetCoreRateLimit;
using Domain.Common;
using Domain.UnitOfWork.Uow;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Infrastructure.UserAccount;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebAppBlazor.Server.Services;

namespace WebAppBlazor.Server
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                using (var scope = host.Services.CreateScope())
                {
                    // get the ClientPolicyStore instance
                    var clientPolicyStore = scope.ServiceProvider.GetRequiredService<IClientPolicyStore>();

                    // seed Client data from appsettings
                    await clientPolicyStore.SeedAsync();

                    // get the IpPolicyStore instance
                    var ipPolicyStore = scope.ServiceProvider.GetRequiredService<IIpPolicyStore>();

                    // seed IP data from appsettings
                    await ipPolicyStore.SeedAsync();



                    var services = scope.ServiceProvider;
                    ServiceLocator.Initialize(new DefaultServiceProviderProxy(services));
                    try
                    {
                        var uowManager = services.GetService<IUnitOfWorkManager>();
                        using (var uow = uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = false }))
                        {
                            //var context = services.GetRequiredService<ApplicationDbContext>();
                            var context = services.GetRequiredService<IDbContextProvider<ApplicationDbContext>>().GetDbContext();

                            //if (context.Database.IsSqlServer())
                            //{
                            //    context.Database.Migrate();
                            //}

                            var userManager = services.GetRequiredService<IUserManager>();
                            var roleManager = services.GetRequiredService<RoleManager<ApplicationUserRoles>>();
                            var permissionManager = services.GetRequiredService<MyPermissionManager>();
                            await ApplicationDbContextSeed.SeedRolesAsync(roleManager);
                            await ApplicationDbContextSeed.SeedPermissionsAsync(permissionManager, context);
                            // await ApplicationDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
                            await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                            await uow.CompleteAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                        throw;
                    }
                }
                await host.RunAsync();
            }
            catch (Exception ex)
            {

            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
