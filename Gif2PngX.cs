using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace Gif2Png
{
  class Program
  {
    static void Main(string[] args)
    {
      for (int j = 0; j < args.Length; j++)
      {
        Console.WriteLine("Convert File of:"+args[j]);
        using(Image gif = Image.FromFile(args[j])){
          FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);

          int count = gif.GetFrameCount(fd);
          using(Image target= new Bitmap(count*150, 150))
          using(Graphics g = Graphics.FromImage(target)){
            g.Clear(Color.Transparent);
            for (int i = 0; i < count; i++)
            {
              gif.SelectActiveFrame(fd, i);
              g.DrawImage(gif,new Point(150/2-gif.Width/2+i*150,150/2-gif.Height/2));
              //gif.Save( Path.GetFileNameWithoutExtension(args[j])+
              //         string.Format("_{0}", i)
              //         + ".png", ImageFormat.Png);
            }
            target.Save(Path.GetFileNameWithoutExtension(args[j])+".png",ImageFormat.Png);
          }
        }
      }
      Console.WriteLine("Convert Fine. Press any key to continue... ");
      Console.ReadKey(true);
    }
  }
}
