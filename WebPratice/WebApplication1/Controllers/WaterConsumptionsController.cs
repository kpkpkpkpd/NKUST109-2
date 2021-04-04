using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConsoleApp1.Model;
using ConsoleApp1.Service;

namespace WebApplication1.Controllers
{
    public class WaterConsumptionsController : Controller
    {
        public IActionResult Index()
        {
            string filePath = ConsoleApp1.Utlis.FilePath.getFilePath("7D1311DD-E1DE-4B01-A5ED-1FC589A7C082.json");

            List<WaterConsumption> results = new ImportJsonService().read(filePath);

            return View(results);
        }
    }
}