using Model.Game;
using Model.Game.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using View.Game;

namespace ViewWPF.Game
{
  public class ViewGameWPF : ViewGame
  {
    private readonly Window _gameWindow;
    private readonly Grid _mainGrid;
    private readonly Canvas _gameCanvas;
    private readonly Canvas _infoCanvas;
    private readonly Canvas _goalCanvas;  // Новый Canvas для целей уровня
    private readonly ImageBrush _backgroundBrush;

    private TextBlock _timeTextBlock;
    private TextBlock _scoreTextBlock;
    private TextBlock _levelTextBlock;

    private TextBlock _goldCoinTextBlock;
    private TextBlock _silverCoinTextBlock;
    private TextBlock _bronzeCoinTextBlock;


    public ViewGameWPF(ModelGame parModelGame) : base(parModelGame)
    {
      _gameWindow = MainScreen.GetInstance().Window;

      // Основной контейнер
      _mainGrid = new Grid();
      _gameWindow.Content = _mainGrid;

      // Создаем строки для Grid
      _mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Для информационной панели    
      _mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Для целей уровня
      _mainGrid.RowDefinitions.Add(new RowDefinition()); // Для игрового поля

      // Информационная панель (Canvas)
      _infoCanvas = new Canvas { Height = 50, Background = Brushes.Black };
      Grid.SetRow(_infoCanvas, 0);
      _mainGrid.Children.Add(_infoCanvas);

      // Игровое поле (Canvas)
      _gameCanvas = new Canvas { Width = 1000, Height = 500 };
      Grid.SetRow(_gameCanvas, 2);
      _mainGrid.Children.Add(_gameCanvas);

      // Canvas для целей уровня
      _goalCanvas = new Canvas { Width = 1000, Height = 150, Background = Brushes.BurlyWood };
      Grid.SetRow(_goalCanvas, 1);
      _mainGrid.Children.Add(_goalCanvas);

      // Устанавливаем фон
      _backgroundBrush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/ViewWPF;component/Assets/background.png")))
      {
        Stretch = Stretch.Fill
      };

      _gameCanvas.Background = _backgroundBrush;

      Application.Current.Dispatcher.Invoke(InitializeScreen);
      _modelGame.NeedRedraw += OnCanRender;
    }

    private void CreateGoalPanel()
    {
      var goal = _modelGame.CurrentGoal;

      // Заголовок "Цели уровня"
      var titleTextBlock = CreateTextBlock("Цели уровня", Brushes.White);
      titleTextBlock.FontSize = 20;
      titleTextBlock.Margin = new Thickness(0, 10, 0, 10);
      _goalCanvas.Children.Add(titleTextBlock);

      // Центрируем заголовок
      titleTextBlock.SizeChanged += (sender, e) =>
      {
        Canvas.SetLeft(titleTextBlock, (_goalCanvas.Width - titleTextBlock.ActualWidth) / 2);
        Canvas.SetTop(titleTextBlock, 10);
      };

      // Создаём монеты
      CreateGoalCoin(ObjectType.GoldCell, goal.CountGoldCoins, _goalCanvas.Width/2 - 150, 50);
      CreateGoalCoin(ObjectType.SilverCell, goal.CountSilverCoins, _goalCanvas.Width/2, 50);
      CreateGoalCoin(ObjectType.BronzeCell, goal.CountBronzeCoins, _goalCanvas.Width/2 + 150, 50);

      // Требуемый счёт
      var scoreTextBlock = new TextBlock
      {
        Text = $"Требуемый счет: {goal.ScoreTarget}",
        FontSize = 16,
        FontWeight = FontWeights.Bold,
        Foreground = Brushes.Black,
        Margin = new Thickness(10)
      };
      _goalCanvas.Children.Add(scoreTextBlock);

      // Центрируем текст с требуемым счётом
      scoreTextBlock.SizeChanged += (sender, e) =>
      {
        Canvas.SetLeft(scoreTextBlock, (_goalCanvas.Width - scoreTextBlock.ActualWidth) / 2);
        Canvas.SetTop(scoreTextBlock, 100);
      };
    }



    private void UpdateGoalPanel()
    {
      var goal = _modelGame.CurrentGoal;

      // Обновляем текст с количеством монет
      UpdateCoinText(ObjectType.GoldCell, goal.CountGoldCoins);
      UpdateCoinText(ObjectType.SilverCell, goal.CountSilverCoins);
      UpdateCoinText(ObjectType.BronzeCell, goal.CountBronzeCoins);

      // Обновляем требуемый счёт
      var scoreTextBlock = _goalCanvas.Children.OfType<TextBlock>().FirstOrDefault(t => t.Text.StartsWith("Требуемый"));
      if (scoreTextBlock != null)
      {
        scoreTextBlock.Text = $"Требуемый счет: {goal.ScoreTarget}";
      }
    }


    private void UpdateCoinText(ObjectType coinType, int coinCount)
    {
      // Обновляем соответствующий TextBlock
      if (coinType == ObjectType.GoldCell && _goldCoinTextBlock != null)
        _goldCoinTextBlock.Text = coinCount.ToString();
      else if (coinType == ObjectType.SilverCell && _silverCoinTextBlock != null)
        _silverCoinTextBlock.Text = coinCount.ToString();
      else if (coinType == ObjectType.BronzeCell && _bronzeCoinTextBlock != null)
        _bronzeCoinTextBlock.Text = coinCount.ToString();
    }


