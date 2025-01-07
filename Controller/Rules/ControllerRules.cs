using Model.Rules;
using View.Rules;

namespace Controller.Rules
{
  /// <summary>
  /// Абстрактный контроллер правил.
  /// </summary>
  public abstract class ControllerRules : IController
  {
    /// <summary>
    /// Модель правил.
    /// </summary>
    protected ModelRules _modelRules;

    /// <summary>
    /// Представление правил.
    /// </summary>
    protected ViewRules? _viewRules;

    /// <summary>
    /// Событие для возврата назад.
    /// </summary>
    public event Action? GoToBack;

    /// <summary>
    /// Конструктор контроллера правил
    /// </summary>
    /// <param name="parModelRules">Экземпляр модели правил</param>
    public ControllerRules(ModelRules parModelRules)
    {
      _modelRules = parModelRules;
    }

    /// <summary>
    /// Абстрактный метод для запуска контроллера.
    /// </summary>
    public abstract void Start();

    /// <summary>
    /// Вызов события для возврата назад.
    /// </summary>
    public void GoBackCall()
    {
      GoToBack?.Invoke();
    }
    /// <summary>
    /// Абстрактный метод остановки контроллера.
    /// </summary>
    public abstract void Stop();
  }
}
