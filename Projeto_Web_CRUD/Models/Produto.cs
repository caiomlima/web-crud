using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Models {
    public class Produto {

        public int? ProdutoId { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Categoria { get; set; }

        [StringLength(500, MinimumLength = 3)]
        [Required]
        public string Descricao { get; set; }

        public int? VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

    }
}
