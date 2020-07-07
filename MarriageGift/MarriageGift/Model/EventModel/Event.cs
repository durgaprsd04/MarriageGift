using System;
using MarriageGift.Model.Interfaces;
using log4net;
namespace MarriageGift.Model.EventModel
{

    public class Event : IEvent
    {
        private readonly IOccassion occassion;
        private readonly ILog logger;
        private string place;
        private string  eventId;
        private DateTime date;
        private bool isCanceled;
        private readonly string custId;

        private IGiftCollection<IGift> giftsRecieved;
        private IGiftCollection<IGift> giftsExpected;

        public string EventId { get => eventId; set => eventId = value; }

        public string CustId => custId;

        public bool IsCanceled { get => isCanceled; set => isCanceled = value; }
        public DateTime Date { get => date; }
        public string Place { get => place; }

        public Event(IOccassion occassion, string place, DateTime date, IGiftCollection<IGift> giftsExpected, IGiftCollection<IGift> giftsRecieved, string custId,ILog logger )
        {
            eventId = Guid.NewGuid().ToString();
            this.occassion =occassion;
            this.date =date;
            this.place =place;
            this.custId=custId;
            this.giftsExpected=giftsExpected;
            this.giftsRecieved =giftsRecieved;
            IsCanceled =false;
            this.logger = logger;
        }

        public bool Cancel(bool response)
        {
            var result = false;
            try
            {
                IsCanceled = response;
                result = true;
            }
            catch(Exception e)
            {
                logger.Error("Canceling of event failed because " + e.Message);
                logger.Error(e.StackTrace);
            }
            return result;
        }

        public bool ModifyDate(DateTime newDate)
        {
            var result = false;
            try
            {
                date = newDate;
                result = true;
            }
            catch(Exception e)
            {
                logger.Error("Modify of event date failed because of " + e.Message);
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
            catch (Exception e)
            {
                logger.Error("Modify of event venue failed because of " + e.Message);
            }
            return result;
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