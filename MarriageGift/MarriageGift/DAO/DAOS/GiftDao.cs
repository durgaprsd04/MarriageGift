using System.Collections.Generic;
using System;
using log4net;
using System.Configuration;
using System.Data.SqlClient;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model;

namespace MarriageGift.DAO.DAOS
{
    class GiftDao
    {
        public readonly static string connectionString = ConfigurationManager.ConnectionStrings[CommonStaticClass.GetConnectionString()].ToString();
        internal static void Delete(string id)
        {
            throw new NotImplementedException();
        }

        internal static void Insert(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }

        internal static IBaseObject Read(string id)
        {
            throw new NotImplementedException();
        }

        internal static void Update(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }
        internal static IGiftCollection<IGift> GetExpectedGiftsForEvent(string eventId)
        {
            throw new NotImplementedException();
        }
        internal static IGiftCollection<IGift> GetRecievedGiftsForEvent(string eventId)
        {
            throw new NotImplementedException();
        }
        internal static IDictionary<string,string> GetAllGifts(ILog logger)
        {
            logger.Info("Getting all gifts from database");
            var resultDict = new Dictionary<string,string>();
            var query = Queries.CURDQueries.SelectGifts.SelectAllGifts;
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    sqlCommand.Connection = conn;
                    using(var reader = sqlCommand.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            resultDict.Add(reader.GetString(0), reader.GetString(1));
                        }
                    }
                    conn.Close();
                }
            }
            catch(Exception e)
            {
                logger.Error("Error occured while fetching full gift list "+e.Message);
                //ignore
            }
            return resultDict;
        }
    }
}
