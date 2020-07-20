using System;
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
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["MarriageGiftDB"].ToString();
        internal static void Delete(string id)
        {
            throw new NotImplementedException();
        }

        internal static void Insert(IBaseObject baseObject)
        {
            throw new NotImplementedException();
        }

        internal static IEvent Read(string eventId)
        {
            string venue = string.Empty, date = string.Empty, custId = string.Empty, occassionId =string.Empty ;
            var isCanceled=false;
            var query = string.Format(CURDQueries.SelectEvents.ByEventId, eventId);
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
        internal static IEventCollection GetEventsForCustId(string custId)
        {
            throw new NotImplementedException();
        }
    }
}
