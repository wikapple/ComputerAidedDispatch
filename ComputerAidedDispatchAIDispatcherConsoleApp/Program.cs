// See https://aka.ms/new-console-template for more information
using ComputerAidedDispatchAIDispatcherConsoleApp;
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
        services.AddScoped<IAuthService, AuthService>();

        services.AddHttpClient<ICallForServiceService, CallForServiceService>();
        services.AddScoped<ICallForServiceService, CallForServiceService>();
        services.AddScoped<IUnitService, UnitService>();

        services.AddTransient<IAuthentication, Authentication>();
    })
    .UseSerilog()
    .Build();

var svc = ActivatorUtilities.CreateInstance<Authentication>(host.Services);
svc.PrintAllUnits().Wait();

Console.WriteLine("Hi");


static void BuildConfig(IConfigurationBuilder builder)
{
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables();

}

