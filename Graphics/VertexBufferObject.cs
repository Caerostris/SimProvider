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
        int elemets;
        public VertexBufferObject(Mesh m)
        {
            Vertex[] vertices = new Vertex[m.Vertices.Count];
            uint[] indices = new uint[m.Faces.Count*3];
            List<Face> f = m.Faces;
            for (int a = 0; a < m.Faces.Count; a++){
                for (int i = 0; i < f[a].vIndices.Length; i++)
                {
                    Vertex v = new Vertex();
                    v.Position = m.Vertices[(int)f[a].vIndices[i]];
                    v.Normal = m.Normals[(int)f[a].nIndices[i]];
                    v.TextureCoordinate = m.TexCoords[(int)f[a].tIndices[i]];
                    vertices[f[a].vIndices[i]] = v;
                    
                }
                indices[a*3] = f[a].vIndices[0];
                indices[a*3+1] = f[a].vIndices[1];
                indices[a*3+2] = f[a].vIndices[2];
            }

            ids = new int[2];
            GL.GenBuffers(2, ids);

            GL.BindBuffer(BufferTarget.ArrayBuffer,ids[0]);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vertex.SizeInBytes*vertices.Length), vertices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ids[1]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(int)*indices.Length), indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            elemets = indices.Length / 3;
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

        public int ElementBuffer
        {
            get { return ids[1]; }
        }
        public int ElementCount
        {
            get { return elemets; }
        }

        public void draw()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
            GL.VertexAttribPointer(
                0,
                3,
                VertexAttribPointerType.Float,
                false,
                Vertex.SizeInBytes,
                new IntPtr(0));
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(
                1,
                3,
                VertexAttribPointerType.Float,
                false,
                Vertex.SizeInBytes,
                new IntPtr(12));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(
                2,
                2,
                VertexAttribPointerType.Float,
                false,
                Vertex.SizeInBytes,
                new IntPtr(24));
            GL.EnableVertexAttribArray(2);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ElementBuffer);
            GL.DrawElements(BeginMode.Triangles, ElementCount * 3, DrawElementsType.UnsignedInt, 0);
            GL.DisableVertexAttribArray(0);
            GL.DisableVertexAttribArray(1);
            GL.DisableVertexAttribArray(2);
        }
    }
}
