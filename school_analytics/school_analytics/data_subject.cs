using Newtonsoft.Json;
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
    public partial class data_subject : Form
    {
        public data_subject()
        {
            InitializeComponent();
        }

        private void data_subject_Load(object sender, EventArgs e)
        {
            Table();
        }
        public void Table()
        {
            BD_subject bdSubject = new BD_subject();

            dataGridView1.DataSource = bdSubject.subject_table();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_menu();
            ifrm.Show();
            this.Close();
        }
    }
}
