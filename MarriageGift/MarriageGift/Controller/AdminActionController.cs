using MarriageGift.Model.Interfaces;
using MarriageGift.FAO.Interfaces;
using MarriageGift.Controller.Interfaces;
using log4net;
namespace MarriageGift.Controller
{
    public class AdminActionController :IAdminController
    {
      private readonly ISaveToFileFao saveToFileFAO;
      private readonly ILog logger;
      public AdminActionController(ISaveToFileFao saveToFileFAO,ILog logger )
      {
        this.saveToFileFAO=saveToFileFAO;
        this.logger=logger;
      }
      public void CreateGift(IGift gift)
      {
          saveToFileFAO.WriteRecords(gift);
      }
    }
}
