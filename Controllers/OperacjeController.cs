using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BudzetDomowyApp.Models;

namespace BudzetDomowyApp.Controllers
{
    public class OperacjeController : Controller
    {
        private readonly AppDbContext _context;

        public OperacjeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Operacje
        public async Task<IActionResult> Index()
        {
            return View(await _context.Operacje.ToListAsync());
        }

        // GET: Operacje/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacja = await _context.Operacje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operacja == null)
            {
                return NotFound();
            }

            return View(operacja);
        }

        // GET: Operacje/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Operacje/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kategoria,Kwota,Data")] Operacja operacja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operacja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(operacja);
        }

        // GET: Operacje/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacja = await _context.Operacje.FindAsync(id);
            if (operacja == null)
            {
                return NotFound();
            }
            return View(operacja);
        }

        // POST: Operacje/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kategoria,Kwota,Data")] Operacja operacja)
        {
            if (id != operacja.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(operacja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperacjaExists(operacja.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(operacja);
        }

        // GET: Operacje/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var operacja = await _context.Operacje
                .FirstOrDefaultAsync(m => m.Id == id);
            if (operacja == null)
            {
                return NotFound();
            }

            return View(operacja);
        }

        // POST: Operacje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operacja = await _context.Operacje.FindAsync(id);
            if (operacja != null)
            {
                _context.Operacje.Remove(operacja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OperacjaExists(int id)
        {
            return _context.Operacje.Any(e => e.Id == id);
        }
    }
}
