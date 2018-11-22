using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Models
{
    public class TrainSchedule
    {
        public Guid Id { get; set; }

        public DateTime DepartureTime { get; set; }

        public string Destination { get; set; }

        public int DistanceKm { get; set; }

    }
}
