using System;

namespace MarriageGiftLibraryV1
{
    public class Gifter:IUser
    {
        private Guid id;
        private string name;
        public Gifter(string name)
        {
            this.name = name;
        }
        public override string ToString()
        {
            return $"Gifter {id}:{name}";
        }
        public void SendGift(Gift gift, IGiftee giftee)
        {
            giftee?.Recieve(gift);
        }
    }
}
