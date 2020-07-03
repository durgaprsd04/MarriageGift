using System;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.OccasionModel
{
    class HouseWarming :Occasions
    {
        private string owner;
        public HouseWarming(string owner)
        {
            occasionId = Guid.NewGuid().ToString();
            this.owner = owner;
            occasion = Enums.Occasion.HouseWarming;
        }

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var successFlag = false;
            var occassion = occassionItem as HouseWarming;
            if (occassion == null)
                throw new ArgumentException("occasionItem");
            try
            {
                owner = occassion.owner;
                successFlag = true;
            }
            catch (Exception e)
            {
                //ignored
            }
            return successFlag;
        }

    }
}
