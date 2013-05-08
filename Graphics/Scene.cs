using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public class Scene
    {
        private Dictionary<string, VertexBufferObject> models;
        private Dictionary<string, Texture> textures;
        private List<SceneObject> objects;
        private List<SceneObject> newObjects;
        private List<SceneObject> street;
        private double addc = 0.0;
        private SceneObject skyDome;
        private Matrix4 view;
        private Matrix4 projection;
        private Random r = new Random((int)DateTime.Now.TimeOfDay.Ticks);
        private BasicShader sp;
        public Scene(int wdh, int hgt)
        {
            load();
            initObjects();
            view = Matrix4.LookAt(new Vector3(0, 2, 2), new Vector3(0, 1.5f, 0), Vector3.UnitY);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(75), wdh / hgt, 0.1f, 200.0f);

            sp = BasicShader.create("Data/Shader/basic.v", "Data/Shader/basic.f");
            sp.addUniform("texture");
            sp.addUniform("lightsrc");
            sp.addUniform("lightstr");
            sp.addUniform("model");
            sp.addUniform("view");
            sp.addUniform("projection");
        }

        private void initObjects()
        {
            skyDome = new SceneObject(Vector3.Zero, Vector3.Zero, new Vector3(100, 100, 100), "sky", "sky");
            objects = new List<SceneObject>();
            for (int i = 0; i < 100; i++)
            {
                if (r.Next(10) == 1)
                    objects.Add(new SceneObject(new Vector3(3, 0, -i * 3), Vector3.Zero, new Vector3(1, 1, 1f), "tree1", "tree1"));
                if (r.Next(10) == 1)
                    objects.Add(new SceneObject(new Vector3(-3, 0, -i * 3), Vector3.Zero, new Vector3(1, 1, 1f), "tree1", "tree1"));
            }
            street = new List<SceneObject>();
            for (int i = 0; i < 100; i++)
            {
                street.Add(new SceneObject(new Vector3(0, 0, -i * 2), Vector3.Zero, new Vector3(1, 1, 1f), "street", "street-asphalt"));
            }
            newObjects = new List<SceneObject>();
            newObjects.Add(new SceneObject(new Vector3(-3, 0, -300), Vector3.Zero, new Vector3(1, 1, 1), "tree1", "tree1"));
            newObjects.Add(new SceneObject(new Vector3(3, 0, -300), Vector3.Zero, new Vector3(1, 1, 1), "tree1", "tree1"));
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
            models.Add("sky", new VertexBufferObject(OBJLoader.loadModelfromOBJ("Data/Models/sky.obj")[0]));
            textures.Add("sky", Texture.fromFile("Data/Textures/sky.png"));
        }

        public void Update(float elapsedTime, float velocity)
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                objects[i].Position += new Vector3(0, 0, velocity * elapsedTime);
                if (objects[i].Position.Z > 5)
                    objects.RemoveAt(i);
            }
            for (int i = street.Count - 1; i >= 0; i--)
            {
                street[i].Position += new Vector3(0, 0, velocity * elapsedTime);
                if (street[i].Position.Z > 5)
                {
                    street[i].Position -= new Vector3(0, 0, 200);
                }
            }
            addc += elapsedTime * velocity *r.NextDouble();
            if (addc > 10)
            {
                addc-=10;
                objects.Add(newObjects[r.Next(newObjects.Count)].Clone);
            }
        }
        public void Render()
        {
            foreach (SceneObject so in objects)
                renderSceneObject(so);
            foreach (SceneObject so in street)
                renderSceneObject(so);
            renderSceneObject(skyDome);
        }
        private void renderSceneObject(SceneObject so)
        {
            sp.use();

            GL.Uniform3(sp.Uniforms["lightsrc"], new Vector3(5, 6, 1));
            GL.Uniform1(sp.Uniforms["lightstr"], 0.5f);

            Matrix4 m = so.ObjectMatrix; ;
            GL.UniformMatrix4(sp.Uniforms["model"], false, ref m);
            GL.UniformMatrix4(sp.Uniforms["view"], false, ref view);
            GL.UniformMatrix4(sp.Uniforms["projection"], false, ref projection);

            GL.ActiveTexture(TextureUnit.Texture0);
            textures[so.Texture].bind();
            GL.Uniform1(sp.Uniforms["texture"], 0);

            models[so.Model].draw();

            GL.UseProgram(0);
        }
    }
}
