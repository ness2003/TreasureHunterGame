using System.Windows.Input;
using ViewWPF;
using Controller.TableOfRecords;
using ViewWPF.TableOfRecords;
using Model.TableOfRecords;

namespace ControllerWPF.TableOfRecords
{
  /// <summary>
  /// Контроллер для таблицы рекордов в WPF.
  /// Управляет процессом отображения таблицы рекордов, обработкой событий и взаимодействием с моделью.
  /// </summary>
  public class ControllerRTableOfRecordsWPF : TableOfRecordsController, IWPFController
  {
    /// <summary>
    /// Конструктор контроллера для таблицы рекордов.
    /// </summary>
    /// <param name="parModelTableOfRecords">Модель таблицы рекордов.</param>
    public ControllerRTableOfRecordsWPF(ModelTableOfRecords parModelTableOfRecords) : base(parModelTableOfRecords)
    {
    }

    /// <summary>
    /// Обработчик нажатий клавиш для управления отображением таблицы рекордов.
    /// </summary>
    /// <param name="parSender">Источник события (окно).</param>
    /// <param name="parArgs">Аргументы события (информация о нажатой клавише).</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Escape:
          Stop();
          break;
      }
    }

    /// <summary>
    /// Запускает контроллер, загружает данные таблицы рекордов и отображает её.
    /// </summary>
    public override void Start()
    {
      _viewTableOfRecords = new ViewTableOfRecordsWPF(_modelTableOfRecords);
      _modelTableOfRecords.Load();
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler;
    }

    /// <summary>
    /// Останавливает контроллер, очищает содержимое экрана и возвращает в предыдущее меню.
    /// </summary>
    public override void Stop()
    {
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler;
      MainScreen.GetInstance().StackPanel.Children.Clear();
      GoBackCall();
    }
  }
}
