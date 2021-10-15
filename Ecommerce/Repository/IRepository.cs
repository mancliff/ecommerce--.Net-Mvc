using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Ecommerce.Repository
{
    public interface IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        IEnumerable<Tbl_Entity> GetUsers();
        IEnumerable<Tbl_Entity> GetProduct();
        
        IEnumerable<Tbl_Entity> GetCallDetails();
        IEnumerable<Tbl_Entity> GetAllRecords();
        IQueryable<Tbl_Entity> GetAllRecordsIQueryable();
        int GetAllrecordsCount();
        void Add(Tbl_Entity entity);
        void Update(Tbl_Entity entity);
        void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachpredict);
        Tbl_Entity GetFirstorDefault(int recordId);
        void Remove(Tbl_Entity entity);
        void RemoveByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict);
        void RemoveRangeByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict);
        void InactiveAndDeleteByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachpredict);
        Tbl_Entity GetFirstorDefaultByParameter(Expression<Func<Tbl_Entity, bool>> wherePredict);
        IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict);
        IEnumerable<Tbl_Entity> GetResultBySqlprocedure(string query, params object[] parameters);
        IEnumerable<Tbl_Entity> GetRecordsShow(int PageNo, int CurrentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict);
    }
}