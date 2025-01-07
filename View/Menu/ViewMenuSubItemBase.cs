namespace View.Menu
{
  /// <summary>
  /// Абстрактное представление элемента многоуровневого меню.  
  /// Наследуется от <see cref="ViewMenuItemBase"/> и предоставляет базовую функциональность для элементов вложенного меню.
  /// </summary>
  public abstract class ViewMenuSubItemBase : ViewMenuItemBase
  {
    /// <summary>
    /// Конструктор класса   
    /// Инициализирует элемент подменю на основе переданной модели подменю.
    /// </summary>
    /// <param name="parMenuSubItem">Модель элемента подменю, содержащая данные и логику для представления.</param>
    public ViewMenuSubItemBase(Model.Menu.MenuSubItem parMenuSubItem)
        : base(parMenuSubItem)
    {
    }
  }
}
