using System;
using System.Collections.Generic;
using Autofac;
using System.IO;
using MarriageGift.Model;
using MarriageGift.Model.InvitationModel;
using MarriageGift.Model.EventModel;
using MarriageGift.Model.CustomerModel;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.OccasionModel;
using MarriageGift.Enums;
using MarriageGift.Controller;
using MarriageGift.Controller.Interfaces;
using log4net;

namespace MarriageGiftConsoler
{
    class Program
    {
        static void Main(string[] args)
        {
            /* var builder = new ContainerBuilder();
            ILog logger = LogManager.GetLogger(typeof(Program));
            CustomerActionController customerActionController = new CustomerActionController();
            builder.RegisterType<InvitationCollection>().As<IInvitationCollection>();
            builder.RegisterType<EventCollection>().As<IEventCollection>();
            builder.RegisterType<StreamWriter>().WithParameter("customer.txt", true);
            builder.RegisterType<SaveToFileController>().As<ISaveToFileController>();
            builder.Register(c=> LogManager.GetLogger(typeof(Customer))).As<ILog>();
            builder.RegisterType<Customer>().As<ICustomer>().WithParameter("userName", "Jeff");
            
            var Container = builder.Build();
            using (var scope = Container.BeginLifetimeScope())
            {
                var customer = (Customer)scope.Resolve<ICustomer>();
                customer.SaveToFile();
                var input = 1;
                while(input>10 && input<1 )
                {
                    Console.WriteLine("**********************************");
                    Console.WriteLine(" 1 AddMyEvents");
                    Console.WriteLine(" 2 Invite CustToEvent");
                    Console.WriteLine(" 3 CancelEvent");
                    Console.WriteLine(" 4 ChangeEventTime");
                    Console.WriteLine(" 5 ChangeEventVenue");
                    Console.WriteLine(" 6 RespondToInvitation");
                    Console.WriteLine(" 7 BuyGiftForInvitation");
                    Console.WriteLine(" 8 ModifyGiftForInvitation");
                    Console.WriteLine(" 9 RemoveGiftForInvitation");
                    Console.WriteLine(" 0 Exit");
                    Console.WriteLine("**********************************");

                    var choice = Console.ReadKey();
                    input = Convert.ToInt16(choice.KeyChar);
                    if(input ==1)
                    {
                        var result = DisplayEvents();
                        var occasion = GetOccassionInputs(result, logger);
                        Console.WriteLine("Enter place for the event");
                        var place = Console.ReadLine();
                        Console.WriteLine("Enter date for the event");
                        var date = DateTime.Parse(Console.ReadLine());
                        var giftcollection1 = customerActionController.GetAvailableGiftCollection(logger);
                        var giftcollection2= customerActionController.GetAvailableGiftCollection(logger);
                        var eventInQuestion= new Event(occasion, place, date, giftcollection1, giftcollection2, customer.CustId, logger);
                        customer.AddMyEvents(eventInQuestion);
                    }
                    else if(input ==2)
                    {

                        /* var regex = Console.ReadLine(); 
                        var listOfCustomers = customerActionController.GetAllCustomers(regex);
                        if(listOfCustomers.Count()==0)
                        {
                            Console.WriteLine("No customer found");
                            break;
                        }
                        var userId = DisplayUsersForInvites(listOfCustomers);
                        var userInQuestion = listOfCustomers.GetCustomer(userId);
                        listOfCustomers.Clear();
                        listOfCustomers.AddCustomer(userInQuestion);
                        var eventToBeInvitedFor = GetMyEvents(customer);  
                        var invitation = new Invitation(customer.CustId, ) 
                    }
                }
            } */
            Console.WriteLine("Hello World!");
        }/*
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
                    newOccassion = new Birthday(name1, logger);
                    break;
                case Occasion.HouseWarming:
                    Console.WriteLine("Get houseonwers name");
                     name1 = Console.ReadLine();
                    newOccassion = new HouseWarming(name1, logger);
                    break;
                case Occasion.Marriage:
                    Console.WriteLine("Get bride and grooms name");
                    name1 = Console.ReadLine();
                    name2 = Console.ReadLine();
                    newOccassion = new Marriage(name1, name2, logger);
                    break;
            }
            return newOccassion;
            
        }*/
      
    }
}
