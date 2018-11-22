using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Models
{
    public class GarageFactory : DbContext
    {
        public GarageFactory(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<TrainSchedule> TrainSchedules { get; set; }
    }
}
