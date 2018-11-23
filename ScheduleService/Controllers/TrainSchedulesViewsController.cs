using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScheduleService.Models;
using ScheduleService.Services;

namespace ScheduleService.Controllers
{
    public class TrainSchedulesViewsController : Controller
    {
        private readonly TrainsDbContext _context;

        public TrainSchedulesViewsController(TrainsDbContext context)
        {
            _context = context;
        }

        // GET: TrainSchedulesViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Schedules.ToListAsync());
        }

        // GET: TrainSchedulesViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainSchedule = await _context.Schedules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainSchedule == null)
            {
                return NotFound();
            }

            return View(trainSchedule);
        }

        // GET: TrainSchedulesViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainSchedulesViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartureTime,Destination,DistanceKm")] TrainSchedule trainSchedule)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(trainSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainSchedule);
        }

        // GET: TrainSchedulesViews/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainSchedule = await _context.Schedules.FindAsync(id);
            if (trainSchedule == null)
            {
                return NotFound();
            }
            return View(trainSchedule);
        }

        // POST: TrainSchedulesViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartureTime,Destination,DistanceKm")] TrainSchedule trainSchedule)
        {
            if (id != trainSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainScheduleExists(trainSchedule.Id))
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
            return View(trainSchedule);
        }

        // GET: TrainSchedulesViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainSchedule = await _context.Schedules
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainSchedule == null)
            {
                return NotFound();
            }

            return View(trainSchedule);
        }

        // POST: TrainSchedulesViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainSchedule = await _context.Schedules.FindAsync(id);
            _context.Schedules.Remove(trainSchedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
