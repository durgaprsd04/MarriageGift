using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.OccasionModel
{
  public  class HouseWarming :Occasions
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

        public string Owner { get => owner;}

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var occassion = occassionItem as HouseWarming;
            if (occassion == null)
                throw new ArgumentNullException("occasionItem");
            
            owner = occassion.owner;
            return true;
        }

    }
}
