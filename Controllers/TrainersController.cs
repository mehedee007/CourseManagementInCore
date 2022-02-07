using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseManagementInCore.Data;
using CourseManagementInCore.Models.ViewModels;
using AutoMapper;

using CourseManagementInCore.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CourseManagementInCore.Controllers
{
    public class TrainersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _host;

        public TrainersController(ApplicationDbContext context, IMapper mapper, IWebHostEnvironment host)
        {
            _context = context;
            _mapper = mapper;
            _host = host;
        }


        public string UploadedImage(TrainerVM tvm)
        {
            string fileName = null;

            if (tvm.ImageUrl != null)
            {
                string uploadsFolder = Path.Combine(_host.WebRootPath, "Images/Trainers/");
                fileName = Guid.NewGuid().ToString() + "_" + tvm.ImageUrl.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    tvm.ImageUrl.CopyTo(fileStream);
                }
            }
            return fileName;
        }

        // GET: Trainer
        public async Task<IActionResult> Index()
        {
            var trainer = _context.Trainers.Include(t => t.Course);
            return View(await trainer.ToListAsync());
        }

        // GET: Trainer/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            var tvm = await _context.Trainers
                .Include(t => t.TrainerExperiences)
                .Where(tm => tm.TrainerID == id)
                .FirstOrDefaultAsync();

            return View(tvm);
        }

        // GET: Trainer/Create
        public IActionResult Create()
        {
            TrainerVM tvm = new TrainerVM();
            tvm.TrainerExperiences.Add(new TrainerExperience() { ID = 1 });

            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName");
            return View(tvm);
        }

        // POST: Trainer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainerVM tvm)
        {
            if (ModelState.IsValid)
            {
                var fileName = UploadedImage(tvm);
                tvm.TrainerImage = fileName;
                var vm = _mapper.Map<TrainerVM, Trainer>(tvm);
                _context.Add(vm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", tvm.CourseID);
            return View(tvm);
        }

        // GET: Trainer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var tvm = await _context.Trainers.FindAsync(id);
            tvm = await _context.Trainers
                .Include(te => te.TrainerExperiences)
                .Where(t => t.TrainerID == id).FirstOrDefaultAsync();
            var vm = _mapper.Map<Trainer, TrainerVM>(tvm);
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", tvm.CourseID);
            return View(vm);
        }

        // POST: Trainer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TrainerVM tvm)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = UploadedImage(tvm);
                    tvm.TrainerImage = fileName;

                    var vm = _mapper.Map<TrainerVM, Trainer>(tvm);
                    _context.Update(vm);
                    foreach (var te in tvm.TrainerExperiences)
                    {
                        _context.Update(te);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainerVMExists(tvm.TrainerID))
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
            ViewData["CourseID"] = new SelectList(_context.Courses, "CourseID", "CourseName", tvm.CourseID);
            return View(tvm);
        }

        //Delete

        public async Task<IActionResult> Delete(int? id)
        {

            //var tvm = await _context.Trainers
            //    .Include(t => t.Course)
            //    .FirstOrDefaultAsync(m => m.TrainerID == id);
            var tvm = await _context.Trainers.FindAsync(id);
            _context.Remove(tvm);
            _context.SaveChanges();
            return RedirectToAction("Index");
            //return View(tvm);
        }

        private bool TrainerVMExists(int id)
        {
            return _context.Trainers.Any(e => e.TrainerID == id);
        }
    }
}
