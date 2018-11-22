using Microsoft.EntityFrameworkCore;
using ScheduleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Services
{
    public class TrainsDbContext : DbContext
    {
        public TrainsDbContext(DbContextOptions<TrainsDbContext> options) : base(options) { }
        public DbSet<TrainSchedule> Schedules { get; set; }
    }
}
