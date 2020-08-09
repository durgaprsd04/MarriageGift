using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.OccasionModel
{
  public  class HouseWarming :Occasions
    {
        private string owner;
       
        public HouseWarming(string owner)
        :base()
        {           
            this.owner = owner;            
            occasion = Enums.Occasion.HouseWarming;
        }
         public HouseWarming(string id, string owner)
        :base(id)
        {           
            this.owner = owner;            
            occasion = Enums.Occasion.HouseWarming;
        }
        public string Owner { get => owner;}

        public override string GetPerson()
        {
            return Owner+"|empty";
        }

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
