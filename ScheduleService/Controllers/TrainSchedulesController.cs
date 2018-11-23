using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScheduleService.Models;
using ScheduleService.Services;

namespace ScheduleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainSchedulesController : ControllerBase
    {
        private readonly TrainsDbContext _context;

        public TrainSchedulesController(TrainsDbContext context)
        {
            _context = context;
        }

        // GET: api/TrainSchedules
        [HttpGet]
        public IEnumerable<TrainSchedule> GetSchedules()
        {
            return _context.Schedules;
        }

        // GET: api/TrainSchedules/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainSchedule([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainSchedule = await _context.Schedules.FindAsync(id);

            if (trainSchedule == null)
            {
                return NotFound();
            }

            return Ok(trainSchedule);
        }

        // PUT: api/TrainSchedules/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainSchedule([FromRoute] int id, [FromBody] TrainSchedule trainSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainSchedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(trainSchedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainScheduleExists(id))
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

        // POST: api/TrainSchedules
        [HttpPost]
        public async Task<IActionResult> PostTrainSchedule([FromBody] TrainSchedule trainSchedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Schedules.Add(trainSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainSchedule", new { id = trainSchedule.Id }, trainSchedule);
        }

        // DELETE: api/TrainSchedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainSchedule([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainSchedule = await _context.Schedules.FindAsync(id);
            if (trainSchedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(trainSchedule);
            await _context.SaveChangesAsync();

            return Ok(trainSchedule);
        }

        private bool TrainScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}