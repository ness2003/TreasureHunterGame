using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Menu;

namespace ViewConsole.Menu
{
  /// <summary>
  /// Представление меню
  /// </summary>
  public class ViewMenuConsole : ViewMenu
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenu"></param>
    public ViewMenuConsole(Model.Menu.Menu parMenu) : base(parMenu)
    {
      Init();
    }
    /// <summary>
    /// Создать представление элмента меню
    /// </summary>
    /// <param name="parItem"></param>
    /// <returns></returns>
    protected override ViewMenuItemBase CreateMenuItem(Model.Menu.MenuItem parItem)
    {
      if (parItem is Model.Menu.MenuSubItem)
        return new ViewMenuSubItemConsole((Model.Menu.MenuSubItem)parItem);
      else if (parItem is Model.Menu.MenuItem)
        return new ViewMenuItemConsole(parItem);

      return null;
    }
    /// <summary>
    /// Нарисовать представление меню
    /// </summary>
    public override void Draw()
    {
      foreach (ViewMenuItemBase elViewMenuItemBase in Items)
      {
        elViewMenuItemBase.Draw();
      }
    }
    /// <summary>
    /// Инициализировать представление меню
    /// </summary>
    private void Init()
    {
      Console.Clear();
      Console.CursorVisible = false;

      int height = 0;
      int maxWidth = 0;

      Height = 2 * Items.Length;
      Width = 20;

      X = Console.WindowWidth / 2 - Width / 2;
      Y = Console.WindowHeight / 2 - Height / 2;

      int y = Y;
      foreach (ViewMenuItemBase elViewMenuItemBase in Items)
      {
        elViewMenuItemBase.X = X;
        elViewMenuItemBase.Y = y++;
      }
    }


  }
}
