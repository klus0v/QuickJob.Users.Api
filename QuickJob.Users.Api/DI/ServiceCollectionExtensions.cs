using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QuickJob.Notifications.Client;
using QuickJob.Users.Api.Middlewares;
using QuickJob.Users.DataModel.Configuration;
using Users.Service.Registration;
using Users.Service.Services;
using Users.Service.Services.Implementations;
using Vostok.Configuration.Sources.Json;
using Vostok.Logging.Abstractions;
using Vostok.Logging.Console;
using Vostok.Logging.File;
using Vostok.Logging.File.Configuration;
using ConfigurationProvider = Vostok.Configuration.ConfigurationProvider;
using IConfigurationProvider = Vostok.Configuration.Abstractions.IConfigurationProvider;

namespace QuickJob.Users.Api.DI;

internal static class ServiceCollectionExtensions
{
    private const string FrontSpecificOrigins = "_frontSpecificOrigins";

    public static void AddServiceCors(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var serviceSettings = serviceProvider.GetRequiredService<IConfigurationProvider>().Get<ServiceSettings>();

        services
            .AddCors(option => option
                .AddPolicy(FrontSpecificOrigins, builder => builder.WithOrigins(serviceSettings.Origins.ToArray())
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
    }

    public static void AddServiceAuthentication(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var keycloackSettings = serviceProvider.GetRequiredService<IConfigurationProvider>().Get<KeycloackSettings>();
        
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Authority = keycloackSettings.Authority;
                options.Audience = keycloackSettings.Audience;
            });
    }
    
    public static void AddServiceSwaggerDocument(this IServiceCollection services)
    {
        services.AddSwaggerDocument(doc =>
        {
            doc.Title = "QuickJob.Users.Api";
            doc.AddSecurity("api.key", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
            {
                Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the textbox: api.key {for your api}."
            });
        });
    }

    public static void AddSettings(this IServiceCollection services)
    {
        var provider = new ConfigurationProvider();
        
        provider.SetupSourceFor<ServiceSettings>(new JsonFileSource($"QuickJob.Settings/{nameof(ServiceSettings)}.json"));
        provider.SetupSourceFor<KeycloackSettings>(new JsonFileSource($"QuickJob.Settings/{nameof(KeycloackSettings)}.json"));
        
        services.AddSingleton<IConfigurationProvider>(provider);
    }

    public static void AddSystemServices(this IServiceCollection services) => services
        .AddDistributedMemoryCache()
        .AddSingleton<IAuthService, AuthService>()
        .AddSingleton<IUsersService, UsersService>()
        .AddSingleton<IRegistrationService, RegistrationService>();

    public static void AddExternalServices(this IServiceCollection services)
    {
        services
            .AddSingleton<ILog>(new CompositeLog(new ConsoleLog(), new FileLog(new FileLogSettings())));
        services
            .AddSingleton<KeycloakFactory>()
            .AddSingleton(x => x.GetRequiredService<KeycloakFactory>().GetHttpClient())
            .AddSingleton(x => x.GetRequiredService<KeycloakFactory>().GetUsersClient())
            .AddSingleton(x => x.GetRequiredService<KeycloakFactory>().GetUserClient());
        services
            .AddSingleton<NotificationsClientFactory>()
            .TryAddSingleton<IQuickJobNotificationsClient>(x => x.GetRequiredService<NotificationsClientFactory>().GetClient());
    }
    
    public static void UseServiceCors(this IApplicationBuilder builder) => 
        builder.UseCors(FrontSpecificOrigins);
    
    public static void UseUnhandledExceptionMiddleware(this IApplicationBuilder builder) => 
        builder.UseMiddleware<UnhandledExceptionMiddleware>();
}
