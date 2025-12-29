using Application;
using Application_Backend;
using CustomLoggers;
using Domain.Common;
using Domain.ServiceLocators;
using Domain.UnitOfWork;
using Infrastructure.Persistence;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Respawn;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAppBlazor.Server.Services;

[SetUpFixture]
public class TestingInfrastructure
{
    public static IServiceScopeFactory ScopeFactory { get; private set; }
    public static ServiceCollection ServiceCollection { get; private set; }

    public static string DefaultUserEmail => "test@local.com";
    public static string ChefUserEmail => "chef@local.com";

    private static IConfigurationRoot _configuration;
    private static Checkpoint _checkpoint;
    private static Guid? _currentUserId;
    private static string _currentEmail;
    private static bool _scopeUpdatedInTest = false;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        string currentDirectly = Directory.GetCurrentDirectory();
        var builder = new ConfigurationBuilder()
            .SetBasePath(currentDirectly)
            .AddJsonFile("appsettings.json", true, true) // this is expected to be used for CICD pipeline
#if DEBUG
            //.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true) // this is expected to be used on local machine by Dvs
#endif

            .AddEnvironmentVariables();

        _configuration = builder.Build();

        SetupServicesForDefaultState();

        //_checkpoint = new Checkpoint
        //{
        //    TablesToIgnore = new[] { "__EFMigrationsHistory" }
        //};

