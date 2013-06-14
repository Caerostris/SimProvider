using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;

namespace SimProvider.Window
{
    public partial class VirtualSimulation : Form
    {
        public OpenTK.GLControl glc;
        public Graphics.Scene _scene;

        Stopwatch t = new Stopwatch();
        double ti = 0;
        int fps = 0;
        int c;

        public VirtualSimulation()
        {
            InitializeComponent();
        }

        private void VirtualSimulation_ResizeEnd(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glc.Width, glc.Height);
            //GL.Viewport(0, 0, glc.Width, glc.Height);
        }

        private void VirtualSimulation_Load(object sender, EventArgs e)
        {
            _scene = new Graphics.Scene(glc.Width, glc.Height);
        }

        private void Updater_Tick(object sender, EventArgs e)
        {
            c++;
            ErrorCode er = GL.GetError();
            if (er != ErrorCode.NoError) { System.Console.WriteLine(er); }
            t.Stop();
            float et = (float)t.Elapsed.TotalSeconds;
            ti += et;
            t.Restart();
            while (ti > 1)
            {
                ti--;
                fps = c;
                c = 0;
                //System.Console.WriteLine(fps);
            }
            //_scene.Update(et, OpenTK.Input.Mouse.GetState().WheelPrecise * 2);
            _scene.Render();
            glc.SwapBuffers();

            fpsLable.Text = "Fps : " + fps;
        }
    }
}
