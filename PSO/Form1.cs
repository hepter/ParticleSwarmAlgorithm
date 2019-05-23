using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PSO.Properties;

namespace PSO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool isRunning;
        private bool ToggleKontrol()
        {
            if (isRunning)
            {
                isRunning = false;
                button1.Text = "HESAPLA";
                groupBox1.Enabled = true;
            }
            else
            {
                button1.Text = "Durdur";
                groupBox1.Enabled = false;
                isRunning = true;
            }
            Application.DoEvents();
            return isRunning;
        }
        private Series CreateKaynakSeries()
        {
            chart1.Series.Clear();
            Series series = chart1.Series.Add("Sonuclar");
            chart1.IsSoftShadows = false;
            series.ChartType = SeriesChartType.Area;
            series.BorderWidth = 3;
            series.Color = Color.IndianRed;
            return series;
        }
        private async void Button1_Click(object sender, EventArgs e)
        {
            if (!ToggleKontrol()) return;

            Global.Rnd = new Random();
            double c1 = double.Parse(numericUpDown3.Value+"");
            double c2 = double.Parse(numericUpDown4.Value+"");
            int iterasyon = (int)numericUpDown1.Value;
            int hız = (int)numericUpDown5.Value;
            int parcacikSayi = (int)numericUpDown2.Value;


            Series series = CreateKaynakSeries();
            List<Parcacik> parcacikList = ParcacikOlustur(parcacikSayi, c1, c2);
            Image img = Resources.matyas;
         
            for (int i = 0; i < iterasyon; i++)
            {
                 img = Resources.matyas;
                PointD best = parcacikList.EnIyiGBest();
                label10.Text = best.Uygunluk().ToString();
                DataPoint dataPoint = new DataPoint(i + 1,best.Uygunluk());
                chart1.Invoke((Action)delegate { series.Points.Add(dataPoint); });
              
                parcacikList.RenderParcacik(img,colorDialog1.Color);
                pictureBox1.Image = img;
                parcacikList.GBestYukle(best);
                parcacikList.Pulse();
              

                await Task.Run(() => { Thread.Sleep(hız); });
                if (!isRunning) break;
                if (i == iterasyon - 1) ToggleKontrol();
            }
        }


        public List<Parcacik> ParcacikOlustur(int miktar,double c1, double c2)
        {
            List<Parcacik> parcaciklar = new List<Parcacik>();
            for (int i = 0; i < miktar; i++)
            {
                parcaciklar.Add(new Parcacik(c1,c2));
            }

            return parcaciklar;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Extensions.imgX = Resources.matyas.Width - 50;
            Extensions.imgY = Resources.matyas.Height - 50;
            NumericUpDown6_ValueChanged(numericUpDown6, null);
            NumericUpDown7_ValueChanged(numericUpDown7, null);
            NumericUpDown8_ValueChanged(numericUpDown8, null);
        }

        private void ButtonClr1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btn.BackColor = colorDialog1.Color;
            }
        }

        private void NumericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            int deger = (int)((NumericUpDown) sender).Value;
            Extensions.kuyruk = deger;
        }

        private void NumericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            int deger = (int)((NumericUpDown)sender).Value;
            Extensions.cap = deger;
        }

        private void NumericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            float deger = (float)((NumericUpDown)sender).Value;
            Extensions.kalinlik = deger;
        }
    }

}
