using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Menu
{
  /// <summary>
  /// Представление элмента многоуровнего меню
  /// </summary>
  public abstract class ViewMenuSubItemBase : ViewMenuItemBase
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuSubItem"></param>
    public ViewMenuSubItemBase(Model.Menu.MenuSubItem parMenuSubItem) : base(parMenuSubItem)
    {
    }
  }
}
