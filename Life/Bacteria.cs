using System;
using System.Collections.Generic;
using Life.Item;
using System.Windows.Controls;
using System.Windows.Media;

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
        public int age = 0;
        public int heal;
        public int maxHeal = 2000;

        protected int typeOfFood;
        protected int maxAge = 10000;
        protected int vision = 100;
        protected double speed = 0.1;
        protected double rotationSpeed = 0.1;
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
                if (bacterias[i].type == typeOfFood)
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
                if (Target == null)
                {
                    if (rand.NextDouble() < directionChangeRate)
                    {
                        tx = rand.Next(0, Game.fieldWidth) - x;
                        ty = rand.Next(0, Game.fieldHeight) - y;
                        targetAngle = Math.Atan2(-tx, -ty);
                    }
                }
                else
                {
                    tx = Target.x - x;
                    ty = Target.y - y;
                    targetAngle = Math.Atan2(-tx, -ty);
                }
                    x += sx;
                    y += sy;
                    sx *= Game.slip;
                    sy *= Game.slip;
                    if (x < 0)
                        sx += speed;
                    if (x > Game.fieldWidth)
                        sx -= speed;
                    if (y < 0)
                        sy += speed;
                    if (y > Game.fieldHeight)
                        sy -= speed;

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
        public void Mutation()
        {
            switch (rand.Next(0, 6))
            {
                case 0:
                    maxAge += Convert.ToInt32(maxAge / 3);
                    break;
                case 1:
                    maxHeal += Convert.ToInt32(maxHeal / 3);
                    break;
                case 2:
                    speed += speed / 3;
                    break;
                case 3:
                    rotationSpeed += rotationSpeed / 3;
                    break;
                case 4:
                    vision += Convert.ToInt32(vision / 3);
                    break;
                default:
                    break;
            }
            //switch (rand.Next(0, 6))
            //{
            //    case 0:
            //        maxAge -= 1000;
            //        break;
            //    case 1:
            //        maxHeal -= 500;
            //        break;
            //    case 2:
            //        speed -= 2;
            //        break;
            //    case 3:
            //        rotationSpeed -= 0.1f;
            //        break;
            //    case 4:
            //        vision -= 10;
            //        break;
            //    default:
            //        break;
            //}
        }
        public Food Die()
        {
            Food F = new Food(x,y);
            return F;
        }
        public static void CopySpecifications(Bacteria Mom, Bacteria child, string PathToTexture)
        {
            child.Texture = new Image
            {
                Source = (new ImageSourceConverter()).ConvertFromString(PathToTexture) as ImageSource,
                Width = Game.BacterialWidth,
                Height = Game.BacterialHeigth,
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
