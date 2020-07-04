using System;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.GiftModel
{
    public class PresentableGift :Gift
    {
        private string presenter;
        private readonly string presentableGiftId;

        public string PresentableGiftId => presentableGiftId;

        public PresentableGift(string presenter, IGift gift)
               :base(gift)
        {
            presentableGiftId = Guid.NewGuid().ToString();
            this.presenter = presenter;
        }

    }
}
