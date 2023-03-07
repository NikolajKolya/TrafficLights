using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using TrafficLights.Automat.Abstract;
using TrafficLights.BlinkingControl.Abstract;
using TrafficLights.BlinkingControl.Inmplementaitons;

namespace TrafficLights
{
    internal class Program
    {
        /// <summary>
        /// Dependency injection service provider
        /// </summary>
        public static ServiceProvider Di { get; set; }

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            // Preparing DI
            Di = ConfigureServices()
                .BuildServiceProvider();

            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();

        // Setting up DI
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IBlinker, Blinker>();
            services.AddSingleton<IAutomat, Automat.Implementations.Automat>();

            return services;
        }
    }
}
