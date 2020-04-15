using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;
using Life.Transmission;

namespace Life
{
    public partial class MainWindow : Window
    {
        Game game;
        System.Windows.Threading.DispatcherTimer timer;

        public MainWindow()
        {
            FirstStatr();
            InitializeComponent();
            RefreshGame();
            RefreshInterface();
        }
        private void FirstStatr()
        {
            Left = 0;
            Top = 0;

            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(TimerTick);
        }
        private void RefreshGame()
        {
            int buf = 4;
            if (game != null)
                buf = 100 / game.Controler.gameSpeed;
            game = new Game(this);
            game.Controler.gameSpeed = buf;
            DataContext = game.Controler;
            CanvasMap.Children.Clear();
            CanvasMap.Height = Settings.fieldHeight;
            CanvasMap.Width = Settings.fieldWidth;
        }
        private void RefreshInterface()
        {
            Height = MinHeight = Settings.fieldHeight + 6 + 33;
            Width = MinWidth = Settings.fieldWidth + 245 + 20;

            FoodGraph.MaxHeight = PeacGraph.MaxHeight = EvilGraph.MaxHeight = Settings.fieldHeight - 250;

            VerticalWall.X1 = VerticalWall.X2 = Settings.fieldWidth;
            VerticalWall.Y2 = Settings.fieldHeight;
            HorizontlWall.Y1 = HorizontlWall.Y2 = Settings.fieldHeight;
            HorizontlWall.X2 = Settings.fieldWidth;
        }
        private void TimerTick(object sender, EventArgs e)
        {
            game.OneDrawing(Settings.frameSkip);
        }
        private void Refresh_Timer(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(game != null)
                timer.Interval = new TimeSpan(0, 0, 0, 0, game.Controler.gameSpeed);
            else timer.Interval = new TimeSpan(0, 0, 0, 0, 4);
        }
        private void StartLife_Click(object sender, RoutedEventArgs e)
        {
            game.Start();
            timer.Start();
            Pause.IsEnabled = true;
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
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            StartLife.IsEnabled = true;
            Pause.IsEnabled = false;
            Continue.IsEnabled = false;
            timer.Stop();
            RefreshGame();
            StartLife_Click(null,null);
        }
        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings.OpenSettings();
        }
        private void OpenStatistic_Click(object sender, RoutedEventArgs e)
        {
            if (Height < Settings.fieldHeight + 200)
                Height = Settings.fieldHeight + 200;

            Binding binding = new Binding();
            binding.StringFormat = Settings.Language.PeacStatistic + "{0}";
            binding.Path = new PropertyPath("allTimePeacCounter");
            Peac.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.StringFormat = Settings.Language.EvilStatistic + "{0}";
            binding.Path = new PropertyPath("allTimeEvilCounter");
            Evil.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.StringFormat = Settings.Language.MaxSpeedStatistic + "{0}";
            binding.Path = new PropertyPath("maxSpeed");
            MaxSpeed.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.StringFormat = Settings.Language.MaxRotationSpeedStatistic + "{0}";
            binding.Path = new PropertyPath("maxRotationSpeed");
            MaxRotationSpeed.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.StringFormat = Settings.Language.MaxMaxHealSpeedStatistic + "{0}";
            binding.Path = new PropertyPath("maxMaxHeal");
            MaxMaxHealSpeed.SetBinding(TextBlock.TextProperty, binding);

            binding = new Binding();
            binding.StringFormat = Settings.Language.MaxMaxAgeSpeedStatistic + "{0}";
            binding.Path = new PropertyPath("maxMaxAge");
            MaxMaxAgeSpeed.SetBinding(TextBlock.TextProperty, binding);

        }

        private void ImportSettings_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog fbd = new CommonOpenFileDialog();
            fbd.Title = "Выберете файл с настройками";
            fbd.ShowDialog();
            if (fbd.IsCollectionChangeAllowed())
                Settings.RefreshSettings(new FileStream(fbd.FileName, FileMode.Open));
        }

        private void ExportStatistic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetViewSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void X2_Click(object sender, RoutedEventArgs e)
        {
            timer.Tick += new EventHandler(TimerTick);
        }

        private void Xsplit2_Click(object sender, RoutedEventArgs e)
        {
            timer.Tick -= new EventHandler(TimerTick);
        }
        private void SaveExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void EndGame()
        {
            if (IsStatisticMod.IsChecked == true)
            {
                ExportStatistic.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                Restart.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else
            {
                Pause.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                MessageBox.Show("Конец");
                OpenStatistic.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
    }
}
