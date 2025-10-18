using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school_analytics
{
    public class BD
    {
        public SqlConnection connection;
        public void connectionBD()
        {
            //string connectionString = "Server=WIN-VF4PLQ89RM2\\SQLEXPRESS;Database=test;Trusted_Connection=True;";
            string connectionString = "Server=DESKTOP-6SVOIOI;Database=analytics_school;Trusted_Connection=True;TrustServerCertificate=True;";
            connection = new SqlConnection(connectionString);
            try
            {
                // Открываем подключение
                connection.Open();
                //Console.WriteLine("Подключение открыто");
            }
            catch (SqlException ex)
            {
                //Console.WriteLine(ex.Message);
            }
        }
        public void closeBD()
        {
            //если подключение открыто
            if (connection.State == ConnectionState.Open)
            {
                //закрываем подключение
                connection.Close();
            }

        }

    }

    public class Message
    {
        public string message { get; set; }
    }


}
