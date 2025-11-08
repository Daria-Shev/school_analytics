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
            DrawGenderDpaInfluenceChart(table);


        }

        private void DrawGenderDpaInfluenceChart(DataTable all)
        {
            // --- 1. Получаем все уникальные ДПА ---
            var allDpa = all.AsEnumerable()
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

            // --- 2. Определяем полы ---
            var genders = all.AsEnumerable()
                .Select(r => r.Field<string>("student_gender"))
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Distinct()
                .ToList();

            // --- 3. Расчёт процентного распределения ---
            var genderDpaPercents = new List<(string Gender, string Dpa, double Percent)>();

            foreach (var gender in genders)
            {
                // Уникальные ученики выбранного пола
                var studentIds = all.AsEnumerable()
                    .Where(r => r.Field<string>("student_gender") == gender)
                    .Select(r => r.Field<int?>("student_id"))
                    .Distinct()
                    .ToList();

                int totalStudents = studentIds.Count;
                if (totalStudents == 0) continue;

                foreach (var dpa in allDpa)
                {
                    int selectedCount = all.AsEnumerable()
                        .Where(r => r.Field<string>("student_gender") == gender &&
                                   (r.Field<string>("dpa_name_1") == dpa ||
                                    r.Field<string>("dpa_name_2") == dpa ||
                                    r.Field<string>("dpa_name_3") == dpa ||
                                    r.Field<string>("dpa_name_4") == dpa))
                        .Select(r => r.Field<int?>("student_id"))
                        .Distinct()
                        .Count();

                    double percent = (double)selectedCount / totalStudents * 100.0;
                    genderDpaPercents.Add((gender, dpa, percent));
                }
            }

            // --- 4. Настройка chart4 ---
            chart4.Series.Clear();
            chart4.ChartAreas.Clear();
            chart4.ChartAreas.Add(new ChartArea("MainArea"));
            var area = chart4.ChartAreas["MainArea"];
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.Title = "Відсоток від учнів своєї статі (%)";
            area.AxisY.Title = "Предмет ДПА";
            area.AxisX.Interval = 10;

            // --- 5. Создаём две серии: мальчики и девочки ---
            var boysSeries = new Series("Чоловіки")
            {
                ChartType = SeriesChartType.Bar,  // горизонтальные столбцы
                BorderWidth = 1,
                IsValueShownAsLabel = true,
                Color = Color.SkyBlue
            };

            var girlsSeries = new Series("Жінки")
            {
                ChartType = SeriesChartType.Bar,
                BorderWidth = 1,
                IsValueShownAsLabel = true,
                Color = Color.LightPink
            };

            // --- 6. Добавляем данные в серии ---
            foreach (var dpa in allDpa)
            {
                var boyPercent = genderDpaPercents.FirstOrDefault(x => x.Gender.ToLower().StartsWith("ч") && x.Dpa == dpa).Percent;
                var girlPercent = genderDpaPercents.FirstOrDefault(x => x.Gender.ToLower().StartsWith("ж") && x.Dpa == dpa).Percent;

                int bIdx = boysSeries.Points.AddXY(dpa, boyPercent);
                int gIdx = girlsSeries.Points.AddXY(dpa, girlPercent);

                boysSeries.Points[bIdx].Label = boyPercent > 0 ? boyPercent.ToString("F1") + "%" : "";
                girlsSeries.Points[gIdx].Label = girlPercent > 0 ? girlPercent.ToString("F1") + "%" : "";
            }

            chart4.Series.Add(boysSeries);
            chart4.Series.Add(girlsSeries);

            // --- 7. Легенда и заголовок ---
            chart4.Legends.Clear();
            var legend = new Legend("Стать")
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center
            };
            chart4.Legends.Add(legend);

            chart4.Titles.Clear();
            chart4.Titles.Add(new Title(
                "Вибір предметів ДПА за статтю (у % від кількості учнів своєї статі)",
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
    }
}
