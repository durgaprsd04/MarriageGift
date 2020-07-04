namespace MarriageGift.Model.Interfaces
{
    public interface IGiftCollection
    {
        bool AddGift(IGift gift);
        bool RemoveGift(IGift gift);
    }
}
