using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Life
{
    public partial class MainWindow : Window
    {
        
        static int frameSkip = 1;    //Пропуск фреймов (при больших значениях повышается производительность)

        Game game;

        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            game = new Game(this);
            InitializeComponent();
            Refresh_Game();
            DataContext = game.Controler;
        }
        private void Refresh_Timer(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timer.Interval = new TimeSpan(0, 0, 0, 0, game.Controler.speed);
        }
        private void InitControl()
        {
            game.Controler.speed = 4;
        }
        private void Refresh_Game()
        {
            game = new Game(this);
            InitControl();

            Height = MinHeight = Game.fieldHeight + 6 + 33;
            Width = MinWidth = Game.fieldWidth + 200 + 6 + 33;

            CanvasMap.Height = Game.fieldHeight;
            CanvasMap.Width = Game.fieldWidth;

            VerticalWall.X1 = VerticalWall.X2 = Game.fieldWidth;
            VerticalWall.Y2 = Game.fieldHeight;
            HorizontlWall.Y1 = HorizontlWall.Y2 = Game.fieldHeight;
            HorizontlWall.X2 = Game.fieldWidth;

            scroll.Visibility = Visibility.Hidden;
            OutPute.MaxWidth = Game.fieldWidth;

            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, game.Controler.speed);
        }
        private void StartLife_Click(object sender, RoutedEventArgs e)
        {
            game.Start();
            timer.Start();
            Pause.IsEnabled = true;
        }
        private void TimerTick(object sender, EventArgs e)
        {
            game.OneDrawing(frameSkip);
        }
        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            Pause.IsEnabled = false;
            Continue.IsEnabled = true;
        }
        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            timer.Start();
            Pause.IsEnabled = true;
            Continue.IsEnabled = false;
        }
        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {

        }
        private void OpenStatistic_Click(object sender, RoutedEventArgs e)
        {
            scroll.Visibility = Visibility.Visible;
            if (Height < Game.fieldHeight + 200)
                Height = Game.fieldHeight + 200;
            Binding binding = new Binding();
            binding.Mode = BindingMode.OneWay;
            binding.Source = game.Controler.frame;
            A.SetBinding(TextBlock.TextProperty, binding);
            //A.Inlines.Add("{Binding frame}");
            Binding b = BindingOperations.GetBinding(PeacCount, Label.ContentProperty);
            PeacCount.SetBinding(ContentProperty, b);
            A.SetBinding(TextBlock.TextProperty, b);


        }
    }
}
