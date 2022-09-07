using System.Collections.Generic;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Application.Products
{
    public interface IProductService
    {
        void Insert(Product p_product);
        void Update(Product p_product);
        void Delete(int p_id);
        Product GetById(int p_id);
        List<Product> GetAll();
        void ExecuteSQL(string p_sql);
        void CreateTable();
        void DropTable();
    }
}
