using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO
{
    static  class Extensions
    {

        public static PointD ResimOranla(this PointD p)
        {
            double x = (int)((p.X + 10) / 20 * imgX);
            double y = (int)((p.Y + 10) / 20 * imgY);
            p = new PointD(x, y);
            return p;
        }
        public static List<PointD> ResimOranla(this List<PointD> p)
        {
            for (int i = 0; i < p.Count; i++)
                p[i] = p[i].ResimOranla();
            return p;
        }
        public static PointD EnIyiGBest(this List<Parcacik> parcaciklar)
        {
            Parcacik p = parcaciklar.OrderBy(a => a.Point.Uygunluk()).First();
            return p.PBest;
        }
        public static void GBestYukle(this List<Parcacik> parcaciklar, PointD gBest)
        {
            parcaciklar.ForEach(a => a.GBest = gBest);
        }
        public static void Pulse(this List<Parcacik> parcaciklar)
        {
            parcaciklar.ForEach(a => a.Pulse());
        }
        public static double Uygunluk(this PointD p)
        {

            double result = (0.26 * (Math.Pow(p.X, 2) + Math.Pow(p.Y, 2))) - (0.48 * p.X * p.Y);
            return (result);
        }

        public static int imgX, imgY;
        public static int kuyruk = 10;
        public static int cap = 3;
        public static float kalinlik = 2.1f;
        public static void RenderParcacik(this List<Parcacik> p, Image img, Color c)
        {

            foreach (Parcacik parca in p)
            {
                var list = parca.RenderPointList.AsEnumerable().Reverse();
                drawLines(list.Take(kuyruk).ToList().ResimOranla(), img, c);
            }
           // drawDot((PointF)(p.EnIyiGBest().ResimOranla()), img, Color.DarkBlue);

        }

        public static void drawLines(List<PointD> p, Image img, Color color)
        {
            Graphics g = Graphics.FromImage(img);
            int alpha = (int)255;
            Color colorTemp = Color.Empty;
            colorTemp = Color.FromArgb(alpha, color.R, color.G, color.B);

            Pen pen = new Pen(color, kalinlik);

            PointF[] points = p.Select(a => (PointF)a).ToArray();
            if (points.Length <= 1)
            {
                g.Dispose();
                return;
            }

            g.DrawLines(pen, points);
            g.FillCircle(new SolidBrush(Color.Black), points[0].X, points[0].Y, cap);

            g.Dispose();
        }
        public static void drawDot(PointF p, Image img, Color color)
        {
            Graphics g = Graphics.FromImage(img);
            int alpha = (int)255;
            g.FillCircle(new SolidBrush(Color.FromArgb(alpha, color.R, color.G, color.B)), p.X, p.Y, 5);
            g.Dispose();
        }
        public static void FillCircle(this Graphics g, Brush brush,
            float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }
    }
}
