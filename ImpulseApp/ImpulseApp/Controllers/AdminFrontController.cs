﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImpulseApp.Controllers
{
    [Authorize(Roles="Administrators")]
    public class AdminFrontController : Controller
    {
        //
        // GET: /AdminFront/
        public ActionResult Index()
        {
            return View();
        }
	}
}