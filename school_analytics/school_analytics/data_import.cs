using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.IO;




namespace school_analytics
{
    public partial class data_import : Form
    {
        public data_import()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_menu();
            ifrm.Show();
            this.Close();
        }


        private void data_import_Load(object sender, EventArgs e)
        {
            BD_teacher bdTeacher = new BD_teacher();
            comboBoxTeacher.DataSource = bdTeacher.teacher_list();
            comboBoxTeacher.DisplayMember = "teacher_short_name";
            comboBoxTeacher.ValueMember = "teacher_id";
            comboBoxCurriculum.DataSource = class_program;


        }

        List<string> class_program = new List<string>
        {
            "Типова освітня програма",
            "Інтелект України"
            
        };

        private void buttonLoadExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                openFileDialog.Title = "Оберіть Excel файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        DataTable dt = LoadExcelToDataTable(filePath);
                        dataGridView1.DataSource = dt;
                        dataGridView1.ColumnHeadersVisible = false; // ← вот это скрывает “Column 1” и т.п.
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка при завантаженні Excel: " + ex.Message);
                    }
                }
            }
        }
        //private DataTable LoadExcelToDataTable(string path)
        //{
        //    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        //    DataTable dt = new DataTable();

        //    using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(path)))
        //    {
        //        var worksheet = package.Workbook.Worksheets[0];

        //        // Создаем нужное количество столбцов (без имен)
        //        int colCount = worksheet.Dimension.End.Column;
        //        for (int col = 1; col <= colCount; col++)
        //        {
        //            dt.Columns.Add($"Column {col}");
        //        }

        //        // Заполняем строки
        //        for (int row = 1; row <= worksheet.Dimension.End.Row; row++)
        //        {
        //            DataRow newRow = dt.NewRow();
        //            for (int col = 1; col <= colCount; col++)
        //            {
        //                newRow[col - 1] = worksheet.Cells[row, col].Text;
        //            }
        //            dt.Rows.Add(newRow);
        //        }
        //    }

        //    return dt;
        //}

        private DataTable LoadExcelToDataTable(string path)
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            DataTable dt = new DataTable();
            HashSet<string> columnNames = new HashSet<string>(); // для уникальности имен колонок

            using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                int colCount = worksheet.Dimension.End.Column;
                int rowCount = worksheet.Dimension.End.Row;

                // 1️⃣ Создаем столбцы с уникальными именами
                for (int col = 1; col <= colCount; col++)
                {
                    string colName = worksheet.Cells[1, col].Text.Trim(); // берем текст из первой строки
                    if (string.IsNullOrEmpty(colName))
                    {
                        colName = $"Column{col}";
                    }

                    // Проверка на повторы
                    string uniqueName = colName;
                    int suffix = 1;
                    while (columnNames.Contains(uniqueName))
                    {
                        uniqueName = $"{colName}_{suffix}";
                        suffix++;
                    }

                    columnNames.Add(uniqueName);
                    dt.Columns.Add(uniqueName);
                }

                // 2️⃣ Заполняем строки, включая первую строку с названиями
                for (int row = 1; row <= rowCount; row++) // начинаем с 1, чтобы сохранить первую строку
                {
                    DataRow newRow = dt.NewRow();
                    for (int col = 1; col <= colCount; col++)
                    {
                        newRow[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    dt.Rows.Add(newRow);
                }
            }

            return dt;
        }

        //создает заголовки
        //private DataTable LoadExcelToDataTable(string path)
        //{
        //    // Указываем контекст лицензии до открытия файла
        //    OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

        //    DataTable dt = new DataTable();

        //    using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(path)))
        //    {
        //        var worksheet = package.Workbook.Worksheets[0];
        //        bool hasHeader = true;

        //        // Создаем столбцы
        //        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        //        {
        //            string columnName = hasHeader ? worksheet.Cells[1, col].Text : $"Column {col}";
        //            dt.Columns.Add(columnName);
        //        }

        //        int startRow = hasHeader ? 2 : 1;
        //        for (int rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
        //        {
        //            DataRow row = dt.NewRow();
        //            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        //            {
        //                row[col - 1] = worksheet.Cells[rowNum, col].Text;
        //            }
        //            dt.Rows.Add(row);
        //        }
        //    }

        //    return dt;


        //}

        //private void SaveToDatabase(DataTable dt)
        //{
        //    string connectionString = "Data Source=.;Initial Catalog=SchoolDB;Integrated Security=True";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        foreach (DataRow row in dt.Rows)
        //        {
        //            string query = @"INSERT INTO StudentGrades
        //                     (Прізвище, Імя, ПоБатькові, Стать, УкрМова, УкрЛіт, ЗарубЛіт, АнглМова, ІстУкраїни, ВсесІсторія, Матем, Біологія, Географія, Фізика, Хімія, Мистецтво, Інформ, Технолог, ЗахВіт, Фізра, Астроном)
        //                     VALUES (@Прізвище, @Імя, @ПоБатькові, @Стать, @УкрМова, @УкрЛіт, @ЗарубЛіт, @АнглМова, @ІстУкраїни, @ВсесІсторія, @Матем, @Біологія, @Географія, @Фізика, @Хімія, @Мистецтво, @Інформ, @Технолог, @ЗахВіт, @Фізра, @Астроном)";

        //            using (SqlCommand cmd = new SqlCommand(query, connection))
        //            {
        //                foreach (DataColumn col in dt.Columns)
        //                {
        //                    cmd.Parameters.AddWithValue("@" + col.ColumnName, row[col] ?? DBNull.Value);
        //                }
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    check_box_data();
        //    BD_import bdImport = new BD_import();
        //    int newClassId = bdImport.InsertClass(
        //        textBox1.Text,                                        // Название класса (string)
        //        Convert.ToInt32(comboBoxTeacher.SelectedValue),      // ID учителя (int)
        //        Convert.ToInt32(textBox2.Text),                      // Учебный год (int)
        //        comboBoxCurriculum.SelectedItem.ToString()          // Учебная программа (string)
        //    );

        //    // 2️⃣ Теперь переносим учеников из таблицы
        //    foreach (DataGridViewRow row in dataGridView1.Rows)
        //    {
        //        if (row.IsNewRow) continue;

        //        if (row.Cells[0].Value == null || row.Cells[1].Value == null) continue;

        //        BD_import.studentData student = new BD_import.studentData
        //        {
        //            student_last_name = row.Cells["прізвище"].Value?.ToString(),
        //            student_first_name = row.Cells["ім’я"].Value?.ToString(),
        //            student_middle_name = row.Cells["по батькові"].Value?.ToString(),
        //            student_gender = row.Cells["стать"].Value?.ToString(),
        //            student_dpa_1 = row.Cells["ДПА2"]?.Value?.ToString(),
        //            student_dpa_2 = row.Cells["ДПА3"]?.Value?.ToString()
        //        };

        //        try
        //        {
        //            int newStudentId = bdImport.InsertStudent(student, newClassId);

        //            // 👉 Здесь у тебя есть ID ученика, можно использовать как нужно:
        //            //Console.WriteLine($"Добавлен ученик {student.student_last_name}, ID = {newStudentId}");

        //            // Если позже нужно добавить оценки:
        //            // bdImport.InsertGrade(teacherShortName, subjectShortName, gradeValue, newStudentId);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Помилка при додаванні учня {student.student_last_name}: {ex.Message}");
        //        }
        //    }

        //}

        private void button1_Click(object sender, EventArgs e)
        {
            check_box_data();
            BD_import bdImport = new BD_import();

            // 1️⃣ Создаем новый класс
            int newClassId = bdImport.InsertClass(
                textBox1.Text,                                        // Название класса
                Convert.ToInt32(comboBoxTeacher.SelectedValue),      // ID учителя
                Convert.ToInt32(textBox2.Text),                      // Учебный год
                comboBoxCurriculum.SelectedItem.ToString()           // Учебная программа
            );

            // 2️⃣ Берем строки с названиями предметов и учителей
            var subjectsRow = dataGridView1.Rows[0]; // строка 0 — предметы
            var teachersRow = dataGridView1.Rows[1]; // строка 1 — учителя

            // 3️⃣ Переносим учеников и их оценки
            for (int i = 2; i < dataGridView1.Rows.Count; i++) // начиная с 2-й строки — ученики
            {
                var row = dataGridView1.Rows[i];
                if (row.IsNewRow) continue; // пропускаем пустую строку в конце

                // Проверяем 0-ю колонку на номер
                string firstCell = row.Cells[0].Value?.ToString()?.Trim();
                if (string.IsNullOrEmpty(firstCell)) break; // если нет номера — дальше пусто, выходим из цикла


                // 3.1️⃣ Создаем объект ученика
                BD_import.studentData student = new BD_import.studentData
                {
                    student_last_name = row.Cells[3].Value?.ToString(),
                    student_first_name = row.Cells[4].Value?.ToString(),
                    student_middle_name = row.Cells[5].Value?.ToString(),
                    student_gender =
                        !string.IsNullOrEmpty(row.Cells[6].Value?.ToString())
                        ? row.Cells[6].Value.ToString().Trim().Substring(0, 1)
                        : null,
                    student_dpa_1 = row.DataGridView.Columns.Contains("ДПА1")
                    ? row.Cells["ДПА1"]?.Value?.ToString()
                    : null,
                    student_dpa_2 = row.DataGridView.Columns.Contains("ДПА2")
                    ? row.Cells["ДПА2"]?.Value?.ToString()
                    : null,
                    student_dpa_3 = row.DataGridView.Columns.Contains("ДПА3")
                    ? row.Cells["ДПА3"]?.Value?.ToString()
                    : null,
                    student_dpa_4 = row.DataGridView.Columns.Contains("ДПА4")
                    ? row.Cells["ДПА4"]?.Value?.ToString()
                    : null
                };

                try
                {
                    // 3.2️⃣ Вставляем ученика в БД и получаем его ID
                    int newStudentId = bdImport.InsertStudent(student, newClassId);

                    // 3.3️⃣ Добавляем оценки
                    // Оценки начинаются с 8-й колонки (индекс 7)
                    for (int col = 7; col < dataGridView1.Columns.Count; col++)
                    {
                        string subject = subjectsRow.Cells[col].Value?.ToString()?.Trim(); // название предмета

                        if (string.IsNullOrEmpty(subject) || subject.Equals("ДПА1", StringComparison.OrdinalIgnoreCase))
                        {
                            // Пустая ячейка или дошли до ДПА1 — заканчиваем цикл
                            MessageBox.Show($"Успішно збережено");

                            break;

                        }


                        string teacher = teachersRow.Cells[col].Value?.ToString()?.Trim(); // учитель
                        string gradeText = row.Cells[col].Value?.ToString()?.Trim();        // оценка ученика

                        // 🔹 Пропускаем, если нет оценки
                        if (string.IsNullOrWhiteSpace(gradeText))
                            continue;

                        if (!string.IsNullOrWhiteSpace(gradeText) && int.TryParse(gradeText, out int grade))
                        {
                            bdImport.InsertGrade(teacher, subject, grade, newStudentId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при додаванні учня {student.student_last_name}: {ex.Message}");
                }
            }
        }



        private void check_box_data()
        {
            // Перевірка полів текстбоксів і комбобоксів
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                comboBoxTeacher.SelectedValue == null ||
                comboBoxCurriculum.SelectedItem == null ||
                string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Будь ласка, заповніть усі поля!",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Окрема перевірка DataGridView
            if (dataGridView1.Rows.Count == 0 ||
                (dataGridView1.AllowUserToAddRows && dataGridView1.Rows.Count == 1))
            {
                MessageBox.Show("Будь ласка, додайте дані в таблицю!",
                                "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
