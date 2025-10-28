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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace school_analytics
{
    public partial class analysis_1 : Form
    {
        public analysis_1()
        {
            InitializeComponent();
        }


        private DataTable allData;


        private void analysis_1_Load(object sender, EventArgs e)
        {
            diagram_table diagram_table = new diagram_table();
            allData = diagram_table.GetClassStudentGrades();

            // Например: строим диаграмму успеваемости по предметам
            DrawChart(allData);


        }

        private void DrawChart(DataTable table)
        {
            DrawChartByGenderAndYear(table);

        }
        private void DrawChartByGenderAndYear(DataTable table)
        {
            // Группируем по году и полу
            var grouped = table.AsEnumerable()
                .GroupBy(r => new
                {
                    Year = r["class_year"].ToString(),
                    Gender = r["student_gender"].ToString()
                })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Gender = g.Key.Gender,
                    AvgGrade = Math.Round(g.Average(r => Convert.ToDouble(r["grade_value"])), 2) // округляем до 2 знаков
                })
                .ToList();

            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.ChartAreas.Add(new ChartArea("MainArea"));

            // Убираем сетку
            chart3.ChartAreas["MainArea"].AxisX.MajorGrid.Enabled = false;
            chart3.ChartAreas["MainArea"].AxisY.MajorGrid.Enabled = false;

            // Легенда по умолчанию
            chart3.Legends.Clear();
            chart3.Legends.Add(new Legend("Default"));

            // Отдельные серии для хлопців та дівчат
            Series boysSeries = new Series("Хлопці");
            boysSeries.ChartType = SeriesChartType.Column;
            boysSeries.Legend = "Default";
            boysSeries.Points.DataBind(
                grouped.Where(x => x.Gender == "Ч" || x.Gender == "ч"),
                "Year", "AvgGrade", ""
            );
            boysSeries["PointWidth"] = "0.4";

            Series girlsSeries = new Series("Дівчата");
            girlsSeries.ChartType = SeriesChartType.Column;
            girlsSeries.Legend = "Default";
            girlsSeries.Points.DataBind(
                grouped.Where(x => x.Gender == "Ж" || x.Gender == "ж"),
                "Year", "AvgGrade", ""
            );
            girlsSeries["PointWidth"] = "0.4";
            girlsSeries.Color = Color.LightPink; // задаем цвет


            chart3.Series.Add(boysSeries);
            chart3.Series.Add(girlsSeries);

            // Оси
            chart3.ChartAreas["MainArea"].AxisX.Title = "Навчальний рік";
            chart3.ChartAreas["MainArea"].AxisY.Title = "Середній бал";
            chart3.ChartAreas["MainArea"].AxisX.Interval = 1;

            // Подписи над столбцами
            boysSeries.IsValueShownAsLabel = true;
            girlsSeries.IsValueShownAsLabel = true;

            // Чтобы два столбца стояли рядом
            boysSeries["DrawSideBySide"] = "True";
            girlsSeries["DrawSideBySide"] = "True";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilteredData();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilteredData();

        }

        private void UpdateFilteredData()
        {
            if (allData == null || allData.Rows.Count == 0)
                return;

            bool show9 = checkBox9.Checked;
            bool show11 = checkBox11.Checked;

            DataTable filtered = allData.Clone();

            if (show9 && show11)
            {
                // оба выбраны → все данные
                filtered = allData.Copy();
            }
            else if (show9)
            {
                // только 9 классы
                var rows = allData.AsEnumerable()
                    .Where(r => r.Field<string>("class_name").TrimStart().StartsWith("9"));
                if (rows.Any()) filtered = rows.CopyToDataTable();
            }
            else if (show11)
            {
                // только 11 классы
                var rows = allData.AsEnumerable()
                    .Where(r => r.Field<string>("class_name").TrimStart().StartsWith("11"));
                if (rows.Any()) filtered = rows.CopyToDataTable();
            }
            else
            {
                // ничего не выбрано → пустая таблица
                filtered = allData.Clone();
            }

            // теперь передаём в твой метод отрисовки
            DrawChartByGenderAndYear(filtered);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ifrm = new analysis_menu();
            ifrm.Show();
            this.Close();
        }
    }
}
