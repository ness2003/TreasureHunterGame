using System;
using static Model.Menu.MenuItem;

namespace Model.Menu
{
  /// <summary>
  /// Конкретная реализация главного меню.
  /// </summary>
  public class MenuMain : Menu
  {
    /// <summary>
    /// Идентификаторы элементов главного меню.
    /// </summary>
    public enum MenuIds : int
    {
      /// <summary>
      /// Новая игра.
      /// </summary>
      New = 0,

      /// <summary>
      /// Просмотр правил.
      /// </summary>
      Rules = 1,

      /// <summary>
      /// Просмотр рекордов.
      /// </summary>
      Records = 2,

      /// <summary>
      /// Выход из приложения.
      /// </summary>
      Exit = 3,

      /// <summary>
      /// Начать новую игру.
      /// </summary>
      NewGame = 4
    }

    /// <summary>
    /// Конструктор главного меню.
    /// </summary>
    public MenuMain() : base(Resources.MainMenu)
    {
      // Добавляем элементы меню с идентификаторами и названиями.
      AddMenuItem(new MenuItem((int)MenuIds.New, "Новая игра"));
      AddMenuItem(new MenuItem((int)MenuIds.Rules, "Правила")); 
      AddMenuItem(new MenuItem((int)MenuIds.Records, "Рекорды"));
      AddMenuItem(new MenuItem((int)MenuIds.Exit, "Выход"));

      // Делаем пункт "Правила" неактивным.
      SetMenuItemStatus((int)MenuIds.Rules, MenuItem.Statuses.Disabled);

      // Устанавливаем фокус на следующий элемент.
      SelectNextItem();
    }

    /// <summary>
    /// Добавляет новый элемент "Новая игра" в меню.
    /// </summary>
    public void AddMenuItemNewGame()
    {
      AddMenuItem(new MenuItem((int)MenuIds.NewGame, "Новая игра"));
    }
  }
}
