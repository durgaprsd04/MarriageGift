using MarriageGift.Model;

namespace MarriageGift.FAO.Interfaces
{
    public interface ISaveToFileFao
    {
        void SaveObject(IBaseObject baseObject);
        void WriteRecords(string record);
    }
}
