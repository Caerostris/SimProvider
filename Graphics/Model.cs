//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using OpenTK;
//using OpenTK.Graphics.OpenGL;

//namespace SimProvider.Graphics
//{
//    public class VBOModel
//    {
//        private List<Child> childs;
//        public List<Child> Childs
//        {
//            get { return childs; }
//        }
//        public VBOModel(Child[] childs)
//        {
//            this.childs = new List<Child>();
//            this.childs.AddRange(childs);
//        }

//        public class Child
//        {
//            VertexBufferObject vbo;
//            Matrix4 transform;
//            VBOModel parent;
//            public VertexBufferObject VBO { get { return vbo; } }
//            public Matrix4 Transform { get { return transform; } set { transform = value; } }
//            public VBOModel Parent { get { return parent; } }
//            public Child(VertexBufferObject vbo, Matrix4 transform, VBOModel parent)
//            {
//                this.vbo = vbo;
//                this.transform = transform;
//                this.parent = parent;
//            }
//        }
//        public void render()
//        {
//            foreach (Child c in childs)
//            {
//            }
//        }
//    }
//}
