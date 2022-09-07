using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dapper;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.Infra.Products
{
    public class ProductRepository : Repository, IProductRepository
    {
        public void Insert(Product p_product)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();
                
                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Insere o dado do produto na tabela "Product"
                        var _sql = "INSERT INTO Product " +
                                   "(Name, Description, Image, Inventory, CostPrice, SalePrice, CreatedAt, UpdatedAt, Status) " +
                                   "VALUES (@Name, @Description, @Image, @Inventory, @CostPrice, @SalePrice, @CreatedAt, @UpdatedAt, @Status);" +
                                   "select last_insert_rowid();";

                        p_product.Id = cnn.Query<int>(_sql, p_product).First();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public void Update(Product p_product)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Altera o dado do produto na tabela "Product"
                        var _sql = "UPDATE Product SET " +
                                   "Name = @Name, " +
                                   "Description = @Description, " +
                                   "Image = @Image, " +
                                   "Inventory = @Inventory, " +
                                   "CostPrice = @CostPrice, " +
                                   "SalePrice = @SalePrice, " +
                                   "UpdatedAt = @UpdatedAt, " +
                                   "Status = @Status " +
                                   "WHERE Id = @Id;";

                        cnn.Execute(_sql, p_product);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public void Delete(int id)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Exclui o dado do produto na tabela "Product"
                        var _sql = "DELETE FROM Product WHERE Id = @id;";

                        cnn.Execute(_sql, new {id});

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public Product GetById(int id)
        {
            Product _produto = null;

            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                try
                {
                    cnn.Open();

                    var _sql = "SELECT * FROM Product WHERE Id = @id;";

                    _produto = cnn.Query<Product>(_sql, new {id}).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                cnn?.Close();
                cnn?.Dispose();
            }

            return _produto;
        }

        public List<Product> GetAll()
        {
            List<Product> _produto = null;

            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                try
                {
                    cnn.Open();

                    var _sql = "SELECT * FROM Product ORDER BY Id ASC;";

                    _produto = cnn.Query<Product>(_sql).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                cnn?.Close();
                cnn?.Dispose();
            }

            return _produto;
        }

        public void ExecuteSQL(string sql)
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Executa as instruções _sql na tabela "Product"
                        cnn.Execute(sql);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public void CreateTable()
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();
                
                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Cria a tabela "Produto"
                        var _sql = "CREATE TABLE IF NOT EXISTS Product (" +
                                   "Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                                   "Name VARCHAR(200) NOT NULL, " +
                                   "Description VARCHAR(1000) NOT NULL, " +
                                   "Image VARCHAR(200) NOT NULL, " +
                                   "Inventory INTEGER NOT NULL, " +
                                   "CostPrice DOUBLE NOT NULL, " +
                                   "SalePrice DOUBLE NOT NULL, " +
                                   "CreatedAt DATETIME NOT NULL, " +
                                   "UpdatedAt DATETIME NULL," +
                                   "Status CHAR(1) NOT NULL DEFAULT 'A');";

                        cnn.Execute(_sql);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }

        public void DropTable()
        {
            if (File.Exists(DbFile))
            {
                using var cnn = SimpleDbConnection();

                cnn.Open();

                using (var transaction = cnn.BeginTransaction())
                {
                    try
                    {
                        // Exclui a tabela "Product"
                        var _sql = "DROP TABLE IF EXISTS Product;";

                        cnn.Execute(_sql);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new Exception(ex.Message);
                    }
                }

                cnn?.Close();
                cnn?.Dispose();
            }
        }
    }
}
