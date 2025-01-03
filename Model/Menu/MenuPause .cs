using System;
using Model.Menu;

namespace Model.Menu
{
  /// <summary>
  /// Меню паузы
  /// </summary>
  public class MenuPause : Menu
  {
    /// <summary>
    /// Значения элементов меню
    /// </summary>
    public enum MenuIds : int
    {
      /// <summary>
      /// Продолжить игру
      /// </summary>
      Continue,
      /// <summary>
      /// Главное меню
      /// </summary>
      MainMenu
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public MenuPause() : base("Меню паузы")
    {
      // Добавляем элементы в меню
      AddMenuItem(new MenuItem((int)MenuIds.Continue, "Продолжить игру"));
      AddMenuItem(new MenuItem((int)MenuIds.MainMenu, "Главное меню"));

      // Устанавливаем состояние для первой кнопки
      SetMenuItemStatus((int)MenuIds.Continue, MenuItem.Statuses.Selected);

      // Выбираем первый элемент
      SelectNextItem();
    }
  }
}
