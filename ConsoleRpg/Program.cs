using ConsoleRpg.Helpers;
using ConsoleRpg.Helpers.Menus;
using ConsoleRpg.Services;
using ConsoleRpgEntities.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleRpg;

public static class Program
{
    private static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        Startup.ConfigureServices(serviceCollection);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var gameEngine = serviceProvider.GetService<GameEngine>();


        gameEngine?.Run();
    }
}

