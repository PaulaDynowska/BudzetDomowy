using Microsoft.AspNetCore.Mvc;
using BudzetDomowyApp.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudzetDomowyApp.Controllers
{
    public class PlanowanieController : Controller
    {
        private readonly AppDbContext _context;

        public PlanowanieController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var wydatki = _context.PlanowaneWydatki.ToList();
            ViewBag.Kategorie = new List<string> { "Jedzenie", "Rachunki", "Transport", "Rozrywka", "Praca", "Media", "Inne" };
            ViewBag.SumaPlanowanych = wydatki.Sum(w => w.Kwota);
                var wszystkieOperacje = _context.Operacje.ToList();
    ViewBag.Suma = wszystkieOperacje.Sum(o => o.Kwota);
            return View(wydatki);
        }

        [HttpPost]
        public IActionResult Dodaj(PlanowanyWydatek wydatek)
        {
            if (ModelState.IsValid)
            {
                _context.PlanowaneWydatki.Add(wydatek);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var wydatki = _context.PlanowaneWydatki.ToList();
            ViewBag.Kategorie = new List<string> { "Jedzenie", "Rachunki", "Transport", "Rozrywka", "Praca", "Media", "Inne" };
            ViewBag.Suma = _context.Operacje.Sum(o => o.Kwota);
            return View("Index", wydatki);
        }

        public IActionResult Delete(int id)
        {
            var wydatek = _context.PlanowaneWydatki.FirstOrDefault(w => w.Id == id);
            if (wydatek != null)
            {
                _context.PlanowaneWydatki.Remove(wydatek);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
