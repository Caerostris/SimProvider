using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace SimProvider.Window
{
    public partial class Editor : Form
    {
        Save.Save save;
        MainWindow _mw;

        public Editor(MainWindow mw)
        {
            _mw = mw;//-----------------------------------------------------------------------------------------------------------------------------
            save = mw.save;

            InitializeComponent();
        }

        private void buttonBikeLoeschen_Click(object sender, EventArgs e)
        {
            for (int i = save._bike.Count - 1; i >= 0; i--)
            {
                if (checkedListBox1.CheckedIndices.Contains(i))
                {
                    save._bike.RemoveAt(i);
                }
            }
            reloadLists();
        }

        private void buttonBikeAdd_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1 || listBox2.SelectedIndex == -1 || listBox3.SelectedIndex == -1)
                return;

            SimProvider.Bike b = new Bike();
            b.Battery=save._battery[listBox1.SelectedIndex];
            b.Engine=save._engine[listBox2.SelectedIndex];
            b.Tire=save._tire[listBox3.SelectedIndex];
            b.Weight = Convert.ToDouble(numericUpDown6.Value);
            if (textBox4.Text == "")
            {
                b.name = "Bike (" + b.Battery.name + "+" + b.Engine.name + "+" + b.Tire.name + ")";
            }
            else
            {
                b.name = textBox4.Text;
            }
            save._bike.Add(b);
            reloadLists();
        }

        private void buttonBLoeschen_Click(object sender, EventArgs e)
        {
            for (int i = save._battery.Count - 1; i >= 0; i--)
            {
                if (checkedListBox2.CheckedIndices.Contains(i))//i == checkedListBox2.SelectedIndex
                {
                    save._battery.RemoveAt(i);
                }
            }
            reloadLists();
        }

        private void buttonBAdd_Click(object sender, EventArgs e)
        {
            SimProvider.Battery b=new Battery();
            b.name = textBox1.Text;
            b.Voltage = Convert.ToDouble(numericUpDown1.Value);
            b.MaxAmpere=Convert.ToDouble(numericUpDown2.Value);
            //b.Watt = Convert.ToDouble(numericUpDown3.Value);
            save._battery.Add(b);
            reloadLists();
        }

        private void buttonELoeschen_Click(object sender, EventArgs e)
        {
            for (int i = save._engine.Count - 1; i >= 0; i--)
            {
                if (checkedListBox3.CheckedIndices.Contains(i))
                {
                    save._engine.RemoveAt(i);
                }
            }
            reloadLists();
        }

        private void buttonEAdd_Click(object sender, EventArgs e)
        {
            SimProvider.Engine en = new Engine(Convert.ToDouble(numericUpDown5.Value), Convert.ToDouble(numericUpDown4.Value));
            en.name = textBox2.Text;
            save._engine.Add(en);
            reloadLists();
        }

        private void buttonTAdd_Click(object sender, EventArgs e)
        {
            SimProvider.Tire t = new Tire(Convert.ToDouble(numericUpDown7.Value));
            t.name = textBox3.Text;
            save._tire.Add(t);
            reloadLists();
        }

        private void buttonTLoeschen_Click(object sender, EventArgs e)
        {
            for (int i = save._tire.Count - 1; i >= 0 ; i--)
            {
                if (checkedListBox4.CheckedIndices.Contains(i))
                {
                    save._tire.RemoveAt(i);
                }
            }
            reloadLists();
        }

        private void reloadLists()
        {
            checkedListBox1.Items.Clear();
            listBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            checkedListBox3.Items.Clear();
            listBox2.Items.Clear();
            checkedListBox4.Items.Clear();
            listBox3.Items.Clear();

            for (int i = 0; i < save._bike.Count; i++)
                checkedListBox1.Items.Add(save._bike[i].name);

            for (int i = 0; i < save._battery.Count; i++)
            {
                checkedListBox2.Items.Add(save._battery[i].name);
                listBox1.Items.Add(save._battery[i].name);
            }

            for (int i = 0; i < save._engine.Count; i++)
            {
                checkedListBox3.Items.Add(save._engine[i].name);
                listBox2.Items.Add(save._engine[i].name);
            }
            for (int i = 0; i < save._tire.Count; i++)
            {
                checkedListBox4.Items.Add(save._tire[i].name);
                listBox3.Items.Add(save._tire[i].name);
            }
            saveSave();
        }

        private void Editor_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveSave();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveSave();
        }


        public void saveSave()
        {
            XmlSerializer s = new XmlSerializer(typeof (Save.Save));
            if (!Directory.Exists(Save.SaveSetting.Default.pathdir))
                Directory.CreateDirectory(Save.SaveSetting.Default.pathdir);
            FileStream fs = new FileStream(Save.SaveSetting.Default.path, FileMode.Create);
            s.Serialize(fs, save);
            fs.Close();

            _mw.save = save;
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            reloadLists();
        }
    }
}
