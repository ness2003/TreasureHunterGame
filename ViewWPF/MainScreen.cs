using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ViewWPF
{
  /// <summary>
  /// Класс, представляющий главный экран приложения.
  /// </summary>
  public class MainScreen
  {
    /// <summary>
    /// Экземпляр класса MainScreen (синглтон).
    /// </summary>
    private static MainScreen _instance;

    /// <summary>
    /// Главное окно приложения.
    /// </summary>
    private Window _window;

    /// <summary>
    /// Контейнер для размещения элементов управления (StackPanel).
    /// </summary>
    private StackPanel _stackPanel;

    /// <summary>
    /// Свойство для доступа к StackPanel.
    /// </summary>
    public StackPanel StackPanel { get => _stackPanel; set => _stackPanel = value; }

    /// <summary>
    /// Свойство для доступа к окну.
    /// </summary>
    public Window Window { get => _window; set => _window = value; }

    /// <summary>
    /// Приватный конструктор для создания окна и его содержимого.
    /// </summary>
    private MainScreen()
    {
      _window = new Window();
      _window.ShowActivated = true;
      _window.WindowState = WindowState.Maximized;
      _stackPanel = new StackPanel();
      _stackPanel.VerticalAlignment = VerticalAlignment.Center;
      _stackPanel.HorizontalAlignment = HorizontalAlignment.Center; 
      _window.Content = _stackPanel;
      _window.Show(); 
    }

    /// <summary>
    /// Получение экземпляра MainScreen с использованием паттерна Singleton.
    /// </summary>
    /// <returns>Единственный экземпляр класса MainScreen.</returns>
    public static MainScreen GetInstance()
    {
      if (_instance == null)
      {
        _instance = new MainScreen();
      }
      return _instance;
    }
  }
}
