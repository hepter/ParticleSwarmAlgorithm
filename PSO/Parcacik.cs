using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PSO
{
    public static class Global
    {
        public static Random Rnd = new Random();
    }
    public class Parcacik
    {
        private readonly double _c1;
        private readonly double _c2;

        private readonly double _c1_const = (double)1 / 100;
        private readonly double _c2_const = (double)1 / 100;
        private static int maxRenderPoint=100;

        public Parcacik(double c1,double c2)
        {
            _c1 = c1;
            _c2 = c2;
            P_Uygunluk=Double.PositiveInfinity;
            var rndPD = getRndPoindD;
            _Point.X = rndPD.X;
            _Point.Y = rndPD.Y;
            RenderPointList.Add(rndPD);
            if (RenderPointList.Count> maxRenderPoint)
            {
                RenderPointList.RemoveAt(0);
            }
            //Pulse();
        }

        public void Pulse()
        {
            // var rndPD = getRndPoindD;
            KonumGüncelle();
            RenderPointList.Add(Point);
        
        }

        private double P_Uygunluk;

        public PointD GBest { get; set; }

        public PointD PBest
        {
            get
            {

                if (_pBest.X == 0 && _pBest.Y == 0)
                    _pBest = Point;

                return _pBest;
            }
            set => _pBest = value;
        }

        public List<PointD> RenderPointList
        {
            get
            {
                if (_renderPointList == null)
                {
                    _renderPointList=new List<PointD>();
                }

                return _renderPointList;
            }
            set => _renderPointList = value;
        }

        public PointD EskiVektör { get; set; }
        public PointD Vektör
        {
            get
            {
                
                PointD vektör=new PointD();
                vektör.X = EskiVektör.X + 
                          _c1 * _c1_const * Global.Rnd.NextDouble() * (PBest.X - Point.X) +
                          _c2 * _c2_const * Global.Rnd.NextDouble() * (GBest.X - Point.X);
                vektör.Y = EskiVektör.Y + 
                          _c1 * _c1_const * Global.Rnd.NextDouble() * (PBest.Y - Point.Y) +
                          _c2 * _c2_const * Global.Rnd.NextDouble() * (GBest.Y - Point.Y);
                EskiVektör = vektör;
                return vektör;

            }
        }

        private PointD _Point;
        private List<PointD> _renderPointList;
        private PointD _pBest;

        public PointD Point
        {
            get { return _Point; }
            set
            {
                _Point = value;
                if (P_Uygunluk> _Point.Uygunluk())
                {
                    PBest = value;
                    P_Uygunluk = _Point.Uygunluk();
                }
            }
        }

        private void KonumGüncelle()
        {
            PointD v = Vektör;
            PointD p = new PointD();

            p.X = Point.X + v.X;
            p.Y = Point.Y + v.Y;
            Point = p;
        }

        

        private PointD getRndPoindD
        {
            get
            {
                double s1 = Global.Rnd.NextDouble() * 20 - 10;
                double s2 = Global.Rnd.NextDouble() * 20 - 10;
                return  new PointD(s1,s2);
            }
        }


    }
}
