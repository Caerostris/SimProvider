using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace SimProvider.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public Vector3 vertex;
        public Vector3 normal;
        public Vector2 texCoord;

        public static int SizeInBytes
        {
            get { return Vector3.SizeInBytes * 2 + Vector2.SizeInBytes; }
        }

    }
}
