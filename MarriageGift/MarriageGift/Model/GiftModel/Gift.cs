using System;
using MarriageGift.Enums;
using MarriageGift.Model.Interfaces;
using log4net;
using Newtonsoft.Json;

namespace MarriageGift.Model.GiftModel
{
    public class Gift: BaseObject, IGift
    {
        private string name;
        private GiftItemType giftItemType;
        private double price;


        public string Name { get => name; set => name=value;}
        public GiftItemType GiftItemType { get => giftItemType; set => giftItemType=value;}
        public double Price { get => price; set => price=value;}

        // TODO need to find  abetter solution
        public Gift(string giftId, string name, GiftItemType giftItemType, double price)
        :base(giftId)
        {
            this.name = name;
            this.giftItemType = giftItemType;
            this.price = price;
        }
        [JsonConstructor]
        public Gift(string name, GiftItemType giftItemType, double price)
        :base()
        {
            this.name = name;
            this.giftItemType = giftItemType;
            this.price = price;
        }
        public Gift(Gift gift)
        :this(gift.getId(),gift.name, gift.giftItemType, gift.price)
        {

        }
        public void ModifyGift( IGift giftItem)
        {
            var gift = giftItem as Gift;
            if (gift == null)
                throw new ArgumentException("giftItem");
            name = gift.name;
            giftItemType = gift.giftItemType;
            price = gift.price;
        }
        public int CompareTo(object obj)
        {
            var gift = obj as Gift;
            if (gift != null)
                return string.Compare(gift.getId(), base.getId());
            return -1;
        }

        public string GetGiftId()
        {
            return this.getId();
        }
        public override string ToString()
        {
          return this.getId()+"|"+name+"|"+giftItemType.ToString()+"|"+price;
        }
    }
}
