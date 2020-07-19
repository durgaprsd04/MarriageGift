using System;
using MarriageGift.Enums;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.OccasionModel
{
    public abstract class Occasions : BaseObject, IOccassion
    {
        public Occasions()
        :base()
        {

        }
        public Occasions(string id)
        :base(id)
        {

        }
        protected Occasion occasion;
        protected string occasionId;
        public abstract bool modifyOccasion(IOccassion occassion); 
    }
}
