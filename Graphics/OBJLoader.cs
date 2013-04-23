using System;
using System.Globalization;
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
                        m.Vertices.Add(new Vector3(Single.Parse(s[1], CultureInfo.InvariantCulture), Single.Parse(s[2], CultureInfo.InvariantCulture), Single.Parse(s[3], CultureInfo.InvariantCulture)));
                        break;
                    case "vn":
                        m.Normals.Add(new Vector3(Single.Parse(s[1], CultureInfo.InvariantCulture), Single.Parse(s[2], CultureInfo.InvariantCulture), Single.Parse(s[3], CultureInfo.InvariantCulture)));
                        break;
                    case "vt":
                        m.TexCoords.Add(new Vector2(Single.Parse(s[1], CultureInfo.InvariantCulture), Single.Parse(s[2], CultureInfo.InvariantCulture)));
                        break;
                    case "f":
                        uint[] vI = new uint[] { UInt32.Parse(s[1].Split('/')[0], CultureInfo.InvariantCulture), UInt32.Parse(s[2].Split('/')[0], CultureInfo.InvariantCulture), UInt32.Parse(s[3].Split('/')[0], CultureInfo.InvariantCulture) };
                        uint[] nI = new uint[] { 0, 0, 0 };
                        uint[] tI = new uint[] { 0, 0, 0 };
                        if (m.TexCoords.Count > 0)
                        {
                            tI = new uint[] { UInt32.Parse(s[1].Split('/')[1], CultureInfo.InvariantCulture), UInt32.Parse(s[2].Split('/')[1], CultureInfo.InvariantCulture), UInt32.Parse(s[3].Split('/')[1], CultureInfo.InvariantCulture) };
                            if (m.Normals.Count > 0)
                            {
                                nI = new uint[] { UInt32.Parse(s[1].Split('/')[2], CultureInfo.InvariantCulture), UInt32.Parse(s[2].Split('/')[2], CultureInfo.InvariantCulture), UInt32.Parse(s[3].Split('/')[2], CultureInfo.InvariantCulture) };
                            }

                        }else if (m.Normals.Count > 0)
                        {
                            nI = new uint[] { UInt32.Parse(s[1].Split('/')[1], CultureInfo.InvariantCulture), UInt32.Parse(s[2].Split('/')[1], CultureInfo.InvariantCulture), UInt32.Parse(s[3].Split('/')[1], CultureInfo.InvariantCulture) };
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
