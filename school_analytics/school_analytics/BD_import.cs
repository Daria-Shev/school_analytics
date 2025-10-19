using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace school_analytics
{
    public class BD_import
    {
        public class studentData
        {
            public string student_last_name { get; set; }
            public string student_first_name { get; set; }
            public string student_middle_name { get; set; }
            public string student_gender { get; set; }
            public string student_dpa_1 { get; set; }   
            public string student_dpa_2 { get; set; }  
        }


        // Метод для додавання класу
        public int InsertClass(string className, int classTeacherId, int studyYear, string classCurriculum)
        {
            BD bd = new BD();
            bd.connectionBD();

            string query = @"
        INSERT INTO class (class_name, class_teacher_id, class_year, class_curriculum)
        OUTPUT INSERTED.class_id
        VALUES (@class_name, @class_teacher_id, @class_year, @class_curriculum)";

            SqlCommand cmd = new SqlCommand(query, bd.connection);
            cmd.Parameters.AddWithValue("@class_name", className);
            cmd.Parameters.AddWithValue("@class_teacher_id", classTeacherId);
            cmd.Parameters.AddWithValue("@class_year", studyYear);
            cmd.Parameters.AddWithValue("@class_curriculum", classCurriculum);

            int newClassId = (int)cmd.ExecuteScalar(); // отримуємо ID створеного класу
            bd.closeBD();

            return newClassId;
        }

        // 🔹 Метод для додавання учня
        public int InsertStudent(studentData student, int classId)
        {
            BD bd = new BD();
            bd.connectionBD();

            string query = @"
        INSERT INTO student 
        (student_last_name, student_first_name, student_middle_name, student_gender, 
         student_dpa_1, student_dpa_2, class_id)
        OUTPUT INSERTED.student_id
        VALUES 
        (@last, @first, @middle, @gender, @dpa1, @dpa2, @class_id)";

            SqlCommand cmd = new SqlCommand(query, bd.connection);
            cmd.Parameters.AddWithValue("@last", student.student_last_name);
            cmd.Parameters.AddWithValue("@first", student.student_first_name);
            cmd.Parameters.AddWithValue("@middle", student.student_middle_name);
            cmd.Parameters.AddWithValue("@gender", student.student_gender);
            cmd.Parameters.AddWithValue("@dpa1", student.student_dpa_1);
            cmd.Parameters.AddWithValue("@dpa2", student.student_dpa_2);
            cmd.Parameters.AddWithValue("@class_id", classId);

            int newStudentId = (int)cmd.ExecuteScalar(); // отримуємо ID створеного учня
            bd.closeBD();

            return newStudentId;
        }


        public void InsertGrade(string teacherShortName, string subjectShortName, int gradeValue, int studentId)
        {
            BD bd = new BD();
            bd.connectionBD();

            string queryTeacher = "SELECT teacher_id FROM teacher WHERE teacher_short_name = @short_name";
            SqlCommand cmdTeacher = new SqlCommand(queryTeacher, bd.connection);
            cmdTeacher.Parameters.AddWithValue("@short_name", teacherShortName);
            int teacherId = Convert.ToInt32(cmdTeacher.ExecuteScalar());

            string querySubject = "SELECT subject_id FROM subject WHERE subject_short_name = @short_name";
            SqlCommand cmdSubject = new SqlCommand(querySubject, bd.connection);
            cmdSubject.Parameters.AddWithValue("@short_name", subjectShortName);
            int subjectId = Convert.ToInt32(cmdSubject.ExecuteScalar());

            string queryInsert = @"
        INSERT INTO grade (subject_id, teacher_id, grade_value, student_id)
        VALUES (@subject_id, @teacher_id, @grade_value, @student_id)";

            SqlCommand cmdInsert = new SqlCommand(queryInsert, bd.connection);
            cmdInsert.Parameters.AddWithValue("@subject_id", subjectId);
            cmdInsert.Parameters.AddWithValue("@teacher_id", teacherId);
            cmdInsert.Parameters.AddWithValue("@grade_value", gradeValue);
            cmdInsert.Parameters.AddWithValue("@student_id", studentId);

            cmdInsert.ExecuteNonQuery();
            bd.closeBD();
        }




    }
}
