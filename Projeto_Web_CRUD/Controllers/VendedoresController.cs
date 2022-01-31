using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_Web_CRUD.Data;
using Projeto_Web_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Controllers {
    public class VendedoresController : Controller {

        public readonly DataBaseContext _context;

        public VendedoresController(DataBaseContext context) {
            _context = context;
        }


        /* -------------------------------------------------- Read - Index -------------------------------------------------- */
        public async Task<IActionResult> Index() {
            return View(await _context.Vendedores.OrderBy(v => v.VendedorId).ToListAsync());
        }


        /* -------------------------------------------------- Create -------------------------------------------------- */
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Nome")] Vendedor vendedor) {
            try {
                if (ModelState.IsValid) {
                    _context.Add(vendedor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            } catch (DbUpdateConcurrencyException) {
                ModelState.AddModelError("", "Não foi possível salvar os dados");
            }
            return View(vendedor);
        }


        ///* -------------------------------------------------- Read - Details -------------------------------------------------- */
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }
            var vendedor = await _context.Vendedores.Include(p => p.Produtos).SingleOrDefaultAsync(v => v.VendedorId == id);
            if (vendedor == null) {
                return NotFound();
            }
            return View(vendedor);
        }

        ///* -------------------------------------------------- Update -------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> Update(int? id) {
            if (id == null) {
                return NotFound();
            }
            var vendedor = await _context.Vendedores.SingleOrDefaultAsync(p => p.VendedorId == id);
            if (vendedor == null) {
                return NotFound();
            }
            return View(vendedor);
        }
        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(int id, [Bind("VendedorId, Nome")] Vendedor vendedor) {
            if (id != vendedor.VendedorId) {
                return NotFound();
            }
            if (ModelState.IsValid) {
                try {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } catch (DbUpdateException) {
                    ModelState.AddModelError("", "Não foi possível alterar os dados");
                }
            }
            return View(vendedor);
        }


        ///* -------------------------------------------------- Delete -------------------------------------------------- */
        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false) {
            if (id == null) {
                return NotFound();
            }
            var vendedor = await _context.Vendedores
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.VendedorId == id);
            if (vendedor == null) {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault()) {
                ViewData["ErrorMessage"] = "Erro ao apagar o Vendedor";
            }
            return View(vendedor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var vendedor = await _context.Vendedores.Include(e => e.Produtos).SingleAsync(i => i.VendedorId == id);
            if (vendedor == null) {
                return RedirectToAction(nameof(Index));
            }
            try {
                _context.Vendedores.Remove(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            } catch (DbUpdateException) {
                return RedirectToAction(nameof(Index), new { id = id, SaveChangesError = true });
            }
        }
    }
}
