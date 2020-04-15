using System.Windows.Controls;
using System.Windows.Media;
using Life.Transmission;

namespace Life.Animals
{
    class PeacBacteria : Bacteria
    {
        public PeacBacteria(double xMax, double yMax) : base(xMax, yMax) 
        {
            Texture = new Image
            {
                Source = (new ImageSourceConverter()).ConvertFromString(Settings.peacTexture) as ImageSource,
                Width = Settings.bacterialWidth,
                Height = Settings.bacterialHeight,
            };
            type = (int)Game.bacteriaType.Peac;
            typeOfFood = (int)Game.bacteriaType.Food;
            transform = Texture.RenderTransform as RotateTransform;
        }
        public override Bacteria Reproduction()
        {
            PeacBacteria Bac = new PeacBacteria(x, y);
            CopySpecifications(this, Bac, Settings.peacTexture);
            Bac.Mutation();
            return Bac;
        }
        public EvilBacteria BeginToEvil()
        {
            EvilBacteria Bac = new EvilBacteria(x, y);
            CopySpecifications(this, Bac, Settings.evilTexture);
            Bac.Mutation();
            return Bac;
        }
    }
}
