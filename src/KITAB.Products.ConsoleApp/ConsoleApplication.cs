using System;
using AutoMapper;
using KITAB.Products.Application.Notificators;
using KITAB.Products.Application.Products;
using KITAB.Products.ConsoleApp.ViewModels;
using KITAB.Products.Domain.Models;

namespace KITAB.Products.ConsoleApp
{
    public class ConsoleApplication
    {
        private readonly INotificatorService _notificatorService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ConsoleApplication(INotificatorService notificatorService, IProductService productService, IMapper mapper)
        {
            _notificatorService = notificatorService;
            _productService = productService;
            _mapper = mapper;
        }

        public void Run()
        {
            Console.WriteLine("Iniciando o processamento...");
            Console.WriteLine("");

            while (true)
            {
                if (!Produtos_Excluir_Tabela()) break;
                if (!Produtos_Criar_Tabela()) break;
                if (!Produtos_Inserir_Dados_Tabela()) break;
                if (!Produtos_Inserir_Dados_Varios_Tabela()) break;
                if (!Produtos_Alterar_Dados_Tabela()) break;
                if (!Produtos_Excluir_Dados_Tabela()) break;
                if (!Produtos_ObterTodos_Dados_Tabela()) break;
                if (!Produtos_ObterPorId_Dados_Tabela()) break;

                break;
            }

            // Verifica se há notificações vinda da camada de negócio
            // Caso tenha notificações, devem ser exibidas ao usuário
            if (_notificatorService.HaveNotification()) Listar_Notificacoes();

            Console.WriteLine("Processo concluído...");
            Console.ReadKey(true);
        }

        private bool Produtos_Excluir_Tabela()
        {
            Console.WriteLine("Excluindo a tabela de Produtos...");
            Console.WriteLine("");

            _productService.DropTable();

            return (!_notificatorService.HaveNotification());
        }

        private bool Produtos_Criar_Tabela()
        {
            Console.WriteLine("Criando a tabela de Produtos...");
            Console.WriteLine("");

            _productService.CreateTable();

            return (!_notificatorService.HaveNotification());
        }

        private bool Produtos_Inserir_Dados_Tabela()
        {
            Console.WriteLine("Inserindo um produto na tabela de Produtos...");
            Console.WriteLine("");

            var _productDTO = new ProductViewModel()
            {
                Name = "Produto 1",
                Description = "Descrição do Produto 1",
                Image = "9d11cb8a-f0dd-4aef-a803-bc257959bbc0_produto-256x256.jpg",
                Inventory = 1,
                CostPrice = 10,
                SalePrice = 20,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                Status = "A"
            };

            var _product = _mapper.Map<Product>(_productDTO);

            // Insere os dados do produto na tabela "Product"
            _productService.Insert(_product);

            return (!_notificatorService.HaveNotification());
        }

        private bool Produtos_Inserir_Dados_Varios_Tabela()
        {
            Console.WriteLine("Inserindo 100 produtos na tabela de Produtos...");
            Console.WriteLine("");

            // Insere os dados do produto na tabela "Product"
            var _products = "";
            var _count = 0;

            for (int i = 2; i <= 101; i++)
            {
                _products += "INSERT INTO Product " +
                             "(Name, Description, Image, Inventory, CostPrice, SalePrice, CreatedAt, Status) VALUES (" +
                             "'Produto " + i.ToString() + "', " +
                             "'Descrição do Produto " + i.ToString() + "', " +
                             "'9d11cb8a-f0dd-4aef-a803-bc257959bbc0_produto-256x256.jpg', " +
                             (i * 2).ToString() + ", " +
                             (i * 10).ToString() + ", " +
                             (i * 15).ToString() + ", " +
                             "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.0000000") + "', " +
                             "'A');";

                _count++;

                if (_count >= 50)
                {
                    // Insere 50 produtos por vez na tabela "Product"
                    _productService.ExecuteSQL(_products);

                    if (_notificatorService.HaveNotification()) break;

                    _products = "";
                    _count = 0;
                }
            }

            return (!_notificatorService.HaveNotification());
        }

        private bool Produtos_Alterar_Dados_Tabela()
        {
            Console.WriteLine("Alterando o produto com o ID = 10 na tabela de Produtos...");
            Console.WriteLine("");

            // Altera os dados do produto na tabela "Product"
            _productService.Update(new Product()
            {
                Id = 10,
                Name = "Produto 10 - Alterado",
                Description = "Descrição do Produto 10 - Alterado",
                Image = "9d11cb8a-f0dd-4aef-a803-bc257959bbc0_produto-256x256.jpg",
                Inventory = 0,
                CostPrice = 0,
                SalePrice = 0,
                UpdatedAt = DateTime.Now,
                Status = "D"
            });

            return (!_notificatorService.HaveNotification());
        }

        private bool Produtos_Excluir_Dados_Tabela()
        {
            Console.WriteLine("Excluindo o produto com o ID = 11 na tabela de Produtos...");
            Console.WriteLine("");

            // Exclui os dados do produto com o ID = 11 na tabela "Product"
            _productService.Delete(11);

            return (!_notificatorService.HaveNotification());
        }

        private bool Produtos_ObterTodos_Dados_Tabela()
        {
            Console.WriteLine("Listando todos os produtos da tabela de Produtos...");
            Console.WriteLine("");

            // Pega todos os produtos na tabela "Product"
            var _products = _productService.GetAll();

            if (!_notificatorService.HaveNotification())
            {
                foreach (var _product in _products)
                {
                    Console.WriteLine("ID: " + _product.Id + " - Produto: " + _product.Description + " - " +
                                      "Quantidade: " + _product.Inventory + " - Preço de Venda: " + _product.SalePrice);
                }
            }

            Console.WriteLine("");

            return (!_notificatorService.HaveNotification());
        }

        private bool Produtos_ObterPorId_Dados_Tabela()
        {
            Console.WriteLine("Listando o produto com o ID = 10 da tabela de Produtos...");
            Console.WriteLine("");

            // Localiza os dados do produto com o ID = 10 na tabela "Product"
            var _product = _productService.GetById(10);

            var _productDTO = _mapper.Map<ProductViewModel>(_product);

            if (!_notificatorService.HaveNotification())
            {
                Console.WriteLine("ID: " + _productDTO.Id + " - " +
                                  "Produto: " + _productDTO.Name + " - " +
                                  "Descrição: " + _productDTO.Description + " - " +
                                  "Imagem: " + _productDTO.Image + " - " +
                                  "Quantidade: " + _productDTO.Inventory + " - " +
                                  "Preço de Custo: " + _productDTO.CostPrice + " - " +
                                  "Preço de Venda: " + _productDTO.SalePrice + " - " +
                                  "Data Cadastro: " + _productDTO.CreatedAt.ToString("dd/MM/yyyy") + " - " +
                                  "Data Alteração: " + _productDTO.UpdatedAt?.ToString("dd/MM/yyyy") + " - " +
                                  "Situação: " + _productDTO.Status);

                Console.WriteLine("");
            }

            return (!_notificatorService.HaveNotification());
        }

        private void Listar_Notificacoes()
        {
            Console.WriteLine("Ocorreu um ERRO !!! Listando as notificações...");
            Console.WriteLine("");

            var _notifications = _notificatorService.GetAll();

            foreach (var _notification in _notifications)
            {
                Console.WriteLine(_notification.Message);
            }

            Console.WriteLine("");
        }
    }
}
