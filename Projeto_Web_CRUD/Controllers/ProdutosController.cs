using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_Web_CRUD.Data;
using Projeto_Web_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Controllers {
    public class ProdutosController : Controller {

        public readonly DataBaseContext _context;

        public ProdutosController(DataBaseContext context) {
            _context = context;
        }

        private void VendedoresDropDownList(object selectedVendedor = null) {
            var vendedoresQuery = from v in _context.Vendedores
                                  orderby v.VendedorId
                                  select v;
            ViewBag.VendedorId = new SelectList(vendedoresQuery, "VendedorId", "Nome", selectedVendedor);
        }


        /* -------------------------------------------------- Read - Index -------------------------------------------------- */
        public async Task<IActionResult> Index() {
            return View(await _context.Produtos.Include(v => v.Vendedor).OrderBy(p => p.ProdutoId).ToListAsync());  
        }


        /* -------------------------------------------------- Create -------------------------------------------------- */
        public IActionResult Create() {
            VendedoresDropDownList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nome, Categoria, Descricao, VendedorId, ProdutosCadastrados")] Produto produto) {
            try {
                if(ModelState.IsValid) {
                    var vendedor = new Vendedor();
                    var produtoContador = vendedor.ProdutosCadastrados += 1;
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch(DbUpdateConcurrencyException) {
                ModelState.AddModelError("", "Não foi possível salvar os dados");
            }
            VendedoresDropDownList(produto.VendedorId);
            return View(produto);
        }


        /* -------------------------------------------------- Read - Details -------------------------------------------------- */
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }
            var produto = await _context.Produtos.SingleOrDefaultAsync(p => p.ProdutoId == id);
            _context.Vendedores.Where(i => produto.VendedorId == i.VendedorId).Load();
            if(produto == null) {
                return NotFound();
            }
            return View(produto);
        }

        /* -------------------------------------------------- Update -------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> Update(int? id) {
            if (id == null) {
                return NotFound();
            }
            var produto = await _context.Produtos.SingleOrDefaultAsync(p => p.ProdutoId == id);
            if (produto == null) {
                return NotFound();
            }
            VendedoresDropDownList(produto.VendedorId);
            return View(produto);
        }
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(int id, [Bind("ProdutoId, Nome, Categoria, Descricao, VendedorId")] Produto produto) {
            if(id != produto.ProdutoId) {
                return NotFound();
            }
            if(ModelState.IsValid) {
                try {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } catch(DbUpdateException) {
                    ModelState.AddModelError("", "Não foi possível alterar os dados");
                }
            }
            VendedoresDropDownList(produto.VendedorId);
            return View(produto);
        }


        /* -------------------------------------------------- Delete -------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false) {
            if(id == null) {
                return NotFound();
            }
            var produto = await _context.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProdutoId == id);
            if(produto == null) {
                return NotFound();
            }
            if(saveChangesError.GetValueOrDefault()) {
                ViewData["ErrorMessage"] = "Erro ao apagar o produto";
            }
            return View(produto);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var produto = await _context.Produtos.FindAsync(id);
            if(produto == null) {
                return RedirectToAction(nameof(Index));
            }
            try {
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch(DbUpdateException) {
                return RedirectToAction(nameof(Index), new { id = id, SaveChangesError = true });
            }
        }
    }
}
