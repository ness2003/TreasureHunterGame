
namespace View.Menu
{
  /// <summary>
  /// Абстрактное представление элемента меню, предоставляющее базовые свойства и методы для отображения и управления элементами меню.
  /// </summary>
  public abstract class ViewMenuItemBase : ViewBase
  {
    /// <summary>
    /// Координата по горизонтали для отображения элемента меню.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Координата по вертикали для отображения элемента меню.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Ширина представления элемента меню.
    /// </summary>
    public int Width { get; protected set; }

    /// <summary>
    /// Высота представления элемента меню.
    /// </summary>
    public int Height { get; protected set; }

    /// <summary>
    /// Модель элемента меню, содержащая данные и логику элемента.
    /// </summary>
    private Model.Menu.MenuItem _menuItem = null;

    /// <summary>
    /// Доступ к модели элемента меню.
    /// </summary>
    protected Model.Menu.MenuItem MenuItem
    {
      get { return _menuItem; }
    }

    /// <summary>
    /// Конструктор абстрактного класса <see cref="ViewMenuItemBase"/>.
    /// Инициализирует элемент меню на основе переданной модели.
    /// </summary>
    /// <param name="parMenuItemBase">Модель элемента меню, используемая для создания представления.</param>
    public ViewMenuItemBase(Model.Menu.MenuItem parMenuItemBase)
    {
      _menuItem = parMenuItemBase;
    }
  }
}
