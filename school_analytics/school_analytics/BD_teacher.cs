using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

//using Microsoft.Data.SqlClient;

namespace school_analytics
{
    public class BD_teacher
    {
        public class teacherData
        {
            public int teacher_id { get; set; }
            public string teacher_last_name { get; set; }
            public string teacher_first_name { get; set; }
            public string teacher_middle_name { get; set; }
            public string teacher_short_name { get; set; }
            public string teacher_category { get; set; }
            public string teacher_rank { get; set; }
            public int teacher_age { get; set; }
            public int teacher_experience { get; set; }

        }

        public DataTable teacher_table()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
            SELECT  teacher_id, teacher_last_name, teacher_first_name, teacher_middle_name, teacher_category, teacher_rank, teacher_experience
            FROM            dbo.teacher
                                    ";

            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            bd.closeBD();
            return table;

        }
        public List<teacherData> teacher_list()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = "SELECT [teacher_id], [teacher_short_name] FROM [analytics_school].[dbo].[teacher]";
            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);
            SqlDataReader reader = cmd.ExecuteReader();

            List<teacherData> teachers = new List<teacherData>();
            while (reader.Read())
            {
                teachers.Add(new teacherData
                {
                    teacher_id = (int)reader["teacher_id"],
                    teacher_short_name = reader["teacher_short_name"].ToString()
                });
            }

            bd.closeBD();
            return teachers;
        }

    }
}
