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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimProvider
{
    public struct Engine
    {
        public double power; // motor's overall power (in Watt)
        public double efficiency; // motor's efficiency (something like 0.9)
        public double torque; // motor's torque (calculated statically)
    }

    public struct Tire
    {
        public double diameter; // obvious
        public double crr; // coefficient of rolling resistance, depending on tire and surface
    }

    public struct Bike
    {
        public Engine engine; // reference to the engine (really a reference, or a copy of the data stored in the engine?)
        public Tire tire; // same for the tire
        public double weight; //in kg, including driver
        public double surface; //surface of the bike (usually 0.8 square meters)
        public double trs; //transmission ratio of the scarf gear
        public double tra; //transmission ratio of the axle drive
    }

    public struct DataSet
    {
        public Bike bike; // obvious
        public double velocity; // in m/s
        public double acceleration; // in m/s²
        public double distanceTraveled; // in m
        public double stepsPerSecond; // not yet functional, supposed to be 1
    }

    public class SimProvider
    {
        /*
         * Two ways to do this, dunno what's best:
         * 1. Every time the function getNextStep() is called, a DataSet is passed as parameter
         * 2. Every time the function getNextStep() is called, it modifies a DataSet stored a class variable
         * I choose the second one for now because I just like it more, any thoughts on this?
         */

        public static DataSet getNextStep(DataSet dataset)
        {
            double forceMoving = (dataset.bike.engine.torque * dataset.bike.trs * dataset.bike.tra * 0.9) / (0.5 * dataset.bike.tire.diameter); // 0.9 is some factor that I can't remember, 0.5 is a factor in formula
            double forceStopping = (0.5 * 0.57 * 1.2041 * dataset.bike.surface * dataset.velocity) + (dataset.bike.tire.crr * dataset.bike.weight * 9.81); // 0.57 is the coefficient of flow resistance, 1.2401 is the density of air, 9.81 is obvious
            double actualForceMoving = forceMoving - forceStopping;
            double acceleration = actualForceMoving / dataset.bike.weight;

            DataSet result = new DataSet();
            result.bike = dataset.bike;
            result.acceleration = acceleration;
            result.velocity = dataset.velocity += acceleration;
            result.distanceTraveled += result.velocity;

            return result;
        }
    }
}
