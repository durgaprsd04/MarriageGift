using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.OccasionModel
{
    public class Birthday:Occasions
    {
        private string person;
        private readonly ILog logger;

        public string Person { get => person; }

        public Birthday(string person, ILog logger)
        {
            occasionId = Guid.NewGuid().ToString();
            this.person = person;
            this.logger = logger;
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
