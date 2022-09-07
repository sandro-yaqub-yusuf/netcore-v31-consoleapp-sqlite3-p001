using System;
using System.Collections.Generic;
using KITAB.Products.Application.Notificators;
using KITAB.Products.Domain.Models;
using KITAB.Products.Infra.Products;

namespace KITAB.Products.Application.Products
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, INotificatorService notificatorService) : base(notificatorService)
        {
            _productRepository = productRepository;
        }

        public void Insert(Product p_product)
        {
            try
            {
                p_product.CreatedAt = DateTime.Now;
                p_product.UpdatedAt = null;

                _productRepository.Insert(p_product);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }
        }

        public void Update(Product p_product)
        {
            try
            {
                p_product.UpdatedAt = DateTime.Now;

                _productRepository.Update(p_product);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }
        }

        public void Delete(int p_id)
        {
            try
            {
                _productRepository.Delete(p_id);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }
        }

        public Product GetById(int p_id)
        {
            Product product = null;

            try
            {
                product = _productRepository.GetById(p_id);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }

            return product;
        }

        public List<Product> GetAll()
        {
            List<Product> products = null;

            try
            {
                products = _productRepository.GetAll();
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }

            return products;
        }

        public void ExecuteSQL(string p_sql)
        {
            try
            {
                _productRepository.ExecuteSQL(p_sql);
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }
        }

        public void CreateTable()
        {
            try
            {
                _productRepository.CreateTable();
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }
        }

        public void DropTable()
        {
            try
            {
                _productRepository.DropTable();
            }
            catch (Exception ex)
            {
                Notify(ex.Message);
            }
        }
    }
}
