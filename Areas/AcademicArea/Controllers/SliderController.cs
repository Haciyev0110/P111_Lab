using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Academic.DAL;
using Academic.Extentions;
using Academic.Models;
using Academic.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Academic.Areas.AcademicArea.Controllers
{
    [Area("AcademicArea")]
    public class SliderController : Controller
    {
        private AppDbContext _context;
        private IHostingEnvironment _env;
        public SliderController(AppDbContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Sliders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {

            if (ModelState["Photo"].ValidationState ==ModelValidationState.Invalid || 
                ModelState["Title"].ValidationState ==ModelValidationState.Invalid)
            {
                return View();
            }

            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "This is not photo");
                return View();
            }
            if (!slider.Photo.CheckImageSize(1))
            {
                ModelState.AddModelError("Photo", "Image size not more than 1 MB");
                return View();
               
                
            }
            
            //extention yazilib:

            //string path = Path.Combine(_env.WebRootPath, "images");
            //string filename = Path.Combine("Sliders",Guid.NewGuid().ToString()+slider.Photo.FileName);
            //string resultPath = Path.Combine(path, filename);
            //using (FileStream fileStream = new FileStream(resultPath, FileMode.Create))
            //{
            //    await slider.Photo.CopyToAsync(fileStream);
            //}
            //string replace_filename=filename.Replace(@"\","/");


            string filename = await slider.Photo.CopyImage(_env.WebRootPath, "sliders");

            slider.Image = filename;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            Slider slider =await _context.Sliders.FindAsync(id);

            Utility.DeleteImageFromFolder(_env.WebRootPath, slider.Image);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Slider slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Slider slider)
        {
            Slider thisIdSlider =await _context.Sliders.FindAsync(id);
            if (thisIdSlider == null)
            {
                return RedirectToAction(nameof(Index));
            }
            if (ModelState["Title"].ValidationState == ModelValidationState.Invalid)
            {
                return View(thisIdSlider);
            }
            if (slider.PhotoUpdate != null)
            {
                if (!slider.PhotoUpdate.IsImage())
                {
                    ModelState.AddModelError("Photo", "This is not photo");
                    return View();
                }
                if (!slider.PhotoUpdate.CheckImageSize(1))
                {
                    ModelState.AddModelError("Photo", "Image size not more than 1 MB");
                    return View();

                }
                string filename = await slider.PhotoUpdate.CopyImage(_env.WebRootPath, "sliders");
                Utility.DeleteImageFromFolder(_env.WebRootPath, thisIdSlider.Image);

                thisIdSlider.Image = filename;
            }
            thisIdSlider.Title = slider.Title;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}