    private void CreateGoalCoin(ObjectType coinType, int coinCount, double xPosition, double yPosition)
    {
      var coinImage = new ImageBrush(new BitmapImage(new Uri(GetImagePath(coinType))));

      var coinEllipse = new Ellipse
      {
        Width = 40,
        Height = 40,
        Fill = coinImage
      };
      Canvas.SetLeft(coinEllipse, xPosition);
      Canvas.SetTop(coinEllipse, yPosition);
      _goalCanvas.Children.Add(coinEllipse);

      // Текст с количеством монет
      var countTextBlock = new TextBlock
      {
        Text = coinCount.ToString(),
        FontSize = 16,
        FontWeight = FontWeights.Bold,
        Foreground = Brushes.Black,
        Margin = new Thickness(5)
      };
      Canvas.SetLeft(countTextBlock, xPosition + 10);
      Canvas.SetTop(countTextBlock, yPosition + 40);
      _goalCanvas.Children.Add(countTextBlock);

      // Сохраняем ссылку на TextBlock в зависимости от типа монеты
      if (coinType == ObjectType.GoldCell)
        _goldCoinTextBlock = countTextBlock;
      else if (coinType == ObjectType.SilverCell)
        _silverCoinTextBlock = countTextBlock;
      else if (coinType == ObjectType.BronzeCell)
        _bronzeCoinTextBlock = countTextBlock;
    }


    private void OnCanRender()
    {
      Application.Current.Dispatcher.Invoke(Render);
    }

    /// <summary>
    /// Инициализация экрана
    /// </summary>
    public void InitializeScreen()
    {
      CreateInfoPanel();
      CreateGoalPanel();
    }

    private void CreateInfoPanel()
    {
      _timeTextBlock = CreateTextBlock("⏱️ Время: 0 сек", Brushes.LightGreen);
      _scoreTextBlock = CreateTextBlock("🏆 Счет: 0", Brushes.Gold);
      _levelTextBlock = CreateTextBlock("📊 Уровень: 1", Brushes.Cyan);

      var panel = new StackPanel
      {
        Orientation = Orientation.Horizontal,
        Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)),
        Margin = new Thickness(5)
      };

      panel.Children.Add(_timeTextBlock);
      panel.Children.Add(_scoreTextBlock);
      panel.Children.Add(_levelTextBlock);

      _infoCanvas.Children.Add(panel);
    }

    private TextBlock CreateTextBlock(string text, Brush color)
    {
      return new TextBlock
      {
        Text = text,
        FontSize = 16,
        FontWeight = FontWeights.Bold,
        Foreground = color,
        Margin = new Thickness(5)
      };
    }

    private void UpdateGameInfo()
    {
      if (_timeTextBlock != null && _scoreTextBlock != null && _levelTextBlock != null)
      {
        var gameState = _modelGame;
        _timeTextBlock.Text = $"⏱️ Время: {gameState.RemainingTime:F1} сек";
        _scoreTextBlock.Text = $"🏆 Счет: {gameState.Score}";
        _levelTextBlock.Text = $"📊 Уровень: {gameState.Level}";
      }
    }

    public override void Draw()
    {
      _gameCanvas.Children.Clear();
      DrawGameField();
      DrawPlayer();
      DrawGameObjects(_modelGame.Coins);
      DrawGameObjects(_modelGame.Bonuses);
      DrawGameObjects(_modelGame.BadObjects);

      UpdateGameInfo();
      UpdateGoalPanel();  // Обновляем цели уровня
    }

    private void DrawGameField()
    {
      Rectangle gameField = new Rectangle
      {
        Width = _modelGame.Width,
        Height = _modelGame.Height,
        Stroke = Brushes.Black,
        StrokeThickness = 5
      };
      Canvas.SetLeft(gameField, 0);
      Canvas.SetTop(gameField, 0);
      _gameCanvas.Children.Add(gameField);
    }

    private void DrawPlayer()
    {
      var player = _modelGame.Player;
      Rectangle playerRect = new Rectangle
      {
        Width = player.Width,
        Height = player.Height,
        Fill = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/ViewWPF;component/Assets/player.png")))
      };
      Canvas.SetLeft(playerRect, player.X);
      Canvas.SetTop(playerRect, player.Y);
      _gameCanvas.Children.Add(playerRect);
    }

    private void DrawGameObjects(IEnumerable<GameObject> gameObjects)
    {
      foreach (var obj in gameObjects)
      {
        ImageBrush objectBrush = new ImageBrush(new BitmapImage(new Uri(GetImagePath(obj.ObjectType))));

        Ellipse objectEllipse = new Ellipse
        {
          Width = obj.Radius * 2,
          Height = obj.Radius * 2,
          Fill = objectBrush
        };

        Canvas.SetLeft(objectEllipse, obj.X);
        Canvas.SetTop(objectEllipse, obj.Y);
        _gameCanvas.Children.Add(objectEllipse);
      }
    }

    private string GetImagePath(ObjectType objectType)
    {
      return objectType switch
      {
        ObjectType.GoldCell => "pack://application:,,,/ViewWPF;component/Assets/gold_coin.png",
        ObjectType.SilverCell => "pack://application:,,,/ViewWPF;component/Assets/silver_coin.png",
        ObjectType.BronzeCell => "pack://application:,,,/ViewWPF;component/Assets/bronze_coin.png",
        ObjectType.Magnet => "pack://application:,,,/ViewWPF;component/Assets/magnet.png",
        ObjectType.Timer => "pack://application:,,,/ViewWPF;component/Assets/timer.png",
        ObjectType.Mul => "pack://application:,,,/ViewWPF;component/Assets/mul.png",
        ObjectType.Thief => "pack://application:,,,/ViewWPF;component/Assets/thief.png",
        ObjectType.Meteorite => "pack://application:,,,/ViewWPF;component/Assets/meteorite.png",
        _ => "pack://application:,,,/ViewWPF;component/Assets/default.png"
      };
    }

    private void Render()
    {
      Draw();
    }

    public override void OnStartGame()
    {
      Draw();
    }
  }
}
