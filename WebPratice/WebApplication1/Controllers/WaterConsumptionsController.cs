﻿using System;
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
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return SearchUnit("");
        }

        [HttpGet("Import")]
        public IActionResult Import()
        {
            string filePath = ConsoleApp1.Utlis.FilePath.getFilePath("7D1311DD-E1DE-4B01-A5ED-1FC589A7C082.json");

            List<WaterConsumption> results = new ImportJsonService().read(filePath);

            _context.waterConsumptions.AddRange(results);
            _context.SaveChanges();

            return Content("OK");
        }

        [HttpGet("DeleteAll")]
        public IActionResult DeleteAll()
        {
            foreach(var data in _context.waterConsumptions)
            {
                _context.waterConsumptions.Remove(data);
            }
            _context.SaveChanges();

            return Content("OK");
        }

        [HttpPost("SearchUnit")]
        public IActionResult SearchUnit(string unitName)
        {
            var query = _context.waterConsumptions.AsQueryable();
            
            if (!String.IsNullOrEmpty(unitName))
            {
                query = query.Where(e => e.executingUnit == unitName);
            }
            query = query.OrderByDescending(x => x.Id);
            //var results = query.Take(10);

            return View("Index", query.ToList());
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(WaterConsumption create)
        {
            _context.waterConsumptions.Add(create);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            var edit = _context.waterConsumptions.Find(id);

            return View(edit);
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(WaterConsumption edit)
        {
            _context.Entry(edit).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.waterConsumptions.Update(edit);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var water = _context.waterConsumptions.Find(id);

            _context.waterConsumptions.Remove(water);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}