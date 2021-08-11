using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LOLProject.Controllers
{
    public class GuidesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
