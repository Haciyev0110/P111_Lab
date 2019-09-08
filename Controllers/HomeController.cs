using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic.DAL;
using Academic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Academic.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            HomeModel homeModel = new HomeModel
            {
                Sliders=_context.Sliders,
                //RepeatTitles=_context.RepeatTitles,
                Works=_context.Works,
                Courses=_context.Courses
            };
            return View(homeModel);
        }
    }
}