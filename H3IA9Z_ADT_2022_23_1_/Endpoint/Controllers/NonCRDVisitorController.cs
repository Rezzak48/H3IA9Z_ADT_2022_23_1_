using H3IA9Z_ADT_2022_23_1_Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace H3IA9Z_ADT_2022_23_1_Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class NonCRDVisitorController : ControllerBase
    {
        IVisitorLogic VisL;

        public NonCRDVisitorController(IVisitorLogic visL)
        {
            VisL = visL;
        }

        // GET: Noncrudvis/ReservationNUM/id
        [HttpGet("{id}")]
        public int ReservationNUM(int id)
        {
            return VisL.ReservationsNumber(id);
        }

        // GET: Noncrudvis/BestVisitor
        [HttpGet]
        public List<KeyValuePair<int, int>> BestVisitors()
        {
            return VisL.BestVisitor();
        }

        // GET: Noncrudvis/WorstVisitors
        [HttpGet]
        public List<KeyValuePair<int, int>> WorstVisitors()
        {
            return VisL.WorstVisitor();
        }

    }
}
