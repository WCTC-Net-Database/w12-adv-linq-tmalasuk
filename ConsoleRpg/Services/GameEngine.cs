using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private readonly GameContext _context;
    private readonly MenuManager _menuManager;
    private readonly OutputManager _outputManager;

    private IPlayer _player;
    private IMonster _goblin;
    private List<Item> _masterItemList;

    public GameEngine(GameContext context, MenuManager menuManager, OutputManager outputManager)
    {
        _menuManager = menuManager;
        _outputManager = outputManager;
        _context = context;
    }

    public void Run()
    {
        if (_menuManager.ShowMainMenu())
        {
            SetupGame();
        }
    }

    private void GameLoop()
    {
        _outputManager.Clear();

        while (true)
        {
            _outputManager.WriteLine("Choose an action:", ConsoleColor.Cyan);
            _outputManager.WriteLine("1. Attack");
            _outputManager.WriteLine("2. Inventory");
            _outputManager.WriteLine("3. Quit");

            _outputManager.Display();

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AttackCharacter();
                    break;
                case "2":
                    if (_player is Player player)
                    {
                        player.ManageItems();
                    }
                    else
                    {
                        Console.WriteLine("Character not of valid type");
                    }
                    break;
                case "3":
                    _outputManager.WriteLine("Exiting game...", ConsoleColor.Red);
                    _outputManager.Display();
                    Environment.Exit(0);
                    break;
                default:
                    _outputManager.WriteLine("Invalid selection. Please choose 1.", ConsoleColor.Red);
                    break;
            }
        }
    }

    private void AttackCharacter()
    {
        if (_goblin is ITargetable targetableGoblin)
        {
            _player.Attack(targetableGoblin);
            _player.UseAbility(_player.Abilities.First(), targetableGoblin);
        }
    }

    private void SetupGame()
    {
        _player = _context.Players.FirstOrDefault();
        _outputManager.WriteLine($"{_player.Name} has entered the game.", ConsoleColor.Green);
        _masterItemList = _context.Items.ToList();
        
        // Make sure _player.Inventory is not null
        if (_player is Player player)
        {
            if (player.Inventory == null)
            {
                player.Inventory = new Inventory
                {
                    PlayerId = player.Id,
                    Items = new List<Item>()
                };
            }

            // Randomly select 3 items
            var rand = new Random();
            var randomItems = _masterItemList
                .OrderBy(x => rand.Next()) // shuffle the list
                .Take(3) // take 3 items
                .ToList();

            // Add them to the player's inventory
            foreach (var item in randomItems)
            {
                player.Inventory.Items.Add(item);
            }
            

            // Optional: output to console
            foreach (var item in randomItems)
            {
                _outputManager.WriteLine($"{player.Name} received {item.Name}.", ConsoleColor.Yellow);
            }
        }


        // Load monsters into random rooms 
        LoadMonsters();

        // Pause before starting the game loop
        Thread.Sleep(500);
        GameLoop();
    }

    private void LoadMonsters()
    {
        _goblin = _context.Monsters.OfType<Goblin>().FirstOrDefault();
    }

}
