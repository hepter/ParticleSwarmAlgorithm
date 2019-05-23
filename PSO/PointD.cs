using System.Drawing;

namespace PSO
{
    public struct PointD
    {
        public PointD(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

       
        public double Y { get; set; }
        public double X { get; set; }


        static public explicit operator PointF(PointD p)
        {
            return new PointF((float)p.X, (float)p.Y);
        }

      
    }

}