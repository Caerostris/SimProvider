using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimProvider.Graphics
{
    /// <summary>
    /// A Face of a Model
    /// </summary>
    public struct Face
    {
        /// <summary>
        /// Vertex Indices
        /// </summary>
        int[] vIndices;
        /// <summary>
        /// Normal Indices
        /// </summary>
        int[] nIndices;
        /// <summary>
        /// Texture Coordinate Indices
        /// </summary>
        int[] tIndices;

        public Face(int[] vIndices, int[] nIndices, int[] tIndices)
        {
            this.vIndices = vIndices;
            this.tIndices = tIndices;
            this.nIndices = nIndices;
        }
        public Face(int[] vIndices, int[] nIndices)
            : this(vIndices, nIndices, new int[] { -1, -1, -1 }) { }
        public Face(int[] vIndices)
            : this(vIndices, new int[] { -1, -1, -1 }, new int[] { -1, -1, -1 }) { }

    }
}
