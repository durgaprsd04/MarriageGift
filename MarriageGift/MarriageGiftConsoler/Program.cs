using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Controller.Interfaces;
using MarriageGift.DAO.Wrappers;
using Autofac;
using System.IO;
using MarriageGift.FAO.Interfaces;
using MarriageGift.FAO;
using MarriageGift.Model;
using MarriageGift.Model.EventModel;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Model.GiftModel;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.OccasionModel;
using MarriageGift.Enums;
using MarriageGift.Controller;
using log4net;

namespace MarriageGiftConsoler
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            ILog logger = LogManager.GetLogger(typeof(Program));
            builder.RegisterType<EventDaoWrapper>().As<IEventDao>();
            builder.RegisterType<CustomerDaoWrapper>().As<ICustomerDao>();
            builder.RegisterType<GiftDaoWrapper>().As<IGiftDao>();
            builder.RegisterType<InvitationDaoWrapper>().As<IInvitationDao>();
            builder.RegisterType<OccasionDaoWrapper>().As<IOccassionDao>();
            builder.Register(s => new StreamWriter("customer.txt", true)).As<StreamWriter>();
            builder.Register(f => new FileStream("customer.dat", FileMode.Create)).As<FileStream>();
            builder.RegisterType<SaveToFileFao>().As<ISaveToFileFao>();
            builder.Register(c => new Customer("Jeff", "pass@jeff")).As<ICustomer>();
            builder.Register(l=>logger).As<ILog>();
            builder.RegisterType<CustomerActionController>().As<ICustomerController>();
            
            var Container = builder.Build();
            using (var scope = Container.BeginLifetimeScope())
            {
                var customerController = (CustomerActionController)scope.Resolve<ICustomerController>();
                var customerDao = (CustomerDaoWrapper)scope.Resolve<ICustomerDao>();
                var eventDao = (EventDaoWrapper)scope.Resolve<IEventDao>();
                var customer = (Customer)scope.Resolve<ICustomer>();
                Console.WriteLine("Enter username:");
                var userName = Console.ReadLine();
                Console.WriteLine("Enter password:");
                var password = Console.ReadLine();
                
                var isExisting = customerController.Login(userName, password);
                if(string.IsNullOrWhiteSpace (isExisting))
                {
                    Console.WriteLine("New user Created, chagne password");
                    var newPassword = Console.ReadLine();
                    var isSuccess=customerController.ChangePassword(userName, password);
                    if(isSuccess)
                        Console.WriteLine("New user created");
                }
                var input =0;
                do 
                {
                    Console.Clear();
                    Console.WriteLine("**********************************");
                    Console.WriteLine(" 1 Create Event");
                    Console.WriteLine(" 2 Invite to Event");
                    Console.WriteLine(" 3 Modify Event");                   
                    Console.WriteLine(" 4 BuyGiftForInvitation");                   
                    Console.WriteLine(" 5 RemoveGiftForInvitation");
                    Console.WriteLine(" 6 Respond to Invite");
                    Console.WriteLine(" 7 Change Password");
                    Console.WriteLine(" 8 Exit");
                    Console.WriteLine("**********************************");
                    input = Convert.ToChar(Console.ReadKey());
                    switch(input)
                    {
                        case '1':
                            var result = DisplayEvents();
                            var occasion = GetOccassionInputs(result, logger);
                            Console.WriteLine("Enter place for the event");
                            var place = Console.ReadLine();
                            Console.WriteLine("Enter date for the event");
                            var date = DateTime.Parse(Console.ReadLine());
                            customerController.CreateEvent(occasion, place, date, new MarriageGift.Model.GiftModel.GiftCollection(),new MarriageGift.Model.GiftModel.GiftCollection());                          
                            break;
                        case '2':
                            var custId = (Customer)GetObjectBasedOnName(customerDao);
                            var events = (Event)GetObjectBasedOnNameFromCurrentScope(customer, "events");
                            customerController.InvitePerson(events, custId);                            
                            break;
                        case '3':
                            var getEventToBeModified =(IEvent)GetObjectBasedOnNameFromCurrentScope(customer, "events");
                            var modfiedEvent = GetModifiedEvent(getEventToBeModified);
                            customerController.ModifEvent(modfiedEvent);
                            break;  
                        case '4':
                            var getInviteInQuestion = (IInvitation)GetObjectBasedOnNameFromCurrentScope(customer, "invite");
                            var gift = GetObjectBasedOnNameFromCurrentScope(getInviteInQuestion, "gift");
                            customerController.BuyGiftForEvent(getInviteInQuestion, gift.getId());
                            break;
                        case '5':
                            var getInviteInQuestion1 = (IInvitation)GetObjectBasedOnNameFromCurrentScope(customer, "invite");
                            var gift1 = GetObjectBasedOnNameFromCurrentScope(getInviteInQuestion1, "gift");
                            customerController.RemoveGiftForEvent(getInviteInQuestion1, gift1.getId());
                            break;
                        case '6':
                            var getInviteInQuestion2 = (IInvitation)GetObjectBasedOnNameFromCurrentScope(customer, "invite");
                            Console.WriteLine("Respond to invite true/false");
                            Console.WriteLine(getInviteInQuestion2);
                            var response = Convert.ToBoolean(Console.ReadLine().ToLower());
                            customerController.RespondToInvite(getInviteInQuestion2.getId(), response );
                            break;
                        case '7':
                                Console.WriteLine("Change password");
                                var newPassword = Console.ReadLine();
                                var isSuccess=customerController.ChangePassword(userName, password);
                            break;
                    }
                    if(input=='8')
                        break;
                }
                while(input<57 && input>48);
            }
                
                Console.WriteLine("Hello World!");
        }
        public static IEvent GetModifiedEvent(IEvent eventInQ)
        {
            Console.WriteLine("Enter new place, leave empty for old");
            var place = Console.ReadLine();
            Console.WriteLine("Enter new date(yyyyMMdd), leave empty for old");
            var date = DateTime.ParseExact(Console.ReadLine(),"yyyyMMdd", CultureInfo.InvariantCulture);
            if(!string.IsNullOrWhiteSpace(place))
            {
                ((Event)eventInQ).ModifyPlace(place);
            }
            if(!string.IsNullOrWhiteSpace(place))
            {
                ((Event)eventInQ).ModifyDate(date);
            }
            return eventInQ;
        }
        public static IBaseObject GetObjectBasedOnName(IGenericDao customerDao)
        {
            Console.WriteLine("Enter name of customer you want to find");
            var name = Console.ReadLine();
            var listOfCustomers = customerDao.GetListOfObjectsByName(name);
            int i=0;
            Dictionary<int,IBaseObject> tempDict = new Dictionary<int, IBaseObject>();
            foreach(var customer in listOfCustomers.GetUnderlyingDictionary().Values)
            {
                tempDict.Add(i, customer);
                Console.WriteLine($"{i} : {customer}");

            }
            var choice = Convert.ToInt32(Console.ReadLine());
            if(tempDict.Count>=choice )
                return null;
            else
                return tempDict[choice];
            
        }
         public static IBaseObject GetObjectBasedOnNameFromCurrentScope(IBaseObject object1, string key)
        {
            Console.WriteLine("Enter name of customer you want to find");
            var name = Console.ReadLine();
            IEnumerable<IBaseObject> listOfCustomers=null;           
            if(object1 is ICustomer)
            {
                    var ObjInQuestion= (ICustomer)object1;
                    if(key=="events")
                        listOfCustomers = ObjInQuestion.GetMyEvents().GetUnderlyingDictionary().Values.Where(x=> ((Event)x).Place.Contains(name));
                    else  
                        listOfCustomers = ObjInQuestion.GetMyInvitations().GetUnderlyingDictionary().Values.Where(x=> ((Invitation)x).getId().Contains(name));
            }
               
            else if(object1 is IInvitation)
            {
                    var ObjInQuestion = (IInvitation)object1;
                    listOfCustomers = ObjInQuestion.GetExpectedGiftsForEvent().GetUnderlyingDictionary().Values.Where(x => ((Gift)x).Name.Contains(name));
            }
                
            
            
            int i=0;
            Dictionary<int,IBaseObject> tempDict = new Dictionary<int, IBaseObject>();
            foreach(var element in listOfCustomers)
            {
                tempDict.Add(i, element);
                Console.WriteLine($"{i} : {element}");

            }
            var choice = Convert.ToInt32(Console.ReadLine());
            if(tempDict.Count>=choice )
                return null;
            else
                return tempDict[choice];
            
        }
        public static Occasion  DisplayEvents()
        {
            var input = 1;
            while(input>4 && input<1)
            {
                Console.Clear();
                Console.WriteLine(" 1 Birthday");
                Console.WriteLine(" 2 Marriage");
                Console.WriteLine(" 3 HouseWarming");
                var choice = Console.ReadKey();
                input = Convert.ToInt16(choice.KeyChar);
            }
            return (Occasion)input;            
        }
        public static IOccassion GetOccassionInputs(Occasion occasion, ILog logger)
            {
            IOccassion newOccassion=null;
            string name1 = string.Empty, name2 = string.Empty;
            switch (occasion)
            {
               
                case Occasion.Birthday:
                    Console.WriteLine("Get birthyguy name");
                     name1 = Console.ReadLine();
                    newOccassion = new Birthday(name1);
                    break;
                case Occasion.HouseWarming:
                    Console.WriteLine("Get houseonwers name");
                     name1 = Console.ReadLine();
                    newOccassion = new HouseWarming(name1);
                    break;
                case Occasion.Marriage:
                    Console.WriteLine("Get bride and grooms name");
                    name1 = Console.ReadLine();
                    name2 = Console.ReadLine();
                    newOccassion = new Marriage(name1, name2);
                    break;
            }
            return newOccassion;
            
        }
        
        /*
        public static IEvent GetMyEvents(ICustomer customer)
        {
            var events = customer.GetMyEvents();
            var eventId = SelectIdFromListofCollection(events);


        }
        public static string SelectIdFromListofCollection(IGenericCollection<IBaseObject> genericCollection )
        {
            return string.Empty;
        }
        public static string DisplayUsersForInvites(ICustomerCollection customerCollection)
        {
            var collection = (CustomerCollection)customerCollection;
            int i = 0;
            var localDict = new Dictionary<int, string>();
            foreach(var keys in collection.CustomerCollectionInner.Keys)
            {
                Console.WriteLine(i + " : " + collection.CustomerCollectionInner[keys].ToString());
                localDict.Add(i, keys);
                if (i>9)
                    break;
            }
            var key = (int)Console.ReadKey().KeyChar;
            return localDict[key];

        }
        
        */
      
    }
}
