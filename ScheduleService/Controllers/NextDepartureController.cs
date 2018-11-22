using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ScheduleService.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NextDepartureController : ControllerBase
    {
        public IActionResult GetNext()
        {
            return Ok(new
            {
                DepartureTime = DateTime.Now.AddMinutes(50),
                Number = "5423",
                Destination = "Paris"
            });
        }
    }
}