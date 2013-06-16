using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimProvider.Save
{
    abstract public class CManager
    {
        System.DateTime _start;
        System.DateTime _end;
        public List<DataVector> _datlist = new List<DataVector>();

        abstract public void newBike();
        abstract public void Update();
        abstract public void Update(double timesetp);
    }

    public class DataVector
    {
        public double X = 0;
        public double Veclocity;
        public double DistanceTraveled;
        public double Acceleration;

        public DataVector(double time, double velocity, double distancetraveld, double acceleration)
        {
            X = time;
            Veclocity = velocity;
            DistanceTraveled = distancetraveld;
            Acceleration = acceleration;
        }
    }
}
