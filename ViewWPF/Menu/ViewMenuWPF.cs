using Model.Menu;
using View.Menu;


namespace ViewWPF.Menu
{
  /// <summary>
  /// Представление меню
  /// </summary>
  public class ViewMenuWPF : ViewMenu
  {

    /// Представление меню
    /// </summary>
    /// <param name="parMenu"></param>
    public ViewMenuWPF(Model.Menu.Menu parMenu) : base(parMenu)
    {
      Draw();
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
    }

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

  }
}
