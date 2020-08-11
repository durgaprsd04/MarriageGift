using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using log4net;
using MarriageGift.Model.GiftModel;
using MarriageGift.Model.Interfaces;
using MarriageGift.Controller.Interfaces;
using System;
using MarriageGift.Model;
using System.Globalization;

namespace MarriageGiftAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerActionController : ControllerBase
    {

        private readonly ILog logger;
        private static MarriageGift.Model.Interfaces.ICustomer loggedInCustomer;
        private static Customer customerObj;
        private static List<Gift> listofGifts = new List<Gift>();
        private ICustomerController customerController;
        private ISelectionController selectionController;

        public CustomerActionController(ICustomerController customerController, ILog logger, ISelectionController selectionController)
        {
            this.logger=logger;
            this.customerController=customerController;
            this.selectionController =selectionController;
        }
        [EnableCors("policy1")]
        [HttpGet("occassionTypes")]
        public Dictionary<int,string> Get()
        {
            return selectionController.GetOccasionTypes();
        }
        [EnableCors("policy1")]
        [HttpGet("allgifts")]
        public IDictionary<string,string> GetAllGifts()
        {
            return selectionController.GetAllGifts();
        }

        [EnableCors("policy1")]
        [HttpGet("{customerName}",  Name="GetCustomerByCustomerName")]
        public ICustomer GetCustomerByCustomerName(string customerName)
        {
          return loggedInCustomer;
        }
        [EnableCors("policy1")]
        [HttpGet("customer_id")]
        public IGenericCollection<IBaseObject> GetCustomerByCustomerId(string customer_id)
        {
          //test included comments
          return loggedInCustomer.GetMyEvents();
        }
        [HttpPost("login")]
        public ActionResult<ICustomer> Login(Customer customer)
        {
            loggedInCustomer = customerController.Login(customer.username, customer.password);
            logger.InfoFormat("result 0 {0} result 1 {1}", loggedInCustomer.getId() ,loggedInCustomer.GetUserName());
            return CreatedAtAction(nameof(GetCustomerByCustomerName),
                   new { customerName =loggedInCustomer.GetUserName()}, loggedInCustomer);
        }
        [HttpPost("createEvent")]
        public ActionResult<string> CreateEvent(Event event1)
        {
            customerController.SetCustomer(loggedInCustomer);
            logger.Info("Came here");
            var format="yyyy-MM-dd HH:mm";
            var giftE = selectionController.GetGiftsForGiftIds(event1.giftIds);
            var giftR =  new GiftCollection();
            var date = DateTime.ParseExact(event1.date+" "+event1.time,format, CultureInfo.InvariantCulture);
            logger.Info("date is formateed to "+date.ToString());
            IOccassion occassionInQ =  selectionController.GetOccassion(event1.occassionType, event1.person1, event1.person2);
            customerController.CreateOccassion(occassionInQ);
            var result = customerController.CreateEvent(occassionInQ, event1.place,date, giftE, giftR );
            customerController.AddToExpectedGifts(giftE,result);
            return result;
        }
    }
}
