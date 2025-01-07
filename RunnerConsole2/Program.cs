using ControllerConsole.Game;
using ControllerConsole.GameResult;
using ControllerConsole.Menu;
using ControllerConsole.MenuPause;
using ControllerConsole.Rules;
using ControllerConsole.TableOfRecords;
using Model.Game.GameInfo;
using Model.Game.GameObjects;
using Model.Game.Новая_папка;
using Model.Game;
using Model.GameResult;
using Model.Menu;
using Model.Rules;
using Model.TableOfRecords;
using ViewConsole.Menu;
using ViewsConsole;

namespace RunnerConsole
{
  /// <summary>
  /// Точка входа в консольное приложение.
  /// Создает и настраивает все необходимые зависимости для игры.
  /// Запускает главный экран меню.
  /// </summary>
  class Program
  {
    /// <summary>
    /// Основной метод для запуска игры.
    /// Создает все зависимости, настраивает объекты модели, контроллеры и представления.
    /// После инициализации запускает главный экран меню.
    /// </summary>
    /// <param name="args">Аргументы командной строки.</param>
    static void Main(string[] args)
    {
      // Устанавливаем фиксированные размеры консольного окна
      ConsoleHelperUtilite.SetFixedConsoleWindowSize(90, 30);

      // Создание всех необходимых зависимостей и объектов модели
      var menuMain = new MenuMain();
      var width = 90; // Ширина игрового поля
      var height = 30; // Высота игрового поля
      var player = new Player(width / 2, height - 5, 5, 5, 4, 90, 30); // Игрок
      var score = 0; // Начальный счет
      var level = 1; // Начальный уровень
      var coins = new List<GameObject>(); // Список монет
      var bonuses = new List<GameObject>(); // Список бонусов
      var badObjects = new List<GameObject>(); // Список плохих объектов
      var goal = Goal.GetInstance(); // Цели игры
      var random = new Random(); // Генератор случайных чисел
      var factory = new GameObjectFactory(height, width, 0, 0, 2, 2); // Фабрика объектов игры
      var modelGame = new ModelGame(player, goal, coins, bonuses, badObjects, factory, random, width, height, level, score); // Модель игры
      var controllerGame = new ControllerGameConsole(modelGame); // Контроллер игры для консоли
      controllerGame.GameUpdatesPerSecond = 10; // Устанавливаем обновления игры в секунду
      var modelRules = new ModelRules(); // Модель правил
      var controllerRules = new ControllerRulesConsole(modelRules); // Контроллер правил для консоли
      var modelTableOfRecords = ModelTableOfRecords.Instance; // Модель таблицы рекордов

      var tableOfRecordsController = new ControllerTableOfRecordsConsole(modelTableOfRecords); // Контроллер таблицы рекордов

      var pauseMenu = new MenuPause(); // Меню паузы
      var viewPauseMenu = new ViewMenuConsole(pauseMenu); // Представление меню паузы
      var pauseMenuController = new ControllerMenuPauseConsole(pauseMenu); // Контроллер меню паузы

      var gameResultController = new ControllerGameResultConsole(new ModelGameResult(0, 0)); // Контроллер результата игры

      // Создаем контроллер главного меню с зависимостями
      var controllerMenuMainConsole = new ControllerMenuMainConsole(menuMain, controllerGame, controllerRules, tableOfRecordsController, pauseMenuController, gameResultController);

      // Запускаем главный экран меню
      controllerMenuMainConsole.Start();
    }
  }
}
