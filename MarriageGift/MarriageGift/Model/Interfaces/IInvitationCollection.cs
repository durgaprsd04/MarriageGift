namespace MarriageGift.Model.Interfaces
{
    public interface IInvitationCollection
    {
        bool AddInvitation(IInvitation invitation,ICustomer customer);
        bool RemoveInvitation(IInvitation invitation);
        IInvitation GetInvitationById(string inviationId);
    }
}
