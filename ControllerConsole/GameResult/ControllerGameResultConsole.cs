using Controller;
using Controller.GameResult;
using Controller.Rules;
using ControllerConsole;
using Model.GameResult;
using Model.Menu;
using Model.TableOfRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using View.GameResult;
using ViewConsole.GameResult;

namespace ControllerWPF.GameResult
{
  public class ControllerGameResultConsole : ControllerGameResult, IConsoleController
  {
    /// <summary>
    /// Конструктор контроллера результата игры для консоли
    /// </summary>
    public ControllerGameResultConsole(ModelGameResult parModelGameResult)
        : base(parModelGameResult)
    {
    }

    /// <summary>
    /// Запустить контроллер
    /// </summary>
    public override void Start()
    {
      _viewGameResult = new ViewGameResultConsole(_modelGameResult);
      _viewGameResult.Draw(); // Отображаем результаты игры
      ProcessKeyPress();
    }

    /// <summary>
    /// Обработка нажатий клавиш
    /// </summary>
    public void ProcessKeyPress()
    {
      bool exitRequested = false; // Флаг для завершения цикла

      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey();

        switch (keyInfo.Key)
        {
          case ConsoleKey.Escape:
            exitRequested = true; // Устанавливаем флаг выхода
            Stop(); // Останавливаем контроллер
            break;
        }

      } while (!exitRequested); // Цикл продолжается, пока exitRequested = false
    }

    /// <summary>
    /// Остановить контроллер
    /// </summary>
    public override void Stop()
    {
      Console.Clear();
      Console.WriteLine("Результаты игры закрыты.");

      // Логика сохранения рекорда
      Console.Write("Введите ваше имя для сохранения результата: ");
      string playerName = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(playerName))
      {
        ModelTableOfRecords.Instance.Add(new Record(playerName, _modelGameResult.Score, _modelGameResult.Level));
        Console.WriteLine("Результат сохранен в таблице рекордов.");
      }
      else
      {
        Console.WriteLine("Результат не был сохранен.");
      }

      GoBackCall(); // Возврат в предыдущее меню
    }
  }
}
