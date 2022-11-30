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
    public class VisitorController : ControllerBase
    {
        IVisitorLogic VisL;
        IHubContext<SignalRHub> hub;
        public VisitorController(IVisitorLogic visL, IHubContext<SignalRHub> hub)
        {
            VisL = visL;
            this.hub = hub;
        }

        // GET: /visitors
        [HttpGet]
        public IEnumerable<Visitor> Get()
        {
            return VisL.GetAllVisitors();
        }


        // GET /visitors/5
        [HttpGet("{id}")]
        public Visitor Get(int id)
        {
            return VisL.GetVisitor(id);
        }

        // POST /visitors
        [HttpPost]
        public void Post([FromBody] Visitor value)
        {
            VisL.AddNewVis(value);
            this.hub.Clients.All.SendAsync("VisitorCreated", value);
        }


        // PUT /visitors
        [HttpPut]
        public void Put([FromBody] Visitor value)
        {
            VisL.UpdateAddress(value);
            this.hub.Clients.All.SendAsync("VisitorUpdated", value);
        }


        // DELETE /visitors/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var visToDelete = this.VisL.GetVisitor(id);
            VisL.DeleteVisitor(id);
            this.hub.Clients.All.SendAsync("VisitorDeleted", visToDelete);
        }

    }
}
