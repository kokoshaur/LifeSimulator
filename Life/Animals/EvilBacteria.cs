using System.Windows.Media;
using System.Windows.Controls;
using Life.Transmission;

namespace Life.Animals
{
    class EvilBacteria : Bacteria
    {
        public EvilBacteria(double xMax, double yMax) : base(xMax, yMax) 
        {
            Texture = new Image
            {
                Source = (new ImageSourceConverter()).ConvertFromString(Settings.evilTexture) as ImageSource,
                Width = Settings.bacterialWidth,
                Height = Settings.bacterialHeight,
            };
            type = (int)Game.bacteriaType.Evil;
            typeOfFood = (int)Game.bacteriaType.Peac;
            transform = Texture.RenderTransform as RotateTransform;
        }
        public override Bacteria Reproduction()
        {
            EvilBacteria Bac = new EvilBacteria(x, y);
            CopySpecifications(this, Bac, Settings.evilTexture);
            Bac.Mutation();
            return Bac;
        }
    }
}
