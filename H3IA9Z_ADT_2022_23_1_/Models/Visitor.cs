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
    [Table("visitors")]
    public class Visitor
    {
        public Visitor()
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
        public string Address { get; set; }

        [Required]
        public int PhoneNumber { get; set; }

        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Reservation> Reservation { get; }


        public override string ToString()
        {
            return $"\n{this.Id,3} | {this.Name,-20} {this.Email,-28} {this.PhoneNumber,10}  \t {this.Address}";
        }
    }

}
