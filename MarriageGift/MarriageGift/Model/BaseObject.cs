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
        public string getId()
        {
            return id;
        }
        
    }
}
