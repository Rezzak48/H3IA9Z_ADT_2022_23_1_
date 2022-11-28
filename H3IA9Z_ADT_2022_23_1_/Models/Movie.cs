using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Movie
    {
        public Movie()
        {
            this.Reservation = new HashSet<Reservation>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }
        public int Duration { get; set; }

        public int Price { get; set; }

        public override string ToString()
        {
            return $"\n{this.Id,3} |  {this.Duration} hours {this.Price,10} HUF {this.Category,10}\t {this.Name,-1}";
        }

        public virtual ICollection<Reservation> Reservation { get; }
    }
}
