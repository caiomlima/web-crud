using Microsoft.EntityFrameworkCore;
using Projeto_Web_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Data {
    public class DataBaseContext : DbContext {

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Produto>().ToTable("produto");
        }

    }
}
