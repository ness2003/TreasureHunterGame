using Controller.TableOfRecords;
using Model.TableOfRecords;
using ViewConsole.TableOfRecords;

namespace ControllerConsole.TableOfRecords
{
  /// <summary>
  /// Контроллер таблицы рекордов для консольного интерфейса.
  /// Отвечает за отображение таблицы рекордов и обработку пользовательского ввода.
  /// </summary>
  public class ControllerTableOfRecordsConsole : TableOfRecordsController, IConsoleController
  {
    /// <summary>
    /// Инициализирует новый экземпляр контроллера таблицы рекордов для консоли.
    /// </summary>
    /// <param name="parModelTableOfRecords">Модель таблицы рекордов.</param>
    public ControllerTableOfRecordsConsole(ModelTableOfRecords parModelTableOfRecords)
        : base(parModelTableOfRecords)
    {
    }

    /// <summary>
    /// Запускает контроллер таблицы рекордов.
    /// Загружает и отображает таблицу рекордов, а затем обрабатывает ввод пользователя.
    /// </summary>
    public override void Start()
    {
      // Создаём представление таблицы рекордов
      _viewTableOfRecords = new ViewTableOfRecordsConsole(_modelTableOfRecords);
      _modelTableOfRecords.Load();
      _viewTableOfRecords.Draw();
      ProcessKeyPress();
    }

    /// <summary>
    /// Обрабатывает нажатия клавиш пользователя.
    /// Пользователь может выйти из экрана таблицы рекордов, нажав Escape.
    /// </summary>
    public void ProcessKeyPress()
    {
      bool exitRequested = false; // Флаг для выхода из режима просмотра таблицы рекордов

      do
      {
        // Считываем нажатие клавиши
        ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

        switch (keyInfo.Key)
        {
          case ConsoleKey.Escape:
            // При нажатии Escape завершаем работу контроллера
            Stop();
            GoBackCall();
            exitRequested = true;
            break;
        }

      } while (!exitRequested);
    }

    /// <summary>
    /// Останавливает контроллер таблицы рекордов.
    /// Очищает консольный экран.
    /// </summary>
    public override void Stop()
    {
      Console.Clear();
    }
  }
}
