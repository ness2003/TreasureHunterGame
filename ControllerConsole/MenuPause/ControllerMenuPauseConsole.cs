using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controller.PauseMenu;
using Model.Menu;

namespace ControllerConsole.MenuPause
{
  /// <summary>
  /// Контроллер меню паузы для консольной версии.
  /// </summary>
  public class ControllerMenuPauseConsole : ControllerMenuPause
  {
    /// <summary>
    /// Флаг продолжения нахождения в меню паузы.
    /// </summary>
    private volatile bool _needExit = false;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuPause">Модель меню паузы</param>
    public ControllerMenuPauseConsole(Model.Menu.MenuPause parMenuPause) : base(parMenuPause)
    {
    }

    /// <summary>
    /// Запуск контроллера.
    /// </summary>
    public override void Start()
    {
      base.Start(); // Вызов метода Start базового класса
      _needExit = false; // Сброс флага выхода из меню
      ProcessKeyPress(); // Обработка ввода клавиш
    }

    /// <summary>
    /// Остановка контроллера.
    /// </summary>
    public override void Stop()
    {
      Clear(); // Очистка экрана
      base.Stop(); // Вызов метода Stop базового класса
    }

    /// <summary>
    /// Обработка нажатия клавиш.
    /// </summary>
    public void ProcessKeyPress()
    {
      do
      {
        ConsoleKeyInfo keyInfo = Console.ReadKey(); // Чтение информации о нажатой клавише

        switch (keyInfo.Key)
        {
          case ConsoleKey.UpArrow:
            _menuPause.SelectPrevItem(); // Выбор предыдущего пункта меню
            break;
          case ConsoleKey.DownArrow:
            _menuPause.SelectNextItem(); // Выбор следующего пункта меню
            break;
          case ConsoleKey.Enter:
            _menuPause.EnterSelectedItem(); // Ввод выбранного пункта меню
            _needExit = true; // Завершаем работу, когда пункт меню выбран
            break;
          case ConsoleKey.Escape:
            //_menuPause.Exit(); // Выход из меню паузы
            _needExit = true; // Завершаем работу при выходе из меню
            break;
        }

      } while (!_needExit); // Повторяем, пока не будет выбран пункт меню или не нажата клавиша для выхода
    }

    /// <summary>
    /// Очистка консольного экрана.
    /// </summary>
    public override void Clear()
    {
      Console.Clear(); // Очищаем экран консоли
    }
  }
}
