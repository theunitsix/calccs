
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BearingDistanceTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            var points = new List<GeoPoint>
            {
            new GeoPoint { Label = "A", LatDeg = Convert.ToInt32(this.textBoxA1.Text), LatMin = Convert.ToInt32(this.textBoxA2.Text), LatSec = Convert.ToInt32(this.textBoxA3.Text), LonDeg = Convert.ToInt32(this.textBoxA4.Text), LonMin = Convert.ToInt32(this.textBoxA5.Text), LonSec = Convert.ToInt32(this.textBoxA6.Text) },
            new GeoPoint { Label = "B", LatDeg = Convert.ToInt32(this.textBoxB1.Text), LatMin = Convert.ToInt32(this.textBoxB2.Text), LatSec = Convert.ToInt32(this.textBoxB3.Text), LonDeg = Convert.ToInt32(this.textBoxB4.Text), LonMin = Convert.ToInt32(this.textBoxB5.Text), LonSec = Convert.ToInt32(this.textBoxB6.Text) },
            new GeoPoint { Label = "C", LatDeg = Convert.ToInt32(this.textBoxC1.Text), LatMin = Convert.ToInt32(this.textBoxC2.Text), LatSec = Convert.ToInt32(this.textBoxC3.Text), LonDeg = Convert.ToInt32(this.textBoxC4.Text), LonMin = Convert.ToInt32(this.textBoxC5.Text), LonSec = Convert.ToInt32(this.textBoxC6.Text) },
            new GeoPoint { Label = "D", LatDeg = Convert.ToInt32(this.textBoxD1.Text), LatMin = Convert.ToInt32(this.textBoxD2.Text), LatSec = Convert.ToInt32(this.textBoxD3.Text), LonDeg = Convert.ToInt32(this.textBoxD4.Text), LonMin = Convert.ToInt32(this.textBoxD5.Text), LonSec = Convert.ToInt32(this.textBoxD6.Text) },
            };
            bool isWorldGeodetic = true;
            var results = GeoUtility.CalculateGeoResults(points, isWorldGeodetic);

            // 出力
            var sb = new StringBuilder();
            foreach (var result in results)
            {
                Console.WriteLine($"{result.From}→{result.To}: 方位角 {result.Bearing}° / 距離 {result.Distance} km");
                sb.AppendLine($"{result.From}→{result.To}: 方位角 {result.Bearing}° / 距離 {result.Distance} km");
            }


            // マップリンクも確認可能
            foreach (var kv in GeoUtility.MapLinks)
            {
                Console.WriteLine($"{kv.Key}点: {kv.Value}");
                sb.AppendLine($"{kv.Key}点: {kv.Value}");
            }
            this.txtResult.Text = sb.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }       
}
