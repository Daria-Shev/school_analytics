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
    public partial class data_teacher : Form
    {
        public data_teacher()
        {
            InitializeComponent();
        }

        private void subject_nameLabel_Click(object sender, EventArgs e)
        {

        }

        private void data_teacher_Load(object sender, EventArgs e)
        {
            BD_teacher bdTeacher = new BD_teacher();

            dataGridView1.DataSource = bdTeacher.teacher_table();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_menu();
            ifrm.Show();
            this.Close();
        }
    }
}
