using Projeto_Web_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Data {
    public class DataBaseInitializer {

        public static void Initialize(DataBaseContext context) {

            context.Database.EnsureCreated();

            if (context.Vendedores.Any()) {
                return;
            }

            var vendedores = new Vendedor[] {
                new Vendedor { Nome = "Ana"},
                new Vendedor { Nome = "Roberto"}
            };
            foreach (Vendedor v in vendedores) {
                context.Vendedores.Add(v);
            }
            context.SaveChanges();


            var produtos = new Produto[] {
                new Produto { Nome = "Produto 1", Categoria = "Alimentos", Descricao = "Este é um produto genérico", VendedorId = 1 },
                new Produto { Nome = "Produto 2", Categoria = "Limpeza", Descricao = "Este é um produto genérico", VendedorId = 1 },
                new Produto { Nome = "Produto 3", Categoria = "Eletrônicos", Descricao = "Este é um produto genérico", VendedorId = 1 },
                new Produto { Nome = "Produto 4", Categoria = "Frios", Descricao = "Este é um produto genérico", VendedorId = 2 },
                new Produto { Nome = "Produto 5", Categoria = "Brinquedos", Descricao = "Este é um produto genérico", VendedorId = 2 }
            };
            foreach (Produto p in produtos) {
                context.Produtos.Add(p);
            }
            context.SaveChanges();
        }
    }
}
