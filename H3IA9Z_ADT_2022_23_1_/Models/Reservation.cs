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
    public class Reservation
    {
        public Reservation()
        {
        }
        public int Id { get; set; }
        public DateTime DateTime { get; set; }

        public virtual Visitor Visitor { get; set; }


        public int? VisitorId { get; set; }



        public virtual Movie Movie { get; set; }


        public int? MovieId { get; set; }




        public override string ToString()
        {
            return $"\n{this.Id,3} | {this.VisitorId,-20} {this.DateTime.Year,10}.{this.DateTime.Month}.{this.DateTime.Day} \t{this.MovieId,15}";
        }
    }
}
