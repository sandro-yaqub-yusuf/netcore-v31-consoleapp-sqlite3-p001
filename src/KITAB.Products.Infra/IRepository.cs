using System.Collections.Generic;

namespace KITAB.Products.Infra
{
    public interface IRepository<T> where T : class
    {
        void Insert(ref T p_obj);
        void Update(ref T p_obj);
        void Delete(ref int p_id);
        T GetById(ref int p_id);
        List<T> GetAll();
        void ExecuteSQL(ref string p_sql);
    }
}
