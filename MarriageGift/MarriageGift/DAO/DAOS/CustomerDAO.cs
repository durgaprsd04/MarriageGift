using System.Data.SqlClient;
using MarriageGift.DAO.Queries;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model;
using System.Configuration;

namespace MarriageGift.DAO.DAOS
{
    public static class CustomerDao 
    {
        public readonly static string connectionString = ConfigurationManager.ConnectionStrings["MarriageGiftDB"].ToString();
        internal static void Delete(string custId)
        {
            var query = string.Format(CURDQueries.Customers.DeleteCustomers.ByCustId, custId);            
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                sqlCommand.Connection = conn;
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        internal static void Insert(IBaseObject baseObject)
        {
            var customer = (ICustomer)baseObject;
            var query = string.Format(CURDQueries.Customers.SelectCustomers.ByCustId, customer.getId(), customer.GetUserName(), customer.GetPassword());
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                sqlCommand.Connection = conn;
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        internal static ICustomer Read(string custId)
        {
            string username = string.Empty, password = string.Empty;
            var query = string.Format(CURDQueries.Customers.SelectCustomers.ByCustId, custId);
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                sqlCommand.Connection = conn;
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        username = reader["username"].ToString();
                        password = reader["password"].ToString();
                    }
                }
                    conn.Close();
            }
            var invites = InvitationDao.GetInvitesForCustId(custId);
            var events = EventDao.GetEventsForCustId(custId);
            return new Customer(custId, username, password, invites, events);
        }

        internal static void Update(IBaseObject baseObject)
        {
            var customer = (ICustomer)baseObject;
            var query = string.Format(CURDQueries.Customers.SelectCustomers.ByCustId, customer.GetUserName(), customer.GetPassword(), customer.getId());
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = query;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                sqlCommand.Connection = conn;
                sqlCommand.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
