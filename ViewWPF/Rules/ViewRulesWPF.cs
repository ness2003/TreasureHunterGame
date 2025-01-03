using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Model.Rules;
using System.Windows.Media;

namespace ViewWPF.Rules
{
  public class ViewRulesWPF : View.Rules.ViewRules
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parModelRules">Экземпляр модели правил игры</param>
    public ViewRulesWPF(ModelRules parModelRules) : base(parModelRules) { }

    /// <summary>
    /// Инициализация параметров представления
    /// </summary>
    public override void Init()
    {
      Width = 700;
      Height = 700;
      X = 50;
      Y = 50;
    }

    /// <summary>
    /// Отрисовка представления правил и управления
    /// </summary>
    public override void Draw()
    {
      StackPanel panel = MainScreen.GetInstance().StackPanel;

      Grid grid = new Grid
      {
        Width = Width,
        Height = Height
      };

      grid.Margin = new Thickness(X, Y, 0, 0);

      StackPanel innerPanel = new StackPanel();

      innerPanel.Children.Add(new TextBlock
      {
        FontSize = 25,
        Text = "Правила игры",
        FontWeight = FontWeights.Bold,
        Margin = new Thickness(0, 0, 0, 10),
        HorizontalAlignment = HorizontalAlignment.Center,
        Foreground = Brushes.Green
      });

      foreach (var line in _modelRules.RulesText)
      {
        innerPanel.Children.Add(new TextBlock
        {
          Text = line,
          TextWrapping = TextWrapping.Wrap,
          MaxWidth = 500,
          FontSize = 16,
          Margin = new Thickness(0, 5, 0, 0),
          Foreground = Brushes.Black
        });
      }

      innerPanel.Children.Add(new TextBlock
      {
        Text = "______________________________",
        FontSize = 18,
        HorizontalAlignment = HorizontalAlignment.Center,
        Margin = new Thickness(0, 20, 0, 20)
      });

      innerPanel.Children.Add(new TextBlock
      {
        FontSize = 25,
        Text = "Управление",
        FontWeight = FontWeights.Bold,
        Margin = new Thickness(0, 0, 0, 10),
        HorizontalAlignment = HorizontalAlignment.Center,
        Foreground = Brushes.Orange
      });

      foreach (var line in _modelRules.ControlsText)
      {
        innerPanel.Children.Add(new TextBlock
        {
          Text = line,
          TextWrapping = TextWrapping.Wrap,
          MaxWidth = 350,
          FontSize = 16,
          Margin = new Thickness(0, 5, 0, 0),
          Foreground = Brushes.Blue
        });
      }

      ScrollViewer scrollViewer = new ScrollViewer
      {
        Content = innerPanel,
        VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
      };

      grid.Children.Add(scrollViewer);

      panel.Children.Add(grid);
    }
  }
}
