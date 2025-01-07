using Model.GameResult;

namespace View.GameResult
{
  /// <summary>
  /// Абстрактное представление результата игры.
  /// </summary>
  public abstract class ViewGameResult : ViewBase
  {
    // Модель результата игры
    protected ModelGameResult _modelGameResult;

    /// <summary>
    /// Конструктор с параметром для инициализации модели.
    /// </summary>
    /// <param name="parGameResult">Результаты игры</param>
    public ViewGameResult(ModelGameResult parGameResult)
    {
      _modelGameResult = parGameResult;
    }
  }
}
