using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public partial class GLTest : Form
    {
        Scene s;
        Stopwatch t = new Stopwatch();
        double ti = 0;
        int fps = 0;
        int c;
        public GLTest()
        {
            InitializeComponent();
        }

        private void GLTest_Load(object sender, EventArgs e)
        {
            s = new Scene(glc.Width, glc.Height);
        }

        private void resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glc.Width, glc.Height);
        }


        private void timer1_Tick(object sender, EventArgs e)
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
                System.Console.WriteLine(fps);
            }
            s.Update(et, OpenTK.Input.Mouse.GetState().WheelPrecise*2);
            s.Render();
            glc.SwapBuffers();
        }
    }
}
