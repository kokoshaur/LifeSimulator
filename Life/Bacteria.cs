using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Life.Animals;
using Life.Transmission;
namespace Life
{
    abstract class Bacteria
    {
        protected static Random rand = new Random();
        public Bacteria Target;
        public Image Texture;
        protected RotateTransform transform;

        public double x;
        public double y;
        public int type;
        public int age;
        public int heal;
        public bool isAlive = true;

        public double speed { get; protected set; } = Settings.bacteriaDefaultSpeed;
        public double rotationSpeed { get; protected set; } = Settings.bacteriaDefaultRotationSpeed;
        public int vision { get; protected set; } = Settings.bacteriaDefaultVision;
        public int maxHeal { get; protected set; } = Settings.bacteriaDefaultMaxHeal;
        public int maxAge { get; protected set; } = Settings.bacteriaDefaultMaxAge;

        protected int typeOfFood;
        protected double angle = 0;
        protected double targetAngle = 0;
        protected double sx = 0;
        protected double sy = 0;
        protected double tx = 0;
        protected double ty = 0;
        protected double directionChangeRate = 0.02;
        protected double rotateAngle = 0;
        protected RotateTransform rotate;

        protected Bacteria(double X, double Y)
        {
            x = X;
            y = Y;
            heal = Convert.ToInt32(maxHeal/2);
        }
        abstract public Bacteria Reproduction();
        public void NextMove(List<Bacteria> bacterias)
        {
            BestTarget(bacterias);
            MoveToTarget();
            Rotation();
            age++;
            heal--;
        }
        protected void BestTarget(List<Bacteria> bacterias)
        {
            double bestTarget = 9999999999;
            double bufTarget;
            for (int i = 0; i < bacterias.Count; i++)
            {
                if ((bacterias[i].type == typeOfFood) && bacterias[i].isAlive)
                {
                    bufTarget = Math.Pow(bacterias[i].x - x, 2) + Math.Pow(bacterias[i].y - y, 2);
                    if ((bufTarget < bestTarget) && (bufTarget < vision*vision))
                    {
                        Target = bacterias[i];
                        bestTarget = bufTarget;
                    }
                }
            }
        }
        protected void MoveToTarget()
        {
            if( age > 50)
            {
                if ((Target == null) || (!Target.isAlive))
                {
                    if (rand.NextDouble() < directionChangeRate)
                    {
                        tx = rand.Next(0, Settings.fieldWidth - Settings.foodSize) - x;
                        ty = rand.Next(0, Settings.fieldHeight - Settings.foodSize) - y;
                        targetAngle = Math.Atan2(-tx, -ty);
                    }
                }
                else
                {
                    tx = Target.x - x - (Settings.bacterialWidth / 2) + (Target.Texture.Width / 2);
                    ty = Target.y - y - (Settings.bacterialHeight / 2) + (Target.Texture.Width / 2);
                    targetAngle = Math.Atan2(-tx, -ty);
                }
                    x += sx;
                    y += sy;
                    sx *= Settings.slip;
                    sy *= Settings.slip;
                if (x < 0)
                    sx += speed * 2;
                if (x > (Settings.fieldWidth - Settings.bacterialWidth))
                    sx -= speed * 2;
                if (y < 0)
                    sy += speed * 2;
                if (y > (Settings.fieldHeight - Settings.bacterialHeight))
                    sy -= speed * 2;

                if (targetAngle < 0)
                    targetAngle += Math.PI * 2;

                if ((Math.Abs(angle - targetAngle) < rotationSpeed) || (Math.Abs(angle - targetAngle) > Math.PI * 2 - rotationSpeed))
                    angle = targetAngle;
                else if (((angle < targetAngle) && (angle + Math.PI > targetAngle)) || ((angle > targetAngle) && (angle - Math.PI > targetAngle)))
                    angle += rotationSpeed;
                else
                    angle -= rotationSpeed;

                if (angle < 0)
                    angle += Math.PI * 2;
                else if (angle > Math.PI * 2)
                    angle -= Math.PI * 2;

                if ((tx * tx + ty * ty) > 1)
                {
                    sx -= Math.Sin(angle) * speed;
                    sy -= Math.Cos(angle) * speed;
                }
            }
            
        }
        protected void Rotation()
        {
            if (angle < 0)
                rotate = new RotateTransform((rotateAngle - angle) * 180 / Math.PI, 0, Texture.Height / 2);
            else
                rotate = new RotateTransform((rotateAngle - angle) * 180 / Math.PI, Texture.Width / 2, Texture.Height / 2);
            Texture.RenderTransform = rotate;
        }
        public void Eat()
        {
            heal += Convert.ToInt32(Target.heal / 2);
            Target.isAlive = false;
        }
        public void Mutation()
        {
            switch (rand.Next(0, 6))
            {
                case 0:
                    speed += Settings.bacteriaDefaultSpeed / 3;
                    break;
                case 1:
                    rotationSpeed += Settings.bacteriaDefaultRotationSpeed / 5;
                    break;
                case 2:
                    vision += Settings.bacteriaDefaultVision / 5;
                    break;
                case 3:
                    maxHeal += Settings.bacteriaDefaultMaxHeal / 5;
                    break;
                case 4:
                    maxAge += Settings.bacteriaDefaultMaxAge / 7;
                    break;
                default:
                    break;
            }
            switch (rand.Next(0, 6))
            {
                case 0:
                    if (speed - Settings.bacteriaDefaultSpeed / 3 > 0)
                        speed -= Settings.bacteriaDefaultSpeed / 3;
                    break;
                case 1:
                    if (rotationSpeed - Settings.bacteriaDefaultRotationSpeed / 5 > 0)
                        rotationSpeed -= Settings.bacteriaDefaultRotationSpeed / 5;
                    break;
                case 2:
                    if (vision - Settings.bacteriaDefaultVision / 5 > 0)
                        vision -= Settings.bacteriaDefaultVision / 5;
                    break;
                case 3:
                    if (maxHeal - Settings.bacteriaDefaultMaxHeal / 5 > 0)
                        maxHeal -= Settings.bacteriaDefaultMaxHeal / 5;
                    break;
                case 4:
                    if (maxAge - Settings.bacteriaDefaultMaxAge / 7 > 0)
                        maxAge -= Settings.bacteriaDefaultMaxAge / 7;
                    break;
                default:
                    break;
            }
            Texture.ToolTip = "speed = " + speed + "\n" + "rotation speed = " + rotationSpeed + "\n" + "vision = " + vision + "\n" + "max heal = " + maxHeal + "\n" + "max age = " + maxAge;
        }
        public Food Die()
        {
            isAlive = false;
            Food F = new Food(x,y);
            return F;
        }
        public static void CopySpecifications(Bacteria Mom, Bacteria child, string PathToTexture)
        {
            child.Texture = new Image
            {
                Source = (new ImageSourceConverter()).ConvertFromString(PathToTexture) as ImageSource,
                Width = Settings.bacterialWidth,
                Height = Settings.bacterialHeight,
            };
            child.x = Mom.x;
            child.y = Mom.y;
            child.maxAge = Mom.maxAge;
            child.maxHeal = Mom.maxHeal;
            child.speed = Mom.speed;
            child.rotationSpeed = Mom.rotationSpeed;
            child.vision = Mom.vision;
        }
    }
}
