using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketService.Models;
using TicketService.Services;

namespace TicketService.Controllers
{
    public class TrainTicketsViewsController : Controller
    {
        private readonly TicketsDbContext _context;

        public TrainTicketsViewsController(TicketsDbContext context)
        {
            _context = context;
        }

        // GET: TrainTicketsViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tickets.ToListAsync());
        }

        // GET: TrainTicketsViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainTicket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainTicket == null)
            {
                return NotFound();
            }

            return View(trainTicket);
        }

        // GET: TrainTicketsViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrainTicketsViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PassengerName,TrainScheduleId")] TrainTicket trainTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainTicket);
        }

        // GET: TrainTicketsViews/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainTicket = await _context.Tickets.FindAsync(id);
            if (trainTicket == null)
            {
                return NotFound();
            }
            return View(trainTicket);
        }

        // POST: TrainTicketsViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PassengerName,TrainScheduleId")] TrainTicket trainTicket)
        {
            if (id != trainTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainTicketExists(trainTicket.Id))
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
            return View(trainTicket);
        }

        // GET: TrainTicketsViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainTicket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainTicket == null)
            {
                return NotFound();
            }

            return View(trainTicket);
        }

        // POST: TrainTicketsViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainTicket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(trainTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainTicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
