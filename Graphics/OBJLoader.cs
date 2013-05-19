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
        public static Mesh[] loadModelfromOBJ(string path)
        {
            if (!File.Exists(path))
                return null;

            System.IO.StreamReader sr = new StreamReader(path);

            List<Mesh> meshes = new List<Mesh>();
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector2> texCoords = new List<Vector2>();
            //List<Face> faces = new List<Face>(); 

            while (!sr.EndOfStream)
            {
                string[] s = sr.ReadLine().Split(' ');
                Mesh m = null;
                if (meshes.Count > 0)
                    m = meshes[meshes.Count - 1];
                switch (s[0])
                {
                    case "o":
                        if(meshes.Count > 0){
                            m.Vertices.AddRange(vertices);
                            m.Normals.AddRange(normals);
                            m.TexCoords.AddRange(texCoords);
                            }
                        meshes.Add(new Mesh());
                        meshes[meshes.Count - 1].Name = s[1];
                        break;
                    case "g":
                        m = new Mesh();
                        m.Name = s[1];
                        meshes.Add(m);
                        break;
                    case "v":
                        vertices.Add(new Vector3(Single.Parse(s[1], CultureInfo.InvariantCulture), Single.Parse(s[2], CultureInfo.InvariantCulture), Single.Parse(s[3], CultureInfo.InvariantCulture)));
                        break;
                    case "vn":
                        normals.Add(new Vector3(Single.Parse(s[1], CultureInfo.InvariantCulture), Single.Parse(s[2], CultureInfo.InvariantCulture), Single.Parse(s[3], CultureInfo.InvariantCulture)));
                        break;
                    case "vt":
                        texCoords.Add(new Vector2(Single.Parse(s[1], CultureInfo.InvariantCulture), Single.Parse(s[2], CultureInfo.InvariantCulture)));
                        break;
                    case "f":
                        uint[] vI = new uint[] { UInt32.Parse(s[1].Split('/')[0], CultureInfo.InvariantCulture) - 1, UInt32.Parse(s[2].Split('/')[0], CultureInfo.InvariantCulture) - 1, UInt32.Parse(s[3].Split('/')[0], CultureInfo.InvariantCulture) - 1 };
                        uint[] nI = new uint[] { 0, 0, 0 };
                        uint[] tI = new uint[] { 0, 0, 0 };
                        if (texCoords.Count > 0)
                        {
                            tI = new uint[] { UInt32.Parse(s[1].Split('/')[1], CultureInfo.InvariantCulture) - 1, UInt32.Parse(s[2].Split('/')[1], CultureInfo.InvariantCulture) - 1, UInt32.Parse(s[3].Split('/')[1], CultureInfo.InvariantCulture) - 1 };
                            if (normals.Count > 0)
                            {
                                nI = new uint[] { UInt32.Parse(s[1].Split('/')[2], CultureInfo.InvariantCulture) - 1, UInt32.Parse(s[2].Split('/')[2], CultureInfo.InvariantCulture) - 1, UInt32.Parse(s[3].Split('/')[2], CultureInfo.InvariantCulture) - 1 };
                            }

                        }
                        else if (normals.Count > 0)
                        {
                            nI = new uint[] { UInt32.Parse(s[1].Split('/')[1], CultureInfo.InvariantCulture) - 1, UInt32.Parse(s[2].Split('/')[1], CultureInfo.InvariantCulture) - 1, UInt32.Parse(s[3].Split('/')[1], CultureInfo.InvariantCulture) - 1 };
                        }
                        m.Faces.Add(new Face(vI, nI, tI));
                        break;
                    default:
                        break;
                }
            }
            if (meshes.Count > 0)
            {
                meshes[meshes.Count - 1].Vertices.AddRange(vertices);
                meshes[meshes.Count - 1].Normals.AddRange(normals);
                meshes[meshes.Count - 1].TexCoords.AddRange(texCoords);
            }
            return meshes.ToArray();
        }
    }
}
