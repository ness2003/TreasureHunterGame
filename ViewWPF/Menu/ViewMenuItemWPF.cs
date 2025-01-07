using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Effects; // Обязательно добавьте этот using
using View.Menu;
using System.Windows.Markup;

namespace ViewWPF.Menu
{
  /// <summary>
  /// WPF представление элемента меню
  /// </summary>
  public class ViewMenuItemWPF : ViewMenuItemBase
  {
    /// <summary>
    /// Высота элемента меню
    /// </summary>
    private const int HEIGHT = 2;

    /// <summary>
    /// Контейнер кнопки меню (прямоугольник)
    /// </summary>
    private Rectangle _rectangle;

    /// <summary>
    /// Текст на кнопке меню
    /// </summary>
    private TextBlock _textBlock;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMenuItemBase"></param>
    /// <param name="parControl"></param>
    public ViewMenuItemWPF(Model.Menu.MenuItem parMenuItemBase, FrameworkElement parControl) : base(parMenuItemBase)
    {
      if (parMenuItemBase != null)
      {
        Height = HEIGHT;
        Width = MenuItem.Name.Length * 9;
      }

      // Слушаем изменения в состоянии
      parMenuItemBase.NeedRedraw += Draw;

      // Создаем основной контейнер (Grid)
      var grid = new Grid();
      ((IAddChild)parControl).AddChild(grid);

      // Инициализация кнопки
      _rectangle = new Rectangle
      {
        Margin = new Thickness(X, Y + 10, 0, 0),
        Width = 150,
        Height = 40, // Увеличим высоту для лучшего вида
        RadiusX = 10, // Закругляем углы
        RadiusY = 10
      };

      // Инициализация текста
      _textBlock = new TextBlock
      {
        Text = parMenuItemBase.Name,
        Foreground = Brushes.White, // Цвет текста
        FontSize = 16,
        FontWeight = FontWeights.Bold, // Жирный шрифт для текста
        HorizontalAlignment = HorizontalAlignment.Center, // Центрируем текст по горизонтали
        VerticalAlignment = VerticalAlignment.Center // Центрируем текст по вертикали
      };

      // Добавляем прямоугольник и текст в Grid
      grid.Children.Add(_rectangle);
      grid.Children.Add(_textBlock);
    }

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
      // Применяем градиент в зависимости от состояния
      switch (MenuItem.Status)
      {
        case Model.Menu.MenuItem.Statuses.Selected:
          _rectangle.Fill = new LinearGradientBrush(Colors.Green, Colors.DarkGreen, 45); // Градиент зеленого
          _textBlock.Foreground = Brushes.White; // Белый текст для выбранной кнопки
          break;
        case Model.Menu.MenuItem.Statuses.Focused:
          _rectangle.Fill = new LinearGradientBrush(Colors.Yellow, Colors.Orange, 45); // Градиент желтого
          _textBlock.Foreground = Brushes.Black; // Черный текст для фокусированной кнопки
          break;
        case Model.Menu.MenuItem.Statuses.Disabled:
          _rectangle.Fill = Brushes.DarkGray;
          _textBlock.Foreground = Brushes.LightGray; // Светло-серый текст для отключенной кнопки
          break;
        case Model.Menu.MenuItem.Statuses.None:
          _rectangle.Fill = new LinearGradientBrush(Colors.Gray, Colors.DarkGray, 45); // Градиент серого
          _textBlock.Foreground = Brushes.White; // Белый текст по умолчанию
          break;
      }

      // Добавляем тень для кнопки (эффект 3D)
      _rectangle.Effect = new DropShadowEffect
      {
        Color = Colors.Black,
        Direction = 315,
        ShadowDepth = 3,
        BlurRadius = 4,
        Opacity = 0.5
      };
    }
  }
}
