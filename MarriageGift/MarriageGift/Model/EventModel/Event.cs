using System;
using MarriageGift.Model.Interfaces;
using MarriageGift.Model.GiftModel;

namespace MarriageGift.Model.EventModel
{

    public class Event : IEvent
    {
        private readonly IOccassion occassion;
        private string place;
        private string  eventId;
        private DateTime date;
        private bool isCanceled;
        private readonly string custId;

        private IGiftCollection<IGift> giftsRecieved;
        private IGiftCollection<IGift> giftsExpected;

        public string EventId { get => eventId; set => eventId = value; }

        public string CustId => custId;

        public Event(IOccassion occassion, string place, DateTime date, IGiftCollection<IGift> giftsExpected, string custId)
        {
            eventId = Guid.NewGuid().ToString();
            this.occassion =occassion;
            this.date =date;
            this.place =place;
            this.custId=custId;
            this.giftsExpected=giftsExpected;
            isCanceled =false;
        }

        public bool Cancel()
        {
            isCanceled=true;
            return true;
        }

        public bool ModifyDate(DateTime newDate)
        {
            date =newDate;
            return true;
        }

        public bool ModifyPlace(string newPlace)
        {
            place = newPlace;
            return true;
        }
        public bool AddExpectedGift(IGift gift)
        {
            var result = giftsExpected.AddGift(gift);

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
            var result2 = giftsRecieved.AddGift(gift);
            return result1&result1;
        }
        public bool RemoveRecievedGifts(IGift gift)
        {
            var result1 = giftsRecieved.RemoveGift(gift);
            var result2 = giftsExpected.AddGift(gift);
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