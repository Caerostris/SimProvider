using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace SimProvider.Graphics
{
    public class Texture
    {
        private int id;
        public int ID { get { return id; } }
        private Texture() { }
        public static Texture fromFile(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException(path);

            Texture t = new Texture();
            t.id = GL.GenTexture();
            
            Bitmap bmp = new Bitmap(path);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.BindTexture(TextureTarget.Texture2D, t.id);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bmp.UnlockBits(data);
            bmp.Dispose();

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            return t;
        }
        public static Texture create(int width, int height)
        {
            Texture t = new Texture();
            t.id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, t.id);
            byte[] data = new byte[width * height * 4];
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data);
            
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.BindTexture(TextureTarget.Texture2D, 0);
            return t;
        }
        public void bind()
        {
            GL.BindTexture(TextureTarget.Texture2D,id);
        }
        public void delete()
        {
            GL.DeleteTexture(id);
        }
        public static Bitmap imageFromFramebuffer(int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, width, height, OpenTK.Graphics.OpenGL.PixelFormat.DepthComponent, PixelType.Float, bd.Scan0);
            bmp.UnlockBits(bd);

            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
            
        }

    }
}
