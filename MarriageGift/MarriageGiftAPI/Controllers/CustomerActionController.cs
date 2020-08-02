using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using log4net;
using MarriageGift.Model.GiftModel;
using MarriageGift.Controller.Interfaces;
using MarriageGift.Enums;

namespace MarriageGiftAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerActionController : ControllerBase
    {

        private readonly ILog logger;
        private static List<Gift> listofGifts = new List<Gift>();
        private ICustomerController customerController;

        public CustomerActionController(ICustomerController customerController, ILog logger)
        {
            this.logger=logger;
            this.customerController=customerController;
        }
        [HttpGet]
        public IEnumerable<Gift> Get()
        {

            var gift = new Gift("plates",GiftItemType.Crockery,150);
            listofGifts.Add(gift);
            return listofGifts;
        }
    }

}