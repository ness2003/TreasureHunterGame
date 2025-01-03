using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using View.GameResult;
using Model.GameResult;
using System.Windows.Media;

namespace ViewWPF.GameResult
{
  public class ViewGameResultWPF : ViewGameResult
  {
    /// <summary>
    /// Ввод имени игрока для записи рекорда
    /// </summary>
    TextBox _name;

    /// <summary>
    /// Ввод имени игрока для записи рекорда
    /// </summary>
    public TextBox Name { get => _name; set => _name = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="gameResult"></param>
    public ViewGameResultWPF(ModelGameResult parModelGameResult) : base(parModelGameResult)
    {
      StackPanel panel = MainScreen.GetInstance().StackPanel;

      // Заголовок с результатами игры
      panel.Children.Add(new TextBlock
      {
        FontSize = 24,
        Text = $"Конец игры! Score: {parModelGameResult.Score} | Level: {parModelGameResult.Level}",
        FontWeight = FontWeights.Bold,
        Margin = new Thickness(0, 0, 0, 10),
        HorizontalAlignment = HorizontalAlignment.Center,
        Foreground = new SolidColorBrush(Colors.White)  // Белый цвет текста
      });

      // Сообщение о новом рекорде
      panel.Children.Add(new TextBlock
      {
        FontSize = 20,
        Text = "Новый Рекорд! Введите ваше имя:",
        FontWeight = FontWeights.Bold,
        Margin = new Thickness(0, 0, 0, 10),
        HorizontalAlignment = HorizontalAlignment.Center,
        Foreground = new SolidColorBrush(Colors.White)  // Белый цвет текста
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

    /// <summary>
    /// Нарисовать представление
    /// </summary>
    public override void Draw()
    {
    }
  }
}
