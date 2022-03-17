using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Domain;
using Infrastructure.EF;

namespace SSWD_Fysio.Controllers
{
    public class PatientFilesController : Controller
    {
        private readonly FysioDBContext _context;

        public PatientFilesController(FysioDBContext context)
        {
            _context = context;
        }

        // GET: PatientFiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.patientFiles.ToListAsync());
        }

        // GET: PatientFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientFile = await _context.patientFiles
                .FirstOrDefaultAsync(m => m.patientFileId == id);
            if (patientFile == null)
            {
                return NotFound();
            }

            return View(patientFile);
        }

        // GET: PatientFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("patientFileId,patientId,treatmentPlanId,intakeByPractitionerId,supervisedBypractitionerId,birthDate,type,registerDate,dischargeDate")] PatientFile patientFile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientFile);
        }

        // GET: PatientFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientFile = await _context.patientFiles.FindAsync(id);
            if (patientFile == null)
            {
                return NotFound();
            }
            return View(patientFile);
        }

        // POST: PatientFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("patientFileId,patientId,treatmentPlanId,intakeByPractitionerId,supervisedBypractitionerId,birthDate,type,registerDate,dischargeDate")] PatientFile patientFile)
        {
            if (id != patientFile.patientFileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientFileExists(patientFile.patientFileId))
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
            return View(patientFile);
        }

        // GET: PatientFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientFile = await _context.patientFiles
                .FirstOrDefaultAsync(m => m.patientFileId == id);
            if (patientFile == null)
            {
                return NotFound();
            }

            return View(patientFile);
        }

        // POST: PatientFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientFile = await _context.patientFiles.FindAsync(id);
            _context.patientFiles.Remove(patientFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientFileExists(int id)
        {
            return _context.patientFiles.Any(e => e.patientFileId == id);
        }
    }
}
