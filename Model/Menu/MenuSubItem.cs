using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Menu;
using System.Threading.Tasks;

namespace Model.Menu
{
  /// <summary>
  /// Элемент многоуровнего меню
  /// </summary>
  public class MenuSubItem : MenuItem
  {
    /// <summary>
    /// Вложенное меню
    /// </summary>
    public Menu SubMenu { get; private set; }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parId"></param>
    /// <param name="parName"></param>
    /// <param name="parMenu"></param>
    public MenuSubItem(int parId, string parName, Menu parMenu) : base(parId, parName)
    {
      SubMenu = parMenu;
    }
    /// <summary>
    /// Вызов события нажатия
    /// </summary>
    protected override void ProcessEnter()
    {
      CallEnterEvent();
    }
  }
}
