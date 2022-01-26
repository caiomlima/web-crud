using Projeto_Web_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Data {
    public class DataBaseInitializer {

        public static void Initialize(DataBaseContext context) {

            context.Database.EnsureCreated();

            if (context.Produtos.Any()) {
                return;
            }
            var produtos = new Produto[] {
                new Produto { Nome = "Produto Genérico", Categoria = "Alimentos", Descricao = "Este é um produto genérico de qualidade mediana, como qualquer um outro" },
                new Produto { Nome = "Produto Sem Nome", Categoria = "Não Identificado", Descricao = "Este é um produto sem nome e de qualidade não identificada" }
            };

            foreach (Produto p in produtos) {
                context.Produtos.Add(p);
            }

            context.SaveChanges();
        }
    }
}
