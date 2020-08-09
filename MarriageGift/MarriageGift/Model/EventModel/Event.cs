using System;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.GiftModel;
using Newtonsoft.Json;
namespace MarriageGift.Model.EventModel
{

    public class Event : BaseObject, IEvent
    {
        private readonly IOccassion occassion;
        private string place;
        private DateTime date;
        private bool isCanceled;
        private readonly string custId;
        private IGiftCollection<IGift> giftsRecieved = new GiftCollection();
        private IGiftCollection<IGift> giftsExpected= new GiftCollection();
        public string CustId => custId;

        public bool IsCanceled { get => isCanceled; set => isCanceled = value; }
        public DateTime Date { get => date; }
        public string Place { get => place; }

        public IOccassion Occassion => occassion;

        [JsonConstructor]
        public Event(IOccassion occassion, string place, DateTime date, string custId)
        : base()
        {
            this.occassion =occassion;
            this.date =date;
            this.place =place;
            this.custId=custId;
            IsCanceled =false;
        }
        public Event(string eventId, IOccassion occassion, string place, DateTime date, IGiftCollection<IGift> giftsExpected, IGiftCollection<IGift> giftsRecieved, string custId, bool isCanceled)
        : base(eventId)
        {
            this.occassion =occassion;
            this.date =date;
            this.place =place;
            this.custId=custId;
            this.giftsExpected=giftsExpected;
            this.giftsRecieved =giftsRecieved;
            IsCanceled =isCanceled;
        }

        public bool Cancel(bool response)
        {
              IsCanceled = response;
            return true;
        }

        public bool ModifyDate(DateTime newDate)
        {
            var result = false;
            try
            {
                date = newDate;
                result = true;
            }
            catch(Exception)
            {
                //logger.Error("Modify of event date failed because of " + e.Message);
            }
            return result;
        }

        public bool ModifyPlace(string newPlace)
        {
            var result = false;
            try
            {
                place = newPlace;
                result = true;
            }
            catch (Exception)
            {
                //logger.Error("Modify of event venue failed because of " + e.Message);
            }
            return result;
        }
        public bool AddExpectedGift(IGift gift)
        {
            var result = giftsExpected.AddGift(gift);
            return result;
        }
        public bool AddExpectedGifts(IGiftCollection<IGift> gifts)
        {
            var result = true;
            foreach (var gift in gifts.GetUnderlyingDictionary().Values)
                result = result && giftsExpected.Add(gift);
            return result;
        }
        public bool RemoveExpectedGift(IGift gift)
        {
            var result  = giftsExpected.RemoveGift(gift);
            return result;
        }
        public bool AddRecievedGifts(IGift gift)
        {
            var result1 =giftsExpected.RemoveGift(gift);
            var result2 = false;
            if (result1)
                result2 = giftsRecieved.AddGift(gift);
            return result1&result1;
        }
        public bool RemoveRecievedGifts(IGift gift)
        {
            var result1 = giftsRecieved.RemoveGift(gift);
            var result2 = false;
            if (result1)
                result2 = giftsExpected.AddGift(gift);
            return result1&&result2;
        }

        public IGiftCollection<IGift> ExpectedGiftCollection()
        {
            return giftsExpected;
        }
        public IGiftCollection<IGift> RecievedGiftCollection()
        {
            return giftsRecieved;
        }
    }

}
