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
                openFileDialog.Title = "Выберите Excel файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        DataTable dt = LoadExcelToDataTable(filePath);
                        dataGridView1.DataSource = dt; // отображаем в DataGridView
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при загрузке Excel: " + ex.Message);
                    }
                }
            }
        }
        private DataTable LoadExcelToDataTable(string path)
        {
            // Указываем контекст лицензии до открытия файла
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            DataTable dt = new DataTable();

            using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(path)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                bool hasHeader = true;

                // Создаем столбцы
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    string columnName = hasHeader ? worksheet.Cells[1, col].Text : $"Column {col}";
                    dt.Columns.Add(columnName);
                }

                int startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    DataRow row = dt.NewRow();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        row[col - 1] = worksheet.Cells[rowNum, col].Text;
                    }
                    dt.Rows.Add(row);
                }
            }

            return dt;


        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            check_box_data();
            BD_import bdImport = new BD_import();
            int newClassId = bdImport.InsertClass(
                textBox1.Text,                                        // Название класса (string)
                Convert.ToInt32(comboBoxTeacher.SelectedValue),      // ID учителя (int)
                Convert.ToInt32(textBox2.Text),                      // Учебный год (int)
                comboBoxCurriculum.SelectedItem.ToString()          // Учебная программа (string)
            );


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
