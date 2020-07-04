using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.OccasionModel
{
   public class Marriage : Occasions
    {
        private string bride, groom;
        private ILog logger;
        public Marriage(string bride, string groom, ILog logger)
        {
            occasionId = Guid.NewGuid().ToString();
            this.bride = bride;
            this.groom = groom;
            this.logger = logger;
            occasion = Enums.Occasion.Marriage;
        }

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var successFlag = false;
            try
            {
                var occassion = occassionItem as Marriage;
                if (occassion == null)
                    throw new ArgumentException("occasionItem");
                
                bride = occassion.bride;
                groom = occassion.groom;
                successFlag = true;
            }
            catch(Exception e)
            {
               logger.Error("Error occured while modifying the occasion"+e.Message);
               logger.Error(e.Message);
            }
            return successFlag;
        }
    }
}
