using System;
using MarriageGift.Enums;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.GiftModel
{
    public class Gift: IGift
    {
        private readonly string giftId;
        private string name;
        private GiftItemType giftItemType;
        private double price;
        private readonly ILog logger;

        public string GiftId
        {
            get
            {
                return giftId;
            }
        }

        public string Name { get => name;}
        public GiftItemType GiftItemType { get => giftItemType;}
        public double Price { get => price; }

        // TODO need to find  abetter solution
        public Gift(string guid, string name, GiftItemType giftItemType, double price, ILog logger)
        {
            giftId = guid;
            this.name = name;
            this.giftItemType = giftItemType;
            this.price = price;
            this.logger = logger;
        }
        public Gift(string name, GiftItemType giftItemType, double price, ILog logger)
        {
            giftId = Guid.NewGuid().ToString();
            this.name = name;
            this.giftItemType = giftItemType;
            this.price = price;
            this.logger = logger;
        }
        public Gift(IGift gift)
        {
            var gift1 = gift as Gift;
            giftId = Guid.NewGuid().ToString();
            name = gift1.name;
            giftItemType = gift1.giftItemType;
            price = gift1.price;
        }
        public bool ModifyGift( IGift giftItem)
        {
            var successFlag = false;
            try
            {
                var gift = giftItem as Gift;
                if (gift == null)
                    throw new ArgumentException("giftItem");       
                name = gift.name;
                giftItemType = gift.giftItemType;
                price = gift.price;
                successFlag = true;
            }
            catch(Exception e)
            {                
               logger.Error("Exception occured while modifying Gift"+e.Message);
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
        public string GetGiftId()
        {
            return giftId;
        }
    }
}
