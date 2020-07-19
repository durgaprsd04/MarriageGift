using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.Model
{
    public abstract class BaseObject : IBaseObject
    {
        private readonly string id;

        protected BaseObject()
        {
            id = Guid.NewGuid().ToString();
        }
         protected BaseObject(string id)
        {
            this.id = id;
        }
        public string getId()
        {
            return id;
        }
        
    }
}
