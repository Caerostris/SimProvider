﻿using System;
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
        private ContentManager<VertexBufferObject> models;
        private ContentManager<Texture> textures;
        private VertexBufferObject grassModel;
        private Texture grassTexture;

        //Scene objects
        private List<SceneObject> objects;
        private List<string[]> newObjects;
        private List<string[]> streetSegments;
        private Vector3[] grassData;
        private SceneObject street;
        private SceneObject landscape;
        private SceneObject skyDome;
        private SceneObject motorcycle;
        private SceneObject wheel;

        //Logics
        private double addc = 0.0;
        private float diameter = 0.75f;
        
        //Camera
        private Matrix4 view;
        private Matrix4 psvp;
        private Matrix4 projection;
        private Matrix4 depthProjection;
        private Matrix4 depthView;
        private Matrix4 bias = new Matrix4(0.5f, 0.0f, 0.0f, 0.0f,
                                            0.0f, 0.5f, 0.0f, 0.0f,
                                            0.0f, 0.0f, 0.5f, 0.0f,
                                            0.5f, 0.5f, 0.5f, 1.0f);
        //Light
        private Vector3 sunLightSrc = new Vector3(20f, 10f, -20f);
        private Vector3 sunLightDir = new Vector3(-0.75f, -0.75f, -0.5f);
        private Vector3 slices = new Vector3(-200, -100, -200);
        //Random
        private Random r = new Random((int)DateTime.Now.TimeOfDay.Ticks);
        //Shaders
        private BasicShader sp;
        private BasicShader depthShader;
        private BasicShader ps;
        private BasicShader bs;

        //Framebuffer
        private uint framebuffer = 0;
        private uint depthTextureArray = 0;
        private int depthTextureWidth = (int)Math.Pow(2, 11);

        private int width;
        private int height;

        private float dist = 0;
        private int pwm = 0;
        public int PWM {
            get { return pwm; }
        }

        private int depthMode = 0;
        private KeyboardState pks;
        private MouseState pms;

        private float v;

        public Scene(int wdh, int hgt)
        {
            width = wdh;
            height = hgt;

            initGL();

            load();
            initObjects();

            //init matrices
            view = Matrix4.LookAt(new Vector3(0, 1.75f, 1.75f), new Vector3(0, 1.5f, 0), Vector3.UnitY);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(75), wdh / hgt, 0.1f, 300.0f);

            depthProjection = Matrix4.CreateOrthographicOffCenter(-30, 30, -30, 30, -30, 100);
            depthView = Matrix4.LookAt(sunLightSrc, sunLightSrc + sunLightDir, Vector3.UnitY);

            psvp = Matrix4.CreateOrthographic(1, 1, -5, 5);

            //load shader
            sp = BasicShader.create("Data/Shader/basic.v", "Data/Shader/basic.f");
            sp.addUniform("modelTexture");
            sp.addUniform("shadowmap");
            sp.addUniform("lightdir");
            sp.addUniform("lightstr");
            sp.addUniform("ambient");
            sp.addUniform("modelViewProjection");
            sp.addUniform("model");
            //sp.addUniform("view");
            sp.addUniform("bias");
            //sp.addUniform("projection");
            sp.addUniform("shadowMat0");
            sp.addUniform("shadowMat1");
            sp.addUniform("shadowMat2");
            sp.addUniform("slices");

            sp.use();
            GL.UniformMatrix4(sp.Uniforms["bias"], false, ref bias);
            GL.UseProgram(0);

            depthShader = BasicShader.create("Data/Shader/depthShader.v", "Data/Shader/depthShader.f");
            depthShader.addUniform("model");
            depthShader.addUniform("view");
            depthShader.addUniform("projection");
            depthShader.addUniform("modelTexture");

            ps = BasicShader.create("Data/Shader/ps.v", "Data/Shader/ps.f");
            ps.addUniform("tex");
            ps.addUniform("texZ");

            bs = BasicShader.create("Data/Shader/basicShader.v", "Data/Shader/basicShader.f");
            bs.addUniform("mvp");
        }

        private void initGL()
        {
            GL.Viewport(0, 0, width, height);
            GL.ClearColor(0.0f, 0.8f, 0.9f, 1.0f);

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);

            GL.AlphaFunc(AlphaFunction.Gequal, 0.5f);
            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        private void initObjects()
        {
            skyDome = new SceneObject(Vector3.Zero, Vector3.Zero, new Vector3(100, 100, 100), "sky", "sky");
            motorcycle = new SceneObject(new Vector3(0, 0, 0.5f), new Vector3(0, MathHelper.Pi / 2, 0), new Vector3(0.25f, 0.25f, 0.25f), "motorcycle", "motorcycle");
            wheel = new SceneObject(new Vector3(0, 0.5f, 0.5f), new Vector3(0, MathHelper.Pi / 2, 0), new Vector3(0.25f, 0.25f, 0.25f), "wheel", "motorcycle");

            newObjects = new List<string[]>();
            newObjects.Add(new string[] { "tree", "leaf", "tree1", "branchstar" });


            streetSegments = new List<string[]>();
            streetSegments.Add(new string[] { "street", "street" });

            objects = new List<SceneObject>();
            for (int i = 0; i < 100; i++)
            {
                addNewObject(i * 5);
            }
            for (int i = 0; i < 6; i++)
            {
                objects.Add(new SceneObject(new Vector3(2.15f, 0, -50 * i), new Vector3(0, -MathHelper.Pi / 2, 0), new Vector3(0.15f, 0.15f, 0.15f), "barke", "barke"));
            }

            street = new SceneObject(new Vector3(-1.5f, 0, 0), Vector3.Zero, new Vector3(3, 1, 1f), "street", "street");
            landscape = new SceneObject(new Vector3(-1.5f, 0, 0), Vector3.Zero, new Vector3(3, 1, 1f), "landscape", "landscape");

            grassData = new Vector3[5000];
            for (int i = 0; i < grassData.Length; i++)
            {
                grassData[i] = new Vector3((float)r.NextDouble() * 50 + 2.5f, 0, (float)r.NextDouble() * -100);
                if (r.Next(2) == 1)
                    grassData[i].X = -grassData[i].X - 3.1f;
            }
            sunLightDir.Normalize();
        }

        private void load()
        {

            models = new ContentManager<VertexBufferObject>("Data/Models/", new LoadContentFunction<VertexBufferObject>(
                (string name, string path) =>
                    new VertexBufferObject(OBJLoader.loadModelfromOBJ(path + name + ".obj"))));
            textures = new ContentManager<Texture>("Data/Textures/", new LoadContentFunction<Texture>(
                (string name, string path) =>
                    Texture.fromFile(path + name + ".png")));

            Mesh[] meshes = OBJLoader.loadModelfromOBJ("Data/Models/Motorrad.obj");
            models.add("motorcycle", new VertexBufferObject(meshes[0]));
            models.add("wheel", new VertexBufferObject(meshes[1]));
            textures.add("motorcycle", Texture.fromFile("Data/Textures/Motorrad.png"));

            Mesh[] tmmeshes = OBJLoader.loadModelfromOBJ("Data/Models/tm.obj");
            models.add("tachometer", new VertexBufferObject(tmmeshes[1]));
            models.add("tachometer|", new VertexBufferObject(tmmeshes[0]));
            textures.add("tachometer", Texture.fromFile("Data/Textures/tm2.png"));
            textures.add("tachometer|", Texture.fromFile("Data/Textures/tm.png"));

            meshes = OBJLoader.loadModelfromOBJ("Data/Models/tree.obj");
            models.add("tree", new VertexBufferObject(meshes[1]));
            models.add("leaf", new VertexBufferObject(meshes[0]));
            //textures.add("leafs",Texture.fromFile("Data/Textures/leafs.png",5));

            grassModel = new VertexBufferObject(OBJLoader.loadModelfromOBJ("Data/Models/grass.obj")[0]);
            grassTexture = Texture.fromFile("Data/Textures/grass.png",5);

            //Framebuffer
            GL.GenFramebuffers(1, out framebuffer);
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);

            //Depth texture
            GL.GenTextures(1, out depthTextureArray);
            GL.BindTexture(TextureTarget.Texture2DArray, depthTextureArray);
            GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            //GL.TexParameter(TextureTarget.Texture2DArray, TextureParameterName.TextureCompareMode, (int)TextureCompareMode.CompareRToTexture);
            GL.TexImage3D(TextureTarget.Texture2DArray, 0, PixelInternalFormat.DepthComponent16, depthTextureWidth, depthTextureWidth, 3, 0, PixelFormat.DepthComponent, PixelType.Float, new IntPtr(0));
            GL.BindTexture(TextureTarget.Texture2DArray, 0);

            //--> framebuffer
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, depthTextureArray, 0);

            GL.DrawBuffer(DrawBufferMode.None);
            GL.ReadBuffer(ReadBufferMode.None);

            FramebufferErrorCode ec = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (ec != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Framebuffer konnte nicht erstellt werden!");

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

        }

        private void updateGrass(float movement)
        {
            int l = grassData.Length;
            for (int i = 0; i < l; i++)
            {
                if (grassData[i].Z >= 0)
                {
                    grassData[i] = new Vector3((float)r.NextDouble() * 50 + 2.5f, 0, (float)r.NextDouble() * -100);
                    if (r.Next(2) == 1)
                        grassData[i].X = -grassData[i].X - 3.1f;
                }
                else
                    grassData[i].Z += movement;
            }
        }

        public void Update(float elapsedTime, float velocity)
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                objects[i].Position += new Vector3(0, 0, velocity * elapsedTime);
                if (objects[i].Position.Z > 20)
                    objects.RemoveAt(i);
            }
            updateGrass(elapsedTime*velocity);
            street.Position += new Vector3(0, 0, velocity * elapsedTime);
            while (street.Position.Z > 20)
            { street.Position -= new Vector3(0, 0, 10); }

            landscape.Position += new Vector3(0, 0, velocity * elapsedTime);
            while (landscape.Position.Z > 20)
            { landscape.Position -= new Vector3(0, 0, 10); }

            dist += elapsedTime * velocity;
            while (dist > 50)
            {
                dist -= 50;
                objects.Add(new SceneObject(new Vector3(2.1f, 0, -300), new Vector3(0, -MathHelper.Pi / 2, 0), new Vector3(0.15f, 0.15f, 0.15f), "barke", "barke"));
            }

            addc += elapsedTime * velocity * r.NextDouble();
            while (addc > 0.5)
            {
                addc -= 0.5;
                addNewObject(300);
            }
            wheel.Rotation -= new Vector3(MathHelper.DegreesToRadians((elapsedTime * velocity) * 180 / (MathHelper.Pi * (float)Math.Pow(diameter, 2))), 0, 0);
            KeyboardState ks = Keyboard.GetState();
            if (pks.IsKeyDown(Key.Minus) && ks.IsKeyUp(Key.Minus))
            {
                depthMode = Math.Max(0, depthMode - 1);
            }
            else if (pks.IsKeyDown(Key.Plus) && ks.IsKeyUp(Key.Plus))
            {
                depthMode = Math.Min(2, depthMode + 1);
            }
            pks = ks;
            MouseState ms = Mouse.GetState();
            if (ks.IsKeyDown(Key.ControlLeft))
                pwm = Math.Min(Math.Max((pwm+(int)(ms.WheelPrecise - pms.WheelPrecise)*10), 0), 255);
            else
                pwm = Math.Min(Math.Max(pwm + (int)(ms.WheelPrecise - pms.WheelPrecise), 0), 255);
            pms = ms;
            v = velocity;
        }

        private void addNewObject(int o)
        {
            string[] s = newObjects[r.Next(newObjects.Count)];
            Vector3 pos = new Vector3((float)r.NextDouble() * 80 + 3.5f, 0, (float)r.NextDouble() * -50 - o);
            if (r.Next(2) == 1)
                pos.X = -pos.X - 3f;
            Vector3 rotation = new Vector3(0, (float)r.NextDouble() * MathHelper.TwoPi, 0);
            float f = (float)r.NextDouble() + 0.5f;
            Vector3 scale = new Vector3(f, f, f);
            int i = s.Length / 2;
            string[] models = new string[i]; Array.Copy(s, models, i);
            string[] textures = new string[i]; Array.Copy(s, i, textures, 0, i);
            objects.Add(new SceneObject(pos, rotation, scale, models, textures));

        }


        public void Render()
        {
            //Render ShadowMap----------------------------------------------------------------------------------------------------
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);
            GL.CullFace(CullFaceMode.Front);
            GL.Clear(ClearBufferMask.DepthBufferBit);
            GL.Viewport(0, 0, depthTextureWidth, depthTextureWidth);

            GL.Disable(EnableCap.CullFace);
            foreach (SceneObject so in objects)
                renderSceneObjectDepth(so, 0);
            GL.Enable(EnableCap.CullFace);
            renderSceneObjectDepth(motorcycle, 0);
            renderSceneObjectDepth(wheel, 0);

            //Render Objects------------------------------------------------------------------------------------------------------
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.CullFace(CullFaceMode.Back);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Viewport(0, 0, width, height);

            GL.Enable(EnableCap.AlphaTest);
            GL.Disable(EnableCap.CullFace);
            foreach (SceneObject so in objects)
                renderSceneObject(so);
            GL.Enable(EnableCap.CullFace);
            for (int i = 0; i < 20; i++)
            { renderSceneObject(street); street.Position -= new Vector3(0, 0, 10); }
            street.Position += new Vector3(0, 0, 200);

            for (int i = 0; i < 20; i++)
            { renderSceneObject(landscape); landscape.Position -= new Vector3(0, 0, 10); }
            landscape.Position += new Vector3(0, 0, 200);

            renderSceneObject(motorcycle);
            renderSceneObject(wheel);
            renderGrass();
            GL.Disable(EnableCap.AlphaTest);
            renderGui();


        }

        private void renderGrassDepth()
        {

            int l = grassData.Length;
            depthShader.use();
            GL.FramebufferTextureLayer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, depthTextureArray, 0, 0);
            //Matrix4 mvp = (projection * view * so.ObjectMatrix);
            //GL.UniformMatrix4(depthShader.Uniforms["modelViewProjection"], false, ref mvp);
           
            GL.ActiveTexture(TextureUnit.Texture0);
            grassTexture.bind();

            for (int i = 0; i < l; i++)
            {
                Matrix4 m = Matrix4.CreateTranslation(grassData[i]);
                GL.UniformMatrix4(depthShader.Uniforms["model"], false, ref m);
                GL.UniformMatrix4(depthShader.Uniforms["view"], false, ref depthView);
                GL.UniformMatrix4(depthShader.Uniforms["projection"], false, ref depthProjection);
                grassModel.draw();
            }

            GL.UseProgram(0);
        }

        private void renderGrass()
        {
            
            int l = grassData.Length;
            sp.use();
            GL.Disable(EnableCap.CullFace);
            GL.Uniform3(sp.Uniforms["lightdir"], sunLightDir);
            GL.Uniform1(sp.Uniforms["lightstr"], 1f);
            GL.Uniform3(sp.Uniforms["slices"], slices);
            GL.Uniform4(sp.Uniforms["ambient"], new Vector4(0.1f, 0.1f, 0.1f, 1f));

            GL.ActiveTexture(TextureUnit.Texture0);
            grassTexture.bind();
            GL.Uniform1(sp.Uniforms["modelTexture"], 0);

            GL.ActiveTexture(TextureUnit.Texture1);
            GL.BindTexture(TextureTarget.Texture2DArray, depthTextureArray);
            GL.Uniform1(sp.Uniforms["shadowmap"], 1);

            for (int i = 0; i < l; i++)
            {   
                Matrix4 m = Matrix4.CreateTranslation(grassData[i]);
                Matrix4 mvp = m * view * projection;
                GL.UniformMatrix4(sp.Uniforms["modelViewProjection"], false, ref mvp);
                GL.UniformMatrix4(sp.Uniforms["model"], false, ref m);
                Matrix4 shadowMat0 = (m * depthView * depthProjection) * bias;
                GL.UniformMatrix4(sp.Uniforms["shadowMat0"], false, ref shadowMat0);
                grassModel.draw();
            }

            GL.UseProgram(0);
            GL.Enable(EnableCap.CullFace);
        }
        private void renderGui()
        {
            bs.use();

            GL.ActiveTexture(TextureUnit.Texture0);
            textures.get("tachometer").bind();
            Matrix4 mvp = Matrix4.Identity *
                Matrix4.Scale(0.2f) *
                Matrix4.CreateTranslation(-0.8f, -0.8f, 0f) *
                Matrix4.LookAt(Vector3.UnitZ, -Vector3.UnitZ, Vector3.UnitY) *
                Matrix4.CreateOrthographicOffCenter(-1, 1, -1, 1, 0.01f, 10.0f);
            GL.UniformMatrix4(bs.Uniforms["mvp"], false, ref mvp); 
            models.get("tachometer").draw();

            GL.ActiveTexture(TextureUnit.Texture0);
            textures.get("tachometer|").bind();
            mvp = Matrix4.Identity *
               Matrix4.Scale(0.2f) *
               Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(((270/120)*-(v*3.6f))+135)) *
               Matrix4.CreateTranslation(-0.8f, -0.8f, 0f) *
               Matrix4.LookAt(Vector3.UnitZ, -Vector3.UnitZ, Vector3.UnitY) *
               Matrix4.CreateOrthographicOffCenter(-1, 1, -1, 1, 0.01f, 10.0f);
            GL.UniformMatrix4(bs.Uniforms["mvp"], false, ref mvp);
            models.get("tachometer|").draw();

            GL.UseProgram(0);
          
            //Debug---------------------------------------------------------------------------------------------------------------
