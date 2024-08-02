// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.Program
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroiderManagementSystem.Helpers;
using EmbroideryData;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace EmbroiderManagement
{
  public class Program
  {
    public static void Main(string[] args)
    {
      IWebHost host = Program.CreateWebHostBuilder(args).Build();
      using (IServiceScope scope = host.Services.CreateScope())
      {
        IServiceProvider serviceProvider = scope.ServiceProvider;
        try
        {
          serviceProvider.GetRequiredService<IDatabaseInitializer>().SeedAsync().Wait();
        }
        catch (Exception ex)
        {
          serviceProvider.GetRequiredService<ILogger<Program>>().LogCritical(LoggingEvents.INIT_DATABASE, ex, LoggingEvents.INIT_DATABASE.Name);
          throw new Exception(LoggingEvents.INIT_DATABASE.Name, ex);
        }
      }
      host.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().ConfigureLogging((Action<WebHostBuilderContext, ILoggingBuilder>) ((hostingContext, logging) =>
    {
      logging.ClearProviders();
      logging.AddConfiguration((IConfiguration) hostingContext.Configuration.GetSection("Logging"));
      logging.AddConsole();
      logging.AddDebug();
      logging.AddEventSourceLogger();
      logging.AddFile((IConfiguration) hostingContext.Configuration.GetSection("Logging"));
    }));
  }
}
