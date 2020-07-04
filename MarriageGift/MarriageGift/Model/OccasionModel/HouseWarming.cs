using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.OccasionModel
{
    class HouseWarming :Occasions
    {
        private string owner;
        private ILog logger;
        public HouseWarming(string owner,ILog logger)
        {
            occasionId = Guid.NewGuid().ToString();
            this.owner = owner;
            this.logger=logger;
            occasion = Enums.Occasion.HouseWarming;
        }

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var successFlag = false;
            try
            {
                var occassion = occassionItem as HouseWarming;
                if (occassion == null)
                    throw new ArgumentException("occasionItem");
            
                owner = occassion.owner;
                successFlag = true;
            }
            catch (Exception e)
            {
                logger.Error("Error occured while modifying person name"+e.Message);
                logger.Error(e.Message);
            }
            return successFlag;
        }

    }
}
