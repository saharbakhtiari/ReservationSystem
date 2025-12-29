using Domain.Books;
using Domain.Common;
using Domain.FileManager;
using Domain.MemberProfiles;
using Domain.Settings;
using Domain.UnitOfWork.Uow;
using Domain.Users;
using Extensions;
using Infrastructure.FileManager;
using Infrastructure.MemberProfiles;
using Infrastructure.News;
using Infrastructure.Persistence;
using Infrastructure.Settings;
using Infrastructure.UnitOfWork.EfCore.EntityFrameworkCore;
using Infrastructure.UnitOfWork.EfCore.Extensions.DependencyInjection;
using Infrastructure.UnitOfWork.EfCore.Uow.EntityFrameworkCore;
using Infrastructure.UserAccount;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSedDbContext<ApplicationDbContext>();
            services.Configure<SedDbContextOptions>(options =>
            {
                options.Configure(sedDbContextConfigurationContext =>
                {
                    sedDbContextConfigurationContext.DbContextOptions.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => { b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName); b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery); });
                });
            });
            services.Configure<SedDbConnectionOptions>(options =>
            {
                options.ConnectionStrings.Default = configuration.GetConnectionString("DefaultConnection");
            });
            services.AddTransient<ApplicationDbContext>();

            services.TryAddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));

            services.AddTransient<IUserMediator, ActiveDirectoryUserMediator>();
            services.AddTransient<IUserManager, UserManager>();

            services.AddTransient<ISprocRepository, SprocRepository>();

            services.AddTransient<IBookRepository, BookRepository>();

            services.AddTransient<IMemberProfileRepository, MemberProfileRepository>();



            //services.AddTransientWithName<IFileStorage, FileDBStorage>("DB");
            services.AddTransientWithName<IFileStorage, ImageDriveStorage>("Image");
            services.AddTransientWithName<IFileStorage, VoiceDriveStorage>("Voice");
            services.AddTransientWithName<IFileFilter, PdfFileFilter>("pdf");
            services.AddTransientWithName<IFileFilter, ExcelFileFilter>("xlsx");
            services.AddTransientWithName<IFileFilter, ExcelFileFilter>("xls");
            services.AddTransientWithName<IFileFilter, ExcelFileFilter>("xlt");
            //services.AddTransientWithName<IFileFilter, ExcelFileFilter>("csv");
            services.AddTransientWithName<IFileFilter, WordFileFilter>("docx");
            services.AddTransientWithName<IFileFilter, WordFileFilter>("doc");
            services.AddTransientWithName<IFileFilter, ImageFileFilter>("jpg");
            services.AddTransientWithName<IFileFilter, ImageFileFilter>("jpeg");
            services.AddTransientWithName<IFileFilter, ImageFileFilter>("png");
            services.AddTransientWithName<IFileFilter, ImageFileFilter>("svg");


            //test
            //services.AddTransient<ITestDataRepository, TestDataRepository>();
            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<ApplicationUserRoles>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var jwtSecurityKey = configuration["JwtSecurityKey"];
            services.Configure<JwtSettings>(options =>
            {
                options.Secret = jwtSecurityKey;
            });

            services.AddFileStorageConfiguration(options =>
            {
                configuration.GetSection("FileStorageConfiguration").Bind(options);
            });
            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    bearer.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            //var accessToken = context.Request.Query["access_token"];
                            var accessToken = context.Request.Cookies["X-Access-Token"];
                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/NotifyHub")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            var descriptorUser = new ServiceDescriptor(
                        typeof(IUserStore<ApplicationUser>),
                        typeof(MyUserStore<ApplicationUser>),
                        ServiceLifetime.Transient);
            services.Replace(descriptorUser);

            services.AddTransient<MyUserStore<ApplicationUser>>();

            var descriptorRole = new ServiceDescriptor(
                        typeof(IRoleStore<ApplicationUserRoles>),
                        typeof(MyRoleStore<ApplicationUserRoles>),
                        ServiceLifetime.Transient);
            services.Replace(descriptorRole);

            var descriptorUserManager = new ServiceDescriptor(
                        typeof(UserManager<ApplicationUser>),
                        typeof(MyUserManager<ApplicationUser>),
                        ServiceLifetime.Transient);
            services.Replace(descriptorUserManager);

            services.AddTransient<MyUserManager<ApplicationUser>>();

            var descriptorRoleManager = new ServiceDescriptor(
                        typeof(RoleManager<ApplicationUserRoles>),
                        typeof(MyRoleManager<ApplicationUserRoles>),
                        ServiceLifetime.Transient);
            services.Replace(descriptorRoleManager);

            var descriptorSignInManager = new ServiceDescriptor(
                        typeof(SignInManager<ApplicationUser>),
                        typeof(SignInManager<ApplicationUser>),
                        ServiceLifetime.Transient);
            services.Replace(descriptorSignInManager);

            services.AddTransient<MyRoleManager<ApplicationUserRoles>>();
            services.AddTransient<MyPermissionStore>();
            services.AddTransient<MyPermissionManager>();

            services.AddTransientWithName<IUserStoreProvider, ActiveDirectoryUserStoreProvider>(LoginProvider.ActiveDirectory.ToString());
            services.AddTransientWithName<IUserStoreProvider, BasicUserStoreProvider>(LoginProvider.BasicAuthentication.ToString());

            services.AddTransient(typeof(IJwtGeneratorService), typeof(JwtGeneratorService));
            services.AddTransient<IUserMediator, ActiveDirectoryUserMediator>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<ISettingRepository, SettingRepository>();

            return services;
        }
    }
}
