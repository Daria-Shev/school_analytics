using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace school_analytics
{
    public partial class data_grade_add : Form
    {
        public data_grade_add()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void data_grade_add_Load(object sender, EventArgs e)
        {
            BD_student bdStudent = new BD_student();

            dataGridView1.DataSource = bdStudent.student_grade_table();
        }
    }
}
