using Model.TableOfRecords;
using View.TableOfRecords;

namespace Controller.TableOfRecords
{
  /// <summary>
  /// Абстрактный контроллер для управления таблицей рекордов.
  /// </summary>
  public abstract class TableOfRecordsController : IController
  {
    /// <summary>
    /// Модель таблицы рекордов, содержащая данные о рекордах игроков.
    /// </summary>
    protected ModelTableOfRecords _modelTableOfRecords;

    /// <summary>
    /// Представление таблицы рекордов для отображения данных пользователю.
    /// </summary>
    protected ViewTableOfRecords? _viewTableOfRecords;

    /// <summary>
    /// Событие, вызываемое при возврате к предыдущему экрану.
    /// </summary>
    public event Action? GoToBack;

    /// <summary>
    /// Конструктор контроллера таблицы рекордов.
    /// </summary>
    /// <param name="parModelTableOfRecords">Экземпляр модели таблицы рекордов.</param>
    public TableOfRecordsController(ModelTableOfRecords parModelTableOfRecords)
    {
      _modelTableOfRecords = parModelTableOfRecords;
    }

    /// <summary>
    /// Абстрактный метод для запуска контроллера таблицы рекордов.
    /// </summary>
    public abstract void Start();

    /// <summary>
    /// Метод для вызова события возврата к предыдущему экрану.
    /// </summary>
    public void GoBackCall()
    {
      GoToBack?.Invoke();
    }

    /// <summary>
    /// Абстрактный метод для остановки контроллера таблицы рекордов.
    /// </summary>
    public abstract void Stop();
  }
}
