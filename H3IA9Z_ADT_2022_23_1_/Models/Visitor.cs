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
    public class Visitor
    {
        public Visitor()
        {
            this.Reservation = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Reservation> Reservation { get; }


        public override string ToString()
        {
            return $"\n{this.Id,3} | {this.Name,-20} {this.Email,-28} {this.PhoneNumber,10}  \t {this.City}";
        }
    }

}
