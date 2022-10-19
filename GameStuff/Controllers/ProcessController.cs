using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameStuff.Controllers
{
    public class ProcessController : Controller
    {
        public IActionResult Index()
        {
            Process[] proceses = Process.GetProcesses();
            ViewBag.myProcesses = proceses;
            return View();
        }

        public IActionResult Welcome()
        {
            Process[] processes = Process.GetProcesses();
            return View(processes);
        }

        public IActionResult Display(int id)
        {
            Process process = Process.GetProcessById(id);
            return View(process);
        }
    }
}