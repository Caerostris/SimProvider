//License: GPLv3


/*
 * This library simulates the speed and acceleration of a motorcycle
 * The formula used is
 * 
 *
 *          (M * Is * Ia * n)
 *  a =     -----------------  -  (0.5 * Cw * p * A * v² + Cr * Fg)
 *                 D/2
 *
 * Where a is the acceleration, M is the motor's torque, Is is the transmission ratio of .... (my hair is green and I'm bored of documenting things that nobody will ever read and nobody's listening anyway)
 */

namespace SimProvider
{
    public class Battery
    {
        public double Voltage
        {
            get
            {
                return 48;
            }
        }
        public double MaxAmpere
        {
            get
            {
                return 104.166;
            }
        }
        public double Watt
        {
            get
            {
                return this.Voltage * this.MaxAmpere;
            }
        }
    }

    public class Engine
    {
        public double efficieny = 0.9;
        public double Torque 
        {
            get
            {
                return 19.2;
            }
        }
    }

    public class Tire
    {
        public double diameter = 0.5;
    }

    public abstract class Surface
    {
        public const double crrAsphalt = 0.015;
    }

    public class Bike
    {
        Battery battery;
        Engine engine;
        Tire tire;
        double surface = Surface.crrAsphalt;
        double velocity = 0;
        double acceleration = 0;
        double weight = 198;
        double bikeSurface = 0.8;
        double tra = 1; //transmission ratio of the axle drive
        double trs = 3.15; //transmission ratio of the scarf gear
        double distanceTraveled = 0;

        public void update(double time)
        {
            double forceMoving = (this.engine.Torque * this.trs * this.tra * 0.9) / (0.5 * this.tire.diameter); // 0.9 is some factor that I can't remember, 0.5 is a factor in formula
            double forceStopping = (0.5 * 0.57 * 1.2041 * this.bikeSurface * this.velocity * this.velocity) + (this.surface * this.weight * 9.81); // 0.57 is the coefficient of flow resistance, 1.2401 is the density of air, 9.81 is obvious
            double actualForceMoving = forceMoving - forceStopping;
            double acceleration = actualForceMoving / this.weight;

            this.acceleration = acceleration;
            this.velocity = this.velocity += acceleration;
            this.distanceTraveled += this.velocity;
        }
    }
}
