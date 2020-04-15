using System.Windows.Controls;
using System.Windows.Media;
using Life.Transmission;

namespace Life.Animals
{
    class Food : Bacteria
    {
        public Food(double xMax, double yMax) : base(xMax, yMax)
        {
            Texture = new Image
            {
                Source = (new ImageSourceConverter()).ConvertFromString(Settings.foodTexture) as ImageSource,
                Width = Settings.foodSize,
                Height = Settings.foodSize,
            };
            type = (int)Game.bacteriaType.Food;
            typeOfFood = -1;
            heal = 1000;
        }
        public override Bacteria Reproduction()
        {
            return null;
        }
    }
}
