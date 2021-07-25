using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Services.Entities;
using Services.Services;
using TestSibers.Models;

namespace TestSibers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}