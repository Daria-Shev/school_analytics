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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace school_analytics
{
    public partial class analysis_dpa : Form
    {
        public analysis_dpa()
        {
            InitializeComponent();
        }
        private DataTable allData;


        private void analysis_dpa_Load(object sender, EventArgs e)
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

            DrawChart(allData);
        }

        private void DrawChart(DataTable table)
        {
            DrawDPASelectionPieChart(table);
            DrawDPASelectionPieChart2(table);
            DrawDpaGradeInfluenceChart(table);
            DrawDpaGenderStackedBarChart(table);


        }
        private void DrawDpaGenderStackedBarChart(DataTable all)
        {
            // --- Подсчёт количества учеников по полу для каждого ДПА ---
            var dpaGenderCounts = all.AsEnumerable()
                .SelectMany(r => new[]
                {
            new { Dpa = r.Field<string>("dpa_name_1"), Gender = r.Field<string>("student_gender") },
            new { Dpa = r.Field<string>("dpa_name_2"), Gender = r.Field<string>("student_gender") },
            new { Dpa = r.Field<string>("dpa_name_3"), Gender = r.Field<string>("student_gender") },
            new { Dpa = r.Field<string>("dpa_name_4"), Gender = r.Field<string>("student_gender") }
                })
                .Where(x => !string.IsNullOrWhiteSpace(x.Dpa))
                .GroupBy(x => x.Dpa)
                .Select(g =>
                {
                    int male = g.Count(x => x.Gender != null &&
                                            (x.Gender.ToLower().StartsWith("ч") || x.Gender.ToLower().StartsWith("m")));
                    int female = g.Count(x => x.Gender != null &&
                                              (x.Gender.ToLower().StartsWith("ж") || x.Gender.ToLower().StartsWith("f")));
                    int total = male + female;
                    double malePercent = total > 0 ? (male * 100.0 / total) : 0;
                    double femalePercent = total > 0 ? (female * 100.0 / total) : 0;
                    return new
                    {
                        Dpa = g.Key,
                        MalePercent = malePercent,
                        FemalePercent = femalePercent
                    };
                })
                .OrderByDescending(x => x.MalePercent + x.FemalePercent)
                .ToList();

            // ---------- ДИЗАЙН ----------
            chart4.Series.Clear();
            chart4.ChartAreas.Clear();
            chart4.Legends.Clear();
            chart4.Titles.Clear();

            chart4.ChartAreas.Add(new ChartArea("MainArea"));
            var area = chart4.ChartAreas["MainArea"];

            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9);

            area.AxisY.Title = "% учнів";
            area.AxisY.Maximum = 100; // проценты
            area.AxisY.Interval = 20; // шкала 0–20–40–60–80–100

            // Серии — тот же стиль
            Series maleSeries = new Series("Хлопці")
            {
                ChartType = SeriesChartType.StackedBar,
                Color = Color.SkyBlue,
                BorderWidth = 1
            };

            Series femaleSeries = new Series("Дівчата")
            {
                ChartType = SeriesChartType.StackedBar,
                Color = Color.LightPink,
                BorderWidth = 1
            };

            // Добавляем данные (лучший сверху)
            for (int i = dpaGenderCounts.Count - 1; i >= 0; i--)
            {
                var d = dpaGenderCounts[i];
                maleSeries.Points.AddXY(d.Dpa, d.MalePercent);
                femaleSeries.Points.AddXY(d.Dpa, d.FemalePercent);
            }

            chart4.Series.Add(maleSeries);
            chart4.Series.Add(femaleSeries);

            // Легенда и заголовок
            chart4.Legends.Add(new Legend("Default")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 9),
                BackColor = Color.White
            });

            chart4.Titles.Add(new Title(
                "Розподіл вибору ДПА за статтю у відсотках",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black
            ));
        }

        private void DrawDpaGradeInfluenceChart(DataTable all)
        {
            string[] mathSubjects = { "Математика", "Алгебра", "Геометрія" };

            var dpaSelections = all.AsEnumerable()
                .SelectMany(r => new[]
                {
            r.Field<string>("dpa_name_1"),
            r.Field<string>("dpa_name_2"),
            r.Field<string>("dpa_name_3"),
            r.Field<string>("dpa_name_4")
                })
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct()
                .ToList();

            var results = dpaSelections
                .Select(dpa => {
                    var grades = all.AsEnumerable()
                        .Where(r => r.Field<string>("dpa_name_1") == dpa
                                 || r.Field<string>("dpa_name_2") == dpa
                                 || r.Field<string>("dpa_name_3") == dpa
                                 || r.Field<string>("dpa_name_4") == dpa)
                        .Select(r => new
                        {
                            Subject = mathSubjects.Contains(r.Field<string>("subject_full_name"))
                                      ? "Математика"
                                      : r.Field<string>("subject_full_name"),
                            Grade = Convert.ToDouble(r["grade_value"])
                        })
                        .GroupBy(x => x.Subject)
                        .Select(g => g.Average(x => x.Grade))
                        .ToList();

                    return new
                    {
                        DPA = dpa,
                        AvgGrade = grades.DefaultIfEmpty(0).Average()
                    };
                })
                .OrderByDescending(x => x.AvgGrade)
                .ToList();

            // ---------- ДИЗАЙН ----------
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.ChartAreas.Add(new ChartArea("MainArea"));
            var area = chart2.ChartAreas["MainArea"];
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            Series series = new Series("Середній бал")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.MediumAquamarine,       // как в chart3
                BorderWidth = 1,
                Legend = "Default"
            };

            // Заполняем (чтобы лучший был сверху)
            for (int i = results.Count - 1; i >= 0; i--)
            {
                int idx = series.Points.AddXY(results[i].DPA, results[i].AvgGrade);
                series.Points[idx].Label = results[i].AvgGrade.ToString("F2"); // подпись значений
            }

            chart2.Series.Add(series);

            chart2.Legends.Clear();

            chart2.Titles.Clear();
            chart2.Titles.Add(
                new Title("Вплив середніх оцінок на вибір ДПА", Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black)
            );
        }


        private void DrawDPASelectionPieChart(DataTable table)
        {
            // ✅ Фильтр: тільки класи "Інтелект України"
            var filtered = table.AsEnumerable()
                .Where(r => r.Field<string>("class_curriculum")?.Trim() == "Інтелект України")
                .ToList();

            // ✅ Собираем все DPA выбора ученика
            var dpaList = filtered
                .SelectMany(r => new[]
                {
            r.Field<string>("dpa_name_1"),
            r.Field<string>("dpa_name_2"),
            r.Field<string>("dpa_name_3"),
            r.Field<string>("dpa_name_4")
                })
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            // ✅ Группируем и считаем
            var grouped = dpaList
                .GroupBy(x => x)
                .Select(g => new { DPA = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            // --- СТИЛЬ КАК У chart2 ---

            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.ChartAreas.Add(new ChartArea("MainArea"));

            var area = chart3.ChartAreas["MainArea"];
            area.Position = new ElementPosition(0, 0, 100, 100);

            // 🔹 Масштабування й зміщення круга (як у chart2)
            double scale = 1;
            double originalWidth = 45;
            double originalHeight = 80;
            double newWidth = originalWidth * scale;
            double newHeight = originalHeight * scale;
            double dy = (originalHeight - newHeight) / 2;
            double dx = 5;

            area.InnerPlotPosition = new ElementPosition(
                (float)dx,
                (float)(10 + dy),
                (float)newWidth,
                (float)newHeight
            );

            Series series = new Series("Вибір ДПА");
            series.ChartType = SeriesChartType.Pie;
            series.BorderColor = Color.White;
            series.BorderWidth = 2;
            series["PieLabelStyle"] = "Disabled";

            double total = grouped.Sum(x => x.Count);

            foreach (var item in grouped)
            {
                int p = series.Points.AddXY(item.DPA, item.Count);
                double percent = item.Count / total * 100;
                series.Points[p].LegendText = $"{item.DPA} — {item.Count} ({percent:F1}%)";
            }

            series.Palette = ChartColorPalette.BrightPastel;

            chart3.Legends.Clear();
            Legend legend = new Legend("RightList")
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center,
                Title = "Предмети ДПА",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                IsTextAutoFit = false,
                TableStyle = LegendTableStyle.Tall,
                TextWrapThreshold = 10,
                BackColor = Color.Transparent
            };
            chart3.Legends.Add(legend);

            chart3.Series.Add(series);

            chart3.Titles.Clear();
            chart3.Titles.Add(new Title("Вибір ДПА - «Інтелект України»",
                Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.Black));
        }
        private void DrawDPASelectionPieChart2(DataTable table)
        {
            // ✅ Фильтр
            var filtered = table.AsEnumerable()
                .Where(r => r.Field<string>("class_curriculum")?.Trim() == "Типова освітня програма")
                .ToList();

            // ✅ Собираем все DPA выбора ученика
            var dpaList = filtered
                .SelectMany(r => new[]
                {
            r.Field<string>("dpa_name_1"),
            r.Field<string>("dpa_name_2"),
            r.Field<string>("dpa_name_3"),
            r.Field<string>("dpa_name_4")
                })
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim())
                .ToList();

            // ✅ Группируем и считаем
            var grouped = dpaList
                .GroupBy(x => x)
                .Select(g => new { DPA = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            // --- СТИЛЬ КАК У chart2 ---

            chart5.Series.Clear();
            chart5.ChartAreas.Clear();
            chart5.ChartAreas.Add(new ChartArea("MainArea"));

            var area = chart5.ChartAreas["MainArea"];
            area.Position = new ElementPosition(0, 0, 100, 100);

            // 🔹 Масштабування й зміщення круга (як у chart2)
            double scale = 1;
            double originalWidth = 45;
            double originalHeight = 80;
            double newWidth = originalWidth * scale;
            double newHeight = originalHeight * scale;
            double dy = (originalHeight - newHeight) / 2;
            double dx = 5;

            area.InnerPlotPosition = new ElementPosition(
                (float)dx,
                (float)(10 + dy),
                (float)newWidth,
                (float)newHeight
            );

            Series series = new Series("Вибір ДПА");
            series.ChartType = SeriesChartType.Pie;
            series.BorderColor = Color.White;
            series.BorderWidth = 2;
            series["PieLabelStyle"] = "Disabled";

            double total = grouped.Sum(x => x.Count);

            foreach (var item in grouped)
            {
                int p = series.Points.AddXY(item.DPA, item.Count);
                double percent = item.Count / total * 100;
                series.Points[p].LegendText = $"{item.DPA} — {item.Count} ({percent:F1}%)";
            }

            series.Palette = ChartColorPalette.BrightPastel;

            chart5.Legends.Clear();
            Legend legend = new Legend("RightList")
            {
                Docking = Docking.Right,
                Alignment = StringAlignment.Center,
                Title = "Предмети ДПА",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                IsTextAutoFit = false,
                TableStyle = LegendTableStyle.Tall,
                TextWrapThreshold = 10,
                BackColor = Color.Transparent
            };
            chart5.Legends.Add(legend);

            chart5.Series.Add(series);

            chart5.Titles.Clear();
            chart5.Titles.Add(new Title("Вибір ДПА - «Типова освітня програма»",
                Docking.Top, new Font("Segoe UI", 12, FontStyle.Bold), Color.Black));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ifrm = new analysis_menu();
            ifrm.Show();
            this.Close();
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
            DrawChart(filtered);
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
    }
}
