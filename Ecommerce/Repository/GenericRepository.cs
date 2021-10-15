﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Ecommerce.DAL;

namespace Ecommerce.Repository
{
    public class GenericRepository<Tbl_Entity> : IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        private dbEcommerceEntities _DBEntity;
        DbSet<Tbl_Entity> _dbSet;


        private dbEcommerceEntities dBEntity;

        public GenericRepository(dbEcommerceEntities DBEntity)
        {
            _DBEntity = DBEntity;
            _dbSet = _DBEntity.Set<Tbl_Entity>();
        }



        public IEnumerable<Tbl_Entity> GetProduct()
        {
            return _dbSet.ToList();
        }



        public IEnumerable<Tbl_Entity> GetCallDetails()
        {
            return _dbSet.ToList();
        }

        public void Add(Tbl_Entity entity)
        {
            _dbSet.Add(entity);
            _DBEntity.SaveChanges();
        }



        public IEnumerable<Tbl_Entity> GetAllRecords()
        {
            return _dbSet.ToList();
        }


        public Tbl_Entity GetFirstorDefaultByParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).FirstOrDefault();
        }

        public IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict)
        {
            return _dbSet.Where(wherePredict).ToList();
        }

        public IEnumerable<Tbl_Entity> GetResultBySqlprocedure(string query, params object[] parameters)
        {
            if (parameters != null)
            {
                return _DBEntity.Database.SqlQuery<Tbl_Entity>(query, parameters).ToList();
            }
            else
                return _DBEntity.Database.SqlQuery<Tbl_Entity>(query).ToList();
        }



        public void Remove(Tbl_Entity entity)
        {
            if (_DBEntity.Entry(entity).State == EntityState.Detached)
                _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }

        public void RemoveRangeByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePrdict)
        {
            List<Tbl_Entity> entity = _dbSet.Where(wherePrdict).ToList();
            foreach (var ent in entity)
            {
                Remove(ent);
            }
        }

        public void RemoveByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePrdict)
        {
            Tbl_Entity entity = _dbSet.Where(wherePrdict).FirstOrDefault();
            Remove(entity);
        }

        public void Update(Tbl_Entity entity)
        {
            _dbSet.Attach(entity);
            _DBEntity.Entry(entity).State = EntityState.Modified;
            _DBEntity.SaveChanges();
        }

        public void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachPredict);
        }


        public IEnumerable<Tbl_Entity> GetUsers()
        {
            return _dbSet.ToList();
        }

        public IQueryable<Tbl_Entity> GetAllRecordsIQueryable()
        {
            return _dbSet;
        }

        public int GetAllrecordsCount()
        {
            return _dbSet.Count();
        }

        public Tbl_Entity GetFirstorDefault(int recordId)
        {
            return _dbSet.Find(recordId);
        }

        public void InactiveAndDeleteByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachpredict)
        {
            _dbSet.Where(wherePredict).ToList().ForEach(ForEachpredict);
        }

        public IEnumerable<Tbl_Entity> GetRecordsShow(int PageNo, int CurrentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict)
        {
            if (wherePredict != null)
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }

            else
            {
                return _dbSet.OrderBy(orderByPredict).ToList();
            }
        }

    }

       
}