        EnsureDatabase();
    }

    //private static void SetupServicesForDefaultState()
    //{
    //    var startup = new Startup(_configuration); // call startup method in the WebApp project!

    //    ServiceCollection = new ServiceCollection();

    //    ServiceCollection.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
    //        w.EnvironmentName == "Development" &&
    //        w.ApplicationName == "BlueJayClean.WebApp"));

    //    ServiceCollection.AddLogging();

    //    startup.ConfigureServices(ServiceCollection);

    //    // Replace service registration for ICurrentUserService
    //    // Remove existing registration
    //    var currentUserServiceDescriptor = ServiceCollection.FirstOrDefault(d =>
    //        d.ServiceType == typeof(ICurrentUserService));

    //    ServiceCollection.Remove(currentUserServiceDescriptor);

    //    // Register testing version
    //    UpdateScopeFactory(ServiceCollection, scopeUpdatedInTest: false);
    //}

    private static void SetupServicesForDefaultState()
    {
        //var startup = new Startup(_configuration); // call startup method in the WebApp project!

        ServiceCollection = new ServiceCollection();

        //ServiceCollection.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
        //    w.EnvironmentName == "Development" &&
        //    w.ApplicationName == "BlueJayClean.WebApp"));


        ServiceCollection.AddTransient<IConfiguration>(x => _configuration);

        ServiceCollection.AddLogging();

       // ServiceCollection.AddTransient<IHybridServiceScopeFactory, DefaultServiceScopeFactory>();
        ServiceCollection.AddApplication();
        ServiceCollection.AddApplicationBackEnd(_configuration);
        ServiceCollection.AddCustomLoggers(_configuration);
        //startup.ConfigureServices(ServiceCollection);

        // Register testing version
        UpdateScopeFactory(ServiceCollection, scopeUpdatedInTest: false);
    }

    public static void UpdateScopeFactory(ServiceCollection serviceCollection, bool scopeUpdatedInTest = true)
    {
        ScopeFactory = ServiceCollection.BuildServiceProvider().GetService<IServiceScopeFactory>();
        var serviceProviderProxy = new ServiceProviderProxy(ScopeFactory.CreateScope());
        ServiceLocator.Initialize(serviceProviderProxy);
        _scopeUpdatedInTest = scopeUpdatedInTest;
    }

    private static void EnsureDatabase()
    {
        using var scope = ScopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        //context.Database.Migrate();
    }

    //public static async Task<TResponse> SendCommandOrQueryAsync<TResponse>(IRequest<TResponse> request)
    //{
    //    //using var scope = ScopeFactory.CreateScope();

    //    var mediator = ServiceLocator.ServiceProvider.GetService<IMediator>();

    //    return await mediator.Send(request);
    //}

    /// <summary>
    /// Call this method to run as 'default user' defined for testing
    /// </summary>
    /// <returns>User ID</returns>
    //public static async Task<Guid> RunAsDefaultUserAsync()
    //{
    //    var userId = await RunAsUserAsync(DefaultUserEmail, "Testing1234!", RoleNames.Admin, lastName: "Default");

    //    await AddEntityToDatabaseAsync(new UserInfo { Id = userId });

    //    return _currentUserId.Value;


    //}

    /// <summary>
    /// Call this method to run as 'default user' defined for testing
    /// </summary>
    /// <returns>User ID</returns>
    //public static async Task<Guid> RunAsAdminUserAsync()
    //{
    //    return await RunAsUserAsync("admin@local.com", "Testing1234!", RoleNames.Admin, lastName: "Admin");
    //}


    /// <summary>
    /// Call this method during Arrange step of test to execute as a User
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns>User ID</returns>
    //public static async Task<Guid> RunAsUserAsync(string userName, string password, string roleName = RoleNames.Default, string firstName = "tester", string lastName = "one")
    //{
    //    using (var scope = ScopeFactory.CreateScope())
    //    {
    //        var uowManager = scope.ServiceProvider.GetService<IUnitOfWorkManager>();
    //        using (var uow2 = uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true }, true))
    //        {
    //            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

    //            var roleManager = scope.ServiceProvider.GetService<RoleManager<ApplicationUserRoles>>();

    //            await roleManager.CreateAsync(new ApplicationUserRoles(roleName));

    //            var user = new ApplicationUser { UserName = userName, Email = userName, FirstName = firstName, LastName = lastName };

    //            var alreadyExists = await userManager.FindByNameAsync(userName);
    //            if (alreadyExists == null)
    //            {
    //                var result = await userManager.CreateAsync(user, password);

    //                await userManager.AddToRoleAsync(user, roleName);

    //                _currentUserId = user.Id;
    //                _currentEmail = user.Email;
    //            }
    //            else
    //            {
    //                _currentUserId = alreadyExists.Id;
    //                _currentEmail = alreadyExists.Email;
    //            }
    //            await uow2.CompleteAsync();
    //        }
    //    }
    //    return _currentUserId.Value;
    //}

    public static Task ResetState()
    {
        // Reset database back to checkpoint state
        //string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //await _checkpoint.Reset(connectionString);
        _currentUserId = null;
        if (_scopeUpdatedInTest)
        {
            // service scope updated (i.e. fake was swapped in) during test.
            // Before running the next test, we need to make sure the services are returned to default state
            SetupServicesForDefaultState();
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// Finds the entity by its ID from fake repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public static async Task<TEntity> FindEntityAsync<TEntity>(Guid id)
        where TEntity : class
    {
        //using var scope = ScopeFactory.CreateScope();

        //var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        var context = ServiceLocator.ServiceProvider.GetService<IDbContextProvider<ApplicationDbContext>>().GetDbContext();

        return await context.FindAsync<TEntity>(id);
    }

    public static async Task<TEntity> FindEntityAsync<TEntity>(long? id, params Expression<Func<TEntity, object>>[] includes)
        where TEntity : AuditableEntity
    {
        //using var scope = ScopeFactory.CreateScope();

        //var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        //var uowManager = ServiceLocator.ServiceProvider.GetService<IUnitOfWorkManager>();
        //using (var uow = uowManager.Begin(new SedUnitOfWorkOptions { IsTransactional = true }))
        {
            var context = ServiceLocator.ServiceProvider.GetService<IDbContextProvider<ApplicationDbContext>>().GetDbContext();

            if (includes == null || includes.Any() == false)
            {
                return await context.FindAsync<TEntity>(id);
            }
            else
            {
                return includes.Aggregate(context.Set<TEntity>().AsNoTracking(),
                    (current, include) => current.Include(include))
                    .FirstOrDefault(x => x.Id == id);
            }
        }
    }

    public static async Task AddEntityToDatabaseAsync<TEntity>(TEntity entity)
        where TEntity : Entity
    {
        //using var scope = ScopeFactory.CreateScope();

        //var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

        var context = ServiceLocator.ServiceProvider.GetService<IDbContextProvider<ApplicationDbContext>>().GetDbContext();

        var existingEntity = context.Find<TEntity>(entity.Id);

        if (existingEntity != null)
        {
            context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }
        else
        {
            context.Add(entity);
        }

        await context.SaveChangesAsync();
    }

    //[OneTimeTearDown]
    //public void RunAfterAnyTests()
    //{

    //}
}
