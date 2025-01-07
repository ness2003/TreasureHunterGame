using System.Windows;
using ControllerWPF.Game;
using ControllerWPF.GameResult;
using ControllerWPF.Menu;
using ControllerWPF.Rules;
using ControllerWPF.TableOfRecords;
using Model.Game;
using Model.Game.GameInfo;
using Model.Game.GameObjects;
using Model.Game.Новая_папка;
using Model.GameResult;
using Model.Menu;
using Model.Rules;
using Model.TableOfRecords;
using ViewWPF.Menu;

namespace RunnerWPF
{
  /// <summary>
  /// Главный класс приложения WPF.
  /// Ответственен за создание и инициализацию всех необходимых зависимостей для игры и запуск главного экрана.
  /// </summary>
  public partial class App : Application
  {
    /// <summary>
    /// Обработчик события старта приложения.
    /// Создает все необходимые модели, контроллеры и виды для игры.
    /// </summary>
    /// <param name="sender">Источник события</param>
    /// <param name="e">Аргументы события</param>
    private void Application_Startup(object sender, StartupEventArgs e)
    {
      // Создание объектов меню и игры, а также установление начальных значений
      var menuMain = new MenuMain();
      var width = 1000; // Ширина экрана игры
      var height = 500; // Высота экрана игры

      // Создание объекта игрока с параметрами
      var player = new Player(width / 2, height - 100, 100, 100, 3, 1000, 500);

      // Установка начальных значений для счета, уровня и других игровых объектов
      var score = 0;
      var level = 1;
      var coins = new List<GameObject>(); // Коллекция монет
      var bonuses = new List<GameObject>(); // Коллекция бонусов
      var badObjects = new List<GameObject>(); // Коллекция плохих объектов
      var goal = Goal.GetInstance(); // Цель игры
      var random = new Random(); // Объект для генерации случайных чисел
      var factory = new GameObjectFactory(height, width, 0, 0, 5, 20); // Фабрика игровых объектов

      // Модель игры, объединяющая все игровые элементы
      var modelGame = new ModelGame(player, goal, coins, bonuses, badObjects, factory, random, width, height, level, score);

      // Создание контроллеров для различных аспектов игры
      var controllerGame = new ControllerGameWPF(modelGame);
      var modelRules = new ModelRules(); // Модель правил игры
      var controllerRules = new ControllerRulesWPF(modelRules);
      var modelTableOfRecords = ModelTableOfRecords.Instance; // Модель таблицы рекордов

      var tableOfRecordsController = new ControllerRTableOfRecordsWPF(modelTableOfRecords);

      // Создание меню паузы и его контроллера
      var pauseMenu = new MenuPause();
      var viewPauseMenu = new ViewMenuWPF(pauseMenu);
      var pauseMenuController = new ControllerMenuPauseWPF(pauseMenu);

      // Создание контроллера для вывода результатов игры
      var gameResultController = new ControllerGameResultWPF(new ModelGameResult(0, 0));

      // Теперь создаем главный контроллер меню и передаем все зависимости через конструктор
      var controllerMenuMainWPF = new ControllerMenuMainWPF(menuMain, controllerGame, controllerRules, tableOfRecordsController, pauseMenuController, gameResultController);

      // Запуск главного меню
      controllerMenuMainWPF.Start();
    }
  }
}
