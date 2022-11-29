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
    [Table("movies")]
    public class Movie
    {
        public Movie()
        {
            this.Reservation = new HashSet<Reservation>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public string Category { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int Price { get; set; }

        public override string ToString()
        {
            return $"\n{this.Id,3} |  {this.Duration} hours {this.Price,10} HUF {this.Category,10}\t {this.Name,-1}";
        }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Reservation> Reservation { get; }
    }
}
