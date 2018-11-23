using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TicketService.Models;

namespace TicketService.Services
{
    public class ScheduleApiProxy
    {
        const string baseUrl = "http://192.168.99.100:8088/api";

        public async Task<ScheduleItem> GetDetailsAsync(int id)
        {
            var url = $"{baseUrl}/TrainSchedules/{id}";
            var client = new HttpClient();
            string json = await client.GetStringAsync(url);
            return JsonConvert.DeserializeObject<ScheduleItem>(json);
        }
    }
}
