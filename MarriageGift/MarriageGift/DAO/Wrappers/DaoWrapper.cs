using System;
using MarriageGift.DAO.DAOS;
using MarriageGift.DAO.Interfaces;
using MarriageGift.Model;

namespace MarriageGift.DAO.Wrappers
{
    class DaoWrapper : AbstractGenericDao
    {
        public DaoWrapper(string daoType) : base(daoType)
        {

        }
    }
}
