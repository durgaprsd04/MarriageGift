using System;
using System.Collections.Generic;
using System.Text;

namespace MarriageGift.DAO.Interfaces
{
    public interface IGiftDao:IGenericDao
    {
        IDictionary<string, string> GetAllGifts();
    }
}
