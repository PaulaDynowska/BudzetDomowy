using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudzetDomowyApp.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace BudzetDomowyApp.Controllers
{
    public class OperacjeController : Controller
    {
        private readonly AppDbContext _context;

        public OperacjeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(DateTime? dataOd, DateTime? dataDo)
        {
            var operacje = _context.Operacje.AsQueryable();

            if (dataOd.HasValue)
                operacje = operacje.Where(o => o.Data >= dataOd.Value);

            if (dataDo.HasValue)
                operacje = operacje.Where(o => o.Data <= dataDo.Value);

            ViewBag.Suma = operacje.Any() ? operacje.Sum(o => o.Kwota) : 0;
            var wydatki = _context.PlanowaneWydatki.ToList();
            ViewBag.SumaPlanowanych = wydatki.Sum(w => w.Kwota);

            return View(operacje.OrderByDescending(o => o.Data).ToList());
        }


        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var operacja = _context.Operacje.FirstOrDefault(o => o.Id == id);
            if (operacja == null) return NotFound();

            return View(operacja);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Operacja operacja)
        {
            if (ModelState.IsValid)
            {
                _context.Operacje.Add(operacja);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(operacja);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var operacja = _context.Operacje.Find(id);
            if (operacja == null) return NotFound();

            return View(operacja);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Operacja operacja)
        {
            if (id != operacja.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(operacja);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(operacja);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var operacja = _context.Operacje.Find(id);
            if (operacja == null) return NotFound();

            return View(operacja);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var operacja = _context.Operacje.Find(id);
            if (operacja != null)
            {
                _context.Operacje.Remove(operacja);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Historia()
        {
            var operacje = _context.Operacje.OrderByDescending(o => o.Data).ToList();
            ViewBag.Suma = operacje.Any() ? operacje.Sum(o => o.Kwota) : 0;

            return View(operacje);
        }

        public IActionResult GenerujPdf()
        {
            var operacje = _context.Operacje.OrderByDescending(o => o.Data).ToList();

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
                                header.Cell().Element(CellStyle).Text("Kategoria").Bold();
                                header.Cell().Element(CellStyle).Text("Kwota").Bold();
                                header.Cell().Element(CellStyle).Text("Data").Bold();
                            });

                            foreach (var op in operacje)
                            {
                                table.Cell().Element(CellStyle).Text(op.Kategoria);
                                table.Cell().Element(CellStyle).Text(op.Kwota.ToString("N2") + " zł");
                                table.Cell().Element(CellStyle).Text(op.Data.ToShortDateString());
                            }

                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .PaddingVertical(5)
                                    .PaddingHorizontal(10);
                            }
                        });
                });
            });

            var stream = new MemoryStream();
            dokument.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream, "application/pdf", "Zestawienie.pdf");
        }
    }
}
