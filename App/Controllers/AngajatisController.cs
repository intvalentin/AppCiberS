using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace App.Controllers
{
    public class AngajatisController : Controller
    {
        private readonly appciberContext _context;

        public AngajatisController(appciberContext context)
        {
            _context = context;
        }

        // GET: Angajatis
        public async Task<IActionResult> Index()
        {
            var appciberContext = _context.Angajati.Include(a => a.IdDepartamentNavigation).Include(a => a.IdFunctiaNavigation);
            return View(await appciberContext.ToListAsync());
        }

        // GET: Angajatis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var angajati = await _context.Angajati
                .Include(a => a.IdDepartamentNavigation)
                .Include(a => a.IdFunctiaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (angajati == null)
            {
                return NotFound();
            }

            return View(angajati);
        }

        // GET: Angajatis/Create
        public IActionResult Create()
        {
            ViewData["IdDepartament"] = new SelectList(_context.Departament, "Id", "NumeDepartament");
            ViewData["IdFunctia"] = new SelectList(_context.Functie, "Id", "Id");
            return View();
        }

        // POST: Angajatis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nume,Prenume,Email,NumarTelefon,IdDepartament,DataAngajari,IdFunctia")] Angajati angajati)
        {
            if (ModelState.IsValid)
            {
                angajati.DataAngajari = DateTime.Now;
                _context.Add(angajati);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartament"] = new SelectList(_context.Departament, "Id", "NumeDepartament", angajati.IdDepartament);
            ViewData["IdFunctia"] = new SelectList(_context.Functie, "Id", "Id", angajati.IdFunctia);
            return View(angajati);
        }

        // GET: Angajatis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var angajati = await _context.Angajati.FindAsync(id);
            if (angajati == null)
            {
                return NotFound();
            }
            ViewData["IdDepartament"] = new SelectList(_context.Departament, "Id", "NumeDepartament", angajati.IdDepartament);
            ViewData["IdFunctia"] = new SelectList(_context.Functie, "Id", "Id", angajati.IdFunctia);
            return View(angajati);
        }

        // POST: Angajatis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Prenume,Email,NumarTelefon,IdDepartament,DataAngajari,IdFunctia")] Angajati angajati)
        {
            if (id != angajati.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(angajati);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AngajatiExists(angajati.Id))
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
            ViewData["IdDepartament"] = new SelectList(_context.Departament, "Id", "NumeDepartament", angajati.IdDepartament);
            ViewData["IdFunctia"] = new SelectList(_context.Functie, "Id", "Id", angajati.IdFunctia);
            return View(angajati);
        }

        // GET: Angajatis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var angajati = await _context.Angajati
                .Include(a => a.IdDepartamentNavigation)
                .Include(a => a.IdFunctiaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (angajati == null)
            {
                return NotFound();
            }

            return View(angajati);
        }

        // POST: Angajatis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var angajati = await _context.Angajati.FindAsync(id);
            _context.Angajati.Remove(angajati);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AngajatiExists(int id)
        {
            return _context.Angajati.Any(e => e.Id == id);
        }
    }
}
