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

        public string Bride { get => bride;}
        public string Groom { get => groom;}

        public override bool modifyOccasion(IOccassion occassionItem)
        {
            var occassion = occassionItem as Marriage;
            if (occassion == null)
                throw new ArgumentNullException("occasionItem");
                
            bride = occassion.bride;
            groom = occassion.groom;
            return true;
        }
    }
}
