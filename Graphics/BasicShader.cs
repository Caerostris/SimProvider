using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public class BasicShader : ShaderProgram
    {
        private Dictionary<string, int> uniforms = new Dictionary<string, int>();
        private Dictionary<string, int> attributes = new Dictionary<string, int>();
        protected BasicShader(string vs, string fs)
            : base(vs, fs)
        {
        }

        public void addUniform(string name)
        {
            uniforms.Add(name,GL.GetUniformLocation(id,name));
        }
        public void addAttribute(string name)
        {
            attributes.Add(name, GL.GetUniformLocation(id, name));
        }
        public Dictionary<string, int> Uniforms
        {
            get { return uniforms; }
        }
        public Dictionary<string, int> Attributes
        {
            get { return attributes; }
        }
        public int ID
        {
            get { return id; }
        }
        

        public static BasicShader create(string vsPath, string fsPath)
        {
            string vs = File.ReadAllText(vsPath);
            string fs = File.ReadAllText(fsPath);
            return new BasicShader(vs, fs);
        }
    }
}
