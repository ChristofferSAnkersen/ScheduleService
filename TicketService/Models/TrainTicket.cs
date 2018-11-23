using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketService.Models
{
    public class TrainTicket
    {
        public int Id { get; set; }

        public string PassengerName { get; set; }

        public int TrainScheduleId { get; set; }

    }
}
