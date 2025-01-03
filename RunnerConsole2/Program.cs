using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllerConsole.Menu;
using Model.Game.GameInfo;
using Model.Game.GameObjects;
using Model.Game.Новая_папка.Factories;
using Model.Game;
using Model.Menu;
using Model.Rules;
using Model.TableOfRecords;
using ControllerConsole.Game;
using ControllerConsole.Rules;
using ControllerConsole.TableOfRecords;
using ViewConsole.Menu;
using ControllerConsole.MenuPause;
using Model.GameResult;
using ControllerWPF.GameResult;

namespace RunnerConsole
{

  class Program
  {
    static void Main(string[] args)
    {
      // Создание всех необходимых зависимостей
      var menuMain = new MenuMain();
      var width = 80;
      var height = 35;
      var player = new Player(width / 2, height - 2, 2, 2, 3, width, height);
      var score = 0;
      var level = 1;
      var coins = new List<GameObject>();
      var bonuses = new List<GameObject>();
      var badObjects = new List<GameObject>();
      var goal = Goal.GetInstance();
      var random = new Random();
      var coinFactory = new CoinFactory();
      var bonusFactory = new BonusFactory();
      var badObjectsFactory = new BadObjectFactory();
      var modelGame = new ModelGame(player, goal, coins, bonuses, badObjects, coinFactory, bonusFactory, badObjectsFactory, random, width, height, level, score);
      modelGame.ObjectsRadius = 1;
      var controllerGame = new ControllerGameConsole(modelGame);
      var modelRules = new ModelRules();
      var controllerRules = new ControllerRulesConsole(modelRules);
      var modelTableOfRecords = ModelTableOfRecords.Instance;

      var tableOfRecordsController = new ControllerTableOfRecordsConsole(modelTableOfRecords);

      var pauseMenu = new MenuPause();
      var viewPauseMenu = new ViewMenuConsole(pauseMenu);
      var pauseMenuController = new ControllerMenuPauseConsole(pauseMenu);

      var gameResultController = new ControllerGameResultConsole(new ModelGameResult(0, 0));
      // Теперь создаем экземпляр ControllerMenuMainWPF с передачей зависимостей через конструктор
      var controllerMenuMainConsole = new ControllerMenuMainConsole(menuMain, controllerGame, controllerRules, tableOfRecordsController, pauseMenuController, gameResultController);

      // Запускаем главный экран
      controllerMenuMainConsole.Start();
    }
  }
}
