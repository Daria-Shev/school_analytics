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
            DrawChartByYear(table);
            DrawChartByTeacherAverage(table);
            DrawSubjectDPACountChart(table);
            DrawAverageByCurriculumChart(table);
        }

        private void DrawAverageByCurriculumChart(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                chart2.Series.Clear();
                chart2.Titles.Clear();
                return;
            }

            var grouped = table.AsEnumerable()
                .Where(r => !r.IsNull("class_curriculum") && r["class_curriculum"].ToString() != "")
                .GroupBy(r => r["class_curriculum"].ToString())
                .Select(g => new
                {
                    Curriculum = g.Key,
                    AvgGrade = Math.Round(g.Average(r => Convert.ToDouble(r["grade_value"])), 2)
                })
                .OrderBy(x => x.Curriculum)
                .ToList();

            chart2.Series.Clear();
            chart2.Titles.Clear();
            chart2.ChartAreas.Clear();
            chart2.ChartAreas.Add(new ChartArea("mainarea"));
            var area = chart2.ChartAreas["mainarea"];

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            chart2.Legends.Clear();

            Series series = new Series("середній бал");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.Color = Color.MediumAquamarine; // 🌿 цвет столбцов
            chart2.Series.Add(series);

            foreach (var item in grouped)
                series.Points.AddXY(item.Curriculum, item.AvgGrade);

            area.AxisX.Title = "Навчальна програма";
            area.AxisY.Title = "Середній бал";

            area.AxisY.Minimum = 0;
            area.AxisY.LabelStyle.Format = "0.00";

            area.AxisX.Interval = 1;
            area.AxisY.Interval = 2;

            chart2.Titles.Add(new Title("Середні оцінки по навчальних програмах",
                Docking.Top,
                new Font("segoe ui", 12, FontStyle.Bold),
                Color.Black));
        }



        private void DrawChartByYear(DataTable table)
        {
            // Групуємо тільки по року
            var grouped = table.AsEnumerable()
                .GroupBy(r => r["class_year"].ToString())
                .Select(g => new
                {
                    Year = g.Key,
                    AvgGrade = Math.Round(g.Average(r => Convert.ToDouble(r["grade_value"])), 2)
                })
                .OrderBy(x => x.Year)
                .ToList();

            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.ChartAreas.Add(new ChartArea("MainArea"));

            var area = chart3.ChartAreas["MainArea"];

            // Убираем сетку
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            // Убираем легенду
            chart3.Legends.Clear();

            // Одна серия
            Series series = new Series("Середній бал");
            series.ChartType = SeriesChartType.Column;
            series.Points.DataBind(grouped, "Year", "AvgGrade", "");
            series.IsValueShownAsLabel = true; // Показ значений над столбцами
            series["PointWidth"] = "0.5";

            // Цвет столбцов
            series.Color = Color.MediumAquamarine;

            chart3.Series.Add(series);

            // Осі
            area.AxisX.Title = "Навчальний рік";
            area.AxisY.Title = "Середній бал";

            area.AxisX.Interval = 1;
            area.AxisY.Minimum = 0;
            area.AxisY.LabelStyle.Format = "0.00";

            // Название диаграммы
            chart3.Titles.Clear();
            chart3.Titles.Add(new Title("Середні оцінки по роках", Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold), Color.Black));
        }


        private void DrawChartByTeacherAverage(DataTable table)
        {
            var grouped = table.AsEnumerable()
                .GroupBy(r => r["teacher_short_name"].ToString())
                .Select(g => new
                {
                    Teacher = g.Key,
                    AvgGrade = Math.Round(g.Average(r => Convert.ToDouble(r["grade_value"])), 2)
                })
                .OrderBy(x => x.Teacher)
                .ToList();

            chart4.Series.Clear();
            chart4.ChartAreas.Clear();
            chart4.ChartAreas.Add(new ChartArea("MainArea"));

            var area = chart4.ChartAreas["MainArea"];
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            chart4.Legends.Clear();
            Legend legend = new Legend("Default");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.LegendStyle = LegendStyle.Row;
            chart4.Legends.Add(legend);

            Series series = new Series("Середній бал");
            series.ChartType = SeriesChartType.Bar; // <-- горизонтальные столбцы
            series.Legend = "Default";
            series.Points.DataBind(grouped, "Teacher", "AvgGrade", "");
            series.IsValueShownAsLabel = true;
            series["PointWidth"] = "0.6";
            series.Color = Color.MediumAquamarine; // <-- добавлен цвет

            chart4.Series.Add(series);

            area.AxisY.Title = "Середній бал";
            //area.AxisX.Title = "Вчителі"; // Баллы снизу

            area.AxisX.Minimum = 0;
            area.AxisX.LabelStyle.Format = "0.00";
            area.AxisY.Interval = 1;

            chart4.Titles.Clear();
            chart4.Titles.Add(new Title(
                "Середні оцінки по вчителям",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black
            ));
        }
        private void DrawSubjectDPACountChart(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();
                return;
            }

            var filtered = table.AsEnumerable()
                .Where(r => !r.IsNull("subject_dpa") && r["subject_dpa"].ToString() != "");

            if (!filtered.Any())
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();
                return;
            }

            var grouped = filtered
                .GroupBy(r => new
                {
                    Subject = r["subject_full_name"].ToString(),
                    Teacher = r["teacher_short_name"].ToString()
                })
                .Select(g => new
                {
                    Teacher = g.Key.Teacher,
                    Count = g.Count(r =>
                        r["subject_dpa"].ToString() == r["dpa_name_1"].ToString() ||
                        r["subject_dpa"].ToString() == r["dpa_name_2"].ToString() ||
                        r["subject_dpa"].ToString() == r["dpa_name_3"].ToString() ||
                        r["subject_dpa"].ToString() == r["dpa_name_4"].ToString()
                    )
                })
                .Where(x => x.Count > 0)
                .OrderByDescending(x => x.Count)
                .ToList();

            chart1.Series.Clear();
            chart1.Titles.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("MainArea"));
            var area = chart1.ChartAreas["MainArea"];

            // --- оформление осей ---
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            // --- легенду убираем ---
            chart1.Legends.Clear();

            // --- создаём серию ---
            Series series = new Series("Кількість учнів");
            series.ChartType = SeriesChartType.Bar; // горизонтальные столбцы
            series.IsValueShownAsLabel = true;
            series.Color = Color.MediumAquamarine; // <-- добавлен цвет
            chart1.Series.Add(series);

            // --- добавляем данные ---
            foreach (var item in grouped)
                series.Points.AddXY(item.Teacher, item.Count);

            // --- подписи и оформление ---
            area.AxisY.Title = "Кількість учнів";

            int maxValue = grouped.Max(x => x.Count);
            if (maxValue <= 10)
                area.AxisY.Interval = 1;
            else if (maxValue <= 30)
                area.AxisY.Interval = 2;
            else if (maxValue <= 60)
                area.AxisY.Interval = 5;
            else
                area.AxisY.Interval = 10;

            area.AxisX.Minimum = 0;
            area.AxisX.LabelStyle.Format = "0";

            chart1.Titles.Add(new Title(
                "Кількість учнів, що обрали ДПА з цього предмету",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black
            ));
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

            // ---------------- Фильтр по году ----------------
            string selectedYear = comboBox1.SelectedItem.ToString();
            if (selectedYear != "Всі роки")
            {
                int year = int.Parse(selectedYear);
                var rows = filtered.AsEnumerable()
                    .Where(r => r.Field<int>("class_year") == year);

                filtered = rows.Any() ? rows.CopyToDataTable() : filtered.Clone();
            }

            // ---------------- Фильтр по предмету ----------------
            int subjectId = Convert.ToInt32(comboBox2.SelectedValue);
            if (subjectId != 0) // 0 = "Всі предмети"
            {
                var rows = filtered.AsEnumerable()
                    .Where(r => r.Field<int>("subject_id") == subjectId);

                filtered = rows.Any() ? rows.CopyToDataTable() : filtered.Clone();
            }

            // ---------------- Фильтр по классу (9/11) ----------------
            bool show9 = checkBox9.Checked;
            bool show11 = checkBox11.Checked;

            if (show9 == false && show11 == false)
            {
                // Если оба чекбокса сняты → показываем пустую таблицу
                filtered = filtered.Clone();
            }
            else if (show9 && !show11)
            {
                // Только 9 класи
                var rows = filtered.AsEnumerable()
                    .Where(r => r.Field<string>("class_name").TrimStart().StartsWith("9"));

                filtered = rows.Any() ? rows.CopyToDataTable() : filtered.Clone();
            }
            else if (!show9 && show11)
            {
                // Только 11 класи
                var rows = filtered.AsEnumerable()
                    .Where(r => r.Field<string>("class_name").TrimStart().StartsWith("11"));

                filtered = rows.Any() ? rows.CopyToDataTable() : filtered.Clone();
            }
            // если show9 && show11 — ничего не делаем, остаётся всё

            // ✅ Строим диаграмму (даже если она пустая — она просто очистится)
            DrawChart(filtered);
        }


        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }
    }
}
