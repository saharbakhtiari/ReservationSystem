using Application;
using Application_Backend;
using Application_Backend.Jobs;
using AspNetCoreRateLimit;
using CustomLoggers;
using DNTCaptcha.Core;
using Domain.Common;
using Domain.Common.Interfaces;
using Domain.UnitOfWork;
using GhasedakSmsService;
using Hangfire;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NotificationManagers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using WebAppBlazor.Server.AppSettings;
using WebAppBlazor.Server.Extensions;
using WebAppBlazor.Server.Filters;
using WebAppBlazor.Server.Localizer;
using WebAppBlazor.Server.Middleware;
using WebAppBlazor.Server.Services;

namespace WebAppBlazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplication();
            services.AddApplicationBackEnd(Configuration);
            services.AddInfrastructure(Configuration);
            services.AddCustomLoggers(Configuration);
            services.AddSeoSms(Configuration);
            services.AddRabbitMqManagers(Configuration);
            services.ConfigureCors(Configuration);

            services.AddAutoMapper(typeof(MappingProfile), typeof(Infrastructure.Common.MappingProfile), typeof(Application_Backend.Common.MappingProfile));

            #region --------------  Localizer  ---------------------
            services.AddLocalization();
            services.AddSingleton<LocalizationMiddleware>();
            services.AddDistributedMemoryCache();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
            #endregion ------------  Localizer-----------------------

            services.ConfigureApiVersion();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.ConfigureSwagger();

            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
            });

            //  api Key
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v12", new OpenApiInfo { Title = "Api Key Auth", Version = "v1" });
                c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "ApiKey must appear in header",
                    Type = SecuritySchemeType.ApiKey,
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Scheme = "ApiKeyScheme"
                });
                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    },
                    In = ParameterLocation.Header
                };
                var requirement = new OpenApiSecurityRequirement
                    {
                             { key, new List<string>() }
                    };
                c.AddSecurityRequirement(requirement);
            });

            services.AddScoped<ICurrentUserService, CurrentUserService>(); // Inject UI services
            services.AddScoped<IRequestOrginService, RequestOrginService>(); // Inject UI services

            services.AddHttpContextAccessor();

            services.AddSingleton<IServiceProviderProxy, HttpContextServiceProviderProxy>();
            services.AddTransient<IHybridServiceScopeFactory, HttpContextServiceScopeFactory>();

            services.AddSingleton<IHostedService, Runner>();

            services.AddSingleton<IBackgroundJobManager, BackgroundJobManager>();
            services.AddSingleton<IJobServiceAdmin, BackgroundJobAdmin>();

            services.AddScoped<IGetClientInfo, GetClientInfo>();
            services.Configure<AuditSetting>(options => Configuration.GetSection("AuditSettings").Bind(options));
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddRazorPages();

            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            // configure client rate limiting middleware
            services.Configure<ClientRateLimitOptions>(Configuration.GetSection("ClientRateLimiting"));
            services.Configure<ClientRateLimitPolicies>(Configuration.GetSection("ClientRateLimitPolicies"));

            // register stores
            services.AddInMemoryRateLimiting();

            services.AddDNTCaptcha(options =>
            {
                options.UseSessionStorageProvider();
                options.AbsoluteExpiration(1)
                    .RateLimiterPermitLimit(30)
                    .ShowThousandsSeparators(false)
                    .WithNoise(0.015f, 0.015f, 1, 0.0f)
                    .WithEncryptionKey("c9plK3nsf4KDqGgEqD3jkF7LrKPLTsVOMGnv")
                    .WithNonceKey("NETESCAPADES_NONCE")
                    .InputNames(new DNTCaptchaComponent
                    {
                        CaptchaHiddenInputName = "DNTCaptchaText",
                        CaptchaHiddenTokenName = "DNTCaptchaToken",
                        CaptchaInputName = "DNTCaptchaInputText",
                    })
                    .Identifier("dntCaptcha");
            });

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddControllers();

            //--------------HangFire---------------
            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            // ثبت Hangfire Server به عنوان سرویس پس‌زمینه
            services.AddHangfireServer(options =>
            {
                options.WorkerCount = 5; // تعداد پردازشگرهای همزمان
                options.Queues = new[] { "default", "critical" }; // صف‌های پردازش
                options.ServerName = "MyServer1"; // نام سرور (اختیاری)
            });
            //--------------HangFire---------------

            // ثبت ExpiredBookingManger در DI
            services.AddScoped<ExpiredBookingManger>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider sp, ILoggerFactory loggerFactory, IHostApplicationLifetime lifetime)
        {
            ServiceLocator.Initialize(sp.GetService<IServiceProviderProxy>());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseWebAssemblyDebugging();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"CMR Service v1");
                });

                #region --------------  Localizer  ---------------------
                var options = new RequestLocalizationOptions
                {
                    DefaultRequestCulture = new RequestCulture(new CultureInfo("fa-IR"))
                };
                app.UseRequestLocalization(options);
                app.UseStaticFiles();
                app.UseMiddleware<LocalizationMiddleware>();
                #endregion ------------  Localizer-----------------------
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("EnableCORS");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseIpRateLimiting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<AuditMiddleware>();

            // ========== تنظیمات Hangfire ==========
            // استفاده از داشبورد Hangfire (اختیاری)
            app.UseHangfireDashboard("/hangfire"); // میتونی آدرس دلخواه بدی

            // ثبت Job زمانبندی شده (Recurring Job)
            // این Job هر 5 دقیقه یکبار اجرا میشه
            RecurringJob.AddOrUpdate<ExpiredBookingManger>(
                "cancel-expired-bookings",
                job => job.CancelExpiredBooking(CancellationToken.None),
                "*/1 * * * *" // هر 5 دقیقه - میتونی زمان رو تغییر بدی
            );

            // اگه میخوای یه بار بلافاصله بعد از استارت اجرا بشه (اختیاری)
             BackgroundJob.Enqueue<ExpiredBookingManger>(x => x.CancelExpiredBooking(CancellationToken.None));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}