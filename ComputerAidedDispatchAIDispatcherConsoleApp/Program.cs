// See https://aka.ms/new-console-template for more information
using ComputerAidedDispatchAIDispatcherConsoleApp.Core;
using ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System.Runtime.CompilerServices;

Thread.Sleep(5000);
// Start of Application:
var builder = new ConfigurationBuilder();
BuildConfig(builder);


// Create Logger:
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("Application Starting");


var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IAuthService, AuthService>();
        services.AddSingleton<IAuthService, AuthService>();

        services.AddHttpClient<ICallForServiceService, CallForServiceService>();
        services.AddSingleton<ICallForServiceService, CallForServiceService>();

        services.AddHttpClient<ICallCommentService, CallCommentService>();
        services.AddSingleton<ICallCommentService, CallCommentService>();

        services.AddHttpClient<IUnitService, UnitService>();
        services.AddSingleton<IUnitService, UnitService>();

        services.AddHttpClient<IDispatcherService, DispatcherService>();
        services.AddSingleton<IDispatcherService, DispatcherService>();

        services.AddSingleton<IApplication, Application>();
        services.AddSingleton<IUserBotFactory, UserBotFactory>();

        services.AddSingleton<ICadSimulator, CadSimulator>();

    })
    .UseSerilog()
    .Build();

var app = ActivatorUtilities.CreateInstance<Application>(host.Services);

app.Run().Wait();


static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables();

}

