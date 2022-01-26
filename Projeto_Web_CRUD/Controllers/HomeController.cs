using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projeto_Web_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Web_CRUD.Controllers {
    public class HomeController : Controller {

        public IActionResult Index() {
            return View();
        }

    }
}
