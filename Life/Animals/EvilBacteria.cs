using System.Windows.Media;
using System.Windows.Controls;

namespace Life.Animals
{
    class EvilBacteria : Bacteria
    {
        public EvilBacteria(double xMax, double yMax) : base(xMax, yMax) 
        {
            Texture = new Image
            {
                Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/Resources/EvilBacterium.png") as ImageSource,
                Width = Game.BacterialWidth,
                Height = Game.BacterialHeigth,
            };
            type = Game.IsEvil;
            typeOfFood = Game.IsPeac;
            transform = Texture.RenderTransform as RotateTransform;
            Game.Controler.nowEvilCounter++;
            Game.Controler.allTimeEvilCounter++;
        }
        public override Bacteria Reproduction()
        {
            EvilBacteria Bac = new EvilBacteria(x, y);
            CopySpecifications(this, Bac, "pack://application:,,,/Resources/EvilBacterium.png");
            Bac.Mutation();
            return Bac;
        }
    }
}
