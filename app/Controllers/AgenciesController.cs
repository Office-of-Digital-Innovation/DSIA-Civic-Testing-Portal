using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.Models;
using app.Data;
using Microsoft.AspNetCore.Authorization;

namespace app.Controllers
{
    public class AgenciesController : Controller
    {
        private readonly FusionAppContext _context;

        public AgenciesController(FusionAppContext context)
        {
            _context = context;
        }

        // GET: Agencies
        // Add Security
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Agencies.ToListAsync());
        }

        // GET: Agencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencies = await _context.Agencies
                .FirstOrDefaultAsync(m => m.AgencyId == id);
            if (agencies == null)
            {
                return NotFound();
            }

            return View(agencies);
        }

        // GET: Agencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgencyId,AgencyName,AgencyUrl")] Agencies agencies)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agencies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agencies);
        }

        // GET: Agencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencies = await _context.Agencies.FindAsync(id);
            if (agencies == null)
            {
                return NotFound();
            }
            return View(agencies);
        }

        // POST: Agencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgencyId,AgencyName,AgencyUrl")] Agencies agencies)
        {
            if (id != agencies.AgencyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agencies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgenciesExists(agencies.AgencyId))
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
            return View(agencies);
        }

        // GET: Agencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agencies = await _context.Agencies
                .FirstOrDefaultAsync(m => m.AgencyId == id);
            if (agencies == null)
            {
                return NotFound();
            }

            return View(agencies);
        }

        // POST: Agencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agencies = await _context.Agencies.FindAsync(id);
            _context.Agencies.Remove(agencies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgenciesExists(int id)
        {
            return _context.Agencies.Any(e => e.AgencyId == id);
        }
    }
}
