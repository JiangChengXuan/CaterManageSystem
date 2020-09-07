using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace CaterCommon
{
    /// <summary>
    /// 验证码类
    /// </summary>
   public class AuthCode
    {

       public AuthCode()
       {
           Width = 80;
           Height = 45;
           Code = CreateCode();
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Code { get; set; }
       

        private string CreateCode()
        {
            Code = string.Empty;
            Random random = new Random();
            string guId = Guid.NewGuid().ToString().Replace("-", "");

            for (int i = 0; i < 4; i++)
            {
                int index = random.Next(guId.Length);
                Code += guId[index];
            }

            return Code;
        } // END CreateCode（）


       public Image DrawAuthCodeImg()
       {
 
               //创建Image-画框
               Bitmap image = new Bitmap(Width, Height);

               //创建画板
               Graphics grap = Graphics.FromImage(image);

               //绘制验证码
               Font font = new Font("微软雅黑", 16, FontStyle.Bold);
               Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
               LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Red, Color.Yellow, LinearGradientMode.Vertical);
               Point point = new Point(5, 3);  // 绘制到图片上的位置
               grap.DrawString(Code, font, brush, point);

               //绘制干扰线
               DrawLines(image, grap);

               //绘制干扰点
               DrawDots(image);
    
           return image;
       }  // END DrawAuthCodeImg（）


       private void DrawDots(Bitmap image)
       {
           Random ran = new Random();
           for (int i = 0; i < 100; i++)
           {
               int x = ran.Next(image.Width);
               int y = ran.Next(image.Height);

               image.SetPixel(x, y, Color.Blue);
           }

       } // END DrawDots（）

       private void DrawLines(Image image,Graphics grap)
       {
           Random ran = new Random();
           Pen pen = new Pen(Color.DarkGray, 1);
           List<Point> points = new List<Point>();

           for (int i = 0; i < 20; i++)
           {
               int x = ran.Next(image.Width);
               int y = ran.Next(image.Height);
               Point point = new Point(x, y);
               points.Add(point);
           }

           grap.DrawLines(pen, points.ToArray());

       }  // END DrawLines（）


    }
}
