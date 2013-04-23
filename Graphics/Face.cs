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
        public uint[] vIndices;
        /// <summary>
        /// Normal Indices
        /// </summary>
        public uint[] nIndices;
        /// <summary>
        /// Texture Coordinate Indices
        /// </summary>
        public uint[] tIndices;

        public Face(uint[] vIndices, uint[] nIndices, uint[] tIndices)
        {
            this.vIndices = vIndices;
            this.tIndices = tIndices;
            this.nIndices = nIndices;
        }
        public Face(uint[] vIndices, uint[] nIndices)
            : this(vIndices, nIndices, new uint[] { 0, 0, 0 }) { }
        public Face(uint[] vIndices)
            : this(vIndices, new uint[] { 0, 0, 0 }, new uint[] { 0, 0, 0 }) { }

    }
}
