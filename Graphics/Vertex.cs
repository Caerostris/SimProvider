using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using OpenTK;

namespace SimProvider.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector3 TextureCoordinate;
        public static int SizeInBytes
        {
            get { return Vector3.SizeInBytes*2+Vector2.SizeInBytes; }
        }
    }
}
