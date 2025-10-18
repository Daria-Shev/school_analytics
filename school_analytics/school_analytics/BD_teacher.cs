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

        //даные в таблицу перенос  DataSource
        //public object teacher_table()
        //{
        //    BD bd = new BD();
        //    bd.connectionBD();


        //    // Используйте параметризованный запрос, чтобы избежать SQL-инъекций
        //    string sqlExpression = @"SELECT dbo.theme.theme_id, dbo.subject.subject_id, dbo.theme.theme_name, dbo.subject.subject_name
        //                 FROM dbo.theme INNER JOIN
        //                 dbo.subject ON dbo.theme.subject_id = dbo.subject.subject_id";

        //    // Создаем SqlDataAdapter и передаем ему SQL-выражение и подключение
        //    SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, bd.connection);

        //    DataTable dataTable = new DataTable();

        //    // Заполняем DataTable данными из запроса
        //    adapter.Fill(dataTable);

        //    // Преобразование DataTable в JSON строку
        //    string json = JsonConvert.SerializeObject(dataTable);

        //    bd.closeBD();
        //    return json;

        //}

        //public object teacher_delete(int teacher_id)
        //{
        //    BD bd = new BD();
        //    bd.connectionBD();

        //    try
        //    {
        //        string sqlExpression = @"DELETE FROM [test].[dbo].[teacher]
        //            WHERE [theme_id] = @theme_id;
        //           ";

        //        using (SqlCommand sqlCommand = new SqlCommand(sqlExpression, bd.connection))
        //        {

        //            sqlCommand.Parameters.AddWithValue("@theme_id", teacher_id);
        //            sqlCommand.ExecuteNonQuery();
        //        }
        //    }
        //    catch
        //    {
        //        return new Message { message = "Виникла помилка" };
        //    }
        //    bd.closeBD();
        //    return new Message { message = "Операція успішна" }; ;
        //}

        //public object teacher_add(string jsonData)
        //{
        //    // Десериализуем JSON строку в объект класса
        //    var classData = JsonConvert.DeserializeObject<teacherData>(jsonData);


        //    BD bd = new BD();
        //    bd.connectionBD();

        //    try
        //    {
        //        string sqlExpression = @"MERGE INTO [test].[dbo].[theme] AS target
        //                USING (VALUES (@theme_id, @theme_name, @subject_id)) 
        //                AS source (theme_id, theme_name, subject_id)
        //                ON target.theme_id = source.theme_id
        //                WHEN MATCHED THEN
        //                    UPDATE SET target.theme_name = source.theme_name,
        //                               target.subject_id = source.subject_id
        //                WHEN NOT MATCHED THEN
        //                    INSERT (theme_name, subject_id) 
        //                    VALUES (source.theme_name, source.subject_id);
        //           ";

        //        using (SqlCommand sqlCommand = new SqlCommand(sqlExpression, bd.connection))
        //        {

        //            sqlCommand.Parameters.AddWithValue("@theme_id", classData.theme_id);
        //            sqlCommand.Parameters.AddWithValue("@theme_name", classData.theme_name);
        //            sqlCommand.Parameters.AddWithValue("@subject_id", classData.subject_id);

        //            sqlCommand.ExecuteNonQuery();
        //        }
        //    }
        //    catch
        //    {
        //        return new Message { message = "Виникла помилка" };
        //    }
        //    bd.closeBD();
        //    return new Message { message = "Операція успішна" }; ;

        //}

        //public object teacher_list()
        //{
        //    BD bd = new BD();
        //    bd.connectionBD();
        //    string sqlExpression = "SELECT [teacher_id] ,[teacher_short_name] FROM [analytics_school].[dbo].[teacher]";
        //    SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, bd.connection);
        //    DataTable dataTable = new DataTable();
        //    adapter.Fill(dataTable);
        //    string json = JsonConvert.SerializeObject(dataTable);
        //    bd.closeBD();
        //    return json;
        //}

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
