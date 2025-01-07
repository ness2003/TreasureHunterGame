
namespace View.Menu
{
  /// <summary>
  /// Абстрактное представление меню, предоставляющее базовые свойства и методы для создания и управления элементами меню.
  /// </summary>
  public abstract class ViewMenu : ViewBase
  {
    /// <summary>
    /// Координата по горизонтали для отображения меню.
    /// </summary>
    protected int X { get; set; }

    /// <summary>
    /// Координата по вертикали для отображения меню.
    /// </summary>
    protected int Y { get; set; }

    /// <summary>
    /// Ширина меню.
    /// </summary>
    protected int Width { get; set; }

    /// <summary>
    /// Высота меню.
    /// </summary>
    protected int Height { get; set; }

    /// <summary>
    /// Список элементов меню.
    /// </summary>
    private List<ViewMenuItemBase> _items = new List<ViewMenuItemBase>();

    /// <summary>
    /// Массив представлений элементов меню.
    /// </summary>
    protected ViewMenuItemBase[] Items
    {
      get
      {
        return _items.ToArray();
      }
    }

    /// <summary>
    /// Конструктор класса
    /// Инициализирует элементы меню на основе переданной модели.
    /// </summary>
    /// <param name="parMenu">Модель меню, содержащая элементы для отображения.</param>
    public ViewMenu(Model.Menu.Menu parMenu)
    {
      foreach (Model.Menu.MenuItem elMenuItem in parMenu.Items)
        _items.Add(CreateMenuItem(elMenuItem));
    }

    /// <summary>
    /// Абстрактный метод для создания представления элемента меню.
    /// </summary>
    /// <param name="parItem">Модель элемента меню.</param>
    /// <returns>Экземпляр <see cref="ViewMenuItemBase"/>, представляющий элемент меню.</returns>
    protected abstract ViewMenuItemBase CreateMenuItem(Model.Menu.MenuItem parItem);
  }
}
