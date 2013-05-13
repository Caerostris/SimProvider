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
        Texture t1;
        Texture t2;
        Texture t3;
        Texture t4;
        BasicShader sp;
        Mesh m1;
        Mesh m2;
        VertexBufferObject vbo1;
        VertexBufferObject vbo2;
        Vector4 ls = new Vector4(0,10,0,1);
        Vector4 cs = new Vector4(0, 2, 5, 1);
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
            t3 = Texture.create(glc.Width , glc.Height);
            t4 = Texture.create(glc.Width, glc.Height);
            initGL();
            m1 = OBJLoader.loadModelfromOBJ("Data/w.obj")[0];
            m2 = OBJLoader.loadModelfromOBJ("Data/k.obj")[0];
            t1 = Texture.fromFile("Data/test.png");
            t2 = Texture.fromFile("Data/HMap.png");
            
            sp = BasicShader.create("Data/Shader/basic.v", "Data/Shader/basic.f");
            sp.addUniform("texture");
            sp.addUniform("lightsrc");
            sp.addUniform("lightstr");
            sp.addAttribute("position");
            sp.addAttribute("normal");
            sp.addUniform("modelViewProjection");

            sp.addAttribute("texCoord");
            //GL.BindFragDataLocation(sp.ID, 0, "FragColor");
            vbo1 = new VertexBufferObject(m1);
            vbo2 = new VertexBufferObject(m2);
            glc.Resize += resize;
            this.FormClosing += GLTest_FormClosing;
            s = new Scene(glc.Width, glc.Height);
        }

        void GLTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            vbo1.delete();
            vbo2.delete();
            t1.delete();
            t2.delete();
        }

        private void resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glc.Width, glc.Height);
        }


        private void initGL()
        {
            GL.Viewport(0, 0, glc.Width, glc.Height);
            GL.ClearColor(0.0f, 0.8f, 0.9f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            c++;
            ErrorCode er = GL.GetError();
            if (er != ErrorCode.NoError)
            {
                System.Console.WriteLine(er);
            }
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
