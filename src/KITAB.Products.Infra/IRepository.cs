using System.Collections.Generic;

namespace KITAB.Products.Infra
{
    public interface IRepository<T> where T : class
    {
        void Insert(T p_obj);
        void Update(T p_obj);
        void Delete(int p_id);
        T GetById(int p_id);
        List<T> GetAll();
        void ExecuteSQL(string p_sql);
    }
}
