using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace school_analytics
{
    public partial class analysis_teacher : Form
    {
        public analysis_teacher()
        {
            InitializeComponent();
        }
        private DataTable allData;
        private void analysis_teacher_Load(object sender, EventArgs e)
        {

            diagram_table diagram_table = new diagram_table();
            allData = diagram_table.GetTeacherGrades();

            // Например: строим диаграмму успеваемости по предметам
            DrawChart(allData);
        }
        private void DrawChart(DataTable table)
        {
            DrawTeacherRankPieChart(table);

        }

        private void DrawTeacherRankPieChart(DataTable table)
        {
            var grouped = table.AsEnumerable()
                .GroupBy(r => r["teacher_rank"].ToString())
                .Select(g => new
                {
                    Rank = string.IsNullOrWhiteSpace(g.Key) ? "Невизначено" : g.Key,
                    Count = g.Select(x => x["teacher_id"]).Distinct().Count()
                })
                .ToList();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("MainArea"));

            // 🔹 Центруємо діаграму
            var area = chart1.ChartAreas["MainArea"];
            area.Position = new ElementPosition(0, 0, 100, 90);
            area.InnerPlotPosition = new ElementPosition(20, 5, 60, 80);

            // 🔹 Створюємо серію
            Series rankSeries = new Series("Ранг викладача");
            rankSeries.ChartType = SeriesChartType.Pie;
            rankSeries.BorderColor = Color.White;
            rankSeries.BorderWidth = 2;
            rankSeries["PieLabelStyle"] = "Disabled"; // без тексту всередині

            double total = grouped.Sum(x => x.Count);

            foreach (var item in grouped)
            {
                int pointIndex = rankSeries.Points.AddXY(item.Rank, item.Count);
                double percent = item.Count / total * 100;
                rankSeries.Points[pointIndex].LegendText = $"{item.Rank} ({item.Count}, {percent:F1}%)";
            }

            // 🔹 Кольори
            rankSeries.Palette = ChartColorPalette.BrightPastel;

            // 🔹 Легенда знизу
            chart1.Legends.Clear();
            Legend legend = new Legend("Default");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.Title = "Ранги викладачів";
            legend.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            chart1.Legends.Add(legend);

            // 🔹 Додаємо серію
            chart1.Series.Add(rankSeries);

            // 🔹 Заголовок зверху
            chart1.Titles.Clear();
            var title = new Title("Розподіл викладачів за рангом", Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.Black);
            chart1.Titles.Add(title);
        }

        private void DrawTeacherCategoryPieChart(DataTable table)
        {
            var grouped = table.AsEnumerable()
                .GroupBy(r => r["teacher_rank"].ToString())
                .Select(g => new
                {
                    Rank = string.IsNullOrWhiteSpace(g.Key) ? "Невизначено" : g.Key,
                    Count = g.Select(x => x["teacher_id"]).Distinct().Count()
                })
                .ToList();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("MainArea"));

            // 🔹 Центруємо діаграму
            var area = chart1.ChartAreas["MainArea"];
            area.Position = new ElementPosition(0, 0, 100, 90);
            area.InnerPlotPosition = new ElementPosition(20, 5, 60, 80);

            // 🔹 Створюємо серію
            Series rankSeries = new Series("Ранг викладача");
            rankSeries.ChartType = SeriesChartType.Pie;
            rankSeries.BorderColor = Color.White;
            rankSeries.BorderWidth = 2;
            rankSeries["PieLabelStyle"] = "Disabled"; // без тексту всередині

            double total = grouped.Sum(x => x.Count);

            foreach (var item in grouped)
            {
                int pointIndex = rankSeries.Points.AddXY(item.Rank, item.Count);
                double percent = item.Count / total * 100;
                rankSeries.Points[pointIndex].LegendText = $"{item.Rank} ({item.Count}, {percent:F1}%)";
            }

            // 🔹 Кольори
            rankSeries.Palette = ChartColorPalette.BrightPastel;

            // 🔹 Легенда знизу
            chart1.Legends.Clear();
            Legend legend = new Legend("Default");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.Title = "Ранги викладачів";
            legend.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            chart1.Legends.Add(legend);

            // 🔹 Додаємо серію
            chart1.Series.Add(rankSeries);

            // 🔹 Заголовок зверху
            chart1.Titles.Clear();
            var title = new Title("Розподіл викладачів за рангом", Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.Black);
            chart1.Titles.Add(title);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Form ifrm = new analysis_menu();
            ifrm.Show();
            this.Close();
        }
    }
}
