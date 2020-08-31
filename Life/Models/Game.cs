using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Life.Animals;
using Life.Transmission;

namespace Life
{
    class Game
    {
        public enum bacteriaType : int
        {
            Food,
            Peac,
            Evil
        };


        public ViewModelControl Controler { get; set; }
        MainWindow Field;
        God god;
        public List<Bacteria> bacterias = new List<Bacteria>();

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
            if (Controler.frame % Settings.foodSpawnRate == 0)
                NewFood();
            for (int i = 0; i < bacterias.Count; i++)
            {
                if (bacterias[i].type != (int)bacteriaType.Food)
                {
                    bacterias[i].NextMove(bacterias);
                    if (bacterias[i].Target != null)
                        if (IsSuffice(i) && (bacterias[i].age > 50))
                            EatFood(bacterias[i]);
                    if (i < bacterias.Count)
                        if ((bacterias[i].heal < 1) && (bacterias[i].type != (int)bacteriaType.Food))
                            NewFood(DieBacteria(bacterias[i]));
                }
            }
        }
        private bool IsSuffice(int i)
        {
            return ((Math.Abs((bacterias[i].x + Settings.bacterialWidth /2) - (bacterias[i].Target.x + bacterias[i].Target.Texture.Width / 2)) < bacterias[i].Target.Texture.Width / 2) && (Math.Abs((bacterias[i].y + Settings.bacterialHeight / 2) - (bacterias[i].Target.y + bacterias[i].Target.Texture.Height / 2))) < bacterias[i].Target.Texture.Height / 2);
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
                god.RefreshCountAfterEat(Bac);
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
            god.RefreshCountAfterDie(Bac);
            bacterias.Remove(Bac);
            Field.CanvasMap.Children.Remove(Bac.Texture);
            Bac.isAlive = false;
            CheckEndGame();
            return god.BacteriaDie(Bac);
        }

        private void CheckEndGame()
        {
            if ((Controler.nowEvilCounter == 0) && (Controler.nowPeacCounter == 0))
                Field.EndGame();
        }
    }
}
