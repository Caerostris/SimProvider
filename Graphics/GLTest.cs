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
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public partial class GLTest : Form
    {
        Texture t1;
        Texture t2;
        BasicShader sp;
        Mesh m1;
        Mesh m2;
        VertexBufferObject vbo1;
        VertexBufferObject vbo2;
        Camera c;
        Vector4 ls = new Vector4(0,10,0,1);
        public GLTest()
        {
            InitializeComponent();
        }

        private void GLTest_Load(object sender, EventArgs e)
        {
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
            c = new Camera(MathHelper.DegreesToRadians(75), glc.Width / glc.Height, 0.001f, 1000f);
            glc.Resize += resize;
            this.FormClosing += GLTest_FormClosing;
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
            GL.ClearColor(0.0f, 0.2f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            render();
        }


        private void render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            ErrorCode e;
            
            rm(vbo1, 0, 0, 0,t1);
            rm(vbo2, 1, 0, -5,t2);
            rm(vbo2, 1, 0, 5,t2);
            while ((e = GL.GetError()) != ErrorCode.NoError)
            {
                System.Console.WriteLine(e);
            }
            glc.SwapBuffers();
        }
        private void rm(VertexBufferObject vbo, float x, float y ,float z,Texture t){
            sp.use();
            ls = Vector4.Transform(ls,Matrix4.CreateRotationX(0.002f)*Matrix4.CreateRotationZ(0.001f));
            GL.Uniform4(sp.Uniforms["lightsrc"], ls);
            GL.Uniform1(sp.Uniforms["lightstr"], 0.5f);
            Matrix4 m = (Matrix4.Scale(0.8f) * Matrix4.CreateRotationY(0.2f) * Matrix4.CreateTranslation(x, y, z)) * Matrix4.LookAt(0.0f, 1.0f, 5.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f) * Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(75), glc.Width / glc.Height, 0.01f, 1000.0f);
            GL.UniformMatrix4(sp.Uniforms["modelViewProjection"], false, ref m);

            GL.ActiveTexture(TextureUnit.Texture0);
            t.bind();
            GL.Uniform1(sp.Uniforms["texture"], 0);
            vbo.draw();
            GL.UseProgram(0);
        }
    }
}
