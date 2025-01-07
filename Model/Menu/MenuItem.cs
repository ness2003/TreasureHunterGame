
namespace Model.Menu
{
  /// <summary>
  /// Класс, представляющий элемент меню.
  /// </summary>
  public class MenuItem
  {
    /// <summary>
    /// Перечисление возможных состояний элемента меню.
    /// </summary>
    public enum Statuses : int
    {
      /// <summary>
      /// Элемент выбран.
      /// </summary>
      Selected,

      /// <summary>
      /// На элемент наведён фокус.
      /// </summary>
      Focused,

      /// <summary>
      /// Элемент отключен.
      /// </summary>
      Disabled,

      /// <summary>
      /// Элемент не имеет состояния.
      /// </summary>
      None
    }

    /// <summary>
    /// Делегат для обработки события перерисовки элемента.
    /// </summary>
    public delegate void dNeedRedraw();

    /// <summary>
    /// Делегат для обработки события активации элемента (например, нажатия).
    /// </summary>
    public delegate void dEnter();

    /// <summary>
    /// Событие, вызываемое при необходимости перерисовки элемента.
    /// </summary>
    public event dNeedRedraw NeedRedraw = null;

    /// <summary>
    /// Событие, вызываемое при активации элемента.
    /// </summary>
    public event dEnter Enter = null;

    /// <summary>
    /// Имя элемента меню.
    /// </summary>
    private string _name = string.Empty;

    /// <summary>
    /// Текущее состояние элемента меню.
    /// </summary>
    private Statuses _status = Statuses.None;

    /// <summary>
    /// Имя элемента меню.
    /// </summary>
    public string Name
    {
      get => _name;
      set => _name = value;
    }

    /// <summary>
    /// Текущее состояние элемента меню.
    /// </summary>
    public Statuses Status
    {
      get => _status;
      set
      {
        _status = value;
        // Вызываем событие перерисовки при изменении состояния.
        NeedRedraw?.Invoke();

        // Если элемент был выбран, обрабатываем событие нажатия.
        if (_status == Statuses.Selected)
          ProcessEnter();
      }
    }

    /// <summary>
    /// Уникальный идентификатор элемента меню.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Конструктор элемента меню.
    /// </summary>
    /// <param name="parId">Уникальный идентификатор элемента.</param>
    /// <param name="parName">Имя элемента.</param>
    public MenuItem(int parId, string parName)
    {
      Id = parId;
      _name = parName;
      Status = Statuses.None;
    }

    /// <summary>
    /// Вызов события активации элемента.
    /// </summary>
    protected void CallEnterEvent()
    {
      Enter?.Invoke();
    }

    /// <summary>
    /// Обработка активации элемента (например, нажатие кнопки).
    /// </summary>
    protected virtual void ProcessEnter()
    {
      CallEnterEvent();
    }

    /// <summary>
    /// Отписывает все события элемента.
    /// </summary>
    public void Unsubscribe()
    {
      NeedRedraw = null;
    }
  }
}
