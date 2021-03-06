﻿using System;
using System.Globalization;
using MarriageGift.DAO.Queries;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.EventModel;
using MarriageGift.Model;
using System.Data.SqlClient;
using System.Configuration;
namespace MarriageGift.DAO.DAOS
{
   public class EventDao
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings[CommonStaticClass.GetConnectionString()].ToString();
        internal static void Delete(string id)
        {
            throw new NotImplementedException();
        }

        internal static void Insert(IBaseObject baseObject)
        {
            var event1 = baseObject as Event ;
            SqlCommand sqlCommand = new SqlCommand();
            if(event1!=null)
            {
                sqlCommand.CommandText= string.Format(CURDQueries.Events.InsertEvents.InsertEvent,  event1.Occassion.getId(), event1.getId(),   event1.Place, event1.Date.ToString(), event1.CustId,0);
                Console.WriteLine(sqlCommand.CommandText);
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    sqlCommand.Connection=conn;
                    sqlCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        internal static IEvent Read(string eventId)
        {
            string venue = string.Empty, date = string.Empty, custId = string.Empty, occassionId =string.Empty ;
            var isCanceled=false;
            var query = string.Format(CURDQueries.Events.SelectEvents.ByEventId, eventId);
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                sqlCommand.Connection = conn;
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        venue = reader["event_venue"].ToString();
                        date = reader["event_date"].ToString();
                        custId = reader["customer_id"].ToString();
                        occassionId = reader["occassion_id"].ToString();
                        isCanceled = reader["is_canceled"].ToString() == "1";
                    }
                }
                conn.Close();
            }
            var date1 = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
            var occassion = OccassionDao.GetOccassionByEventId(eventId);
            var giftE = GiftDao.GetExpectedGiftsForEvent(eventId);
            var giftR = GiftDao.GetRecievedGiftsForEvent(eventId);

            return new Event(eventId, occassion, venue, date1, giftE, giftR, custId, isCanceled);
        }

        internal static void Update(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }
        internal static IEvent GetEventsForCustId(string custId)
        {
            IEventCollection eventCollection = new EventCollection();
            var query = string.Format(CURDQueries.Events.SelectEvents.ByCustId, custId);
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                sqlCommand.Connection = conn;
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        venue = reader["event_venue"].ToString();
                        date = reader["event_date"].ToString();
                        custId = reader["customer_id"].ToString();
                        occassionId = reader["occassion_id"].ToString();
                        isCanceled = reader["is_canceled"].ToString() == "1";
                        var date1 = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture);
                        var occassion = OccassionDao.GetOccassionByEventId(eventId);
                        var giftE = GiftDao.GetExpectedGiftsForEvent(eventId);
                        var giftR = GiftDao.GetRecievedGiftsForEvent(eventId);
                        eventCollection.AddEvent(new Event(eventId, occassion, venue, date0, giftE, giftR, custId, isCanceled));
                    }
                }
                conn.Close();
          }
      }
    }
}
