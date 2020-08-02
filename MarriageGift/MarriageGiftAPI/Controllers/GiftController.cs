using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using MarriageGift.Model.GiftModel;
using MarriageGift.Enums;
using MarriageGift.Controller;
using MarriageGift.Controller.Interfaces;
using MarriageGift.FAO;
using log4net;

namespace MarriageGiftAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiftController : ControllerBase
    {

      private readonly ILogger<GiftController> _logger;
      private static List<Gift> listofGifts = new List<Gift>();
      private IAdminController admin;

              public GiftController(ILogger<GiftController> logger)
              {
                  _logger = logger;
                  var loggertest = LogManager.GetLogger(typeof(GiftController));
                  var saveToFileFAO = new SaveToFileFao(loggertest,new FileStream("result.csv",FileMode.Create), new StreamWriter("result.dat" ,true));
                  admin = new AdminActionController(saveToFileFAO, loggertest);
              }

              [HttpGet]
              public IEnumerable<Gift> Get()
              {

                  var gift = new Gift("plates",GiftItemType.Crockery,150);
                  listofGifts.Add(gift);
                  return listofGifts;
              }
              [HttpPost]
              public ActionResult<Gift> PostGiftItem(Gift gift)
              {
                  listofGifts.Clear();
                  var newGift = new Gift (gift.Name, gift.GiftItemType, gift.Price);
                  listofGifts.Add(newGift);
                  admin.CreateGift(newGift);
                  return CreatedAtAction("Get",newGift , gift);
              }
              [HttpPost("About")]
              public ActionResult<List<Gift>> PostGiftItem1(Gift gift)
              {
                  listofGifts.Clear();
                var gift1 = new Gift("plates",GiftItemType.Crockery,150);
                  listofGifts.Add(gift1);
                  var newGift = new Gift (gift.Name, gift.GiftItemType, gift.Price);
                  listofGifts.Add(newGift);
                  admin.CreateGift(newGift);
                  return listofGifts;
              }
              
    }
}
