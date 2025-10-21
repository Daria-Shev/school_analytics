using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

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
            public string student_dpa_3 { get; set; }
            public string student_dpa_4 { get; set; }
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
 student_dpa_1, student_dpa_2, student_dpa_3, student_dpa_4, class_id)
OUTPUT INSERTED.student_id
VALUES 
(@last, @first, @middle, @gender, @dpa1, @dpa2, @dpa3, @dpa4, @class_id)";

            SqlCommand cmd = new SqlCommand(query, bd.connection);
            cmd.Parameters.AddWithValue("@last", student.student_last_name);
            cmd.Parameters.AddWithValue("@first", student.student_first_name);
            cmd.Parameters.AddWithValue("@middle", student.student_middle_name);
            cmd.Parameters.AddWithValue("@gender", student.student_gender);

            // безопасная вставка DPA полей
            cmd.Parameters.AddWithValue("@dpa1", string.IsNullOrWhiteSpace(student.student_dpa_1) ? (object)DBNull.Value : student.student_dpa_1);
            cmd.Parameters.AddWithValue("@dpa2", string.IsNullOrWhiteSpace(student.student_dpa_2) ? (object)DBNull.Value : student.student_dpa_2);
            cmd.Parameters.AddWithValue("@dpa3", string.IsNullOrWhiteSpace(student.student_dpa_3) ? (object)DBNull.Value : student.student_dpa_3);
            cmd.Parameters.AddWithValue("@dpa4", string.IsNullOrWhiteSpace(student.student_dpa_4) ? (object)DBNull.Value : student.student_dpa_4);

            cmd.Parameters.AddWithValue("@class_id", classId);

            int newStudentId = (int)cmd.ExecuteScalar(); // отримуємо ID створеного учня
            bd.closeBD();

            return newStudentId;
        }

        //Временая проверка удалить потом 1

        public void InsertGrade1(string teacherShortName, string subjectShortName, int gradeValue, int studentId)
        {
            BD bd = new BD();
            bd.connectionBD();

            string queryTeacher = "SELECT teacher_id FROM teacher WHERE teacher_short_name = @short_name";
            SqlCommand cmdTeacher = new SqlCommand(queryTeacher, bd.connection);
            cmdTeacher.Parameters.AddWithValue("@short_name", teacherShortName);
            int teacherId = Convert.ToInt32(cmdTeacher.ExecuteScalar());
            MessageBox.Show($"Предмет: '{teacherShortName}'\nSubjectId: {teacherId}", "Debug Subject");

            //string querySubject = "SELECT subject_id FROM subject WHERE subject_short_name = @short_name";
            //SqlCommand cmdSubject = new SqlCommand(querySubject, bd.connection);
            //cmdSubject.Parameters.AddWithValue("@short_name", subjectShortName);
            //int subjectId = Convert.ToInt32(cmdSubject.ExecuteScalar());

            //// 2️⃣ Получаем subject_id регистронезависимо
            //string querySubject = @"
            //    SELECT subject_id 
            //    FROM subject 
            //    WHERE RTRIM(LTRIM(subject_short_name)) COLLATE SQL_Latin1_General_CP1_CI_AS = @short_name";

            //SqlCommand cmdSubject = new SqlCommand(querySubject, bd.connection);

            //// Обрезаем пробелы в C# тоже на всякий случай
            //string subjectClean = subjectShortName?.Trim();

            //cmdSubject.Parameters.AddWithValue("@short_name", subjectClean);
            //int subjectId = Convert.ToInt32(cmdSubject.ExecuteScalar());

            // Получаем subject_id точно так же, как teacher
            string querySubject = "SELECT subject_id FROM subject WHERE subject_short_name = @short_name";
            SqlCommand cmdSubject = new SqlCommand(querySubject, bd.connection);
            MessageBox.Show($"SubjectClean: '{subjectShortName}'\nTeacherClean: '{subjectShortName}'", "Debug");

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


        public void InsertGrade(string teacherShortName, string subjectShortName, int gradeValue, int studentId)
        {
            BD bd = new BD();
            bd.connectionBD();

            // Чистим строки от пробелов, неразрывных пробелов и табуляций
            string teacherClean = teacherShortName?.Trim().Replace("\u00A0", "").Replace("\t", "");
            string subjectClean = subjectShortName?.Trim().Replace("\u00A0", "").Replace("\t", "");
            // 🔹 Выводим для проверки
            //MessageBox.Show($"SubjectClean: '{subjectClean}'\nTeacherClean: '{teacherClean}'", "Debug");



            string queryTeacher = "SELECT teacher_id FROM teacher WHERE teacher_short_name = @short_name";
            SqlCommand cmdTeacher = new SqlCommand(queryTeacher, bd.connection);
            cmdTeacher.Parameters.AddWithValue("@short_name", teacherShortName);
            int teacherId = Convert.ToInt32(cmdTeacher.ExecuteScalar());




            
            // Получаем subject_id, регистронезависимо
            string querySubject = @"
 SELECT TOP 1 subject_id 
 FROM subject 
 WHERE LOWER(REPLACE(REPLACE(LTRIM(RTRIM(subject_short_name)), CHAR(160), ''), CHAR(9), ''))
       LIKE LOWER(@short_name + '%')";

            SqlCommand cmdSubject = new SqlCommand(querySubject, bd.connection);
            cmdSubject.Parameters.AddWithValue("@short_name", subjectClean.ToLower());
            object subjectObj = cmdSubject.ExecuteScalar();
            if (subjectObj == null)
                throw new Exception($"Предмет '{subjectClean}' не найден в базе!");
            int subjectId = Convert.ToInt32(subjectObj);
            // Получаем teacher_id, регистронезависимо
            
           
            // Вставляем оценку
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
