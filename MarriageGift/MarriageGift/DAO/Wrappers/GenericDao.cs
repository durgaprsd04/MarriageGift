﻿using MarriageGift.DAO.Interfaces;
using MarriageGift.DAO.DAOS;
using MarriageGift.Model;

namespace MarriageGift.DAO.Wrappers
{
    class GenericDao : IGenericDao
    {
        private IGenericDao baseDao;
        public GenericDao(ICustomerDao baseDao)
        {
           this.baseDao = baseDao;
        }

        public void Delete(string id)
        {
            baseDao.Delete(id);
        }

        public IGenericCollection<IBaseObject> GetListOfObjectsByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(IBaseObject baseObject)
        {
            baseDao.Insert(baseObject);
        }

        public IBaseObject Read(string id)
        {
            return baseDao.Read(id);
        }

        public void Update(IBaseObject baseObject)
        {
            baseDao.Update(baseObject);
        }
    }
}
