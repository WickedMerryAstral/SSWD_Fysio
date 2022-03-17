using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Domain;
using Infrastructure.EF;
using Microsoft.AspNetCore.Authorization;

namespace SSWD_Fysio.Controllers
{
    [AllowAnonymous]
    public class PractitionersController : Controller
    {
        private readonly FysioDBContext _context;

        public PractitionersController(FysioDBContext context)
        {
            _context = context;
        }

        // GET: Practitioners
        public async Task<IActionResult> Index()
        {
            return View(await _context.practitioners.ToListAsync());
        }

        // GET: Practitioners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practitioner = await _context.practitioners
                .FirstOrDefaultAsync(m => m.practitionerId == id);
            if (practitioner == null)
            {
                return NotFound();
            }

            return View(practitioner);
        }

        // GET: Practitioners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Practitioners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("practitionerId,type,name,mail,studentNumber,employeeNumber,phone,BIGNumber")] Practitioner practitioner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(practitioner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(practitioner);
        }

        // GET: Practitioners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practitioner = await _context.practitioners.FindAsync(id);
            if (practitioner == null)
            {
                return NotFound();
            }
            return View(practitioner);
        }

        // POST: Practitioners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("practitionerId,type,name,mail,studentNumber,employeeNumber,phone,BIGNumber")] Practitioner practitioner)
        {
            if (id != practitioner.practitionerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(practitioner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PractitionerExists(practitioner.practitionerId))
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
            return View(practitioner);
        }

        // GET: Practitioners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practitioner = await _context.practitioners
                .FirstOrDefaultAsync(m => m.practitionerId == id);
            if (practitioner == null)
            {
                return NotFound();
            }

            return View(practitioner);
        }

        // POST: Practitioners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var practitioner = await _context.practitioners.FindAsync(id);
            _context.practitioners.Remove(practitioner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PractitionerExists(int id)
        {
            return _context.practitioners.Any(e => e.practitionerId == id);
        }
    }
}
