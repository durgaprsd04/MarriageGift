using System;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.OccasionModel
{
   public class Marriage : Occasions
    {
        private string bride, groom;

        public Marriage(string bride, string groom)
        {
            occasionId = Guid.NewGuid().ToString();
            this.bride = bride;
            this.groom = groom;
            occasion = Enums.Occasion.Marriage;
        }

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var successFlag = false;
            var occassion = occassionItem as Marriage;
            if (occassion == null)
                throw new ArgumentException("occasionItem");
            try
            {
                bride = occassion.bride;
                groom = occassion.groom;
                successFlag = true;
            }
            catch(Exception e)
            {
                //ignored
            }
            return successFlag;
        }
    }
}
