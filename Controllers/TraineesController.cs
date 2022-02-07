using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseManagementInCore.Data;
using CourseManagementInCore.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CourseManagementInCore.Controllers
{
    public class TraineesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHosting;

        public TraineesController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHosting = webHost;
        }

        // GET: Trainees
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Trainees.Include(t => t.Course);
            return View(await applicationDbContext.ToListAsync());
        }

        public string UploadedImage(Trainee trainee)
        {
            string fileName = null;

            if(trainee.fileBase != null)
            {
                string uploadsFolder = Path.Combine(_webHosting.WebRootPath, "Images/Trainees/");
                fileName = Guid.NewGuid().ToString() + "_" + trainee.fileBase.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    trainee.fileBase.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        // GET: Trainees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainee = await _context.Trainees
                .Include(t => t.TraineeModuleDescriptions)
                .Where(tm =>tm.TraineeID ==id)
                .FirstOrDefaultAsync();
            if (trainee == null)
            {
                return NotFound();
            }

            return View(trainee);
        }

        // GET: Trainees/Create
        public IActionResult Create()
        {
            Trainee trainee = new Trainee();
            trainee.TraineeModuleDescriptions.Add(new TraineeModuleDescription() { ID = 1 });
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName");
            return View(trainee);
        }

        // POST: Trainees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                string tImage = UploadedImage(trainee);
                trainee.TraineeImage = tImage;
                _context.Add(trainee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", trainee.CourseID);
            return View(trainee);
        }

        // GET: Trainees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //var trainee = await _context.Trainees.FindAsync(id);
            var trainee = await _context.Trainees
                .Include(t => t.TraineeModuleDescriptions)
                .Where(tm => tm.TraineeID == id)
                .FirstOrDefaultAsync();
            //trainee = await _context.Trainees.FindAsync(id);
            //trainee.TraineeModuleDescriptions.Add(new TraineeModuleDescription() { ID = 1 });
            
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", trainee.CourseID);
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Trainee trainee)
        {

            if (ModelState.IsValid)
            {
    
                 string fileName = UploadedImage(trainee);
                 trainee.TraineeImage = fileName;

                //_context.Update(trainee);
                _context.Entry(trainee).State = EntityState.Modified;
                foreach (var tmd in trainee.TraineeModuleDescriptions)
                    {
                        _context.Entry(tmd).State = EntityState.Modified;
                    }
                    
                    await _context.SaveChangesAsync();
           
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", trainee.CourseID);
            return View(trainee);
        }

        // GET: Trainees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            _context.Remove(trainee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

  

        private bool TraineeExists(int id)
        {
            return _context.Trainees.Any(e => e.TraineeID == id);
        }

        //Notes!!!
        //BugReport : Add Works for 2 columns!! if the 3rd column is added and the datetime changes to yyyy-MM-dd from
        //01-01-0001 then it doesn't add the record to the database

    }
}
