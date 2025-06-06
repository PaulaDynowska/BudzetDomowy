using Microsoft.AspNetCore.Mvc;
using BudzetDomowyApp.Models;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Linq;
using QuestPDF.Helpers;

namespace BudzetDomowyApp.Controllers
{
    public class OperacjeController : Controller
    {
        private readonly AppDbContext _context;

        public OperacjeController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            var operacje = await _context.Operacje
                .Where(o => !o.CzyUsunieta)
                .OrderByDescending(o => o.Data)
                .ToListAsync();

            ViewBag.Suma = operacje.Sum(o => o.Kwota);
            ViewBag.Wplywy = operacje.Where(o => o.Kwota > 0).Sum(o => o.Kwota);
            ViewBag.Wydatki = operacje.Where(o => o.Kwota < 0).Sum(o => o.Kwota);

            return View(operacje);
        }

        // CREATE
        public IActionResult Create()
        {
            ViewBag.Suma = _context.Operacje.Where(o => !o.CzyUsunieta).Sum(o => o.Kwota);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Operacja operacja)
        {
            if (ModelState.IsValid)
            {
                _context.Add(operacja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Suma = _context.Operacje.Where(o => !o.CzyUsunieta).Sum(o => o.Kwota);
            return View(operacja);
        }

        // EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var operacja = await _context.Operacje.FindAsync(id);
            if (operacja == null) return NotFound();

            ViewBag.Suma = _context.Operacje.Where(o => !o.CzyUsunieta).Sum(o => o.Kwota);
            return View(operacja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Operacja operacja)
        {
            if (id != operacja.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(operacja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Suma = _context.Operacje.Where(o => !o.CzyUsunieta).Sum(o => o.Kwota);
            return View(operacja);
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var operacja = await _context.Operacje.FindAsync(id);
            if (operacja == null) return NotFound();

            ViewBag.Suma = _context.Operacje.Where(o => !o.CzyUsunieta).Sum(o => o.Kwota);
            return View(operacja);
        }

        // DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var operacja = await _context.Operacje.FindAsync(id);
            if (operacja == null) return NotFound();

            ViewBag.Suma = _context.Operacje.Where(o => !o.CzyUsunieta).Sum(o => o.Kwota);
            return View(operacja);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var operacja = await _context.Operacje.FindAsync(id);
            if (operacja != null)
            {
                operacja.CzyUsunieta = true;
                _context.Update(operacja);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // HISTORIA
        public async Task<IActionResult> Historia()
        {
            var wszystkie = await _context.Operacje
                .OrderByDescending(o => o.Data)
                .ToListAsync();

            ViewBag.Suma = wszystkie
                .Where(o => !o.CzyUsunieta)
                .Sum(o => o.Kwota);

            return View(wszystkie);
        }

        // GENERUJ PDF
        public IActionResult GenerujPdf()
        {
            var operacje = _context.Operacje
                .Where(o => !o.CzyUsunieta)
                .OrderByDescending(o => o.Data)
                .ToList();

            var dokument = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Zestawienie operacji")
                        .FontSize(20)
                        .Bold()
                        .AlignCenter();

                    page.Content()
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Kategoria").Bold();
                                header.Cell().Text("Kwota").Bold();
                                header.Cell().Text("Data").Bold();
                            });

                            foreach (var op in operacje)
                            {
                                table.Cell().Text(op.Kategoria);
                                table.Cell().Text($"{op.Kwota} zł");
                                table.Cell().Text(op.Data.ToShortDateString());
                            }
                        });
                });
            });

            var pdf = dokument.GeneratePdf();
            return File(pdf, "application/pdf", "RaportOperacji.pdf");
        }
    }
}
