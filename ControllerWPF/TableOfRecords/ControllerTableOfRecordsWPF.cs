using Controller.Rules;
using Model.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Rules;
using ViewWPF.Rules;
using ViewWPF;
using Controller.TableOfRecords;
using ViewWPF.TableOfRecords;
using Model.TableOfRecords;
using View.TableOfRecords;

namespace ControllerWPF.TableOfRecords
{
  /// <summary>
  /// Контроллер для таблицы рекордов в WPF
  /// </summary>
  public class ControllerRTableOfRecordsWPF : TableOfRecordsController, IWPFController
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    public ControllerRTableOfRecordsWPF(ModelTableOfRecords parModelTableOfRecords) : base(parModelTableOfRecords)
    {
    }

    /// <summary>
    /// Обработчик нажатий клавиш
    /// </summary>
    /// <param name="parSender">Источник события</param>
    /// <param name="parArgs">Аргументы события</param>
    public void KeyEventHandler(object parSender, KeyEventArgs parArgs)
    {
      switch (parArgs.Key)
      {
        case Key.Escape:
          Stop(); // Остановка контроллера при нажатии Escape
          break;
      }
    }

    /// <summary>
    /// Запуск контроллера
    /// </summary>
    public override void Start()
    {
      _viewTableOfRecords = new ViewTableOfRecordsWPF(_modelTableOfRecords);
      _modelTableOfRecords.Load(); 
      MainScreen.GetInstance().Window.KeyDown += KeyEventHandler;
    }

    /// <summary>
    /// Остановка контроллера
    /// </summary>
    public override void Stop()
    {
      MainScreen.GetInstance().Window.KeyDown -= KeyEventHandler; // Отключение обработчика событий
      MainScreen.GetInstance().StackPanel.Children.Clear(); // Очистка содержимого экрана
      GoBackCall(); // Возврат в предыдущее меню
    }
  }
}
