using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Life.Item;

namespace Life
{
    class Game
    {
        public static int fieldWidth = 800;  //ширина экрана
        public static int fieldHeight = 800; //высота экрана
        public static int foodSpawnRate = 30; //период спавна еды (чем больше, тем реже)
        public static double slip = 0.8;    //вязкость среды(чем больше, тем сильнее заносит бактерий)
        public static int changeOfBeginEvil = 1000;
        public static int BacterialWidth = 20;
        public static int BacterialHeigth = 27;
        public static int FoodSize = 10;

        static int frame = 1;

        public static int IsFood = 0;
        public static int IsPeac = 1;
        public static int IsEvil = 2;
        public static long allTimePeacCounter = 0;
        public static long allTimeEvilCounter = 0;
        public static int nowPeacCounter = 0;
        public static int nowEvilCounter = 0;

        MainWindow Field;

        God god = new God();

        public static List<Bacteria> bacterias = new List<Bacteria>();
        public Game(MainWindow F)
        {
            Field = F;
        }

        public void Start()
        {
            NewBacteria(god.Start(Convert.ToInt32(fieldHeight / 2), Convert.ToInt32(fieldWidth / 2)));
            NewFood();
            Field.StatrLife.IsEnabled = false;
        }
        public void OneDrawing(int frameSkip)
        {
            while (frame % (frameSkip+1) != 0)
            {
                NextFrame();
                frame++;
            }
            RefreshMap();
            NextFrame();
            frame++;
        }
        private void NextFrame()
        {
            if (frame % foodSpawnRate == 0)
                NewFood();
            for (int i = 0; i < bacterias.Count; i++)
            {
                if (bacterias[i].type != IsFood)
                {
                    bacterias[i].NextMove(bacterias);
                    if (bacterias[i].Target != null)
                        if (((Math.Abs(bacterias[i].x - bacterias[i].Target.x) < 4) && (Math.Abs(bacterias[i].y - bacterias[i].Target.y)) < 4) && (bacterias[i].age > 50))
                            EatFood(bacterias[i]);
                    if (i < bacterias.Count)
                        if ((bacterias[i].heal < 1) && (bacterias[i].type != IsFood))
                            NewFood(DieBacteria(bacterias[i]));
                }
            }
        }
        private void RefreshMap()
        {
            for (int i = 0; i < bacterias.Count; i++)
            {
                Canvas.SetLeft(bacterias[i].Texture, bacterias[i].x);
                Canvas.SetTop(bacterias[i].Texture, bacterias[i].y);
            }
        }
        private void NewBacteria(Bacteria Mom)
        {
            Bacteria Bac = god.CreateBacteria(Mom);
            bacterias.Add(Bac);
            Canvas.SetLeft(Bac.Texture, Bac.x);
            Canvas.SetTop(Bac.Texture, Bac.y);
            Field.CanvasMap.Children.Add(Bac.Texture);
        }
        private void NewFood(Bacteria F = null)
        {
            if (F == null)
                F = god.CreateFood();
            bacterias.Add(F);
            Canvas.SetLeft(F.Texture, F.x);
            Canvas.SetTop(F.Texture, F.y);
            Field.CanvasMap.Children.Add(F.Texture);
        }
        private void EatFood(Bacteria Bac)
        {
            Bac.heal += Convert.ToInt32(Bac.Target.heal / 4);
            bacterias.Remove(Bac.Target);
            Field.CanvasMap.Children.Remove(Bac.Target.Texture);
            Bac.Target = null;
            if (Bac.heal > Bac.maxHeal)
            {
                Bac.heal = Bac.maxHeal;
                Bac.heal = Convert.ToInt32(Bac.heal / 2);
                NewBacteria(Bac);
            }
        }
        private Food DieBacteria(Bacteria Bac)
        {
            bacterias.Remove(Bac);
            Field.CanvasMap.Children.Remove(Bac.Texture);
            return god.BacteriaDie(Bac);
        }
    }
}
