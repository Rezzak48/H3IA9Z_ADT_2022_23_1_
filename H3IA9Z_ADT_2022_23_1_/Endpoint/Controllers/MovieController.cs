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
        private IMovieLogic ML;

        private IHubContext<SignalRHub> hub;

        public MovieController(IMovieLogic mL, IHubContext<SignalRHub> hub)
        {
            ML = mL;
            this.hub = hub;
        }

        // GET: /movies
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return ML.GetAllMovies();
        }

        // GET /movies/5
        [HttpGet("{id}")]
        public Movie Get(int id)
        {
            return ML.GetMovie(id);
        }

        // POST /movies
        [HttpPost]
        public void Post([FromBody] Movie value)
        {
            ML.AddNewMovie(value);
            this.hub.Clients.All.SendAsync("MovieCreated", value);
        }

        // PUT /movies
        [HttpPut]
        public void Put([FromBody] Movie value)
        {
            ML.UpdateMovieCost(value);
            this.hub.Clients.All.SendAsync("MovieUpdated", value);
        }

        // DELETE /movies/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var movieToDelete = this.ML.GetMovie(id);
            ML.DeleteMovie(id);
            this.hub.Clients.All.SendAsync("MovieDeleted", movieToDelete);
        }
    }
}