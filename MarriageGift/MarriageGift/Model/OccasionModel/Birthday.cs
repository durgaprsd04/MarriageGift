using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.OccasionModel
{
    class Birthday:Occasions
    {
        private string person;
        private readonly ILog logger;
        public Birthday(string person, ILog logger)
        {
            occasionId = Guid.NewGuid().ToString();
            this.person = person;
            this.logger = logger;
            occasion = Enums.Occasion.Birthday;
        }

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var successFlag = false;
            try
            {
                var occassion = occassionItem as Birthday;
                if (occassion == null)
                    throw new ArgumentException("occasionItem");
            
                person = occassion.person;
                successFlag = true;
            }
            catch (Exception e)
            {
                logger.Error("Exception while moidfying person name"+e.Message);
                logger.Error(e.Message);
            }
            return successFlag;
        }
    }
}
