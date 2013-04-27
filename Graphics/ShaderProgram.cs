using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public class ShaderProgram
    {
        protected int id;

        protected ShaderProgram(string vs, string fs)
        {
            int vertexShader;
            int fragmentShader;
            //---------------------------------------------------------
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vs);
            GL.CompileShader(vertexShader);

            string il = GL.GetShaderInfoLog(vertexShader);
            if (!String.IsNullOrWhiteSpace(il))
                System.Console.WriteLine(il);
            //---------------------------------------------------------
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fs);
            GL.CompileShader(fragmentShader);

            il = GL.GetShaderInfoLog(fragmentShader);
            if (!String.IsNullOrWhiteSpace(il))
                System.Console.WriteLine(il);
            //---------------------------------------------------------
            id = GL.CreateProgram();
            GL.AttachShader(id, vertexShader);
            GL.AttachShader(id, fragmentShader);
            GL.LinkProgram(id);

            il = GL.GetProgramInfoLog(id);
            if (!String.IsNullOrWhiteSpace(il))
                System.Console.WriteLine(il);
            //---------------------------------------------------------
            GL.DetachShader(id, vertexShader);
            GL.DetachShader(id, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void use()
        {
            GL.UseProgram(id);
        }

        public static ShaderProgram create(string vsPath, string fsPath)
        {
            string vs = File.ReadAllText(vsPath);
            string fs = File.ReadAllText(fsPath);
            return new ShaderProgram(vs, fs);
        }
    }
}
