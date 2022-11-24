using Data;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Repository
{
    public class ReservationRepository : Repository<Reservation>, IReservationsRepository
    {
        public ReservationRepository(ChooseYourMovieDbContext DbContext) : base(DbContext) { }
        public void UpdateDate(int id, DateTime newDate)
        {
            var reservation = this.GetOne(id);
            if (reservation == null)
            {
                throw new Exception("This reservation doesn't exist for this date");

            }
            else
            {
                reservation.DateTime = newDate;
                this.cntx.SaveChanges();
            }

        }
        public override Reservation GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(reservation => reservation.Id == id);

        }
    }
}
