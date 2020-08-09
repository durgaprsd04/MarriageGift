using System;
using System.Collections.Generic;
using System.Text;
using MarriageGift.Enums;
namespace MarriageGift.Model.Interfaces
{
    public interface IOccassion:IBaseObject
    {
        bool modifyOccasion(IOccassion occassion);
        Occasion GetOccassionType();
        string GetPerson();
    }
}
