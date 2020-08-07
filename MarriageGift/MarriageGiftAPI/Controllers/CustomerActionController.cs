using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using log4net;
using MarriageGift.Model.GiftModel;
using MarriageGift.Controller.Interfaces;
using MarriageGift.Model.CustomerModel;

namespace MarriageGiftAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerActionController : ControllerBase
    {

        private readonly ILog logger;
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
        [HttpPost("login")]
        public ActionResult<string> Login(Customer customer)
        {
            var custId = customerController.Login(customer.GetUserName(), customer.GetPassword());
            return  selectionController.GetCustomerById(custId);            
        }

    }

}