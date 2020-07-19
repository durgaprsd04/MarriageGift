using System.Collections.Generic;
namespace MarriageGift.Model
{
    public interface IGenericCollection<T>
    {
        T GetItem(string id);
        bool Add(T baseObject);
        bool Remove(T baseObject);
        void Clear();
        Dictionary<string, T> GetUnderlyingDictionary();
    }
}
