using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;

namespace SimProvider.Graphics
{
    public partial class GLTest : Form
    {
        Texture t1;
        Texture t2;
        ShaderProgram sp;
        Model m;
        VertexBufferObject vbo;
        public GLTest()
        {
            InitializeComponent();
        }

        private void GLTest_Load(object sender, EventArgs e)
        {
            m = OBJLoader.loadModelfromOBJ("Data/test.obj");
            t1 = Texture.fromFile("Data/test1.png");
            t2 = Texture.fromFile("Data/test2.png");
            sp = ShaderProgram.create("Data/Shader/basic.v", "Data/Shader/basic.f");
            vbo = new VertexBufferObject(m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glc.SwapBuffers();
        }
    }
}
