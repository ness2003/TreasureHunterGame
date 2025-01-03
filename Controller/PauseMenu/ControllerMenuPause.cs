using System;
using Controller.Game;
using Controller.Rules;
using Controller.TableOfRecords;
using Model.Menu;
using View.Menu;

namespace Controller.PauseMenu
{
  /// <summary>
  /// Контроллер меню паузы.
  /// Управляет взаимодействием между моделью и представлением меню паузы.
  /// </summary>
  public abstract class ControllerMenuPause : IController
  {
    /// <summary>
    /// Модель меню паузы.
    /// </summary>
    protected MenuPause _menuPause;

    /// <summary>
    /// Представление меню паузы.
    /// </summary>
    protected ViewMenu? _pauseMenuView;

    /// <summary>
    /// Событие, вызываемое при возврате к предыдущему экрану.
    /// </summary>
    public event Action? GoToBack;

    /// <summary>
    /// Событие, вызываемое при переходе к главному меню.
    /// </summary>
    public event Action? GoToMainMenu;

    /// <summary>
    /// Конструктор контроллера меню паузы.
    /// Инициализирует модель меню паузы.
    /// </summary>
    /// <param name="parMenuPause">Экземпляр модели меню паузы.</param>
    public ControllerMenuPause(MenuPause parMenuPause)
    {
      _menuPause = parMenuPause;
    }

    /// <summary>
    /// Инициализация контроллера меню паузы.
    /// Подписывает методы на события элементов меню.
    /// </summary>
    public virtual void Start()
    {
      Clear();
      _menuPause[(int)MenuPause.MenuIds.Continue].Enter += GoBackCall;
      _menuPause[(int)MenuPause.MenuIds.Continue].Enter += Stop;
      _menuPause[(int)MenuPause.MenuIds.MainMenu].Enter += GoToMainMenuCall;
    }

    /// <summary>
    /// Останавливает контроллер меню паузы.
    /// Отписывает методы от событий элементов меню.
    /// </summary>
    public virtual void Stop()
    {
      Clear();
      _menuPause[(int)MenuPause.MenuIds.Continue].Enter -= GoBackCall;
      _menuPause[(int)MenuPause.MenuIds.MainMenu].Enter -= GoToMainMenuCall;
      _menuPause[(int)MenuPause.MenuIds.Continue].Enter -= Stop;
    }

    /// <summary>
    /// Метод, вызываемый при выборе пункта "Продолжить".
    /// Вызывает событие <see cref="GoToBack"/>.
    /// </summary>
    public void GoBackCall()
    {
      GoToBack?.Invoke();
    }

    /// <summary>
    /// Метод, вызываемый при выборе пункта "Главное меню".
    /// Вызывает событие <see cref="GoToMainMenu"/>.
    /// </summary>
    public void GoToMainMenuCall()
    {
      GoToMainMenu?.Invoke();
    }

    /// <summary>
    /// Очищает ресурсы контроллера.
    /// Должен быть реализован в производных классах.
    /// </summary>
    public abstract void Clear();
  }
}
