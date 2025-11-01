using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace school_analytics
{
    internal class BD_subject
    {
        public class subjectData
        {
            public int subject_id { get; set; }
            public string subject_full_name { get; set; }
            public string subject_short_name { get; set; }
            public int dpa_id { get; set; }


        }

        public DataTable subject_table()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
SELECT        dbo.subject.subject_id, dbo.subject.subject_full_name, dbo.subject.subject_short_name, dbo.subject.dpa_id, dbo.dpa.dpa_name
FROM            dbo.dpa INNER JOIN
                         dbo.subject ON dbo.dpa.dpa_id = dbo.subject.dpa_id;
                        ";

            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            bd.closeBD();
            return table;

        }
        public List<subjectData> subject_list()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = "SELECT [[subject_id]], [subject_full_name] FROM [analytics_school].[dbo].[[subject]]";
            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);
            SqlDataReader reader = cmd.ExecuteReader();

            List<subjectData> teachers = new List<subjectData>();
            while (reader.Read())
            {
                teachers.Add(new subjectData
                {
                    subject_id = (int)reader["[subject_id]"],
                    subject_full_name = reader["subject_full_name"].ToString()
                });
            }

            bd.closeBD();
            return teachers;
        }
    }
}
