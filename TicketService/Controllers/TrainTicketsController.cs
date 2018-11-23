using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketService.Models;
using TicketService.Services;

namespace TicketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainTicketsController : ControllerBase
    {
        private readonly TicketsDbContext _context;

        public TrainTicketsController(TicketsDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainTickets
        [HttpGet]
        public IEnumerable<TrainTicket> GetTickets()
        {
            return _context.Tickets;
        }

        // GET: api/TrainTickets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainTicket([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainTicket = await _context.Tickets.FindAsync(id);

            if (trainTicket == null)
            {
                return NotFound();
            }

            return Ok(trainTicket);
        }

        // PUT: api/TrainTickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainTicket([FromRoute] Guid id, [FromBody] TrainTicket trainTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainTicket.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainTicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TrainTickets
        [HttpPost]
        public async Task<IActionResult> PostTrainTicket([FromBody] TrainTicket trainTicket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tickets.Add(trainTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainTicket", new { id = trainTicket.Id }, trainTicket);
        }

        // DELETE: api/TrainTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainTicket([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainTicket = await _context.Tickets.FindAsync(id);
            if (trainTicket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(trainTicket);
            await _context.SaveChangesAsync();

            return Ok(trainTicket);
        }

        private bool TrainTicketExists(Guid id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }

        // GET: api/TrainTickets/5/details
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetTrainTicketDetails(
            [FromRoute] Guid id,
            [FromServices] ScheduleApiProxy proxy
        )
        {
            var trainTicket = await _context.Tickets
                .SingleOrDefaultAsync(m => m.Id == id);
            if (trainTicket == null)
            {
                return NotFound();
            }
            var trainSchedule = await proxy.GetDetailsAsync(trainTicket.TrainScheduleId);
            if (trainSchedule == null)
            {
                return NotFound();
            }
            var details = new TicketDetails()
            {
                ID = trainTicket.Id,
                PassengerName = trainTicket.PassengerName,
                Destination = trainSchedule.Destination,
                DepartureTime = trainSchedule.DepartureTime,
                Price = trainSchedule.DistanceKm * 0.6M
            };
            return Ok(details);
        }
    }
}