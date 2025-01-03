using Model.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using View.Menu;
using ViewWPF.Menu;


namespace ViewWPF.Menu
{
  /// <summary>
  /// Представление меню
  /// </summary>
  public class ViewMenuWPF : ViewMenu
  { 

  /// <summary>
  /// Нарисовать представление
  /// </summary>
  public override void Draw()
  {

      ViewMenuItemBase[] items = Items;
    foreach (ViewMenuItemBase elViewMenuItemBase in items)
    {
      elViewMenuItemBase.Draw();
    }
  }
  /// <summary>
  /// Создать представление элмента меню
  /// </summary>
  /// <param name="parItem"></param>
  /// <returns></returns>
  protected override ViewMenuItemBase CreateMenuItem(Model.Menu.MenuItem parItem)
  {
    if (parItem is Model.Menu.MenuSubItem)
      return new ViewSubMenuItemWPF((MenuSubItem)parItem);
    else if (parItem is Model.Menu.MenuItem)
      return new ViewMenuItemWPF(parItem, MainScreen.GetInstance().StackPanel);
    return null;
  } /// Представление меню
    /// </summary>
    /// <param name="parMenu"></param>
  public ViewMenuWPF(Model.Menu.Menu parMenu) : base(parMenu)
  {
    Draw();
  }
  /// <summary>
  /// Нарисовать представление
  /// </summary>
 
}
}
