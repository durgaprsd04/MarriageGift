using System;
using System.Data.SqlClient;
using MarriageGift.Model;
using MarriageGift.Model.Interfaces;
using System.Configuration;
using System.Collections.Generic;
namespace MarriageGift.DAO.DAOS
{
    public static class OccassionDao
    {
        
        public readonly static string connectionString = ConfigurationManager.ConnectionStrings[CommonStaticClass.GetConnectionString()].ToString();
        internal static void Update(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }
        internal static IEventCollection Delete(string custId)
        {
            throw new NotImplementedException();
        }
        internal static void Insert(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }
        internal static IOccassion Read(string custId)
        {
            throw new NotImplementedException();
        }
        public static IOccassion GetOccassionByEventId(string eventId)
        {
            throw new NotImplementedException();
        }
        public static Dictionary<int, string> GetOcccasionTypes()
        {
            var resultDict = new Dictionary<int,string>();
            var query = Queries.CURDQueries.OccassionTypes.SelectAll;
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                sqlCommand.Connection = conn;
                using(var reader = sqlCommand.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        resultDict.Add(reader.GetInt32(0), reader.GetString(1));
                    }
                }
                conn.Close();
            }
            return resultDict;
        }
    }
}
