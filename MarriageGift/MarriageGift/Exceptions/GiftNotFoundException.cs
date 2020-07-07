using System;
namespace MarriageGift.Exceptions
{
    public class GiftNotFoundException:Exception
    {
        public GiftNotFoundException(string giftId):base(string.Format("Gift id {0} not found ",giftId))
        {

        }

    }
}