#if DEBUG  
            ps.use();
            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2DArray, depthTextureArray);
            GL.Uniform1(ps.Uniforms["tex"], 0);
            GL.Uniform1(ps.Uniforms["texZ"], depthMode);
            models.get("plane").draw();
            GL.UseProgram(0);
#endif
            
        }

        private void renderSceneObjectDepth(SceneObject so, int slice)
        {
            for (int i = 0; i < so.Model.Length; i++)
            {
                depthShader.use();
                GL.FramebufferTextureLayer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, depthTextureArray, 0, slice);
                //Matrix4 mvp = (projection * view * so.ObjectMatrix);
                //GL.UniformMatrix4(depthShader.Uniforms["modelViewProjection"], false, ref mvp);
                Matrix4 m = so.ObjectMatrix;
                GL.UniformMatrix4(depthShader.Uniforms["model"], false, ref m);
                GL.UniformMatrix4(depthShader.Uniforms["view"], false, ref depthView);
                GL.UniformMatrix4(depthShader.Uniforms["projection"], false, ref depthProjection);
                GL.ActiveTexture(TextureUnit.Texture0);
                textures.get(so.Texture[i]).bind();

                models.get(so.Model[i]).draw();

                GL.UseProgram(0);
            }
        }
        private void renderSceneObject(SceneObject so)
        {
            for (int i = 0; i < so.Model.Length; i++)
            {
                sp.use();

                GL.Uniform3(sp.Uniforms["lightdir"], sunLightDir);
                GL.Uniform1(sp.Uniforms["lightstr"], 1f);
                GL.Uniform3(sp.Uniforms["slices"], slices);
                GL.Uniform4(sp.Uniforms["ambient"], new Vector4(0.1f, 0.1f, 0.1f, 1f));

                Matrix4 m = so.ObjectMatrix;
                Matrix4 mvp = so.ObjectMatrix * view * projection;
                GL.UniformMatrix4(sp.Uniforms["modelViewProjection"], false, ref mvp);
                GL.UniformMatrix4(sp.Uniforms["model"], false, ref m);
                //GL.UniformMatrix4(sp.Uniforms["view"], false, ref view);
                //GL.UniformMatrix4(sp.Uniforms["projection"], false, ref projection);

                Matrix4 shadowMat0 = (m * depthView * depthProjection) * bias;
                GL.UniformMatrix4(sp.Uniforms["shadowMat0"], false, ref shadowMat0);
                //GL.UniformMatrix4(sp.Uniforms["dprojection"], false, ref depthProjection);

                GL.ActiveTexture(TextureUnit.Texture0);
                textures.get(so.Texture[i]).bind();
                GL.Uniform1(sp.Uniforms["modelTexture"], 0);

                GL.ActiveTexture(TextureUnit.Texture1);
                GL.BindTexture(TextureTarget.Texture2DArray, depthTextureArray);
                GL.Uniform1(sp.Uniforms["shadowmap"], 1);

                models.get(so.Model[i]).draw();

                GL.UseProgram(0);
            }
        }
    }
}
