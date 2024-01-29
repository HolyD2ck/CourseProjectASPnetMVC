using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseProjectBlazor.Models;


namespace CourseProjectASPnetMVC.Controllers
{
    public class EmployersController : Controller
    {
        private readonly CourseProjectASPnetMVCContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployersController(CourseProjectASPnetMVCContext context, IWebHostEnvironment web)
        {
            _context = context;
            webHostEnvironment = web;
        }
        public string UploadedFile(Employer employer, IFormFile photo)
        {
            string? relativePath = null;

            if (photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img/employers");
                string fileName = photo.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
                relativePath = Path.Combine("img/employers", fileName);
                employer.Фото = relativePath;
            }

            return relativePath;
        }

        // GET: Employers
        public async Task<IActionResult> Index()
        {
            _context.GetData();
            return View(await _context.Employer.ToListAsync());
        }

        // GET: Employers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer
                .FirstOrDefaultAsync(m => m.id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // GET: Employers/Create
        public IActionResult Create()
        {
            Employer employer = new Employer();
            return View(employer);
        }

        // POST: Employers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employer employer)
        {
            if (ModelState.IsValid)
            {
                var photo = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
                string UniqueFileName = UploadedFile(employer, photo);
                employer.Фото = UniqueFileName;
                _context.Attach(employer);
                _context.Entry(employer).State = EntityState.Added;
                _context.Add(employer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employer);
        }

        // GET: Employers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
            {
                return NotFound();
            }
            return View(employer);
        }

        // POST: Employers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employer employer)
        {
            if (id != employer.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var photo = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
                    string UniqueFileName = UploadedFile(employer, photo);
                    employer.Фото = UniqueFileName;
                    _context.Attach(employer);
                    _context.Entry(employer).State = EntityState.Added;
                    _context.Update(employer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployerExists(employer.id))
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
            return View(employer);
        }

        // GET: Employers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employer = await _context.Employer
                .FirstOrDefaultAsync(m => m.id == id);
            if (employer == null)
            {
                return NotFound();
            }

            return View(employer);
        }

        // POST: Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employer = await _context.Employer.FindAsync(id);
            if (employer != null)
            {
                _context.Employer.Remove(employer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployerExists(int id)
        {
            return _context.Employer.Any(e => e.id == id);
        }
    }
}
