using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace school_analytics
{
    public class diagram_table
    {
        public DataTable GetClassStudentGrades()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
        SELECT 
            dbo.class.class_id,
            dbo.class.class_name, 
            dbo.class.class_year, 
            dbo.class.class_curriculum,
            dbo.student.student_id,
            dbo.student.student_gender,
            dbo.student.student_dpa_1, 
            dbo.student.student_dpa_2, 
            dbo.student.student_dpa_3, 
            dbo.student.student_dpa_4,
            dbo.subject.subject_id,
            dbo.subject.subject_full_name,
            dbo.grade.grade_id,
            dbo.grade.grade_value
        FROM dbo.grade
        INNER JOIN dbo.student ON dbo.grade.student_id = dbo.student.student_id
        INNER JOIN dbo.class ON dbo.student.class_id = dbo.class.class_id
        INNER JOIN dbo.subject ON dbo.grade.subject_id = dbo.subject.subject_id
        INNER JOIN dbo.teacher ON dbo.grade.teacher_id = dbo.teacher.teacher_id 
            AND dbo.class.class_teacher_id = dbo.teacher.teacher_id";

            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            bd.closeBD();
            return table;
        }
    }
}
