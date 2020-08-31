using System;
using Life.Animals;
using Life.Transmission;

namespace Life
{
    class God
    {
        Random rnd = new Random();
        ViewModelControl Control;

        public God(ViewModelControl control)
        {
            Control = control;
        }
        public Bacteria Start()
        {
            Control.nowPeacCounter++;
            Control.allTimePeacCounter++;
            return new PeacBacteria(Settings.fieldHeight / 2, Settings.fieldWidth / 2);
        }
        public Bacteria CreateBacteria(Bacteria Mom)
        {
            if (Mom.type == (int)Game.bacteriaType.Evil)
            {
                Control.nowEvilCounter++;
                Control.allTimeEvilCounter++;
                Bacteria Bac = Mom.Reproduction();
                RefreshStatistic(Bac);
                return Bac;
            }
            else if (rnd.Next(0, 1000) < Settings.changeOfBeginEvil)
            {
                Control.nowEvilCounter++;
                Control.allTimeEvilCounter++;
                PeacBacteria Bac = (PeacBacteria)Mom.Reproduction();
                RefreshStatistic(Bac);
                return Bac.BeginToEvil();
            }
            else
            {
                Control.nowPeacCounter++;
                Control.allTimePeacCounter++;
                Bacteria Bac = Mom.Reproduction();
                RefreshStatistic(Bac);
                return Bac;
            }
        }
        public Food CreateFood()
        {
            Control.nowFoodCounter++;
            return new Food(rnd.Next(Settings.bacterialWidth, Settings.fieldWidth - Settings.bacterialWidth), rnd.Next(Settings.bacterialWidth, Settings.fieldHeight - Settings.bacterialWidth));
        }
        public Food BacteriaDie(Bacteria Bac)
        {
            Control.nowFoodCounter++;
            return Bac.Die();
        }
        public void RefreshCountAfterEat(Bacteria Bac)
        {
            if (Bac.type == (int)Game.bacteriaType.Evil)
                Control.nowPeacCounter--;
            else if (Bac.type == (int)Game.bacteriaType.Peac)
                Control.nowFoodCounter--;
        }
        public void RefreshCountAfterDie(Bacteria Bac)
        {
            if (Bac.type == (int)Game.bacteriaType.Evil)
                Control.nowEvilCounter--;
            else if (Bac.type == (int)Game.bacteriaType.Peac)
                Control.nowPeacCounter--;
        }
        public void RefreshStatistic(Bacteria Bac)
        {
            Control.maxSpeed = Math.Round(Bac.speed, 2);
            Control.maxRotationSpeed = Math.Round(Bac.rotationSpeed, 2);
            Control.maxVision = Bac.vision;
            Control.maxMaxHeal = Bac.maxHeal;
            Control.maxMaxAge = Bac.maxAge;
        }
    }
}
