using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using log4net;
using MarriageGift.Model.GiftModel;
using MarriageGift.Controller.Interfaces;
using MarriageGift.Model;

namespace MarriageGiftAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerActionController : ControllerBase
    {

        private readonly ILog logger;
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
        public Customer GetCustomerByCustomerName(string customerName)
        {
          return customerObj;
        }

        [HttpPost("login")]
        public ActionResult<Customer> Login(Customer customer)
        {
            var custId = customerController.Login(customer.username, customer.password);
            var result = selectionController.GetCustomerById(custId).Split('|');
            logger.InfoFormat("result 0 {0} result 1 {1}", result[0],result[1]);
            customerObj = new Customer(result[0], result[1], customer.password);
            return CreatedAtAction(nameof(GetCustomerByCustomerName),
                   new { customerName =result[0] }, customerObj);
        }

    }

}
