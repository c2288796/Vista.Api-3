﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vista.Web.Data;

namespace Vista.Web.Controllers
{
    public class WorkshopController : Controller
    {
        private readonly WorkshopsContext _context;

        public WorkshopController(WorkshopsContext context)
        {
            _context = context;
        }

        // GET: Workshop
        public async Task<IActionResult> Index()
        {
              return View(await _context.Workshops.ToListAsync());
        }

        // GET: Workshop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workshops == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops
                .FirstOrDefaultAsync(m => m.WorkshopId == id);
            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }

        // GET: Workshop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workshop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkshopId,Name,DateAndTime,CategoryCode,BookingRef")] Workshop workshop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workshop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workshop);
        }

        // GET: Workshop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Workshops == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop == null)
            {
                return NotFound();
            }
            return View(workshop);
        }

        // POST: Workshop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkshopId,Name,DateAndTime,CategoryCode,BookingRef")] Workshop workshop)
        {
            if (id != workshop.WorkshopId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workshop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkshopExists(workshop.WorkshopId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workshop);
        }

        // GET: Workshop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Workshops == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops
                .FirstOrDefaultAsync(m => m.WorkshopId == id);
            if (workshop == null)
            {
                return NotFound();
            }

            return View(workshop);
        }

        // POST: Workshop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workshops == null)
            {
                return Problem("Entity set 'WorkshopsContext.Workshops'  is null.");
            }
            var workshop = await _context.Workshops.FindAsync(id);
            if (workshop != null)
            {
                _context.Workshops.Remove(workshop);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkshopExists(int id)
        {
          return _context.Workshops.Any(e => e.WorkshopId == id);
        }
    }
}
