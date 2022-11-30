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
    [Table("reservations")]
    public class Reservation
    {
        public Reservation()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]

        public DateTime DateTime { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Visitor Visitor { get; set; }

        [ForeignKey(nameof(Visitor))]
        public int? VisitorId { get; set; }


        [NotMapped]
        [JsonIgnore]
        public virtual Movie Movie { get; set; }

        [ForeignKey(nameof(Movie))]
        public int? MovieId { get; set; }




        public override string ToString()
        {
            return $"\n{this.Id,3} | {this.VisitorId,-20} {this.DateTime.Year,10}.{this.DateTime.Month}.{this.DateTime.Day} \t{this.MovieId,15}";
        }
    }
}
