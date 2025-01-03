using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewConsole.Rules;
using Model.Rules;
using View.Rules;
using Controller.Rules;


using System.Windows.Input;
using Model.Menu;

namespace ControllerConsole.Rules
{
  /// <summary>
  /// Контроллер правил
  /// </summary>
  public class ControllerRulesConsole : ControllerRules, IConsoleController
  {

    public ControllerRulesConsole(ModelRules parModelRules) : base(parModelRules)
    {
    }


    /// <summary>
    /// Запустить контроллер
    /// </summary>
    public override void Start()
    {
      _viewRules = new ViewRulesConsole(_modelRules);
      ProcessKeyPress();
    }
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


    public override void Stop()
    {
      Console.Clear();
    }
  }
}
