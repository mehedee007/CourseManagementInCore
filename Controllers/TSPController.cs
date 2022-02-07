using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseManagementInCore.Data;
using CourseManagementInCore.Models;

namespace CourseManagementInCore.Controllers
{
    public class TSPController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TSPController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TSP
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TSPs.Include(t => t.Course).Include(t => t.Trainer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TSP/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSP = await _context.TSPs
                .Include(t => t.Course)
                .Include(t => t.Trainer)
                .FirstOrDefaultAsync(m => m.TspID == id);
            if (tSP == null)
            {
                return NotFound();
            }

            return View(tSP);
        }

        // GET: TSP/Create
        public IActionResult Create()
        {
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName");
            ViewData["TrainerID"] = new SelectList(_context.Trainers, "TrainerID", "TrainerName");
            return View();
        }

        // POST: TSP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TspID,TspName,TspAddress,TspContact,TspEmail,TrainerID,CourseID")] TSP tSP)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tSP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", tSP.CourseID);
            ViewData["TrainerID"] = new SelectList(_context.Trainers, "TrainerID", "TrainerName", tSP.TrainerID);
            return View(tSP);
        }

        // GET: TSP/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSP = await _context.TSPs.FindAsync(id);
            if (tSP == null)
            {
                return NotFound();
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", tSP.CourseID);
            ViewData["TrainerID"] = new SelectList(_context.Trainers, "TrainerID", "TrainerName", tSP.TrainerID);
            return View(tSP);
        }

        // POST: TSP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TspID,TspName,TspAddress,TspContact,TspEmail,TrainerID,CourseID")] TSP tSP)
        {
            if (id != tSP.TspID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tSP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSPExists(tSP.TspID))
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
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", tSP.CourseID);
            ViewData["TrainerID"] = new SelectList(_context.Trainers, "TrainerID", "TrainerName", tSP.TrainerID);
            return View(tSP);
        }

        // GET: TSP/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tSP = await _context.TSPs
                .Include(t => t.Course)
                .Include(t => t.Trainer)
                .FirstOrDefaultAsync(m => m.TspID == id);
            if (tSP == null)
            {
                return NotFound();
            }

            return View(tSP);
        }

        // POST: TSP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tSP = await _context.TSPs.FindAsync(id);
            _context.TSPs.Remove(tSP);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TSPExists(int id)
        {
            return _context.TSPs.Any(e => e.TspID == id);
        }
    }
}
