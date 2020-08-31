using System.IO;

namespace Life.Transmission
{
    static class Languages
    {
        public struct Main
        {
            public static string Title = "Рабочая поверхность";
            public static string SettingsFile = "Выберете файл с настройками";

            public static string StartLife = "Начать";
            public static string Continue = "Продолжить";
            public static string Pause = "Пауза";
            public static string Restart = "Перезапустить";

            public static string Other = "Доп";
            public static string ImportSettings = "Импортировать настройки";
            public static string ExportStatistic = "Экспортировать статистику";
            public static string ResetViewSettings = "Сбросить настройки графики";

            public static string SaveExit = "Сохранить";

            public static string X2 = "x2";
            public static string Xsplit2 = "/2";
            public static string LabelSpeedControl = "Скорость игры";
            public static string IsStatisticMod = "Режим сбора статистики";

            public static string End = "Конец";

            public struct Tooltips
            {
                public static string OpenSettingsTooltip = "Настройки";
                public static string OpenStatisticTooltip = "Статистика";
                public static string X2 = "Удвоить текущую скорость";
                public static string Xsplit2 = "Располовинить текущую скорость";
            }
        }

        public struct Statistic
        {
            public static string ErrorFile = "Ошибка открытия файла";
            public static string PeacStatistic = "Травоядных за всё время: ";
            public static string EvilStatistic = "Плотоядных за всё время: ";
            public static string MaxSpeedStatistic = "Максимальная скорость: ";
            public static string MaxRotationSpeedStatistic = "Максимальная скорость разворота: ";
            public static string MaxMaxHealSpeedStatistic = "Наибольший максимальный запас жизни: ";
            public static string MaxMaxAgeSpeedStatistic = "Наибольший максимальный возраст: ";
        }

        public static void RefreshLanguages(FileStream fileSettings)
        {
            //fieldWidth = FieldHeight;
            //fieldHeight = FieldHeight;
            //bacterialWidth = BacterialWidth;
            //bacterialHeight = BacterialHeight;
        }
    }
}
