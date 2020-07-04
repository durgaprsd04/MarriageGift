using System;
using System.Collections.Generic;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.Model.EventModel
{

    public class Event : IEvent
    {
        private readonly IOccassion occassion;
        private string place;
        private string  eventId;
        private DateTime date;
        private bool isCanceled;

        private IGiftCollection giftsRecieved;
        private IGiftCollection giftsExpected;

        public string EventId { get => eventId; set => eventId = value; }

        public Event(IOccassion occassion, string place, DateTime date, IGiftCollection giftsExpected)
        {
            eventId = Guid.NewGuid().ToString();
            this.occassion =occassion;
            this.date =date;
            this.place =place;
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
            var result = giftsRecieved.AddGift(gift);
            return result;
        }
        public bool RemoveRecievedGifts(IGift gift)
        {
            var result = giftsRecieved.RemoveGift(gift);
            return result;
        }

    }

}