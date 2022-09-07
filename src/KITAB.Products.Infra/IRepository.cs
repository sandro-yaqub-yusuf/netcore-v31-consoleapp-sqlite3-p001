using System.Collections.Generic;

namespace KITAB.Products.Infra
{
    public interface IRepository<T> where T : class
    {
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        T GetById(int id);
        List<T> GetAll();
        void ExecuteSQL(string sql);
    }
}
