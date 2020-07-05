namespace MarriageGift.Model.Interfaces
{
    public interface IGiftCollection<IGift>
    {
        bool AddGift(IGift gift);
        bool RemoveGift(IGift gift);
        IGift GetGiftById(string giftId);
        int Count();
    }
}
