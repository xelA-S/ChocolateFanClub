﻿using Microsoft.AspNetCore.Mvc;

namespace ChocolateFanClub.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
