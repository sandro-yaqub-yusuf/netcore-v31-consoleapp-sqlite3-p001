using System.Collections.Generic;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Application.Products
{
    public interface IProductService
    {
        void Insert(Product produto);
        void Update(Product produto);
        void Delete(int id);
        Product GetById(int id);
        List<Product> GetAll();
        void ExecuteSQL(string sql);
        void CreateTable();
        void DropTable();
    }
}
