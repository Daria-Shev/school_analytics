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
using static school_analytics.BD_teacher;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace school_analytics
{
    public partial class analysis_teacher : Form
    {
        public analysis_teacher()
        {
            InitializeComponent();
        }
        private DataTable allData;
        private DataTable teacherData;

        private void analysis_teacher_Load(object sender, EventArgs e)
        {

            diagram_table diagram_table = new diagram_table();
            allData = diagram_table.GetTeacherGrades();
            teacherData = diagram_table.GetTeachersOnly();
            var years = allData.AsEnumerable()
    .Select(r => r.Field<int>("class_year").ToString()) // ✅ сразу преобразуем в string
       .Distinct()
       .OrderBy(y => y)
       .ToList();


            // 🔹 (по желанию) добавляем "Все года"
            years.Insert(0, "Всі роки");

            comboBox1.DataSource = years;
            comboBox1.SelectedIndex = 0;
            // Например: строим диаграмму успеваемости по предметам
            DrawChart(allData, teacherData);
        }
        private void DrawChart(DataTable table, DataTable t_table)
        {
            DrawTeacherRankPieChart(t_table);
            DrawTeacherCategoryPieChart(t_table);
            DrawTopTeachersBarChart(table);
            DrawTeacherExperienceHistogram(t_table);
            DrawClassTeacherAverageChart(table);
        }

        private void DrawTeacherRankPieChart(DataTable table)
        {
            // 🔹 Власний порядок назв рангів
            string[] customOrder = { "вчитель-методист", "старший вчитель", "нема" };

            // 🔹 Групування з врахуванням пустих значень та сортування за customOrder
            //var grouped = table.AsEnumerable()
            //    .GroupBy(r => r["teacher_rank"].ToString())
            //    .Select(g => new
            //    {
            //        Rank = string.IsNullOrWhiteSpace(g.Key) ? "нема" : g.Key.Trim(),
            //        Count = g.Select(x => x["teacher_id"]).Distinct().Count()
            //    })
            //    .OrderBy(g => Array.IndexOf(customOrder, g.Rank))
            //    .ToList();
            var grouped = table.AsEnumerable()
    .GroupBy(r => r.Field<string>("teacher_rank") ?? "")
    .Select(g => new
    {
        Rank = string.IsNullOrWhiteSpace(g.Key) ? "нема" : g.Key.Trim(),
        Count = g.Count() // 🔹 замість Distinct.Count()
    })
    .OrderBy(g => Array.IndexOf(customOrder, g.Rank))
    .ToList();
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("MainArea"));

            var area = chart1.ChartAreas["MainArea"];
            area.Position = new ElementPosition(0, 0, 100, 90);
            area.InnerPlotPosition = new ElementPosition(20, 5, 60, 80);


            Series rankSeries = new Series("Звання вчителя");
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

            rankSeries.Palette = ChartColorPalette.BrightPastel;

            chart1.Legends.Clear();
            Legend legend = new Legend("Default")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                Title = "Звання вчителя",
                Font = new Font("Segoe UI", 9, FontStyle.Regular)
            };
            chart1.Legends.Add(legend);

            chart1.Series.Add(rankSeries);

            chart1.Titles.Clear();
            chart1.Titles.Add(new Title("Розподіл вчителів за званням", Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.Black));
        }


       
        private void DrawTeacherCategoryPieChart(DataTable table)
        {
            // 🔹 Власний порядок категорій
            string[] customOrder = { "вчитель вищої категорії", "вчитель І категорії", "вчитель ІІ категорії", "спеціаліст", "невизначено" };

            var grouped = table.AsEnumerable()
                .GroupBy(r => r.Field<string>("teacher_category") ?? "")
                .Select(g => new
                {
                    Category = string.IsNullOrWhiteSpace(g.Key) ? "невизначено" : g.Key.Trim(),
                    Count = g.Count() // 🔹 замість Distinct.Count()
                })
                .OrderBy(g => Array.IndexOf(customOrder, g.Category))
                .ToList();


            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.ChartAreas.Add(new ChartArea("MainArea"));

            var area = chart2.ChartAreas["MainArea"];
            area.Position = new ElementPosition(0, 0, 100, 100);

            // 🔹 Зменшення круга на 10% та зсув ліворуч для легенди справа
            double scale = 1;
            double originalWidth = 45;
            double originalHeight = 80;
            double newWidth = originalWidth * scale;
            double newHeight = originalHeight * scale;
            double dy = (originalHeight - newHeight) / 2;
            double dx = 5; // зсув ліворуч
            area.InnerPlotPosition = new ElementPosition(
                (float)dx,
                (float)(10 + dy),
                (float)newWidth,
                (float)newHeight
            );

            Series catSeries = new Series("Категорія вчителя");
            catSeries.ChartType = SeriesChartType.Pie;
            catSeries.BorderColor = Color.White;
            catSeries.BorderWidth = 2;
            catSeries["PieLabelStyle"] = "Disabled";

            double total = grouped.Sum(x => x.Count);

            foreach (var item in grouped)
            {
                int pointIndex = catSeries.Points.AddXY(item.Category, item.Count);
                double percent = item.Count / total * 100;
                catSeries.Points[pointIndex].LegendText = $"{item.Category} — {item.Count} ({percent:F1}%)";
            }

            catSeries.Palette = ChartColorPalette.BrightPastel;

            chart2.Legends.Clear();
            Legend legend = new Legend("RightList")
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center,
                Title = "Категорії вчителів",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                IsTextAutoFit = false,
                TableStyle = LegendTableStyle.Tall,
                InterlacedRows = false,
                TextWrapThreshold = 10,
                BackColor = Color.Transparent
            };

            chart2.Legends.Add(legend);

            chart2.Series.Add(catSeries);

            chart2.Titles.Clear();
            chart2.Titles.Add(new Title("Розподіл вчителів за категорією", Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.Black));
        }

        void DrawTopTeachersBarChart(DataTable table)
        {
            var topTeachers = table.AsEnumerable()
                .GroupBy(r => new
                {
                    TeacherId = r.Field<int>("teacher_id"),
                    TeacherName = r.Field<string>("teacher_short_name")
                })
                .Select(g => new
                {
                    g.Key.TeacherId,
                    g.Key.TeacherName,
                    AverageGrade = g
                        .Where(x => x["grade_value"] != DBNull.Value)
                        .Select(x => Convert.ToDouble(x["grade_value"]))
                        .DefaultIfEmpty(0)
                        .Average()
                })
                .OrderByDescending(x => x.AverageGrade) // сортировка по убыванию — чем выше оценка, тем выше в списке
                .Take(5)
                .ToList();

            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.ChartAreas.Add(new ChartArea("MainArea"));

            Series barSeries = new Series("Середній бал")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.MediumAquamarine,
                BorderWidth = 1,
                Legend = "Default"
            };

            // Заполняем столбцы (чтобы самый лучший был сверху)
            for (int i = topTeachers.Count - 1; i >= 0; i--)
            {
                int idx = barSeries.Points.AddXY(topTeachers[i].TeacherName, topTeachers[i].AverageGrade);
                barSeries.Points[idx].Label = topTeachers[i].AverageGrade.ToString("F2"); // 2 знака после запятой
            }

            chart3.Legends.Clear();
            chart3.Legends.Add(new Legend("Default")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 9)
            });

            chart3.Series.Add(barSeries);

            chart3.Titles.Clear();
            chart3.Titles.Add(
                new Title("Топ-5 вчителів за оцінками", Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black)
            );
        }


        private void DrawTeacherExperienceHistogram(DataTable table)
        {
            // 🔹 Групуємо стаж по діапазонах
            var grouped = table.AsEnumerable()
                .Select(r => r.Field<int?>("teacher_experience") ?? 0)
                .GroupBy(exp =>
                {
                    if (exp <= 10) return "0–10";
                    else if (exp <= 20) return "11–20";
                    else if (exp <= 30) return "21–30";
                    else if (exp <= 40) return "31–40";
                    else return "41+";
                })
                .Select(g => new { Range = g.Key, Count = g.Count() })
                .OrderBy(g => g.Range)
                .ToList();

            chart4.Series.Clear();
            chart4.ChartAreas.Clear();
            chart4.ChartAreas.Add(new ChartArea("MainArea"));

            var series = new Series("Кількість вчителів")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.MediumAquamarine,
                BorderWidth = 2,
                Legend = "Default",
                IsValueShownAsLabel = true
            };

            foreach (var g in grouped)
            {
                int idx = series.Points.AddXY(g.Range, g.Count);
                series.Points[idx].Label = g.Count.ToString();
            }

            // 🔹 Легенда
            chart4.Legends.Clear();
            chart4.Legends.Add(new Legend("Default")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                //Title = "Розподіл викладачів за стажем",
                Font = new Font("Segoe UI", 9)
            });

            // 🔹 Налаштування осей
            var area = chart4.ChartAreas["MainArea"];
            area.AxisX.Title = "Стаж";
            area.AxisY.Title = "Кількість вчителів";
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

            chart4.Series.Add(series);

            // 🔹 Заголовок
            chart4.Titles.Clear();
            chart4.Titles.Add(new Title(
                "Розподіл вчителів за стажем роботи",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black
            ));
        }


        private void DrawClassTeacherAverageChart(DataTable table)
        {
            if (table == null || table.Rows.Count == 0)
            {
                chart5.Series.Clear();
                chart5.Titles.Clear();
                return;
            }

            // 1) Средний балл внутри каждого класса
            var classAverages = table.AsEnumerable()
                .Where(r => r["grade_value"] != DBNull.Value)
                .GroupBy(r => new
                {
                    ClassId = r.Field<int>("class_id"),
                    TeacherId = r.Field<int>("class_teacher_id"),
                    TeacherName = r.Field<string>("class_teacher_name")
                })
                .Select(g => new
                {
                    g.Key.TeacherId,
                    g.Key.TeacherName,
                    ClassAverage = g.Average(x => Convert.ToDouble(x["grade_value"]))
                })
                .ToList();

            if (!classAverages.Any())
            {
                chart5.Series.Clear();
                chart5.Titles.Clear();
                return;
            }

            // 2) Средний по классным руководителям → округление → сортировка → TOP-5
            var topClassTeachers = classAverages
                .GroupBy(x => new { x.TeacherId, x.TeacherName })
                .Select(g => new
                {
                    TeacherName = g.Key.TeacherName,
                    FinalAverage = Math.Round(g.Average(x => x.ClassAverage), 2)
                })
                .OrderByDescending(x => x.FinalAverage) // чем выше оценка, тем выше
                .Take(5)
                .ToList();

            // 3) Оформление графика — как chart3
            chart5.Series.Clear();
            chart5.ChartAreas.Clear();
            chart5.Legends.Clear();
            chart5.Titles.Clear();

            chart5.ChartAreas.Add(new ChartArea("MainArea"));
            Series barSeries = new Series("Середній бал")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.MediumAquamarine,
                BorderWidth = 1,
                Legend = "Default"
            };

            // Добавляем так, чтобы лучший был сверху
            for (int i = topClassTeachers.Count - 1; i >= 0; i--)
            {
                int idx = barSeries.Points.AddXY(topClassTeachers[i].TeacherName, topClassTeachers[i].FinalAverage);
                barSeries.Points[idx].Label = topClassTeachers[i].FinalAverage.ToString("F2");
            }

            chart5.Series.Add(barSeries);

            // Заголовок как в chart3
            chart5.Titles.Add(
                new Title("Топ-5 класних керівників за середнім балом класу",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black)
            );
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

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (allData == null || allData.Rows.Count == 0)
                return;

            DataTable filtered = allData;

            // ---- Фильтр по году (комбо) ----
            string selected = comboBox1.SelectedItem.ToString();

            if (selected != "Всі роки")
            {
                int year = int.Parse(selected);

                var rowsByYear = filtered.AsEnumerable()
                    .Where(r => r.Field<int>("class_year") == year);

                if (rowsByYear.Any())
                    filtered = rowsByYear.CopyToDataTable();
                else
                    filtered = filtered.Clone();
            }

            // ---- Фильтр по классу (чексбоксы 9 / 11) ----
            bool show9 = checkBox9.Checked;
            bool show11 = checkBox11.Checked;

            if (show9 && !show11)
            {
                var rows = filtered.AsEnumerable()
                    .Where(r => r.Field<string>("class_name").TrimStart().StartsWith("9"));

                filtered = rows.Any() ? rows.CopyToDataTable() : filtered.Clone();
            }
            else if (!show9 && show11)
            {
                var rows = filtered.AsEnumerable()
                    .Where(r => r.Field<string>("class_name").TrimStart().StartsWith("11"));

                filtered = rows.Any() ? rows.CopyToDataTable() : filtered.Clone();
            }
            else if (!show9 && !show11)
            {
                // ничего не выбрано — пусто
                filtered = filtered.Clone();
            }
            // если show9 && show11 → оставляем как есть (все классы)

            // ---- Отрисовываем график ----
            DrawTopTeachersBarChart(filtered);
            DrawClassTeacherAverageChart(filtered);
        }


    }
}
