using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.OccasionModel
{
   public class Marriage : Occasions
    {
        private string bride, groom;
        
        public Marriage(string bride, string groom)
        :base()
        {           
            this.bride = bride;
            this.groom = groom;
            occasion = Enums.Occasion.Marriage;
        }
        public Marriage(string id, string bride, string groom)
        :base(id)
        {           
            this.bride = bride;
            this.groom = groom;
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
        public override string GetPerson()
        {
            return bride+"|"+groom;
        }
    }
}
