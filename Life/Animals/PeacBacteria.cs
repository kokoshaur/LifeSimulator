using System.Windows.Controls;
using System.Windows.Media;

namespace Life.Animals
{
    class PeacBacteria : Bacteria
    {
        public PeacBacteria(double xMax, double yMax) : base(xMax, yMax) 
        {
            Texture = new Image
            {
                Source = (new ImageSourceConverter()).ConvertFromString("pack://application:,,,/Resources/PeacBacterium.png") as ImageSource,
                Width = Game.BacterialWidth,
                Height = Game.BacterialHeigth,
            };
            type = Game.IsPeac;
            typeOfFood = Game.IsFood;
            transform = Texture.RenderTransform as RotateTransform;
        }
        public override Bacteria Reproduction()
        {
            PeacBacteria Bac = new PeacBacteria(x, y);
            CopySpecifications(this, Bac, "pack://application:,,,/Resources/PeacBacterium.png");
            Bac.Mutation();
            return Bac;
        }
        public EvilBacteria BeginToEvil()
        {
            EvilBacteria Bac = new EvilBacteria(x, y);
            CopySpecifications(this, Bac, "pack://application:,,,/Resources/EvilBacterium.png");
            Bac.Mutation();
            return Bac;
        }
    }
}
