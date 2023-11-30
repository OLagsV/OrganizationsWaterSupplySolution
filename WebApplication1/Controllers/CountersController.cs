using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrganizationsWaterSupplyL4.Data;
using OrganizationsWaterSupplyL4.Services;
using OrganizationsWaterSupplyL4.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OrganizationsWaterSupplyL4.Controllers
{
    public class CountersController : Controller
    {
        private readonly OrganizationsWaterSupplyContext _context;
        CachedCountersService _countersService;
        List<Counter> _counters;

        public CountersController(OrganizationsWaterSupplyContext context)
        {
            _context = context;
            _countersService = (CachedCountersService)context.GetService<ICachedService<Counter>>();
            _counters = _countersService.GetData("counters").ToList();
        }

        // GET: Counters
        public async Task<IActionResult> Index()
        {
            _counters = _countersService.GetData("counters").ToList();
            var organizationsWaterSupplyContext = _counters;
            /*var organizationsWaterSupplyContext = _context.Counters.Include(c => c.Model).Include(c => c.Organization)*/;
            return View(organizationsWaterSupplyContext);
        }

        // GET: Counters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Counters == null)
            {
                return NotFound();
            }

            var counter = await _context.Counters
                .Include(c => c.Model)
                .Include(c => c.Organization)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (counter == null)
            {
                return NotFound();
            }

            return View(counter);
        }

        // GET: Counters/Create
        public IActionResult Create()
        {
            ViewData["ModelId"] = new SelectList(_context.CounterModels, "ModelId", "ModelId");
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId");
            return View();
        }

        // POST: Counters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegistrationNumber,ModelId,TimeOfInstallation,OrganizationId")] Counter counter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(counter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelId"] = new SelectList(_context.CounterModels, "ModelId", "ModelId", counter.ModelId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", counter.OrganizationId);
            return View(counter);
        }

        // GET: Counters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Counters == null)
            {
                return NotFound();
            }

            var counter = await _context.Counters.FindAsync(id);
            if (counter == null)
            {
                return NotFound();
            }
            ViewData["ModelId"] = new SelectList(_context.CounterModels, "ModelId", "ModelName", counter.ModelId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrgName", counter.OrganizationId);
            return View(counter);
        }

        // POST: Counters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RegistrationNumber,ModelId,TimeOfInstallation,OrganizationId")] Counter counter)
        {
            if (id != counter.RegistrationNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(counter);
                    await _context.SaveChangesAsync();
                    _countersService.UpdateData("counters");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CounterExists(counter.RegistrationNumber))
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
            ViewData["ModelId"] = new SelectList(_context.CounterModels, "ModelId", "ModelId", counter.ModelId);
            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationId", counter.OrganizationId);
            return View(counter);
        }

        // GET: Counters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Counters == null)
            {
                return NotFound();
            }

            var counter = await _context.Counters
                .Include(c => c.Model)
                .Include(c => c.Organization)
                .FirstOrDefaultAsync(m => m.RegistrationNumber == id);
            if (counter == null)
            {
                return NotFound();
            }

            return View(counter);
        }

        // POST: Counters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Counters == null)
            {
                return Problem("Entity set 'OrganizationsWaterSupplyContext.Counters'  is null.");
            }
            var counter = await _context.Counters.FindAsync(id);
            if (counter != null)
            {
                _context.Counters.Remove(counter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CounterExists(int id)
        {
          return (_context.Counters?.Any(e => e.RegistrationNumber == id)).GetValueOrDefault();
        }
    }
}
