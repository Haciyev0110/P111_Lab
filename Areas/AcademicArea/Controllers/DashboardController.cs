﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academic.Areas.AcademicArea.Controllers
{
    [Area("AcademicArea")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
    }
}