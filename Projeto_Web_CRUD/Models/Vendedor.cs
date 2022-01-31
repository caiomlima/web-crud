using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Models {
    public class Vendedor {

        public int? VendedorId { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [Required]
        public string Nome { get; set; }

        public ICollection<Produto> Produtos { get; set; }

    }
}
