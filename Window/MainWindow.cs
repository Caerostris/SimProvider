using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimProvider.Window
{
    public partial class MainWindow : Form
    {

        private Window.Editor _editor;
        public Save.Save save;


        public MainWindow()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            if (!File.Exists(Save.SaveSetting.Default.path))
            {
                save = new Save.Save();
                return;
            }
            try
            {
                FileStream fs = new FileStream(Save.SaveSetting.Default.path,FileMode.Open);
                XmlSerializer s = new XmlSerializer(typeof(Save.Save));
                save = (Save.Save)s.Deserialize(fs);        
                fs.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void DragAndDropBox_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void DragAndDropBox_DragDrop(object sender, DragEventArgs e)
        {
            
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            string file = files[0];
            //foreach (string file in files)
            //{
            //}
        }

        private void buttonEditor_Click(object sender, EventArgs e)
        {
            //if (_editor == null)
            //{
                _editor = new Editor(this);
            //}
            _editor.Visible = true;
        }

        private void buttonCalculation_Click(object sender, EventArgs e)
        {
            SelectBike f = new SelectBike("calculation",this);
            f.Show();
        }

        private void buttonMultiCalculation_Click(object sender, EventArgs e)
        {
            SelectBike f = new SelectBike("multicalculation1",this);
            f.Show();
        }

        private void buttonSimulation_Click(object sender, EventArgs e)
        {
            SelectBike f = new SelectBike("simulation",this);
            f.Show();
        }
    }
}
