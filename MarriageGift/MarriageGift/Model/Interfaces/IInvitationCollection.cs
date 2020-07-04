namespace MarriageGift.Model.Interfaces
{
    public interface IInvitationCollection
    {
        bool AddInvitation(IInvitation invitation);
        bool RemoveInvitation(IInvitation invitation);
        IInvitation GetInvitationById(string inviationId);
    }
}
