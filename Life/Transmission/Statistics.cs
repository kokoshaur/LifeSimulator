using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life.Transmission
{
    class Statistics
    {
        static int Speed;
        static int Age;
        static int Heal;
        static int Rotation;

        public static int getMaxSpeed(int speed)
        {
            if (speed > Speed)
                Speed = speed;

            return Speed;
        }
        public static int getMaxAge(int age)
        {
            if (age > Age)
               Age = age;

            return Age;
        }
        public static int getMaxHeal(int heal)
        {
            if (heal > Heal)
                Heal = heal;

            return Heal;
        }
        public static int getMaxRotation(int rotation)
        {
            if (rotation > Rotation)
                Rotation = rotation;

            return Rotation;
        }
    }
}
