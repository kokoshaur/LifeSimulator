using System.Windows.Controls;
using System.Windows.Media;

namespace Life.Item
{
    class Food : Bacteria
    {
        public Food(double xMax, double yMax) : base(xMax, yMax)
        {
            Texture = new Image
            {
                Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/Resources/Food.png") as ImageSource,
                Width = Game.FoodSize,
            };
            type = Game.IsFood;
            typeOfFood = -1;
            heal = 2000;
            Game.Controler.nowFoodCounter++;
        }
        public override Bacteria Reproduction()
        {
            return null;
        }
    }
}
