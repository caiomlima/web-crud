using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        /* -------------------------------------------------- Read - Index -------------------------------------------------- */
        public async Task<IActionResult> Index() {
            return View(await _context.Produtos.Include(v => v.Vendedor).OrderBy(p => p.ProdutoId).ToListAsync());
        }


        /* -------------------------------------------------- Create -------------------------------------------------- */
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nome,Categoria,Descricao")] Produto produto) {
            try {
                if(ModelState.IsValid) {
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch(DbUpdateConcurrencyException) {
                ModelState.AddModelError("", "Não possível salvar os dados");
            }
            return View(produto);
        }


        /* -------------------------------------------------- Read - Details -------------------------------------------------- */
        public async Task<IActionResult> Details(int? id) {
            if(id == null) {
                return NotFound();
            }
            var produtos = await _context.Produtos.SingleOrDefaultAsync(p => p.ProdutoId == id);
            if(produtos == null) {
                return NotFound();
            }
            return View(produtos);
        }

        /* -------------------------------------------------- Update -------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> Update(int? id) {
            if (id == null) {
                return NotFound();
            }
            var produtos = await _context.Produtos.SingleOrDefaultAsync(p => p.ProdutoId == id);
            if (produtos == null) {
                return NotFound();
            }
            return View(produtos);
        }
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(int id, [Bind("ProdutoId, Nome, Categoria, Descricao")] Produto produto) {
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
