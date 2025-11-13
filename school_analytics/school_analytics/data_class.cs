using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace school_analytics
{
    public partial class data_class : Form
    {
        public data_class()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



        public DataTable class_table()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
SELECT        dbo.class.class_id, dbo.class.class_year, dbo.class.class_name, dbo.class.class_curriculum, dbo.teacher.teacher_short_name
FROM            dbo.class INNER JOIN
                         dbo.teacher ON dbo.class.class_teacher_id = dbo.teacher.teacher_id
                        ";

            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            bd.closeBD();
            return table;

        }

        private void data_class_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = class_table();
        }
    }
}
