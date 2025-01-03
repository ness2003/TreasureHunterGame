using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Model.Menu;

namespace View.Menu
{
  public abstract class ViewMenuItemBase : ViewBase
  {
    /// <summary>
    /// Координата по горизонтали
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Координата по вертикали
    /// </summary>
    public int Y { get; set; }
    /// <summary>
    /// Ширина представления
    /// </summary>
    public int Width { get; protected set; }
    /// <summary>
    /// Высота представления
    /// </summary>
    public int Height { get; protected set; }
    /// <summary>
    /// Элемент меню
    /// </summary>
    private Model.Menu.MenuItem _menuItem = null;
    protected Model.Menu.MenuItem MenuItem
    {
      get { return _menuItem; }
    }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuItemBase"></param>
    public ViewMenuItemBase(Model.Menu.MenuItem parMenuItemBase)
    {
      _menuItem = parMenuItemBase;
    }
  }
}
