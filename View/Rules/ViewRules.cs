using Model.Rules;

namespace View.Rules
{
  /// <summary>
  /// Абстрактное представление правил игры.  
  /// Отвечает за визуализацию и отображение правил игры для пользователя.
  /// </summary>
  public abstract class ViewRules : ViewBase
  {
    /// <summary>
    /// Координата по горизонтали. Определяет позицию представления на экране по оси X.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Координата по вертикали. Определяет позицию представления на экране по оси Y.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Ширина представления правил.
    /// </summary>
    public int Width { get; protected set; }

    /// <summary>
    /// Высота представления правил.
    /// </summary>
    public int Height { get; protected set; }

    /// <summary>
    /// Модель правил, содержащая текстовые описания правил и управления.
    /// </summary>
    protected ModelRules _modelRules = new ModelRules();

    /// <summary>
    /// Конструктор класса
    /// Инициализирует модель правил и вызывает методы для инициализации и отрисовки представления.
    /// </summary>
    /// <param name="parModelRules">Экземпляр модели правил, содержащий информацию о правилах и управлении.</param>
    public ViewRules(ModelRules parModelRules)
    {
      _modelRules = parModelRules;
      Init();
      Draw();
    }

    /// <summary>
    /// Абстрактный метод для инициализации представления правил.  
    /// Должен быть реализован в производных классах.
    /// </summary>
    public abstract void Init();
    
  }
}
