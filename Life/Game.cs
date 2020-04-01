using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Life.Item;

namespace Life
{
    class Game
    {
        public static int fieldWidth = 800;  //ширина экрана
        public static int fieldHeight = 800; //высота экрана
        public static int foodSpawnRate = 30; //период спавна еды (чем больше, тем реже)
        public static double slip = 0.8;    //вязкость среды(чем больше, тем сильнее заносит бактерий)
        public const int changeOfBeginEvil = 1000;
        public const int BacterialWidth = 20;  //Ширина бактерии
        public const int BacterialHeigth = 27; //Высота бактерии
        public const int FoodSize = 10;
        public const int IsFood = 0;
        public const int IsPeac = 1;
        public const int IsEvil = 2;


        public ViewModelControl Controler { get; set; }

        MainWindow Field;

        God god;

        public static List<Bacteria> bacterias = new List<Bacteria>();
        public Game(MainWindow F)
        {
            Field = F;
            Controler = new ViewModelControl();
            god = new God(Controler);
        }

        public void Start()
        {
            AddBacteria(god.Start());
            NewFood();
            Field.StartLife.IsEnabled = false;
        }
        public void OneDrawing(int frameSkip)
        {
            while (Controler.frame % (frameSkip+1) != 0)
            {
                NextFrame();
                Controler.frame++;
            }
            RefreshMap();
            NextFrame();
            Controler.frame++;
        }
        private void NextFrame()
        {
            if (Controler.frame % foodSpawnRate == 0)
                NewFood();
            for (int i = 0; i < bacterias.Count; i++)
            {
                if (bacterias[i].type != IsFood)
                {
                    bacterias[i].NextMove(bacterias);
                    if (bacterias[i].Target != null)
                        if (IsSuffice(i) && (bacterias[i].age > 50))
                            EatFood(bacterias[i]);
                    if (i < bacterias.Count)
                        if ((bacterias[i].heal < 1) && (bacterias[i].type != IsFood))
                            NewFood(DieBacteria(bacterias[i]));
                }
            }
        }
        private bool IsSuffice(int i)
        {
            return ((Math.Abs((bacterias[i].x + BacterialWidth/2) - (bacterias[i].Target.x + bacterias[i].Target.Texture.Width / 2)) < bacterias[i].Target.Texture.Width / 2) && (Math.Abs((bacterias[i].y + BacterialHeigth / 2) - (bacterias[i].Target.y + bacterias[i].Target.Texture.Height / 2))) < bacterias[i].Target.Texture.Height / 2);
        }
        private void RefreshMap()
        {
            for (int i = 0; i < bacterias.Count; i++)
            {
                Canvas.SetLeft(bacterias[i].Texture, bacterias[i].x);
                Canvas.SetTop(bacterias[i].Texture, bacterias[i].y);
            }
        }
        private void AddBacteria(Bacteria Bac)
        {
            bacterias.Add(Bac);
            Canvas.SetLeft(Bac.Texture, Bac.x);
            Canvas.SetTop(Bac.Texture, Bac.y);
            Field.CanvasMap.Children.Add(Bac.Texture);
        }
        private void NewBacteria(Bacteria Mom)
        {
            if (Mom != null)
                AddBacteria(god.CreateBacteria(Mom));
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
            if (Bac.Target.isAlive)
            {
                god.RefreshCount(Bac);
                Bac.Eat();
                bacterias.Remove(Bac.Target);
                Field.CanvasMap.Children.Remove(Bac.Target.Texture);
                Bac.Target.isAlive = false;
                Bac.Target = null;
                if (Bac.heal > Bac.maxHeal)
                {
                    Bac.heal = Convert.ToInt32(Bac.maxHeal / 2);
                    NewBacteria(Bac);
                }
            }
        }
        private Food DieBacteria(Bacteria Bac)
        {
            bacterias.Remove(Bac);
            Field.CanvasMap.Children.Remove(Bac.Texture);
            Bac.isAlive = false;
            return god.BacteriaDie(Bac);
        }
    }
}
