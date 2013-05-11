using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public class Scene
    {
        //Models and textures
        private Dictionary<string, VertexBufferObject> models;
        private Dictionary<string, Texture> textures;

        private VertexBufferObject plane;

        //Scene objects
        private List<SceneObject> objects;
        private List<string[]> newObjects;
        private List<SceneObject> street;
        private SceneObject skyDome;

        //Logics
        private double addc = 0.0;

        //Camera
        private Matrix4 view;
        private Matrix4 projection;
        private Matrix4 depthProjection;
        private Matrix4 depthView;
        private Matrix4 bias = new Matrix4(0.5f, 0.0f, 0.0f, 0.0f,
                                            0.0f, 0.5f, 0.0f, 0.0f,
                                            0.0f, 0.0f, 0.5f, 0.0f,
                                            0.5f, 0.5f, 0.5f, 1.0f);
        //Light
        private Vector3 pointLightSrc = new Vector3(20f, 10f, -20f);
        private Vector3 pointLightDir = new Vector3(-0.75f, -0.75f, -0.5f);
        //Random
        private Random r = new Random((int)DateTime.Now.TimeOfDay.Ticks);
        //Shaders
        private BasicShader sp;
        private BasicShader depthShader;
        private BasicShader ps;
        //Framebuffer
        private uint framebuffer = 0;
        private uint depthTexture = 0;
        private int depthTextureWidth = 4096;

        private int width;
        private int height;

        public Scene(int wdh, int hgt)
        {
            width = wdh;
            height = hgt;

            load();
            initObjects();

            //init matrices
            view = Matrix4.LookAt(new Vector3(0, 2, 2), new Vector3(0, 1.5f, 0), Vector3.UnitY);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(75), wdh / hgt, 0.1f, 300.0f);
           
            depthProjection = Matrix4.CreateOrthographicOffCenter(-30, 30, -30, 30, -10, 50);
            depthView = Matrix4.LookAt(pointLightSrc, pointLightSrc + pointLightDir, Vector3.UnitY);

            //load shader
            sp = BasicShader.create("Data/Shader/basic.v", "Data/Shader/basic.f");
            sp.addUniform("texture");
            sp.addUniform("shadowmap");
            sp.addUniform("lightsrc");
            sp.addUniform("lightstr");
            sp.addUniform("ambient");
            sp.addUniform("model");
            sp.addUniform("view");
            sp.addUniform("bias");
            sp.addUniform("projection");
            sp.addUniform("dview");
            sp.addUniform("dprojection");
          
            sp.use();
            GL.UniformMatrix4(sp.Uniforms["bias"], false, ref bias);
            GL.UseProgram(0);
            depthShader = BasicShader.create("Data/Shader/depthShader.v", "Data/Shader/depthShader.f");
            depthShader.addUniform("model");
            depthShader.addUniform("view");
            depthShader.addUniform("projection");                
            ps = BasicShader.create("Data/Shader/ps.v", "Data/Shader/ps.f");
            ps.addUniform("tex");  
        }

        private void initObjects()
        {
            skyDome = new SceneObject(Vector3.Zero, Vector3.Zero, new Vector3(100, 100, 100), "sky", "sky");

            newObjects = new List<string[]>();
            newObjects.Add(new string[]{"tree1","tree1"});
            newObjects.Add(new string[] { "test", "tree1" });

            objects = new List<SceneObject>();
            for (int i = 0; i < 100; i++)
            {
                addNewObject(i * 5);
            }

            street = new List<SceneObject>();
            for (int i = 0; i < 100; i++)
            {
                street.Add(new SceneObject(new Vector3(0, 0, -i * 2), Vector3.Zero, new Vector3(1, 1, 1f), "street", "street-asphalt"));
            }
        }

        private void load()
        {

            models = new Dictionary<string, VertexBufferObject>();
            textures = new Dictionary<string, Texture>();

            models.Add("street", new VertexBufferObject(OBJLoader.loadModelfromOBJ("Data/Models/street.obj")[0]));
            textures.Add("street-asphalt", Texture.fromFile("Data/Textures/street-asphalt.png"));
            textures.Add("street-sand", Texture.fromFile("Data/Textures/street-sand.png"));
            textures.Add("street-mud", Texture.fromFile("Data/Textures/street-mud.png"));

            models.Add("tree1", new VertexBufferObject(OBJLoader.loadModelfromOBJ("Data/Models/tree1.obj")[0]));
            textures.Add("tree1", Texture.fromFile("Data/Textures/tree1.png"));

            models.Add("test", new VertexBufferObject(OBJLoader.loadModelfromOBJ("Data/Models/test.obj")[0]));

            models.Add("sky", new VertexBufferObject(OBJLoader.loadModelfromOBJ("Data/Models/sky.obj")[0]));
            textures.Add("sky", Texture.fromFile("Data/Textures/sky.png"));

            models.Add("plane", new VertexBufferObject(OBJLoader.loadModelfromOBJ("Data/Models/plane.obj")[0]));

            //Framebuffer
            GL.GenFramebuffers(1, out framebuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);

            //Depth texture
            GL.GenTextures(1, out depthTexture);
            GL.BindTexture(TextureTarget.Texture2D, depthTexture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent16, depthTextureWidth, depthTextureWidth, 0, PixelFormat.DepthComponent, PixelType.Float, new IntPtr(0));
            GL.BindTexture(TextureTarget.Texture2D, 0);

            //--> framebuffer
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, depthTexture, 0);

            GL.DrawBuffer(DrawBufferMode.None);

            FramebufferErrorCode ec = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (ec != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Framebuffer konnte nicht erstellt werden!");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        }

        public void Update(float elapsedTime, float velocity)
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                objects[i].Position += new Vector3(0, 0, velocity * elapsedTime);
                if (objects[i].Position.Z > 10)
                    objects.RemoveAt(i);
            }
            street[0].Position += new Vector3(0, 0, velocity * elapsedTime);
            for (int i = street.Count - 1; i >= 0; i--)
            {
                street[i].Position = street[0].Position - new Vector3(0, 0, 2.0f * i);
                if (street[i].Position.Z > 5)
                {
                    street.RemoveAt(i);
                    street.Add(new SceneObject(new Vector3(0, 0, -2 * street.Count), Vector3.Zero, new Vector3(1, 1, 1f), "street", "street-asphalt"));
                }
            }
            addc += elapsedTime * velocity * r.NextDouble();
            if (addc > 1)
            {
                addc -= 1;
                addNewObject(300);
            }
        }

        private void addNewObject(int o)
        {
            string[] s = newObjects[r.Next(newObjects.Count)];
            Vector3 pos = new Vector3((float)r.NextDouble() * 40 + 3 , 0, (float)r.NextDouble() * -50 - o);
            if (r.Next(2) == 1)
                pos.X = -pos.X;
            Vector3 rotation = new Vector3(0, (float)r.NextDouble() * MathHelper.TwoPi, 0);
            float f = (float)r.NextDouble() + 0.5f;
            Vector3 scale = new Vector3(f, f, f);
            objects.Add(new SceneObject(pos, rotation, scale, s[0], s[1]));
        }

        public void Render()
        {
            
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);
            GL.CullFace(CullFaceMode.Front);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Viewport(0, 0, depthTextureWidth, depthTextureWidth);
            //Render ShadowMap
            foreach (SceneObject so in objects)
                renderSceneObjectDepth(so);
            foreach (SceneObject so in street)
               renderSceneObjectDepth(so);
            //renderSceneObject(skyDome);
            
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.CullFace(CullFaceMode.Back);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Viewport(0, 0, width, height);
            //Render Objects
            foreach (SceneObject so in objects)
                renderSceneObject(so);
            foreach (SceneObject so in street)
                renderSceneObject(so);
           // renderSceneObject(skyDome);

