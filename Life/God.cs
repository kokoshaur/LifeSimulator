using System;
using Life.Animals;
using Life.Item;
namespace Life
{
    class God
    {
        Random rnd = new Random();

        int randomize = 0;
        public Bacteria Start(int x, int y)
        {
            Game.nowPeacCounter++;
            Game.allTimePeacCounter++;
            return new PeacBacteria(x, y);
        }
        public Bacteria CreateBacteria(Bacteria Mom)
        {
            if (Mom.type == Game.IsEvil)
            {
                Game.nowEvilCounter++;
                Game.allTimeEvilCounter++;
                return Mom.Reproduction();
            }
            else if (rnd.Next(0, Game.changeOfBeginEvil + randomize) > Game.changeOfBeginEvil)
            {
                randomize = 0;
                Game.nowEvilCounter++;
                Game.allTimeEvilCounter++;
                PeacBacteria Bac = (PeacBacteria)Mom.Reproduction();
                return Bac.BeginToEvil();
            }
            else
            {
                Game.allTimePeacCounter++;
                Game.nowPeacCounter++;
                randomize++;
                return Mom.Reproduction();
            }
        }
        public Food CreateFood()
        {
            return new Food(rnd.Next(1, Game.fieldWidth), rnd.Next(1, Game.fieldHeight));
        }
        public Food BacteriaDie(Bacteria Bac)
        {
            if (Bac.type == Game.IsEvil)
                Game.nowEvilCounter--;
            else
                Game.nowPeacCounter--;
            return Bac.Die();
        }
    }
}
