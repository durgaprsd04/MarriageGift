namespace MarriageGift.DAO.Queries
{
    public static class CURDQueries
    {
        public static class Customers
        {
            public static class SelectCustomers
            {

                public static readonly string SelectCustomer = "select username, password from MarriageGift.dbo.Customer ";
                public static readonly string ByCustId = SelectCustomer + " where customer_id ={0}";
                public static readonly string ByUsername = SelectCustomer + " where username  like '{0}'";
            }
            public static class DeleteCustomers
            {

                public static readonly string deleteCustomer = "delete from MarriageGift.dbo.Customer ";
                public static readonly string ByCustId = deleteCustomer + " where customer_id ={0}";
            }
            public static class InsertCustomers
            {
                public static readonly string deleteCustomer = "insert into  MarriageGift.dbo.Customer "
                                                            + " values({0},{1},{2})";
            }
            public static class UpdateCustomers
            {
                public static readonly string updateCustomer = "Update  MarriageGift.dbo.Customer "
                                                            + " set username='{0}',"
                                                            + " set password='{1}'"
                                                            + " where customer_id ={2}";
            }
            public static class  LoginCustomers
            {
                public static readonly string loginCustomer = "select customer_id from customer"
                                                        + "where username = '{0}' and[password]='{1}'";
            }
        }
       
        public class SelectEvents
        {
            public static readonly string SelectEvent = "select event_venue, event_date, customer_id, is_Canceled from MarriageGift.dbo.[Events] ";
            public static readonly string ByEventId = SelectEvent + " where event_id ={0}";
            public static readonly string ByCustId = SelectEvent + " where customer_id ={0}";
            public static readonly string AndCustId = ByEventId + " and customer_id ={0}";
            public static readonly string ByCanceled = ByEventId + " and is_canceled ={0}";
            public static readonly string AndCanceled = AndCustId + " and is_canceled ={0}";
        }
        public class SelectGifts
        {
            public static readonly string SelectGift = "select gift_name, gift_price, gt.gift_type from gift g "+
                                                        " inner join gift_type gt on gt.gift_type_id =g.gift_Type_id";
            public static readonly string ByGiftId = SelectGift + " where gift_id ={0}";
          
        }
        public class Selectinivitations
        {
            public static readonly string Selectinivitation = "select event_id, cust_id_of_invitee from INVITATION ";
            public static readonly string ByInviteId = " where invitation_id ={0}";
        }
        public class OccassionTypes
        {
            public static readonly string SelectAll = "select occassion_type_id, occassion_type from [OCCASSION_TYPE] ";
            public static readonly string ByOccassionType = SelectAll + " where occassion_type={0}";
        }
    }
}
