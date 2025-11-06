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
        //не нужно?
        public DataTable GetTeacherGrades()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
SELECT 
    t.teacher_id, 
    t.teacher_short_name,        -- учитель, который ставит оценку (предметник)
    g.grade_value, 
    c.class_id,
    c.class_year, 
c.class_name,
    c.class_teacher_id,          -- ID класного керівника
    t2.teacher_short_name AS class_teacher_name -- ФИО класного керівника
FROM dbo.grade g
INNER JOIN dbo.student s ON g.student_id = s.student_id
INNER JOIN dbo.class c ON s.class_id = c.class_id
INNER JOIN dbo.teacher t ON g.teacher_id = t.teacher_id
INNER JOIN dbo.teacher t2 ON c.class_teacher_id = t2.teacher_id  -- ВАЖНО

            ";


            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            bd.closeBD();
            return table;
        }

        public DataTable GetTeachersOnly()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
            SELECT
                teacher_id,
                teacher_short_name,
                teacher_category,
                teacher_experience,
                teacher_rank
            FROM dbo.teacher;
            ";

            SqlCommand cmd = new SqlCommand(sqlExpression, bd.connection);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            adapter.Fill(table);

            bd.closeBD();
            return table;
        }

        public DataTable GetSubjectDPAGrades()
        {
            BD bd = new BD();
            bd.connectionBD();

            string sqlExpression = @"
            SELECT 
                g.grade_value,
                s.subject_full_name,
                s.subject_id,
                t.teacher_short_name,
                c.class_name,
                c.class_year,
                c.class_curriculum,
                st.student_gender,
                dpasubj.dpa_name AS subject_dpa,
                d1.dpa_name AS dpa_name_1,
                d2.dpa_name AS dpa_name_2,
                d3.dpa_name AS dpa_name_3,
                d4.dpa_name AS dpa_name_4
            FROM dbo.grade g
            INNER JOIN dbo.subject s ON g.subject_id = s.subject_id
            INNER JOIN dbo.teacher t ON g.teacher_id = t.teacher_id
            INNER JOIN dbo.student st ON g.student_id = st.student_id
            INNER JOIN dbo.class c ON st.class_id = c.class_id
            LEFT JOIN dbo.dpa dpasubj ON s.dpa_id = dpasubj.dpa_id
            LEFT JOIN dbo.dpa d1 ON st.student_dpa_1 = d1.dpa_id
            LEFT JOIN dbo.dpa d2 ON st.student_dpa_2 = d2.dpa_id
            LEFT JOIN dbo.dpa d3 ON st.student_dpa_3 = d3.dpa_id
            LEFT JOIN dbo.dpa d4 ON st.student_dpa_4 = d4.dpa_id;
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
