using System.IO;

namespace Life.Transmission
{
    static class Settings
    {
        static SettingsWindow settingsWindow;

        public const string foodTexture = "pack://application:,,,/Resources/Food.png";
        public const string peacTexture = "pack://application:,,,/Resources/PeacBacterium.png";
        public const string evilTexture = "pack://application:,,,/Resources/EvilBacterium.png";

        public const string pathToStatistings = @"C://Users//grinchenkosr//Desktop//Stat.txt";
        public const string pathToBestStatistings = @"C://Users//grinchenkosr//Desktop//BestStat.txt";

        public static int fieldWidth = 800;             //Ширина экрана
        public static int fieldHeight = 800;            //Высота экрана
        public static int bacterialWidth = 20;          //Ширина бактерии
        public static int bacterialHeight = 27;         //Высота бактерии
        public static int foodSize = 10;                //Ширина еды
        public static int frameSkip = 1;                //Пропуск фреймов (при больших значениях повышается производительность)

        public static int foodSpawnRate = 30;           //Период спавна еды (чем больше, тем реже)
        public static int changeOfBeginEvil = 10;       //Шанс рождения плотоядной бактерии
        public static double slip = 0.8;                //Вязкость среды(чем больше, тем сильнее заносит бактерий)
        public static double bacteriaDefaultSpeed = 0.1;
        public static double bacteriaDefaultRotationSpeed = 0.1;
        public static int bacteriaDefaultVision = 100;
        public static int bacteriaDefaultMaxHeal = 2000;
        public static int bacteriaDefaultMaxAge = 10000;
        public static void OpenSettings()
        {
            settingsWindow = new SettingsWindow();
            settingsWindow.Show();
        }
        public static void RefreshSettings(FileStream fileSettings)
        {
            //fieldWidth = FieldHeight;
            //fieldHeight = FieldHeight;
            //bacterialWidth = BacterialWidth;
            //bacterialHeight = BacterialHeight;
        }
    }
}
