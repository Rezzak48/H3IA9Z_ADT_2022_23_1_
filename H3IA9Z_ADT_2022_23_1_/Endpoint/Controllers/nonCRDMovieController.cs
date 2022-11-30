using H3IA9Z_ADT_2022_23_1_Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace H3IA9Z_ADT_2022_23_1_Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class nonCRDMovieController : ControllerBase
    {
        IMovieLogic Mvl;

        public nonCRDMovieController(IMovieLogic mvl)
        {
            Mvl = mvl;
        }


        // GET: Noncrudmovies/MovieEarnings
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> MovieEarnings()
        {
            return Mvl.MovieEarnings();
        }


        // GET: Noncrudmovies/MostSellmovie
        [HttpGet]
        public List<KeyValuePair<string, int>> MostSellmovie()
        {
            return Mvl.MostSellMovie();
        }


        // GET: Noncrudmovies/LessSellmovie
        [HttpGet]
        public List<KeyValuePair<string, int>> LessSellmovie()
        {
            return Mvl.LessSellMovie();
        }
    }
}
