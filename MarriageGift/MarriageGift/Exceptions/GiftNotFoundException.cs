using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Exceptions
{
    public class GiftNotFoundException:Exception
    {
        public GiftNotFoundException(string giftId):base(string.Format("Gift id {} not found ",giftId))
        {

        }

    }
}
