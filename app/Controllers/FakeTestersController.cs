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
    public class FakeTestersController : Controller
    {
        private readonly FusionAppContext _context;

        public FakeTestersController(FusionAppContext context)
        {
            _context = context;
        }

        // GET: FakeTesters
        public async Task<IActionResult> Index()
        {
            return View(await _context.FakeTesters.ToListAsync());
        }

        // GET: FakeTesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fakeTesters = await _context.FakeTesters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fakeTesters == null)
            {
                return NotFound();
            }

            return View(fakeTesters);
        }

        // GET: FakeTesters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FakeTesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,City,Zipcode,Phone,Age,Gender,IsConfirmed,NoOfTests,PointsEarned")] FakeTesters fakeTesters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fakeTesters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fakeTesters);
        }

        // GET: FakeTesters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fakeTesters = await _context.FakeTesters.FindAsync(id);
            if (fakeTesters == null)
            {
                return NotFound();
            }
            return View(fakeTesters);
        }

        // POST: FakeTesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,City,Zipcode,Phone,Age,Gender,IsConfirmed,NoOfTests,PointsEarned")] FakeTesters fakeTesters)
        {
            if (id != fakeTesters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fakeTesters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FakeTestersExists(fakeTesters.Id))
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
            return View(fakeTesters);
        }

        // GET: FakeTesters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fakeTesters = await _context.FakeTesters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fakeTesters == null)
            {
                return NotFound();
            }

            return View(fakeTesters);
        }

        // POST: FakeTesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fakeTesters = await _context.FakeTesters.FindAsync(id);
            _context.FakeTesters.Remove(fakeTesters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FakeTestersExists(int id)
        {
            return _context.FakeTesters.Any(e => e.Id == id);
        }
    }
}
