using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Menu;

namespace ViewConsole.Menu
{
  /// <summary>
  /// Представление элмента многоуровнего меню
  /// </summary>
  public class ViewMenuSubItemConsole : View.Menu.ViewMenuSubItemBase
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuSubItem"></param>
    public ViewMenuSubItemConsole(Model.Menu.MenuSubItem parMenuSubItem) : base(parMenuSubItem)
    {
    }
    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
    }
  }
}
