using MarriageGift.Model.Interfaces;
using MarriageGift.Model;

namespace MarriageGift.FAO.Interfaces
{
    public interface ISaveToFileFao
    {
        void SaveObject(ICustomer baseObject);
        void WriteRecords(IBaseObject record);
    }
}
