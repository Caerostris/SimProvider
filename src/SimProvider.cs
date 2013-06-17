//License: GPLv3
/*
 * What should be functional?
 * Simulation of acceleration and velocity
 * 
 * TODO:
 * Simulate battery drain
*/

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

using System;

namespace SimProvider
{
    [Serializable]
    public class Battery
    {
        public string name = "";
        private double voltage;
        public double Voltage
        {
            get
            {
                return this.voltage;
            }
            set
            {
                this.voltage = value;
            }
        }

        private double maxAmpere;
        public double MaxAmpere
        {
            get
            {
                return this.maxAmpere;
            }
            set
            {
                this.maxAmpere = value;
            }
        }

        public double Watt
        {
            get
            {
                return this.Voltage * this.MaxAmpere;
            }
        }

        private double initialRunTime;
        private double runTime;
        public double RunTime
        {
            get
            {
                return this.runTime;
            }
            set
            {
                this.runTime = value;
            }
        }

        private double ampereHours;
        public double AmpereHours
        {
            get
            {
                return this.ampereHours;
            }
            set
            {
                this.ampereHours = value;
            }
        }

        public double BatteryLoad
        {
            get
            {
                return this.runTime * (this.initialRunTime / 100);
            }
        }

        public Battery()
        {
            this.voltage = 48;
            this.maxAmpere = 104.166;
            this.ampereHours = 42;
            this.runTime = (this.AmpereHours / this.maxAmpere) * 60 * 60;
            this.initialRunTime = runTime;
        }

        public Battery(double voltage, double maxAmpere, double ampereHours)
        {
            this.voltage = voltage;
            this.maxAmpere = maxAmpere;
            this.ampereHours = ampereHours;
        }
    }
    [Serializable]
    public class Engine
    {
        public string name = "";
        private double efficieny;
        public double Efficiency
        {
            get
            {
                return this.efficieny;
            }
            set
            {
                this.efficieny = value;
            }
        }

        private double torque;
        public double Torque
        {
            get
            {
                return this.torque;
            }
            set
            {
                this.torque = value;
            }
        }

        public Engine()
        {
            this.efficieny = 0.9;
            this.torque = 19.2;
        }

        public Engine(double efficiency, double torque)
        {
            this.efficieny = efficiency;
            this.torque = torque;
        }
    }
    [Serializable]
    public class Tire
    {
        public string name = "";
        private double diameter;
        public double Diameter
        {
            get
            {
                return this.diameter;
            }
            set
            {
                this.diameter = value;
            }
        }

        public Tire()
        {
            this.diameter = 0.5;
        }

        public Tire(double diameter)
        {
            this.diameter = diameter;
        }
    }
    [Serializable]
    //shouldn't this be part of the game?
    public abstract class Surfaces
    {
        public string name = "";
        public const double crrAsphalt = 0.015;
    }
    [Serializable]
    public class Bike
    {
        public string name = "";
        private Battery battery;
        public Battery Battery
        {
            get
            {
                return this.battery;
            }
            set
            {
                this.battery = value;
            }
        }

        private Engine engine;
        public Engine Engine
        {
            get
            {
                return this.engine;
            }
            set
            {
                this.engine = value;
            }
        }

        private Tire tire;
        public Tire Tire
        {
            get
            {
                return this.tire;
            }
            set
            {
                this.tire = value;
            }
        }

        private double surface;
        public double Surface
        {
            get
            {
                return this.surface;
            }
            set
            {
                this.surface = value;
            }
        }

        private double weight;
        public double Weight
        {
            get
            {
                return this.weight;
            }
            set
            {
                this.weight = value;
            }
        }

        private double bikeSurface;
        public double BikeSurface
        {
            get
            {
                return this.bikeSurface;
            }
            set
            {
                this.bikeSurface = value;
            }
        }

        private double tra; //transmission ratio of the axle drive
        public double Tra
        {
            get
            {
                return this.tra;
            }
            set
            {
                this.tra = value;
            }
        }

        private double trs; //transmission ratio of the scarf gear
        public double Trs
        {
            get
            {
                return this.trs;
            }
            set
            {
                this.trs = value;
            }
        }

        double distanceTraveled = 0;
        public double DistanceTraveled
        {
            get
            {
                return this.distanceTraveled;
            }
            set
            {
                this.distanceTraveled = value;
            }
        }

        private double acceleration = 0;
        public double Acceleration
        {
            get
            {
                return this.acceleration;
            }
            set
            {
                this.acceleration = value;
            }
        }

        private double velocity = 0;
        public double Veclocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
                this.velocity = value;
            }
        }

        public Bike()
        {
            this.tire = new Tire();
            this.engine = new Engine();
            this.battery = new Battery();
            this.surface = Surfaces.crrAsphalt;
            this.weight = 198;
            this.bikeSurface = 0.8;
            this.trs = 3.15;
            this.tra = 1;
        }

        public Bike(Tire tire, Engine engine, Battery battery, double weight, double bikeSurface, double trs, double tra)
        {
            this.tire = tire;
            this.engine = engine;
            this.battery = battery;
            this.weight = weight;
            this.bikeSurface = bikeSurface;
            this.trs = trs;
            this.tra = tra;

            this.surface = Surfaces.crrAsphalt;
        }

        public Bike(Tire tire, Engine engine, Battery battery, double surface, double weight, double bikeSurface, double trs, double tra)
        {
            this.tire = tire;
            this.engine = engine;
            this.battery = battery;
            this.surface = surface;
            this.weight = weight;
            this.bikeSurface = bikeSurface;
            this.trs = trs;
            this.tra = tra;
        }

        public void update(double time, int PWM)
        {
            double PWMPercent = Convert.ToDouble(PWM) / 255;

            double forceMoving = (this.engine.Torque * this.trs * this.tra * 0.9) / (0.5 * this.tire.Diameter);
            forceMoving *= PWMPercent;

            double forceStopping = (0.5 * 0.57 * 1.2041 * this.bikeSurface * this.velocity * this.velocity) + (this.surface * this.weight * 9.81); // 0.57 is the coefficient of flow resistance, 1.2401 is the density of air, 9.81 is obvious
            double actualForceMoving = forceMoving - forceStopping;
            double acceleration = actualForceMoving / this.weight;

            if (this.velocity <= 0 && acceleration < 0)
                acceleration = 0;

            this.battery.RunTime -= time * PWMPercent;

            this.acceleration = acceleration;
            this.velocity += (this.acceleration * time);
            this.distanceTraveled += (this.velocity * time);
        }
        public void update(double time)
        {
            this.update(time, 255);
        }
    }
}
