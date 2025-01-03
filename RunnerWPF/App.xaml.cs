using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Media3D;
using Controller.Game;
using Controller.PauseMenu;
using Controller.Rules;
using Controller.TableOfRecords;
using ControllerWPF.Game;
using ControllerWPF.GameResult;
using ControllerWPF.Menu;
using ControllerWPF.Rules;
using ControllerWPF.TableOfRecords;
using Model.Game;
using Model.Game.GameInfo;
using Model.Game.GameObjects;
using Model.Game.Новая_папка.Factories;
using Model.GameResult;
using Model.Menu;
using Model.Rules;
using Model.TableOfRecords;
using View.Menu;
using ViewWPF.Game;
using ViewWPF.Menu;
using ViewWPF.Rules;
using ViewWPF.TableOfRecords;
using ViewWPF.TableOfRecords;

namespace RunnerWPF
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  /// 
  public partial class App : Application
  {

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      // Создание всех необходимых зависимостей
      var menuMain = new MenuMain();
      var width = 1000;
      var height = 500;
      var player = new Player(width / 2, height - 100, 100, 100, 3, 1000, 500);
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
      var controllerGame = new ControllerGameWPF(modelGame);
      var modelRules = new ModelRules();
      var controllerRules = new ControllerRulesWPF(modelRules);
      var modelTableOfRecords = ModelTableOfRecords.Instance;

      var tableOfRecordsController = new ControllerRTableOfRecordsWPF(modelTableOfRecords);
      var pauseMenu = new MenuPause();
      var viewPauseMenu = new ViewMenuWPF(pauseMenu);
      var pauseMenuController = new ControllerMenuPauseWPF(pauseMenu);
      var gameResultController = new ControllerGameResultWPF(new ModelGameResult(0,0));
      // Теперь создаем экземпляр ControllerMenuMainWPF с передачей зависимостей через конструктор
      var controllerMenuMainWPF = new ControllerMenuMainWPF(menuMain, controllerGame, controllerRules, tableOfRecordsController, pauseMenuController, gameResultController);

      // Запускаем главный экран
      controllerMenuMainWPF.Start();
    }


  }
}
