using System;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.OccasionModel
{
    class Birthday:Occasions
    {
        private string person;
        public Birthday(string person)
        {
            occasionId = Guid.NewGuid().ToString();
            this.person = person;
            occasion = Enums.Occasion.Birthday;
        }

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var successFlag = false;
            var occassion = occassionItem as Birthday;
            if (occassion == null)
                throw new ArgumentException("occasionItem");
            try
            {
                person = occassion.person;
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
