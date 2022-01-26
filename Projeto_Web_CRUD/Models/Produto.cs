using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Models {
    public class Produto {

        public int? ProdutoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Categoria { get; set; }

        [Required]
        public string Descricao { get; set; }

    }
}
