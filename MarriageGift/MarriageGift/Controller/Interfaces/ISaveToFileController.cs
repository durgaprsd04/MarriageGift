using MarriageGift.Model;

namespace MarriageGift.Controller.Interfaces
{
    public interface ISaveToFileController
    {
        void SaveCustomer(IBaseObject customer);
        void WriteRecords(string record);
    }
}
