using DBLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Evaluation_Manager
{
    public static class TeacherRepository
    {
        public static List<Teacher> GetAll()
        {
            string sql = $"SELECT * FROM Teachers";
            return FetchTeachers(sql);
        }

        public static Teacher GetTeacher (string username)
        {
            string sql = $"SELECT * FROM Teachers WHERE Username = '{username}'";
            return FetchTeacher(sql);
        }

        public static Teacher GetTeacher(int id)
        {
            string sql = $"SELECT * FROM Teachers WHERE Id = '{id}'";
            return FetchTeacher(sql);
        }

        private static Teacher FetchTeacher(string sql)
        {
            DB.OpenConnection();
            SqlDataReader reader = DB.GetDataReader(sql);
            Teacher teacher = null;

            if (reader.HasRows)
            {
                reader.Read();
                teacher = CreateObject(reader);
                reader.Close();
            }

            DB.CloseConnection();
            return teacher;
        }

        private static List<Teacher> FetchTeachers(string sql)
        {
            DB.OpenConnection();
            SqlDataReader reader = DB.GetDataReader(sql);
            List<Teacher> teachers = new List<Teacher>();

            while (reader.Read())
            {
                Teacher teacher = CreateObject(reader);
                teachers.Add(teacher);
            }

            reader.Close();
            DB.CloseConnection();
            return teachers;
        }

        private static Teacher CreateObject(SqlDataReader reader)
        {
            Teacher teacher = new Teacher();

            teacher.Id = Convert.ToInt32(reader["Id"].ToString());
            teacher.FirstName = reader["FirstName"].ToString();
            teacher.LastName = reader["LastName"].ToString();
            teacher.Username = reader["Username"].ToString();
            teacher.Password = reader["Password"].ToString();

            return teacher;
        }
    }
}
