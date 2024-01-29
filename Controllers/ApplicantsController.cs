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
    public class ApplicantsController : Controller
    {
        private readonly CourseProjectASPnetMVCContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ApplicantsController(CourseProjectASPnetMVCContext context, IWebHostEnvironment web)
        {
            _context = context;
            webHostEnvironment = web;
        }
        public string UploadedFile(Applicant applicant, IFormFile photo)
        {
            string? relativePath = null;

            if (photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img/applicants");
                string fileName = photo.FileName;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }
                relativePath = Path.Combine("img/applicants", fileName);
                applicant.Фото = relativePath;
            }

            return relativePath;
        }

        // GET: Applicants
        public async Task<IActionResult> Index()
        {
            _context.GetData2();
            return View(await _context.Applicant.ToListAsync());
        }

        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant
                .FirstOrDefaultAsync(m => m.id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                var photo = Request.Form.Files.Count > 0 ? Request.Form.Files[0] : null;
                string UniqueFileName = UploadedFile(applicant, photo);
                applicant.Фото = UniqueFileName;
                _context.Attach(applicant);
                _context.Entry(applicant).State = EntityState.Added;
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Фамилия,Имя,Отчество,Образование,Специальность,Дата_Рождения,Телефон,Email,Фото")] Applicant applicant)
        {
            if (id != applicant.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.id))
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
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant
                .FirstOrDefaultAsync(m => m.id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant != null)
            {
                _context.Applicant.Remove(applicant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicant.Any(e => e.id == id);
        }
    }
}
