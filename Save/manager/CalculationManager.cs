using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimProvider.Save.Manager
{
    class CalculationCManager : CManager
    {
        private double _timestepSEC = 0.1;
        private int _amountOfCounts;
        public bool _infCalculation = false;
        private SimProvider.Bike _bike;

        int run;

        public CalculationCManager(double timestepSize,double calculationTime)
        {
            _timestepSEC = timestepSize;
            _amountOfCounts = (int)timestepSize * (int)calculationTime;
            
        }
        public CalculationCManager(double timestepSize)
        {
            _timestepSEC = timestepSize;
            _amountOfCounts = 0;
            _infCalculation = true;
        }
        public CalculationCManager(SimProvider.Bike bike, double timestepSize, double calculationTime)
        {
            _bike = bike;
            _timestepSEC = timestepSize;
            _amountOfCounts = (int)timestepSize * (int)calculationTime;

        }
        public CalculationCManager(SimProvider.Bike bike, double timestepSize)
        {
            _bike = bike;
            _timestepSEC = timestepSize;
            _amountOfCounts = 0;
            _infCalculation = true;
        }


        public override void newBike()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            double tss = _timestepSEC;
            if (_infCalculation == false)
            {
                for (int i = 0; i < _amountOfCounts; i++)
                {
                    _bike.update(_timestepSEC);
                    _datlist.Add(new DataVector(i* (int)(tss*1000),_bike.Veclocity,_bike.DistanceTraveled,_bike.Acceleration));
                    //get _bike status
                }
            }
            else
            {
                _bike.update(_timestepSEC);
                _datlist.Add(new DataVector(run * (int)(tss * 1000), _bike.Veclocity, _bike.DistanceTraveled, _bike.Acceleration));
                run++;
                //get _bike status
            }

        }

        public override void Update(double timesetp)
        {
            double tss = timesetp;
            if (_infCalculation == false)
            {
                for (int i = 0; i < _amountOfCounts; i++)
                {
                    _bike.update(_timestepSEC);
                    _datlist.Add(new DataVector(i * (int)(tss * 1000), _bike.Veclocity, _bike.DistanceTraveled, _bike.Acceleration));
                    //get _bike status
                }
            }
            else
            {
                _bike.update(_timestepSEC);
                _datlist.Add(new DataVector(run * (int)(tss * 1000), _bike.Veclocity, _bike.DistanceTraveled, _bike.Acceleration));
                run++;
                //get _bike status
            }
        }
    }
}
