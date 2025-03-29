
using System;
using System.Collections.Generic;
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

        // Opens the location with Edge 

        private void OpenMap(string n1, string n2, string n3, string e1, string e2, string e3)
        {
            string dmsLat = n1.ToString() + "°" + n2.ToString() + "'" + n3.ToString() + "\"N";
            string dmsLng = e1.ToString() + "°" + e2.ToString() + "'" + e3.ToString() + "\"E";

            // URLエンコード（スペースや記号があるので注意）
            string url = $"https://www.google.com/maps/place/{Uri.EscapeDataString(dmsLat + " " + dmsLng)}";
            // Edgeを使って開く
            var psi = new System.Diagnostics.ProcessStartInfo
            {
                FileName = "msedge.exe",
                Arguments = url,
                UseShellExecute = true
            };

            try
            {
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edgeで開けませんでした: " + ex.Message);
            }

        }
        // Open the location
        private void button1_Click(object sender, EventArgs e)
        {
            OpenMap(this.textBoxA1.Text, this.textBoxA2.Text, this.textBoxA3.Text, this.textBoxA4.Text, this.textBoxA5.Text, this.textBoxA6.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenMap(this.textBoxB1.Text, this.textBoxB2.Text, this.textBoxB3.Text, this.textBoxB4.Text, this.textBoxB5.Text, this.textBoxB6.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenMap(this.textBoxC1.Text, this.textBoxC2.Text, this.textBoxC3.Text, this.textBoxC4.Text, this.textBoxC5.Text, this.textBoxC6.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenMap(this.textBoxD1.Text, this.textBoxD2.Text, this.textBoxD3.Text, this.textBoxD4.Text, this.textBoxD5.Text, this.textBoxD6.Text);
        }
    }       
}
