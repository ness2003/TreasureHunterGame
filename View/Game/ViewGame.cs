using Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace View.Game
{
  /// <summary>
  /// Абстрактное представление игры.
  /// </summary>
  public abstract class ViewGame : ViewBase
  {
    // Модель игры
    protected ModelGame _modelGame;

    /// <summary>
    /// Конструктор. Подписывается на событие старта игры.
    /// </summary>
    /// <param name="parModelGame">Модель игры</param>
    public ViewGame(ModelGame parModelGame)
    {
      _modelGame = parModelGame;
      parModelGame.GameStarted += OnStartGame;
    }

    /// <summary>
    /// Абстрактный метод старта игры.
    /// </summary>
    public abstract void OnStartGame();
  }
}
