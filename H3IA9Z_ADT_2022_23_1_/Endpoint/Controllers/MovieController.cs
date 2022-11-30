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
    public class MovieController : ControllerBase
    {
        IMovieLogic MovL;
        IHubContext<SignalRHub> hub;
        public MovieController(IMovieLogic mvL, IHubContext<SignalRHub> hub)
        {
            MovL = mvL;
            this.hub = hub;
        }

        // GET: /movies
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return MovL.GetAllMovies();
        }


        // GET /movies/5
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return MovL.GetMovie(id);
        }

        // POST /movies
        [HttpPost]
        public void Post([FromBody] Movie value)
        {
            MovL.AddNewMovie(value);
            this.hub.Clients.All.SendAsync("MovieCreated", value);
        }


        // PUT /movies
        [HttpPut]
        public void Put([FromBody] Movie value)
        {
            MovL.UpdateMovieCost(value);
            this.hub.Clients.All.SendAsync("MovieUpdated", value);
        }


        // DELETE /movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movieToDelete = this.MovL.GetMovie(id);
            MovL.DeleteMovie(id);
            this.hub.Clients.All.SendAsync("MovieDeleted", movieToDelete);
        }

    }
}