#if DEBUG
            ps.use();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, depthTexture);
            GL.Uniform1(ps.Uniforms["tex"], 0);
            models["plane"].draw();
            GL.UseProgram(0);
#endif

        }
        private void renderSceneObjectDepth(SceneObject so)
        {
            depthShader.use();

            //Matrix4 mvp = (projection * view * so.ObjectMatrix);
            //GL.UniformMatrix4(depthShader.Uniforms["modelViewProjection"], false, ref mvp);
            Matrix4 m = so.ObjectMatrix;
            GL.UniformMatrix4(depthShader.Uniforms["model"], false, ref m);
            GL.UniformMatrix4(depthShader.Uniforms["view"], false, ref depthView);
            GL.UniformMatrix4(depthShader.Uniforms["projection"], false, ref depthProjection);
            models[so.Model].draw();

            GL.UseProgram(0);
        }
        private void renderSceneObject(SceneObject so)
        {
            sp.use();

            GL.Uniform3(sp.Uniforms["lightsrc"], pointLightSrc);
            GL.Uniform1(sp.Uniforms["lightstr"], 1f);
            //GL.Uniform4(sp.Uniforms["ambient"], new Vector4(0f,0f,00f,1f));

            Matrix4 m = so.ObjectMatrix;
            GL.UniformMatrix4(sp.Uniforms["model"], false, ref m);
            GL.UniformMatrix4(sp.Uniforms["view"], false, ref view);
            GL.UniformMatrix4(sp.Uniforms["projection"], false, ref projection);
            GL.UniformMatrix4(sp.Uniforms["dview"], false, ref depthView);
            GL.UniformMatrix4(sp.Uniforms["dprojection"], false, ref depthProjection);

            GL.ActiveTexture(TextureUnit.Texture0);
            textures[so.Texture].bind();
            GL.Uniform1(sp.Uniforms["texture"], 0);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2D, depthTexture);
            GL.Uniform1(sp.Uniforms["shadowmap"], 1);

            models[so.Model].draw();

            GL.UseProgram(0);
        }
    }
}
