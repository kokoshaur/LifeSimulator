using System;
using Life.Animals;
using Life.Item;
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
            return new PeacBacteria(Game.fieldHeight / 2, Game.fieldWidth / 2);
        }
        public Bacteria CreateBacteria(Bacteria Mom)
        {
            if (Mom.type == Game.IsEvil)
            {
                Control.nowEvilCounter++;
                Control.allTimeEvilCounter++;
                return Mom.Reproduction();
            }
            else if (rnd.Next(0, 1000) < Game.changeOfBeginEvil)
            {
                Control.nowEvilCounter++;
                Control.allTimeEvilCounter++;
                PeacBacteria Bac = (PeacBacteria)Mom.Reproduction();
                return Bac.BeginToEvil();
            }
            else
            {
                Control.nowPeacCounter++;
                Control.allTimePeacCounter++;
                return Mom.Reproduction();
            }
        }
        public Food CreateFood()
        {
            Control.nowFoodCounter++;
            return new Food(rnd.Next(Game.BacterialWidth / 2, Game.fieldWidth - Game.BacterialWidth), rnd.Next(Game.BacterialWidth / 2, Game.fieldHeight - Game.BacterialWidth));
        }
        public Food BacteriaDie(Bacteria Bac)
        {
            Control.nowFoodCounter++;
            return Bac.Die();
        }
        public void RefreshCount(Bacteria Bac)
        {
            if (Bac.Target.type == Game.IsPeac)
                Control.nowPeacCounter--;
            else if (Bac.Target.type == Game.IsFood)
                Control.nowFoodCounter--;
        }
    }
}
