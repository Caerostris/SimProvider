using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK;

namespace SimProvider.Graphics
{
    public static class OBJLoader
    {
        public static Model loadModelfromOBJ(string path)
        {
            if (!File.Exists(path))
                return null;

            System.IO.StreamReader sr = new StreamReader(path);
           
            List<Model> models = new List<Model>();
            //List<Vector3> vertieces = new List<Vector3>();
            //List<Vector3> normals = new List<Vector3>();
            //List<Vector2> texCoords = new List<Vector2>();
            //List<Face> faces = new List<Face>(); 
            Model m = null;
            while (!sr.EndOfStream)
            {
                string[] s = sr.ReadLine().Split(' ');

                switch (s[0]) {
                    case "o":
                        m = new Model();
                        m.Name = s[1];
                        models.Add(m);
                        break;
                    case "v":
                        m.Vertices.Add(new Vector3(Single.Parse(s[1]), Single.Parse(s[2]), Single.Parse(s[3])));
                        break;
                    case "vn":
                        m.Normals.Add(new Vector3(Single.Parse(s[1]), Single.Parse(s[2]), Single.Parse(s[3])));
                        break;
                    case "vt":
                        m.TexCoords.Add(new Vector2(Single.Parse(s[1]), Single.Parse(s[2])));
                        break;
                    case "f":
                        int[] vI = new int[]{ Int32.Parse(s[1].Split('/')[0]), Int32.Parse(s[2].Split('/')[0]), Int32.Parse(s[3].Split('/')[0])};
                        int[] nI = new int[] { -1, -1, -1 };
                        int[] tI = new int[] { -1, -1, -1 };
                        if (m.TexCoords.Count > 0)
                        {
                            tI = new int[] { Int32.Parse(s[1].Split('/')[1]), Int32.Parse(s[2].Split('/')[1]), Int32.Parse(s[3].Split('/')[1]) };
                            if (m.Normals.Count > 0)
                            {
                                nI = new int[] { Int32.Parse(s[1].Split('/')[2]), Int32.Parse(s[2].Split('/')[2]), Int32.Parse(s[3].Split('/')[2]) };
                            }

                        }else if (m.Normals.Count > 0)
                        {
                            nI = new int[]{ Int32.Parse(s[1].Split('/')[1]), Int32.Parse(s[2].Split('/')[1]), Int32.Parse(s[3].Split('/')[1])};
                        }
                        m.Faces.Add(new Face(vI, nI, tI));
                        break;
                    default:
                        break;
                }
            }
            return m;
        }
    }
}
