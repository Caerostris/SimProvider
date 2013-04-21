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
        private int id;
        private int vertexShader;
        private int fragmentShader;

        private ShaderProgram(string vs, string fs)
        {
            vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vs);
            GL.CompileShader(vertexShader);
            System.Console.WriteLine(GL.GetShaderInfoLog(vertexShader));
            
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, vs);
            GL.CompileShader(fragmentShader);
            System.Console.WriteLine(GL.GetShaderInfoLog(fragmentShader));

            id = GL.CreateProgram();
            GL.AttachShader(id, vertexShader);
            GL.AttachShader(id, fragmentShader);
            GL.LinkProgram(id);
            System.Console.WriteLine(GL.GetProgramInfoLog(id));
        }

        public void use()
        {
            GL.UseProgram(id);
        }

        public static ShaderProgram create(string vsPath, string fsPath)
        {
            string vs = File.ReadAllText(vsPath);
            string fs = File.ReadAllText(fsPath);
            return new ShaderProgram(fs, vs);
        }
    }
}
