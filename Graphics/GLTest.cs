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
        Model m;
        VertexBufferObject vbo;
        public GLTest()
        {
            InitializeComponent();
        }

        private void GLTest_Load(object sender, EventArgs e)
        {
            initGL();
            m = OBJLoader.loadModelfromOBJ("Data/t.obj");
            t1 = Texture.fromFile("Data/test1.png");
            t2 = Texture.fromFile("Data/test2.png");
            sp = BasicShader.create("Data/Shader/basic.v", "Data/Shader/basic.f");
            sp.addUniform("texture");
            sp.addAttribute("position");
            sp.addAttribute("normal");
        //    sp.addAttribute("texCoord");
            vbo = new VertexBufferObject(m);
            
        }


        private void initGL()
        {
            GL.Enable(EnableCap.DepthTest);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            render();
        }

        private void render(){
            ErrorCode e;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            sp.use();

            GL.ActiveTexture(TextureUnit.Texture0);
            t1.bind();
            GL.Uniform1(sp.Uniforms["texture"], 0);
            GL.BindBuffer(BufferTarget.ArrayBuffer,vbo.VertexBuffer);
            GL.VertexAttribPointer(
                sp.Attributes["position"],
                3,
                VertexAttribPointerType.Float, 
                false, 
                Vertex.SizeInBytes,
                new IntPtr(0));
            GL.EnableVertexAttribArray(sp.Attributes["position"]);
            GL.VertexAttribPointer(
                sp.Attributes["normal"],
                3,
                VertexAttribPointerType.Float,
                false,
                Vertex.SizeInBytes,
                new IntPtr(12));
            GL.EnableVertexAttribArray(sp.Attributes["normal"]);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vbo.ElementBuffer);
            GL.DrawElements(BeginMode.TriangleStrip, vbo.ElementCount, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(sp.Attributes["position"]);
            GL.DisableVertexAttribArray(sp.Attributes["normal"]);
            while((e = GL.GetError())!=ErrorCode.NoError){
                System.Console.WriteLine(e);
            }
            glc.SwapBuffers();
        }
    }
}
