using System;

namespace MarriageGiftLibraryV1
{
    public interface IGiftee :IUser
    {
        void Recieve(Gift gift);
    }
}
