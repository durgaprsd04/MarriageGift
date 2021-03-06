﻿using System;
using System.Data.SqlClient;
using MarriageGift.Model;
using MarriageGift.Enums;
using MarriageGift.Model.OccasionModel;
using MarriageGift.Model.Interfaces;
using System.Configuration;
using System.Collections.Generic;
using log4net;
namespace MarriageGift.DAO.DAOS
{
    public static class OccassionDao
    {

        public static Dictionary<string, int> OccassionTypeDict =new Dictionary<string, int>(); 
        public readonly static string connectionString = ConfigurationManager.ConnectionStrings[CommonStaticClass.GetConnectionString()].ToString();
        internal static void Update(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }
        internal static IEventCollection Delete(string custId)
        {
            throw new NotImplementedException();
        }
        internal static int GetOccassionTypeId(Occasion occasion)
        {
            if(!OccassionTypeDict.ContainsKey(occasion.ToString()))
            {
                OccassionTypeDict.Clear();
                var sqlCommand = new SqlCommand();
                sqlCommand.CommandText=Queries.CURDQueries.Occassion.SelectOccassionTypes;;
                using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        sqlCommand.Connection = conn;
                        using(var reader = sqlCommand.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                OccassionTypeDict.Add(reader.GetString(1),reader.GetInt32(0));
                            }
                        }
                        conn.Close();
                    }
            }
            return OccassionTypeDict[occasion.ToString()];
        }
        internal static void Insert(IBaseObject baseObject)
        {
          var occassionInQ = (IOccassion)baseObject;
          var occassionTypeId = GetOccassionTypeId(occassionInQ.GetOccassionType());
          var personList = occassionInQ.GetPerson().Split('|');
          var sqlCommand = new SqlCommand();
          sqlCommand.CommandText = string.Format(Queries.CURDQueries.Occassion.InsertOccassion,occassionInQ.getId(), personList[0], personList[1],occassionTypeId);
          using (var conn = new SqlConnection(connectionString))
          {
              conn.Open();
              sqlCommand.Connection = conn;
              sqlCommand.ExecuteNonQuery();
              conn.Close();
          }
        }
        internal static IOccassion Read(string custId)
        {
            throw new NotImplementedException();
        }
        public static IOccassion GetOccassionByEventId(string eventId)
        {
            throw new NotImplementedException();
        }
        public static Dictionary<int, string> GetOcccasionTypes(ILog logger)
        {
            logger.Info("Getting all occassions from database");
            var resultDict = new Dictionary<int,string>();
            var query = Queries.CURDQueries.OccassionTypes.SelectAll;
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
                            resultDict.Add(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                    conn.Close();
                }
            }
            catch(Exception e)
            {
                logger.Error(e.Message+" occured while accessing occassions");
            }

            return resultDict;
        }
    }
}
