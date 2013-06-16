using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SimProvider.Window
{
    public partial class SelectBike : Form
    {
        List<Bike> _bList = new List<Bike>();
        List<string> _bnList = new List<string>();
        private string _mode = "";
        private Save.CManager _manager;

        MainWindow _mw;

        public SelectBike(string mode,Save.CManager manager,MainWindow mw)
        {
            _mw = mw;
            genBListBNList();
            _mode = mode;
            _manager = manager;

            InitializeComponent();
        }
        public SelectBike(string mode, MainWindow mw)
        {
            _mw = mw;
            genBListBNList();
            _mode = mode;

            InitializeComponent();
        }

        private void SelectBike_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < _bnList.Count; i++)
			{
                listBox1.Items.Insert(i, _bnList[i]);
			}
            
        }





        private void genBListBNList()
        {
            Save.Save save = new Save.Save();
            ////using (FileStream fs=new FileStream("d:\\temp.txt",FileMode.Create))
            ////{
            ////}
            //Serializer_Deserializer_Database.Biare.Binare s;
            //using (StreamWriter sw = new StreamWriter(Save.SaveSetting.Default.phat, false))
            //{
            //    sw.Write(Save.SaveSetting.Default.SaveObject);
            //}
            //if (new FileInfo(Save.SaveSetting.Default.phat).Length != 0)
            //{
            //    s = new Serializer_Deserializer_Database.Biare.Binare();
            //    save = (Save.Save)s.DeSerialize(Save.SaveSetting.Default.phat);
            //}
            save = _mw.save;

            if (save != null)
            {
                for (int i = 0; i < save._bike.Count; i++)
                {
                    _bnList.Add(save._bike[i].name);
                    _bList.Add(save._bike[i]);
                }
            }
            //lade
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _bList.Count; i++)
			{
                if (listBox1.GetSelected(i))
                {
                    //MessageBox.Show(_mode);
                    switch (_mode)
                    {
                        case "calculation":
                            Calculation(_bList[i]);
                            break;
                        case "multicalculation1":
                            break;
                        case "simulation":
                            Simulation(_bList[i]);
                            break;
                        default:
                            MessageBox.Show("FAIL");
                            break;
                    }
                }
			}
            
        }

        private void Calculation(SimProvider.Bike bike)
        {
            Save.Manager.CalculationCManager manager = new Save.Manager.CalculationCManager(bike, 0.0001);
            MainChartWindow m= new MainChartWindow(manager);
            if (checkBox1.Checked)
            {
                m._simulate = true;
            }
            m.Show();
            m._mode = "CalculationManager";
            this.Close();
        }
        private void Simulation(SimProvider.Bike bike)
        {
            //VirtualSimulation vs = new VirtualSimulation();
            //vs.Show();
            Save.Manager.SimulationCManager manager = new Save.Manager.SimulationCManager();
            manager._bike = bike;
            MainChartWindow m = new MainChartWindow(manager);
            m._mode = "SimulationManager";
            //Graphics.Scene s = new Graphics.Scene(800, 600);
            m.Show();
            this.Close();
        }
    }
}
