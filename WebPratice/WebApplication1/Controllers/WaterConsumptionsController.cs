using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConsoleApp1.Model;
using ConsoleApp1.Service;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    public class WaterConsumptionsController : Controller
    {
        private ApplicationDbContext _context;

        public WaterConsumptionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //string filePath = ConsoleApp1.Utlis.FilePath.getFilePath("7D1311DD-E1DE-4B01-A5ED-1FC589A7C082.json");

            // List<WaterConsumption> results = new ImportJsonService().read(filePath);

            List<WaterConsumption> results = _context.waterConsumptions.ToList<WaterConsumption>();

            return View(results);
        }

        [HttpPost]
        public IActionResult Import()
        {
            string filePath = ConsoleApp1.Utlis.FilePath.getFilePath("7D1311DD-E1DE-4B01-A5ED-1FC589A7C082.json");

            List<WaterConsumption> results = new ImportJsonService().read(filePath);

            _context.waterConsumptions.AddRange(results);
            _context.SaveChanges();

            return Content("OK");
        }

        [Route("Unit/{unitName}")]
        public IActionResult Unit(string unitName)
        {
            List<WaterConsumption> results = _context.waterConsumptions.Where(e => e.executingUnit == unitName).ToList<WaterConsumption>();

            return View("../WaterConsumptions/Index", results);
        }
    }
}