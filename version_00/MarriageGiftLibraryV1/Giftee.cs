using System.Collections.Generic;

namespace MarriageGiftLibraryV1
{
    public class Giftee:IGiftee
    {
        private int id ;
        private string name;
        public Giftee(int id, string name)
        {
            this.id=id;
            this.name=name;
        }
        public override string ToString()
        {
            return $"Giftee {id} :{name}";
        }
        List<Gift> listOfGifts = new List<Gift>();
        public void Recieve(Gift gift)
        {
            listOfGifts.Add(gift);
        }
        public  List<Gift> GetAllRecievedGifts()
        {
            return listOfGifts;
        }
    }
}