using System;
using MarriageGift.Model.Interfaces;
namespace MarriageGift.Controller.Interfaces
{
    public interface IAdminController
    {
      void CreateGift(IGift gift);
    }
  }
