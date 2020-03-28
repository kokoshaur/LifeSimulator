using System;
using Life.Animals;
using Life.Item;
namespace Life
{
    class God
    {
        Random rnd = new Random();

        public Bacteria Start()
        {
            return new PeacBacteria(Game.fieldHeight / 2, Game.fieldWidth / 2);
        }
        public Bacteria CreateBacteria(Bacteria Mom)
        {
            if (Mom.type == Game.IsEvil)
            {
                return Mom.Reproduction();
            }
            else if (rnd.Next(0, 1000) < Game.changeOfBeginEvil)
            {
                PeacBacteria Bac = (PeacBacteria)Mom.Reproduction();
                return Bac.BeginToEvil();
            }
            else
            {
                return Mom.Reproduction();
            }
        }
        public Food CreateFood()
        {
            return new Food(rnd.Next(Game.BacterialWidth / 2, Game.fieldWidth - Game.BacterialWidth), rnd.Next(Game.BacterialWidth / 2, Game.fieldHeight - Game.BacterialWidth));
        }
        public Food BacteriaDie(Bacteria Bac)
        {
            return Bac.Die();
        }
    }
}
