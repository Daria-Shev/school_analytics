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
            allData = diagram_table.GetGrades();


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
            DrawChart(allData);


        }

        private void DrawChart(DataTable table)
        {
            //DrawChartByGenderAndYear(table);
            DrawAverageGradeByRankChart(table);
            DrawAverageGradeByCategoryChart(table);
            DrawAverageGradeByYearChart(table);
            DrawAverageGradeByExperienceChart(table);
            DrawAverageByGenderChart(table);
            DrawAverageByCurriculumChart(table);

        }

        private void DrawAverageByCurriculumChart(DataTable table)
        {
            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.Titles.Clear();
            chart3.Legends.Clear();  // убираем легенды

            chart3.Titles.Add(new Title(
                "Середній бал за навчальною програмою",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black));

            var area = new ChartArea("MainArea");
            chart3.ChartAreas.Add(area);

            area.AxisX.Title = "Навчальна програма";
            area.AxisY.Title = "Середній бал";

            // Убираем все сетки
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            area.AxisY.Minimum = 0;

            // Группируем по учебной программе
            var avgByCurriculum = table.AsEnumerable()
                .Where(r => r["grade_value"] != DBNull.Value)
                .GroupBy(r => r.Field<string>("class_curriculum") ?? "Невідомо")
                .Select(g => new
                {
                    Curriculum = g.Key,
                    AvgGrade = g.Average(r => Convert.ToDouble(r["grade_value"]))
                })
                .OrderBy(x => x.Curriculum)  // сортировка по имени программы (опционально)
                .ToList();

            // Создаем серию
            Series series = new Series()
            {
                ChartType = SeriesChartType.Column,
                Color = Color.MediumAquamarine,
                BorderWidth = 2,
                IsValueShownAsLabel = true
            };

            chart3.Series.Add(series);

            // Добавляем точки
            foreach (var item in avgByCurriculum)
            {
                series.Points.AddXY(item.Curriculum, Math.Round(item.AvgGrade, 2));
            }
        }

        private void DrawAverageByGenderChart(DataTable table)
        {
            chart6.Series.Clear();
            chart6.ChartAreas.Clear();
            chart6.Titles.Clear();
            chart6.Legends.Clear();  // убираем все легенды

            chart6.Titles.Add(new Title(
                "Середня успішність за статтю",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black));

            var area = new ChartArea("MainArea");
            chart6.ChartAreas.Add(area);

            area.AxisX.Title = "Стать";
            area.AxisY.Title = "Середній бал";

            // Убираем все сетки
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            area.AxisY.Minimum = 0;

            // Обчислення середнього бала за статтю
            var avgByGender = table.AsEnumerable()
                .Where(r => r["grade_value"] != DBNull.Value)
                .GroupBy(r => r.Field<string>("student_gender"))
                .Select(g => new
                {
                    GenderKey = g.Key,
                    Gender = g.Key == "ч" || g.Key == "Ч" ? "Хлопці" :
                             g.Key == "ж" || g.Key == "Ж" ? "Дівчата" :
                             "Невідомо",
                    AvgGrade = g.Average(x => Convert.ToDouble(x["grade_value"]))
                })
                .ToList();

            // Серія
            Series series = new Series()
            {
                ChartType = SeriesChartType.Column,
                BorderWidth = 2,
                IsValueShownAsLabel = true  // подписи над столбцами включены!
            };

            chart6.Series.Add(series);

            foreach (var item in avgByGender)
            {
                var point = series.Points.AddXY(item.Gender, Math.Round(item.AvgGrade, 2));
                if (item.GenderKey == "ч" || item.GenderKey == "Ч")
                    series.Points[point].Color = Color.SkyBlue;
                else if (item.GenderKey == "ж" || item.GenderKey == "Ж")
                    series.Points[point].Color = Color.LightPink;
                else
                    series.Points[point].Color = Color.Gray;
            }

        }






        private void DrawAverageGradeByExperienceChart(DataTable table)
        {
            // 🔹 Групуємо стаж по діапазонах і рахуємо середній бал
            var grouped = table.AsEnumerable()
                .Where(r => r["grade_value"] != DBNull.Value)
                .Select(r => new
                {
                    Grade = Convert.ToDouble(r["grade_value"]),
                    Experience = r.Field<int?>("teacher_experience") ?? 0
                })
                .GroupBy(x =>
                {
                    if (x.Experience <= 10) return "0–10";
                    else if (x.Experience <= 20) return "11–20";
                    else if (x.Experience <= 30) return "21–30";
                    else if (x.Experience <= 40) return "31–40";
                    else return "41+";
                })
                .Select(g => new
                {
                    Range = g.Key,
                    AverageGrade = g.Average(x => x.Grade)
                })
                .OrderBy(g =>
                {
                    // забезпечуємо правильний порядок діапазонів
                    switch (g.Range)
                    {
                        case "0–10": return 1;
                        case "11–20": return 2;
                        case "21–30": return 3;
                        case "31–40": return 4;
                        default: return 5;
                    }
                })
                .ToList();

            chart5.Series.Clear();
            chart5.ChartAreas.Clear();
            chart5.ChartAreas.Add(new ChartArea("MainArea"));

            var series = new Series("Середній бал")
            {
                ChartType = SeriesChartType.Column,
                Color = Color.MediumAquamarine,
                BorderWidth = 2,
                Legend = "Default",
                IsValueShownAsLabel = true
            };

            foreach (var g in grouped)
            {
                int idx = series.Points.AddXY(g.Range, g.AverageGrade);
                series.Points[idx].Label = g.AverageGrade.ToString("F2");
            }

            // 🔹 Легенда
            chart5.Legends.Clear();
            chart5.Legends.Add(new Legend("Default")
            {
                Docking = Docking.Top,
                Alignment = StringAlignment.Center,
                Font = new Font("Segoe UI", 9)
            });

            // 🔹 Налаштування осей
            var area = chart5.ChartAreas["MainArea"];
            area.AxisX.Title = "Стаж (років)";
            area.AxisY.Title = "Середній бал";
            area.AxisX.Interval = 1;

            // 🔹 Прибираємо сітку повністю
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            // 🔹 Автоматичне масштабування осі Y
            if (grouped.Count > 0)
            {
                double min = grouped.Min(x => x.AverageGrade);
                double max = grouped.Max(x => x.AverageGrade);
                double margin = (max - min) * 0.1;
                area.AxisY.Minimum = Math.Max(0, Math.Floor(min - margin));
                area.AxisY.Maximum = Math.Ceiling(max + margin);
            }

            chart5.Series.Add(series);

            // 🔹 Заголовок
            chart5.Titles.Clear();
            chart5.Titles.Add(new Title(
                "Залежність середнього балу від стажу роботи вчителя",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black
            ));
        }


        private void DrawAverageGradeByYearChart(DataTable table)
        {
            // Группируем по году класса и считаем средний бал
            var yearAverages = table.AsEnumerable()
                .Where(r => r["grade_value"] != DBNull.Value && r["class_year"] != DBNull.Value)
                .GroupBy(r => r.Field<int>("class_year"))
                .Select(g => new
                {
                    Year = g.Key,
                    AverageGrade = g
                        .Select(x => Convert.ToDouble(x["grade_value"]))
                        .DefaultIfEmpty(0)
                        .Average()
                })
                .OrderBy(x => x.Year)
                .ToList();

            chart4.Series.Clear();
            chart4.ChartAreas.Clear();
            chart4.ChartAreas.Add(new ChartArea("MainArea"));

            Series lineSeries = new Series("Середній бал за роками")
            {
                ChartType = SeriesChartType.Line,
                Color = Color.MediumAquamarine,
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 8,
                MarkerColor = Color.MediumSeaGreen,
                Legend = "Default"
            };

            foreach (var item in yearAverages)
            {
                int idx = lineSeries.Points.AddXY(item.Year, item.AverageGrade);
                lineSeries.Points[idx].Label = item.AverageGrade.ToString("F2");
            }

            chart4.Legends.Clear();
            chart4.Series.Add(lineSeries);

            chart4.Titles.Clear();
            chart4.Titles.Add(
                new Title("Динаміка середніх оцінок за роками",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black)
            );

            var area = chart4.ChartAreas["MainArea"];
            area.AxisX.Title = "Рік";
            area.AxisY.Title = "Середній бал";
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);

            // === Убираем ВСЮ сетку ===
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisX.MinorGrid.Enabled = false;
            area.AxisY.MinorGrid.Enabled = false;

            // === Автоматический диапазон шкалы Y ===
            if (yearAverages.Count > 0)
            {
                double min = yearAverages.Min(x => x.AverageGrade);
                double max = yearAverages.Max(x => x.AverageGrade);

                double margin = (max - min) * 0.1;
                area.AxisY.Minimum = Math.Max(0, Math.Floor(min - margin));
                area.AxisY.Maximum = Math.Ceiling(max + margin);
            }

            area.RecalculateAxesScale();
        }




        private void DrawAverageGradeByRankChart(DataTable table)
        {
            // Фиксированный порядок рангов
            var rankOrder = new List<string>
    {
                "вчитель-методист",
                "старший вчитель",
                "відсутня"
    };

            // Группируем по рангу учителя
            var rankAverages = table.AsEnumerable()
                .GroupBy(r =>
                {
                    var rank = r.Field<string>("teacher_rank");
                    return string.IsNullOrEmpty(rank) ? "відсутня" : rank;
                })
                .Select(g => new
                {
                    TeacherRank = g.Key,
                    AverageGrade = g
                        .Where(x => x["grade_value"] != DBNull.Value)
                        .Select(x => Convert.ToDouble(x["grade_value"]))
                        .DefaultIfEmpty(0)
                        .Average()
                })
                // сортировка по фиксированному порядку
                .OrderBy(x => rankOrder.IndexOf(x.TeacherRank))
                .ToList();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("MainArea"));

            Series barSeries = new Series("Середній бал")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.MediumAquamarine,
                BorderWidth = 1,
                Legend = "Default"
            };

            // Добавляем данные на диаграмму (сверху вниз)
            for (int i = rankAverages.Count - 1; i >= 0; i--)
            {
                int idx = barSeries.Points.AddXY(rankAverages[i].TeacherRank, rankAverages[i].AverageGrade);
                barSeries.Points[idx].Label = rankAverages[i].AverageGrade.ToString("F2");
            }

            chart1.Legends.Clear();
            chart1.Series.Add(barSeries);

            chart1.Titles.Clear();
            chart1.Titles.Add(
                new Title("Залежність середнього балу від звання вчителя",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black)
            );

            // Оформление осей
            chart1.ChartAreas["MainArea"].AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            chart1.ChartAreas["MainArea"].AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            chart1.ChartAreas["MainArea"].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas["MainArea"].AxisY.MajorGrid.Enabled = false;
        }


        private void DrawAverageGradeByCategoryChart(DataTable table)
        {
            // --- фиксированный порядок категорий (все с маленькой буквы, как ты задала) ---
            var categoryOrder = new List<string>
    {
        "вчитель вищої категорії",
        "вчитель І категорії",
        "вчитель ІІ категорії",
        "спеціаліст",
        "відсутня"
    };

            // --- группировка и расчёт среднего балла по категориям ---
            var categoryAverages = table.AsEnumerable()
                .GroupBy(r =>
                {
                    var category = (r.Field<string>("teacher_category") ?? "").Trim();
                    return string.IsNullOrEmpty(category) ? "Відсутня" : category;
                })
                .Select(g => new
                {
                    TeacherCategory = g.Key,
                    AverageGrade = g
                        .Where(x => x["grade_value"] != DBNull.Value)
                        .Select(x => Convert.ToDouble(x["grade_value"]))
                        .DefaultIfEmpty(0)
                        .Average()
                })
                .OrderBy(x => categoryOrder
                    .FindIndex(c => string.Equals(c, x.TeacherCategory, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            // --- настройка чарта ---
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.ChartAreas.Add(new ChartArea("MainArea"));

            Series barSeries = new Series("середній бал")
            {
                ChartType = SeriesChartType.Bar,
                Color = Color.MediumAquamarine,
                BorderWidth = 1,
                Legend = "Default"
            };

            // --- добавляем данные (сверху вниз в нужном порядке) ---
            for (int i = categoryAverages.Count - 1; i >= 0; i--)
            {
                int idx = barSeries.Points.AddXY(categoryAverages[i].TeacherCategory, categoryAverages[i].AverageGrade);
                barSeries.Points[idx].Label = categoryAverages[i].AverageGrade.ToString("F2");
            }

            chart2.Legends.Clear();
            chart2.Series.Add(barSeries);

            // --- заголовок ---
            chart2.Titles.Clear();
            chart2.Titles.Add(
                new Title("Залежність середнього балу від категорії вчителя",
                Docking.Top,
                new Font("Segoe UI", 12, FontStyle.Bold),
                Color.Black)
            );

            // --- оформление осей и пространства под подписи ---
            var area = chart2.ChartAreas["MainArea"];
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.Enabled = false;

            // увеличиваем отступ слева, чтобы подписи не обрезались
            area.AxisY.IsLabelAutoFit = false;
            area.Position = new ElementPosition(20, 10, 75, 80);
            area.InnerPlotPosition = new ElementPosition(25, 5, 70, 90);
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
                .OrderBy(x => int.TryParse(x.Year, out var y) ? y : 0)
                .ToList();

            chart3.Series.Clear();
            chart3.ChartAreas.Clear();
            chart3.ChartAreas.Add(new ChartArea("MainArea"));

            // Убираем сетку
            chart3.ChartAreas["MainArea"].AxisX.MajorGrid.Enabled = false;
            chart3.ChartAreas["MainArea"].AxisY.MajorGrid.Enabled = false;

            // Легенда по умолчанию
            chart3.Legends.Clear();
            Legend legend = new Legend("Default");
            legend.Docking = Docking.Bottom;            // Легенда внизу
            legend.Alignment = StringAlignment.Center;  // По центру
            legend.LegendStyle = LegendStyle.Row;       // Горизонтально в один ряд
            chart3.Legends.Add(legend);

            // Отдельные серии для хлопців та дівчат
            Series boysSeries = new Series("Хлопці");
            boysSeries.ChartType = SeriesChartType.Column;
            boysSeries.Legend = "Default";
            boysSeries.Points.DataBind(
                grouped.Where(x => x.Gender == "Ч" || x.Gender == "ч"),
                "Year", "AvgGrade", ""
            );
            boysSeries["PointWidth"] = "0.4";
            boysSeries.Color = Color.SkyBlue;


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
            ApplyFilters();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
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
            DrawChart(filtered);
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
    }
}
