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
            comboBox2.DataSource = class_program;


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
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // исправлено
            DataTable dt = new DataTable();

            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // первый лист
                bool hasHeader = true;

                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(hasHeader ? firstRowCell.Text : $"Column {firstRowCell.Start.Column}");
                }

                int startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                    DataRow row = dt.NewRow();
                    int i = 0;
                    foreach (var cell in wsRow)
                    {
                        row[i++] = cell.Text;
                    }
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }


    }
}
