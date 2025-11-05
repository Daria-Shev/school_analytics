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
using static school_analytics.BD_subject;
using static school_analytics.BD_teacher;

namespace school_analytics
{
    public partial class analysis_subject : Form
    {
        public analysis_subject()
        {
            InitializeComponent();
        }
        private DataTable allData;

        private void analysis_subject_Load(object sender, EventArgs e)
        {

            diagram_table diagram_table = new diagram_table();
            allData = diagram_table.GetSubjectDPAGrades();

            // Например: строим диаграмму успеваемости по предметам
            
            var years = allData.AsEnumerable()
            .Select(r => r.Field<int>("class_year").ToString()) // ✅ сразу преобразуем в string
            .Distinct()
            .OrderBy(y => y)
            .ToList();
            years.Insert(0, "Всі роки");
            comboBox1.DataSource = years;
            comboBox1.SelectedIndex = 0;

            //var subject = allData.AsEnumerable()
            //    .Select(r => r.Field<string>("subject_full_name"))
            //    //.Where(s => !string.IsNullOrWhiteSpace(s))   // для надійності
            //    .Distinct()
            //    .OrderBy(y => y)
            //    .ToList();

            //subject.Insert(0, "Всі предмети");
            //comboBox2.DataSource = subject;
            //comboBox2.SelectedIndex = 0;
            BD_subject bdSubject = new BD_subject();

            var subjects = bdSubject.subject_list();

            // Добавляем пункт "всі" вручную
            subjects.Insert(0, new subjectData
            {
                subject_id = 0,
                subject_full_name = "Всі предмети"
            });

            comboBox2.DisplayMember = "subject_full_name";
            comboBox2.ValueMember = "subject_id";
            comboBox2.DataSource = subjects;

            comboBox2.SelectedIndex = 0;


            DrawChart(allData);


        }

        private void DrawChart(DataTable table)
        {
            DrawChartByGenderAndYear(table);

        }
        private void DrawChartByGenderAndYear(DataTable table)
        {
            // 🔹 Групуємо по року та статі
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
                    AvgGrade = Math.Round(g.Average(r => Convert.ToDouble(r["grade_value"])), 2)
                })
                .OrderBy(x => x.Year)
                .ToList();

            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.ChartAreas.Add(new ChartArea("MainArea"));

            // 🔹 Убираем сетку
            var area = chart3.ChartAreas["MainArea"];
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            // 🔹 Легенда
            chart3.Legends.Clear();
            //chart3.Legends.Add(new Legend("Default"));
            Legend legend = new Legend("Default");
            legend.Docking = Docking.Bottom;            // Легенда внизу
            legend.Alignment = StringAlignment.Center;  // По центру
            legend.LegendStyle = LegendStyle.Row;       // Горизонтально в один ряд
            chart3.Legends.Add(legend);

            // 🔹 Серія хлопців
            Series boysSeries = new Series("Хлопці");
            boysSeries.ChartType = SeriesChartType.Column;
            boysSeries.Legend = "Default";
            boysSeries.Points.DataBind(
                grouped.Where(x => x.Gender == "Ч" || x.Gender == "ч"),
                "Year", "AvgGrade", ""
            );
            boysSeries["PointWidth"] = "0.4";
            boysSeries.Color = Color.SkyBlue;

            // 🔹 Серія дівчат
            Series girlsSeries = new Series("Дівчата");
            girlsSeries.ChartType = SeriesChartType.Column;
            girlsSeries.Legend = "Default";
            girlsSeries.Points.DataBind(
                grouped.Where(x => x.Gender == "Ж" || x.Gender == "ж"),
                "Year", "AvgGrade", ""
            );
            girlsSeries["PointWidth"] = "0.4";
            girlsSeries.Color = Color.LightPink;

            chart3.Series.Add(boysSeries);
            chart3.Series.Add(girlsSeries);

            // 🔹 Оформлення осей
            area.AxisX.Title = "Навчальний рік";
            area.AxisY.Title = "Середній бал";
            area.AxisX.Interval = 1;

            // 🔹 Підписи над стовпчиками
            boysSeries.IsValueShownAsLabel = true;
            girlsSeries.IsValueShownAsLabel = true;

            // 🔹 Щоб стовпці стояли поруч
            boysSeries["DrawSideBySide"] = "True";
            girlsSeries["DrawSideBySide"] = "True";

            // 🔹 Красиве оформлення
            area.AxisY.Minimum = 0;
            area.AxisY.LabelStyle.Format = "0.00"; // формат з двома знаками після коми
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ifrm = new analysis_menu();
            ifrm.Show();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (allData == null || allData.Rows.Count == 0)
                return;

            DataTable filtered = allData.Copy();

            // Фильтр по году
            string selectedYear = comboBox1.SelectedItem.ToString();
            if (selectedYear != "Всі роки")
            {
                int year = int.Parse(selectedYear);
                filtered = filtered.AsEnumerable()
                    .Where(r => r.Field<int>("class_year") == year)
                    .CopyToDataTable();
            }

            // Фильтр по предмету
            int subjectId = Convert.ToInt32(comboBox2.SelectedValue);
            if (subjectId != 0) // 0 = "Всі предмети"
            {
                filtered = filtered.AsEnumerable()
                    .Where(r => r.Field<int>("subject_id") == subjectId)
                    .CopyToDataTable();
            }

            // ✅ Рисуем диаграмму
            DrawChart(filtered);
        }



    }
}
