using H3IA9Z_ADT_2022_23_1_Endpoint.Services;
using H3IA9Z_ADT_2022_23_1_Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models;
using System.Collections.Generic;

namespace H3IA9Z_ADT_2022_23_1_Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        IReservationLogic RL;
        IHubContext<SignalRHub> hub;
        public ReservationsController(IReservationLogic rL, IHubContext<SignalRHub> hub)
        {
            RL = rL;
            this.hub = hub;
        }


        // GET: /reservations
        [HttpGet]
        public IEnumerable<Reservation> Get()
        {
            return RL.GetAllReservations();
        }


        // GET /resevations/5
        [HttpGet("{id}")]
        public Reservation Get(int id)
        {
            return RL.GetReservation(id);
        }

        // POST /reservations
        [HttpPost]
        public void Post([FromBody] Reservation value)
        {
            RL.AddNewReservation(value);
            this.hub.Clients.All.SendAsync("ReservationCreated", value);
        }


        // PUT /reservations
        [HttpPut]
        public void Put([FromBody] Reservation value)
        {
            RL.UpdateReservationDate(value);
            this.hub.Clients.All.SendAsync("ReservationUpdated", value);
        }


        // DELETE /reservations/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var MovieToDelete = this.RL.GetReservation(id);
            RL.DeleteReservation(id);
            this.hub.Clients.All.SendAsync("ReservationDeleted", MovieToDelete);
        }
    }
}
