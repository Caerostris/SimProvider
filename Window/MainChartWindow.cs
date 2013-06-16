using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace SimProvider.Window
{
    public partial class MainChartWindow : Form
    {
        public static Save.CManager _manager;
        public bool _simulate = false;
        public string _mode = "";
        public int ticks = 0;

        public int c = 0;

        public Boolean _active = false;

        VirtualSimulation vs;

        public MainChartWindow()
        {
            InitializeComponent();
        }
        public MainChartWindow(Save.CManager manager)
        {
            _manager = manager;
            InitializeComponent();
        }
        /*public MainChartWindow(Save.CManager manager)
        {
            _manager = manager;
            InitializeComponent();
        }*/

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (_mode)
            {
                case "CalculationManager":
                    //
                    //_manager[0].Update(timer.Interval / 10000);
                    _manager.Update(timer.Interval *10);
                    //
                    break;
                /*case "MultiCalculationManager":
                    //
                    for (int i = 0; i < _manager.Count; i++)
                    {
                        _manager[i].Update(timer.Interval / 10000);
                    }
                    //
                    break;*/
                case "SimulationManager":
                    ((Save.Manager.SimulationCManager)_manager).Update();
                    break;
                default:
                    break;
            }
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //if (_simulate == false)
            //{
            //    //kann sich nur um calculation handeln(CalculationManager)
            //    Save.Manager.CalculationCManager m = (Save.Manager.CalculationCManager)_manager[0];
            //    if (m._infCalculation==false)
            //    {
            //        //nur einmal
            //        m.Update();
            //    }
            //    else
            //    {
            //        timer.Interval = 10;
            //        timer.Enabled = true;
            //    }
            //}
            switch (_mode)
            {
                case"CalculationManager":
                    //
                    Save.Manager.CalculationCManager m = (Save.Manager.CalculationCManager)_manager;
                    if (_simulate == true)
                    {
                        timer.Interval = 10;
                        timer.Enabled = true;
                        //soll mit 1/10 time actualisiert werden
                    }
                    else
                    {
                        //nur einmal
                        m.Update();
                    }
                    //
                    break;
                case"MultiCalculationManager":
                    break;
                case"SimulationManager":
                    if (!_active)
                    {
                        VSUpdate();
                        timer.Interval = 10;
                        timer.Enabled = true;
                    }
                    break;
                default:
                    break;
            }

        }

        public void UpdateData(object sender, EventArgs e)
        {
            //MainChart.Series.Add("Velocity");
            //MainChart.Series.Add("DistanceTraveled");
            //MainChart.Series.Add("Acceleration");
            //MainChart.Series[0].Points.Clear();
            //MainChart.Series[1].Points.Clear();
            //MainChart.Series[2].Points.Clear();
            for (int i = c; i < _manager._datlist.Count; i++)
            {
                MainChart.Series[0].Points.AddXY(_manager._datlist[i].X, _manager._datlist[i].Veclocity);
                MainChart.Series[1].Points.AddXY(_manager._datlist[i].X, _manager._datlist[i].DistanceTraveled);
                MainChart.Series[2].Points.AddXY(_manager._datlist[i].X, _manager._datlist[i].Acceleration);
            }
            c = _manager._datlist.Count;
            //while (c > MainChart.Width / 2)
            //    simplify();
        }

        private void simplify()
        {
            Console.WriteLine("|");
            MainChart.Series[0].Points.Clear();
            MainChart.Series[1].Points.Clear();
            MainChart.Series[2].Points.Clear();
            c = 0;
            for (int i = _manager._datlist.Count - 1; i >= 0; i -= 2)
            {
                _manager._datlist.RemoveAt(i);
            }
        }

        private void VSUpdate()
        {
            vs = new VirtualSimulation();
            vs.Show();
            Save.Manager.SimulationCManager m = (Save.Manager.SimulationCManager)_manager;
            m._scene = vs._scene;

            /*while (!_active)
            {
                m.Update();
                vs.glc.SwapBuffers();
                System.Threading.Thread.Sleep(20);
            }*/
        }

        private void MainChartWindow_Load(object sender, EventArgs e)
        {
            this.FormClosing += close;
        }

        private void close(object o, CancelEventArgs e)
        {
            vs.Dispose();
            vs.Close();
        }

    }
}
