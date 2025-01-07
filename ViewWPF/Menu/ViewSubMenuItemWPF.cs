using System.Windows;
using View.Menu;

namespace ViewWPF.Menu
{
  /// <summary>
  /// Представление элемента многоуровнего меню
  /// </summary>
  public class ViewSubMenuItemWPF : ViewMenuSubItemBase
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parSubMenuItem"></param>
    public ViewSubMenuItemWPF(Model.Menu.MenuSubItem parSubMenuItem) : base(parSubMenuItem)
    {

    }
    /// <summary>
    /// Установить владельца
    /// </summary>
    /// <param name="parControl"></param>
    public void SetParentControl(FrameworkElement parControl)
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
