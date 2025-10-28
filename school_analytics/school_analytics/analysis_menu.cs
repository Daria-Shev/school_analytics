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
    public partial class analysis_menu : Form
    {
        public analysis_menu()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form ifrm = new Form1();
            ifrm.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form ifrm = new analysis_1();
            ifrm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form ifrm = new analysis_teacher();
            ifrm.Show();
            this.Close();
        }
    }
}
