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
using Microsoft.EntityFrameworkCore;

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
        bool running = true;
        while (running && _playerManager.Player.Health > 0) 
        {
             _gameLoopMenu.MainMenu();
        }
        
    }


    private void SetupGame()
    {
        _roomSeeder.LinkRooms(_context);

        LoadMonsters();

        GameLoop();
    }

    private void LoadMonsters()
    {
        //assign monsters based on their room ID
        var rooms = _context.Rooms
        .Include(r => r.MonstersInRoom)
        .ToList();

        var monsters = _context.Monsters.ToList();

        foreach (var monster in monsters)
        {
            var room = rooms.FirstOrDefault(r => r.Id == monster.RoomId);

            if (room != null)
            {
                room.MonstersInRoom.Add(monster);
            }
        }

    }



}
