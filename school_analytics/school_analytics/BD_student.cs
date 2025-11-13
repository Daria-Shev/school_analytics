using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace school_analytics
{
    internal class BD_student
    {
        
        public DataTable student_table()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
           SELECT 
    s.student_id,
    s.student_last_name,
    s.student_first_name,
    s.student_middle_name,
    s.student_gender,
    c.class_name,
    d1.dpa_name AS dpa_subject_1,
    d2.dpa_name AS dpa_subject_2,
    d3.dpa_name AS dpa_subject_3,
    d4.dpa_name AS dpa_subject_4
FROM dbo.student AS s
LEFT JOIN dbo.class AS c 
    ON s.class_id = c.class_id
LEFT JOIN dbo.dpa AS d1 
    ON s.student_dpa_1 = d1.dpa_id
LEFT JOIN dbo.dpa AS d2 
    ON s.student_dpa_2 = d2.dpa_id
LEFT JOIN dbo.dpa AS d3 
    ON s.student_dpa_3 = d3.dpa_id
LEFT JOIN dbo.dpa AS d4 
    ON s.student_dpa_4 = d4.dpa_id
ORDER BY s.student_last_name, s.student_first_name;

                        ";

            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            bd.closeBD();
            return table;

        }
        public DataTable student_grade_table()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
SELECT 
    s.student_id,
    subj.subject_full_name,
    g.grade_value,
    g.grade_id,
    t.teacher_short_name
FROM dbo.student AS s
INNER JOIN dbo.grade AS g 
    ON s.student_id = g.student_id
INNER JOIN dbo.subject AS subj 
    ON g.subject_id = subj.subject_id
INNER JOIN dbo.teacher AS t 
    ON g.teacher_id = t.teacher_id
WHERE s.student_id = 144
ORDER BY subj.subject_full_name;
                        ";

            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            bd.closeBD();
            return table;

        }
    }
}
