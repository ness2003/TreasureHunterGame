using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace View.Menu
{
  /// <summary>
  /// Представление меню
  /// </summary>
  public abstract class ViewMenu : ViewBase
  {
    /// <summary>
    /// Координата по горизонтали
    /// </summary>
    protected int X { get; set; }
    /// <summary>
    /// Координата по вертикали
    /// </summary>
    protected int Y { get; set; }
    /// <summary>
    /// Ширина меню
    /// </summary>
    protected int Width { get; set; }
    /// <summary>
    /// Высота меню
    /// </summary>
    protected int Height { get; set; }
    /// <summary>
    /// Представления элментов меню
    /// </summary>
    private List<ViewMenuItemBase> _items = new List<ViewMenuItemBase>();
    /// <summary>
    /// Представления элментов меню
    /// </summary>
    protected ViewMenuItemBase[] Items
    {
      get
      {
        return _items.ToArray();
      }
    }
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenu"></param>
    public ViewMenu(Model.Menu.Menu parMenu)
    {
      foreach (Model.Menu.MenuItem elMenuItem in parMenu.Items)
        _items.Add(CreateMenuItem(elMenuItem));
    }
    /// <summary>
    /// Создать представление элемента меню
    /// </summary>
    /// <param name="parItem"></param>
    /// <returns></returns>
    protected abstract ViewMenuItemBase CreateMenuItem(Model.Menu.MenuItem parItem);

  }
}
