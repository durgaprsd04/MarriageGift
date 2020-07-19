using System.Collections.Generic;

namespace MarriageGift.Model
{
    public abstract class GenericCollection : IGenericCollection<IBaseObject>
    {
        protected Dictionary<string, IBaseObject> underlyingCollection;

        protected GenericCollection()
        {
            this.underlyingCollection = new Dictionary<string, IBaseObject>();
        }
        public bool Add(IBaseObject baseObject)
        {
            underlyingCollection.Add(baseObject.getId(), baseObject);
            return true;
        }
        public bool Remove(IBaseObject baseObject)
        {
            var succesFlag = false;
            if (underlyingCollection.ContainsKey(baseObject.getId()))
            {
                underlyingCollection.Remove(baseObject.getId());
                succesFlag = true;
            }
            return succesFlag;
        }
        public IBaseObject GetItem(string id)
        {
            return underlyingCollection[id];
        }
        
        public int Count()
        {
            return underlyingCollection.Count;
        }

        public void Clear()
        {
            underlyingCollection.Clear();
        }

        public Dictionary<string, IBaseObject> GetUnderlyingDictionary()
        {
           return underlyingCollection;
        }
    }
}
