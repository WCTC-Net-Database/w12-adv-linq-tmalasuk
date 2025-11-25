using ConsoleRpg.Helpers;
using ConsoleRpg.Helpers.EntityHelper;
using ConsoleRpg.Helpers.Menus;
using ConsoleRpgEntities.Data;
using ConsoleRpgEntities.Data.Seeding;
using ConsoleRpgEntities.Models.Attributes;
using ConsoleRpgEntities.Models.Characters;
using ConsoleRpgEntities.Models.Characters.Monsters;
using ConsoleRpgEntities.Models.Equipments;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Models.Rooms.Interfaces;

namespace ConsoleRpg.Services;

public class GameEngine
{
    private readonly GameContext _context;
    private MainMenu _menuManager;
    private readonly OutputManager _outputManager;
    private InventoryManager _inventoryManager;
    private PlayerManager _playerManager;
    private GameLoopMenu _gameLoopMenu;
    private Player _player;
    private IMonster _goblin;
    private List<Item> _masterItemList;
    private readonly  RoomSeeder _roomSeeder;

    public GameEngine(GameContext context, MainMenu mainMenu, OutputManager outputManager, PlayerManager playerManager, GameLoopMenu gameLoop, RoomSeeder roomseeder)
    {
        _gameLoopMenu = gameLoop;
        _menuManager = mainMenu;
        _outputManager = outputManager;
        _context = context;
        _playerManager = playerManager;
        _roomSeeder = roomseeder;
    }

    public void Run()
    {
        bool startGame = false;

        while (!startGame)
        {
            startGame = _menuManager.ShowMainMenu();
        }

        SetupGame(); 
    }



    private void GameLoop()
    {
        // give player 5 items
        List<Item> allItems = _context.Items.ToList();
        Random random = new Random();
        List<Item> randomItems = allItems
            .OrderBy(item => random.Next()) 
            .Take(5)
            .ToList();

        List<Item> monsterItems = _context.Items.ToList().OrderBy(item => random.Next()).ToList();
        Monster[] monsterArray= _context.Monsters.ToArray();


        foreach (Item item in randomItems)
        {
            _playerManager.Player.Inventory.Items.Add(item);
        }

        // add items to monsters 
        for (int i = 0; i < monsterArray.Count() ; i++)
        {
            var monster = monsterArray[i];
            var item = monsterItems[i % monsterItems.Count]; // cycles through items if there are fewer items than monsters
            monster.itemDrop.Add(item);
        }

        var Rooms = _context.Rooms.ToList();
        var combatRooms = Rooms.OfType<ICombatRoom>().ToList();
        int roomIndex = 0;

        foreach (var monster in monsterArray)
        {
            // Cycle through combat rooms if more monsters than rooms
            var room = combatRooms[roomIndex % combatRooms.Count];
            if (room is Room actualRoom)
            {
                actualRoom.MonstersInRoom.Add(monster);
            }
            roomIndex++;
        }

        _roomSeeder.LinkRooms(_context);

        while (_playerManager.Player.Health > 0)
        {
            _gameLoopMenu.MainMenu();
        }
        
    }


    private void SetupGame()
    {
        // Load monsters into random rooms 
        LoadMonsters();

        GameLoop();
    }

    private void LoadMonsters()
    {
        
    }

    

}
