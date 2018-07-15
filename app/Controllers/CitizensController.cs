using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using app.Models;

namespace app.Controllers
{
    public class CitizensController : Controller
    {
        private readonly FusionAppContext _context;

        public CitizensController(FusionAppContext context)
        {
            _context = context;
        }

        // GET: Citizens
        public async Task<IActionResult> Index()
        {
            return View(await _context.Citizens.ToListAsync());
        }

        // GET: Citizens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizens = await _context.Citizens
                .FirstOrDefaultAsync(m => m.CitizenId == id);
            if (citizens == null)
            {
                return NotFound();
            }

            return View(citizens);
        }

        // GET: Citizens/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitizenId,FirstName,LastName,Email,Phone,IsConfirmed")] Citizens citizens)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citizens);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citizens);
        }

        // GET: Citizens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizens = await _context.Citizens.FindAsync(id);
            if (citizens == null)
            {
                return NotFound();
            }
            return View(citizens);
        }

        // POST: Citizens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitizenId,FirstName,LastName,Email,Phone,IsConfirmed")] Citizens citizens)
        {
            if (id != citizens.CitizenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citizens);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitizensExists(citizens.CitizenId))
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
            return View(citizens);
        }

        // GET: Citizens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citizens = await _context.Citizens
                .FirstOrDefaultAsync(m => m.CitizenId == id);
            if (citizens == null)
            {
                return NotFound();
            }

            return View(citizens);
        }

        // POST: Citizens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citizens = await _context.Citizens.FindAsync(id);
            _context.Citizens.Remove(citizens);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitizensExists(int id)
        {
            return _context.Citizens.Any(e => e.CitizenId == id);
        }
    }
}
