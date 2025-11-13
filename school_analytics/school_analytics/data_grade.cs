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
    public partial class data_grade : Form
    {
        public data_grade()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_menu();
            ifrm.Show();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void data_grade_Load(object sender, EventArgs e)
        {
            BD_student bdStudent = new BD_student();

            dataGridView1.DataSource = bdStudent.student_table();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_grade_add();
            ifrm.Show();
            //this.Close();
        }
    }
}
