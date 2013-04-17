using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SimGL
{
    public class Texture
    {
        private int id;
        private Texture();
        public void fromFile(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException(path);

            Texture t = new Texture();
            t.id = GL.GenTexture();

            Bitmap bmp = new Bitmap(path);
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            bmp.UnlockBits(bmpData);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
        public void bind()
        {
           
        }
        public void delete()
        {
            GL.DeleteTexture(id);
        }

    }
}
