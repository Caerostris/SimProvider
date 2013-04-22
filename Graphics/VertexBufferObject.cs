using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public class VertexBufferObject
    {
        int[] ids;
        public VertexBufferObject(Model m)
        {
            Vertex[] vertices = new Vertex[m.Vertices.Count];
            int[] indices = new int[m.Faces.Count];
            List<Face> f = m.Faces;
            for (int a = 0; a < m.Faces.Count; a++){
                for (int i = 0; i < f[a].vIndices.Length; i++)
                {
                    Vertex v = new Vertex();
                    v.Position = m.Vertices[f[a].vIndices[i]-1];
                    v.Normal = m.Vertices[f[a].nIndices[i]-1];
                    v.TextureCoordinate = m.Vertices[f[a].tIndices[i]-1];
                    vertices[f[a].vIndices[i]-1] = v;
                    indices[a] = f[a].vIndices[i];
                }
            }

            ids = new int[2];
            GL.GenBuffers(2, ids);

            GL.BindBuffer(BufferTarget.ArrayBuffer,ids[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vertex.SizeInBytes*vertices.Length), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ids[1]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(int)*indices.Length), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void delete()
        {
            GL.DeleteBuffers(2, ids);
            ids[0] = 0; ids[1] = 0;
        }

        public int VertexBuffer
        {
            get { return ids[0]; }
        }

        public int IndexBuffer
        {
            get { return ids[1]; }
        }
    }
}
