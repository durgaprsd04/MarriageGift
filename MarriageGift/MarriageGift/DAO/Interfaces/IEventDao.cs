using System;
using System.Collections.Generic;
using System.Text;
using MarriageGift.Model.Interfaces;

namespace MarriageGift.DAO.Interfaces
{
    public interface IEventDao : IGenericDao
    {
      IEventCollection GetEventsByCustomerId(string custId);
    }
}
