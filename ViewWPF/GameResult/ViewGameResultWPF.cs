using System.Windows.Controls;
using System.Windows;
using View.GameResult;
using Model.GameResult;
using System.Windows.Media;

namespace ViewWPF.GameResult
{
  /// <summary>
  /// Представление результатов игры для WPF
  /// </summary>
  public class ViewGameResultWPF : ViewGameResult
  {
    /// <summary>
    /// Поле для ввода имени игрока при записи рекорда
    /// </summary>
    private TextBox _name;

    /// <summary>
    /// Свойство для доступа к полю ввода имени игрока
    /// </summary>
    public TextBox Name { get => _name; set => _name = value; }

    /// <summary>
    /// Конструктор представления результатов игры
    /// </summary>
    /// <param name="parModelGameResult">Модель результатов игры</param>
    public ViewGameResultWPF(ModelGameResult parModelGameResult) : base(parModelGameResult)
    {
      _modelGameResult = parModelGameResult;
      Draw();
    }

    /// <summary>
    /// Нарисовать представление результатов игры (переопределение базового метода)
    /// </summary>
    public override void Draw()
    {
      StackPanel panel = MainScreen.GetInstance().StackPanel;
      panel.Children.Add(new TextBlock
      {
        FontSize = 24,
        Text = $"Конец игры! Score: {_modelGameResult.Score} | Level: {_modelGameResult.Level}",
        FontWeight = FontWeights.Bold,
        Margin = new Thickness(0, 0, 0, 10),
        HorizontalAlignment = HorizontalAlignment.Center,
        Foreground = new SolidColorBrush(Colors.CadetBlue)  // Цвет текста
      });

      // Сообщение о новом рекорде
      panel.Children.Add(new TextBlock
      {
        FontSize = 20,
        Text = "Новый Рекорд! Введите ваше имя:",
        FontWeight = FontWeights.Bold,
        Margin = new Thickness(0, 0, 0, 10),
        HorizontalAlignment = HorizontalAlignment.Center,
        Foreground = new SolidColorBrush(Colors.Black)  // Цвет текста
      });

      // Поле для ввода имени игрока
      _name = new TextBox
      {
        FontSize = 18,
        FontWeight = FontWeights.Bold,
        HorizontalAlignment = HorizontalAlignment.Center,
        Width = 250,
        Background = new SolidColorBrush(Colors.White),
        Foreground = new SolidColorBrush(Colors.Black),
        Margin = new Thickness(0, 10, 0, 20)
      };
      panel.Children.Add(_name);
    }
  }
}
