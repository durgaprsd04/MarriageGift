namespace MarriageGift.Model.Interfaces
{
    public interface IGiftCollection<IGift> : IGenericCollection<IBaseObject>
    {
        bool AddGift(IGift gift);
        bool RemoveGift(IGift gift);
        IGift GetGift(string giftId);       
    }
}
