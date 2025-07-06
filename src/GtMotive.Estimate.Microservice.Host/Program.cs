using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GtMotive.Estimate.Microservice.Api;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Fleet;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle;
using GtMotive.Estimate.Microservice.Host.Configuration;
using GtMotive.Estimate.Microservice.Host.DependencyInjection;
using GtMotive.Estimate.Microservice.Infrastructure;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using IdentityServer4.AccessTokenValidation;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder();

// Configuration.
if (!builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("serilogsettings.json", optional: false, reloadOnChange: true);

    var secretClient = new SecretClient(
        new Uri($"https://{builder.Configuration.GetValue<string>("KeyVaultName")}.vault.azure.net/"),
        new DefaultAzureCredential());

    builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
}

// Logging configuration for host bootstrapping.
builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
        formatProvider: CultureInfo.InvariantCulture)
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

// Add services to the container.
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
    builder.Services.AddApplicationInsightsKubernetesEnricher();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddControllers(ApiConfiguration.ConfigureControllers)
    .WithApiControllers();

builder.Services.AddBaseInfrastructure(builder.Environment.IsDevelopment());

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                               ForwardedHeaders.XForwardedProto;

    // Only loopback proxies are allowed by default.
    // Clear that restriction because forwarders are enabled by explicit
    // configuration.
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
    })
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = appSettings.JwtAuthority;
        options.ApiName = "estimate-api";
        options.SupportedTokens = SupportedTokens.Jwt;
    });

builder.Services.AddSwagger(appSettings, builder.Configuration);

// Registro de MongoService como singleton
builder.Services.AddSingleton<MongoService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IFleetRepository, FleetRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();

// Registro de UseCases y Presenters
// UseCase Vehicles
builder.Services.AddScoped<VehiclePresenter<GetVehiclesUseCaseOutput>>();
builder.Services.AddScoped<VehiclePresenter<GetVehicleUseCaseOutput>>();
builder.Services.AddScoped<VehiclePresenter<CreateVehicleUseCaseOutput>>();

builder.Services.AddScoped<IOutputPortStandard<GetVehiclesUseCaseOutput>, VehiclePresenter<GetVehiclesUseCaseOutput>>();
builder.Services.AddScoped<IOutputPortStandard<GetVehicleUseCaseOutput>, VehiclePresenter<GetVehicleUseCaseOutput>>();
builder.Services.AddScoped<IOutputPortStandard<CreateVehicleUseCaseOutput>, VehiclePresenter<CreateVehicleUseCaseOutput>>();

builder.Services.AddScoped<IUseCaseWithPresenter<GetVehiclesUseCaseInput, GetVehiclesUseCaseOutput>, GetVehiclesUseCase>();
builder.Services.AddScoped<IUseCaseWithPresenter<GetVehicleUseCaseInput, GetVehicleUseCaseOutput>, GetVehicleUseCase>();
builder.Services.AddScoped<IUseCaseWithPresenter<CreateVehicleUseCaseInput, CreateVehicleUseCaseOutput>, CreateVehicleUseCase>();

// UseCase Fleets
builder.Services.AddScoped<FleetPresenter<GetVehiclesByFleetUseCaseOutput>>();
builder.Services.AddScoped<FleetPresenter<GetFleetsUseCaseOutput>>();
builder.Services.AddScoped<FleetPresenter<GetFleetUseCaseOutput>>();
builder.Services.AddScoped<FleetPresenter<AddVehicleToFleetUseCaseOutput>>();

builder.Services.AddScoped<IOutputPortStandard<GetVehiclesByFleetUseCaseOutput>, VehiclePresenter<GetVehiclesByFleetUseCaseOutput>>();
builder.Services.AddScoped<IOutputPortStandard<GetFleetsUseCaseOutput>, VehiclePresenter<GetFleetsUseCaseOutput>>();
builder.Services.AddScoped<IOutputPortStandard<GetFleetUseCaseOutput>, VehiclePresenter<GetFleetUseCaseOutput>>();
builder.Services.AddScoped<IOutputPortStandard<AddVehicleToFleetUseCaseOutput>, VehiclePresenter<AddVehicleToFleetUseCaseOutput>>();

builder.Services.AddScoped<IUseCaseWithPresenter<GetVehiclesByFleetUseCaseInput, GetVehiclesByFleetUseCaseOutput>, GetVehiclesByFleetUseCase>();
builder.Services.AddScoped<IUseCaseWithPresenter<GetFleetsUseCaseInput, GetFleetsUseCaseOutput>, GetFleetsUseCase>();
builder.Services.AddScoped<IUseCaseWithPresenter<GetFleetUseCaseInput, GetFleetUseCaseOutput>, GetFleetUseCase>();
builder.Services.AddScoped<IUseCaseWithPresenter<AddVehicleToFleetUseCaseInput, AddVehicleToFleetUseCaseOutput>, AddVehicleToFleetUseCase>();

// UseCase Rents
builder.Services.AddScoped<RentPresenter<GetRentsUseCaseOutput>>();
builder.Services.AddScoped<RentPresenter<RentVehicleUseCaseOutput>>();
builder.Services.AddScoped<RentPresenter<ReturnVehicleUseCaseOutput>>();

builder.Services.AddScoped<IOutputPortStandard<GetRentsUseCaseOutput>, VehiclePresenter<GetRentsUseCaseOutput>>();
builder.Services.AddScoped<IOutputPortStandard<RentVehicleUseCaseOutput>, VehiclePresenter<RentVehicleUseCaseOutput>>();
builder.Services.AddScoped<IOutputPortStandard<ReturnVehicleUseCaseOutput>, VehiclePresenter<ReturnVehicleUseCaseOutput>>();

builder.Services.AddScoped<IUseCaseWithPresenter<GetRentsUseCaseInput, GetRentsUseCaseOutput>, GetRentsUseCase>();
builder.Services.AddScoped<IUseCaseWithPresenter<RentVehicleUseCaseInput, RentVehicleUseCaseOutput>, RentVehicleUseCase>();
builder.Services.AddScoped<IUseCaseWithPresenter<ReturnVehicleUseCaseInput, ReturnVehicleUseCaseOutput>, ReturnVehicleUseCase>();

var app = builder.Build();

// Logging configuration.
Log.Logger = builder.Environment.IsDevelopment() ?
    new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
        .MinimumLevel.Override("System", LogEventLevel.Warning)
        .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
            theme: AnsiConsoleTheme.Literate,
            formatProvider: CultureInfo.InvariantCulture)
        .CreateLogger() :
    new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "addoperation")
        .WriteTo.Console(
            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
            formatProvider: CultureInfo.InvariantCulture)
        .WriteTo.ApplicationInsights(
            app.Services.GetRequiredService<TelemetryConfiguration>(), TelemetryConverter.Traces)
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

var pathBase = new PathBase(builder.Configuration.GetValue("PathBase", defaultValue: PathBase.DefaultPathBase));

if (!pathBase.IsDefault)
{
    app.UsePathBase(pathBase.CurrentWithoutTrailingSlash);
}

app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwaggerInApplication(pathBase, builder.Configuration);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Seed initial data.
await DataSeeder.SeedAsync(app.Services);

await app.RunAsync();
