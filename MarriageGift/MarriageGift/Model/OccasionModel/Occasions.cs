using System;
using MarriageGift.Enums;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.OccasionModel
{
    public abstract class Occasions : IOccassion
    {
        protected Occasion occasion;
        protected string occasionId;
        public abstract bool modifyOccasion(IOccassion occassion); 
    }
}
