using System;
using MarriageGiftLibraryV1;
namespace MarriageGiftV1
{
    class Program
    {
        static void Main(string[] args)
        {
            var gift = new Gift("mixie");
            var gift1 = new Gift("washing machine");
            var giftee = new Giftee(1,"Ramu");
            var gifter = new Gifter("Sabu");
            gifter.SendGift(gift,giftee);
            gifter.SendGift(gift1, giftee);
            var res = giftee.GetAllRecievedGifts();
            foreach(var g in res)
            {
                Console.WriteLine(g);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
