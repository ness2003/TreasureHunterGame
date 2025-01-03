using Controller.TableOfRecords;
using Model.TableOfRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.TableOfRecords;
using ViewConsole.TableOfRecords;

namespace ControllerConsole.TableOfRecords
{
  public class ControllerTableOfRecordsConsole : TableOfRecordsController, IConsoleController
  {
    /// <summary>
    /// Конструктор контроллера таблицы рекордов для консоли
    /// </summary>
    public ControllerTableOfRecordsConsole(ModelTableOfRecords parModelTableOfRecords)
        : base(parModelTableOfRecords)
    {
    }

    /// <summary>
    /// Обработка нажатий клавиш
    /// </summary>
    public void ProcessKeyPress()
    {
      bool exitRequested = false;

      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        switch (keyInfo.Key)
        {
          case ConsoleKey.Escape:
            Stop();
            GoBackCall();  
            exitRequested = true; 
            break;
        }

      } while (!exitRequested); 
    }

    /// <summary>
    /// Запустить контроллер
    /// </summary>
    public override void Start()
    {
      _viewTableOfRecords = new ViewTableOfRecordsConsole(_modelTableOfRecords);
      _modelTableOfRecords.Load();
      _viewTableOfRecords.Draw(); // Отображаем таблицу рекордов
      ProcessKeyPress();
    }

    /// <summary>
    /// Остановить контроллер
    /// </summary>
    public override void Stop()
    {
      Console.Clear();
    }
  }
}
