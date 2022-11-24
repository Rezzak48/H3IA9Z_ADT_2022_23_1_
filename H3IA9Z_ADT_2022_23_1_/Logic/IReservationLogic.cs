using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Logic
{
    public interface IReservationLogic
    {
        void UpdateReservationDate(Reservation rsv);
        public Reservation AddNewReservation(Reservation rsv);
        public void DeleteReservation(int id);
        Reservation GetReservation(int id);
        IEnumerable<Reservation> GetAllReservations();
    }
}
