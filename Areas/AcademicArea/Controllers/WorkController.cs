using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic.DAL;
using Academic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Academic.Areas.AcademicArea.Controllers
{
    [Area("AcademicArea")]
    public class WorkController : Controller
    {
        private AppDbContext _context;
        public WorkController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Works);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Work work =await _context.Works.FindAsync(id);
            if (work == null) return NotFound();
            return View(work);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create(Work work)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

           await _context.Works.AddAsync(work);
           await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Work work =await _context.Works.FindAsync(id);
            if (work == null) return NotFound();
            return View(work);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Work work =await _context.Works.FindAsync(id);
            _context.Works.Remove(work);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Work work = await _context.Works.FindAsync(id);
            if (work == null) return NotFound();
            return View(work);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Work work)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Work workUpdate =await _context.Works.FindAsync(id);

            workUpdate.Title = work.Title;
            workUpdate.Description = work.Description;
            workUpdate.IconField = work.IconField;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

           


            
        }
    }
}