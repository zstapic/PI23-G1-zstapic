using DBLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation_Manager
{
    public static class ActivityRepository
    {
        private static Activity CreateObject(SqlDataReader reader)
        {
            Activity aktivnost = new Activity();

            aktivnost.Id = Convert.ToInt32(reader["Id"].ToString());
            aktivnost.Name = reader["Name"].ToString();
            aktivnost.Description = reader["Description"].ToString();
            int.TryParse(reader["MaxPoints"].ToString(), out int maxPoints);
            int.TryParse(reader["MinPointsForGrade"].ToString(), out int minPointsForGrade);
            int.TryParse(reader["MinPointsForSignature"].ToString(), out int minPointsForSignature);
            aktivnost.MaxPoints = maxPoints;
            aktivnost.MinPointsForGrade = minPointsForGrade;
            aktivnost.MinPointsForSignature = minPointsForSignature;

            return aktivnost;
        }

        public static Activity GetActivity(int id)
        {
            Activity actvity = null;
            DB.OpenConnection();
            SqlDataReader reader = DB.GetDataReader($"SELECT * FROM Activities WHERE Id = {id}");
            if (reader.HasRows)
            {
                reader.Read();
                actvity = CreateObject(reader);
                reader.Close();
            }

            DB.CloseConnection();
            return actvity;
        }

        public static List<Activity> GetActivities()
        {
            List<Activity> aktivnosti = new List<Activity>();

            DB.OpenConnection();
            SqlDataReader reader = DB.GetDataReader($"SELECT * FROM Activities");
            while (reader.Read())
            {
                Activity student = CreateObject(reader);
                aktivnosti.Add(student);
            }

            reader.Close();
            DB.CloseConnection();
            return aktivnosti;
        }

    }
}
