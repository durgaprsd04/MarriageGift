using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.OccasionModel
{
    public class Birthday:Occasions
    {
        private string person;       

        public string Person { get => person; }

        public Birthday(string person)
        :base()
        {            
            this.person = person;           
            occasion = Enums.Occasion.Birthday;
        }
        public Birthday(string id, string person)
        :base(id)
        {            
            this.person = person;           
            occasion = Enums.Occasion.Birthday;
        }

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var occassion = occassionItem as Birthday;
            if (occassion == null)
                throw new ArgumentNullException("occasionItem");
            
            person = occassion.person;
            return true ;
        }
    }
}
