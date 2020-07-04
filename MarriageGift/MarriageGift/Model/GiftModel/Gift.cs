using System;
using MarriageGift.Enums;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model
{
    public class Gift: IGift
    {
        private readonly string giftId;
        private string name;
        private GiftItemType giftItemType;
        private double price;

        public string GiftId
        {
            get
            {
                return giftId;
            }
        }

        public Gift(string name, GiftItemType giftItemType, double price)
        {
            giftId = Guid.NewGuid().ToString();
            this.name = name;
            this.giftItemType = giftItemType;
            this.price = price;
        }

        public bool ModifyGift( IGift giftItem)
        {
            var gift = giftItem as Gift;
            if (gift == null)
                throw new ArgumentException("giftItem");
            var successFlag = false;
            try
            {
                name = gift.name;
                giftItemType = gift.giftItemType;
                price = gift.price;
                successFlag = true;
            }
            catch(Exception e)
            {                
                // ignored for now
            }
            return successFlag;
        }

        public int CompareTo(object obj)
        {
            var gift = obj as Gift;
            if (gift != null)
                return string.Compare(gift.GiftId, GiftId);
            return -1;
        }

        
    }
}
