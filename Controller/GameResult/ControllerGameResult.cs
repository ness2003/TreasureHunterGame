using Model;
using Model.Game;
using Model.GameResult;
using System;
using View.GameResult;

namespace Controller.GameResult
{
  /// <summary>
  /// Абстрактный контроллер для управления результатами игры.
  /// </summary>
  public abstract class ControllerGameResult : IController
  {
    /// <summary>
    /// Модель результата игры, содержащая данные об уровне и очках.
    /// </summary>
    protected ModelGameResult _modelGameResult;

    /// <summary>
    /// Представление результатов игры.
    /// </summary>
    protected ViewGameResult? _viewGameResult;

    /// <summary>
    /// Событие, вызываемое при возврате к предыдущему экрану.
    /// </summary>
    public event Action? GoToBack;

    /// <summary>
    /// Конструктор контроллера результатов игры.
    /// </summary>
    /// <param name="parModelGameResult">Экземпляр модели результата игры.</param>
    public ControllerGameResult(ModelGameResult parModelGameResult)
    {
      _modelGameResult = parModelGameResult;
    }

    /// <summary>
    /// Абстрактный метод для запуска контроллера результатов игры.
    /// </summary>
    public abstract void Start();

    /// <summary>
    /// Абстрактный метод для остановки контроллера результатов игры.
    /// </summary>
    public abstract void Stop();

    /// <summary>
    /// Устанавливает новые значения очков и уровня в модели результата игры.
    /// </summary>
    /// <param name="parScore">Новое значение очков.</param>
    /// <param name="parLevel">Новое значение уровня.</param>
    public void ChangeScoreAndLevel(int parScore, int parLevel)
    {
      _modelGameResult.Score = parScore;
      _modelGameResult.Level = parLevel;
    }

    /// <summary>
    /// Вызывает событие для возврата к предыдущему экрану.
    /// </summary>
    public void GoBackCall()
    {
      GoToBack?.Invoke();
    }
  }
}
