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
        Console.WriteLine("Export:"+args[j]);
        Image gif = Image.FromFile(args[j]);
        FrameDimension fd = new FrameDimension(gif.FrameDimensionsList[0]);

        int count = gif.GetFrameCount(fd);

        for (int i = 0; i < count; i++)
        {
          gif.SelectActiveFrame(fd, i);

          gif.Save( Path.GetFileNameWithoutExtension(args[j])+
                   string.Format("_{0}", i)
                   + ".png", ImageFormat.Png);
        }
      }
      Console.WriteLine("Export OK!");
      Console.Write("Press any key to continue . . . ");
      Console.ReadKey(true);
    }
  }
}
