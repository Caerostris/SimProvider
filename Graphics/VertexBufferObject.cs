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
        uint[] ids;
        private VertexBufferObject(Vertex[] data,Int32[] indices)
        {
            ids = new uint[2];
            GL.GenBuffers(2, ids);
            GL.BindBuffer(BufferTarget.ArrayBuffer, ids[0]);
            GL.BufferData<Vertex>(BufferTarget.ArrayBuffer, new IntPtr(data.Length * Vertex.SizeInBytes), data, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ids[1]);
            GL.BufferData<int>(BufferTarget.ElementArrayBuffer, new IntPtr(indices.Length * 4), indices, BufferUsageHint.StaticDraw);
        }
        public void bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, ids[0]);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ids[1]);
        }
        public void delete()
        {
            GL.DeleteBuffers(2, ids);
        }
    }
}
