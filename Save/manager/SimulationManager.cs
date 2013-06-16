using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SimProvider.Save.Manager
{
    class SimulationCManager : CManager
    {
        Stopwatch _watch = new Stopwatch();
        public Graphics.Scene _scene;//=new Graphics.Scene(4,3);
        public SimProvider.Bike _bike = new Bike();
        double runtime = 0;

        public void newBike(Graphics.Scene scene,SimProvider.Bike bike)
        {
            _scene = scene;
            _bike = bike;
           // throw new NotImplementedException();
        }

        public override void Update()
        {
            double rt = 0;
            if (_watch.IsRunning)
            {
                _watch.Stop();
                rt = _watch.Elapsed.TotalSeconds;
                _watch.Restart();
                runtime += rt;
                _bike.update(rt, Convert.ToInt16(_scene.PWM));
                _datlist.Add(new DataVector(runtime, _bike.Veclocity, _bike.DistanceTraveled,_bike.Acceleration));
                _scene.Update((float)rt, (float)_bike.Veclocity);
            }
            else
            {
                _bike.update(0, Convert.ToInt16(_scene.PWM));
                _datlist.Add(new DataVector(0, _bike.Veclocity, _bike.DistanceTraveled, _bike.Acceleration));
                _scene.Update(0, 0);
                _watch.Start();
            }
            
            //throw new NotImplementedException();
        }

        public override void Update(double timesetp)
        {
            Update();
            //throw new NotImplementedException();
        }

        public override void newBike()
        {
            //throw new NotImplementedException();
        }
    }
}
