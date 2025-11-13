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
    public partial class data_menu : Form
    {
        public data_menu()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_import();
            ifrm.Show();
            this.Close();
            //this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form ifrm = new Form1();
            ifrm.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_subject();
            ifrm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_teacher();
            ifrm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_grade();
            ifrm.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form ifrm = new data_class();
            ifrm.Show();
            this.Close();
        }

        private void data_menu_Load(object sender, EventArgs e)
        {

        }
    }
}
