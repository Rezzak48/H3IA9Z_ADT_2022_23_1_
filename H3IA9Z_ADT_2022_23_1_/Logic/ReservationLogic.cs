using H3IA9Z_ADT_2022_23_1_Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Logic
{
    public class ReservationLogic : IReservationLogic
    {
        protected IReservationsRepository ireservationsRepository;
        protected IMovieRepository imoviesrepo;
        protected IVisitorRepository ivistrepo;
        public ReservationLogic(IReservationsRepository reservationsRepository, IVisitorRepository vistrepo, IMovieRepository moviesrepo)
        {
            this.ivistrepo = vistrepo;
            this.ireservationsRepository = reservationsRepository;
            this.imoviesrepo = moviesrepo;
        }
        public Reservation AddNewReservation(Reservation rsv)
        {
            if (ivistrepo.GetOne((int)rsv.VisitorId) == null || imoviesrepo.GetOne((int)rsv.MovieId) == null)
            {
                throw new Exception("Invalid data");
            }
            else
            {
                this.ireservationsRepository.Insert(rsv);
                return rsv;
            }
        }

        public void DeleteReservation(int id)
        {
            Reservation ReservationToDelete = this.ireservationsRepository.GetOne(id);
            if (ReservationToDelete != null)
            {
                this.ireservationsRepository.Remove(ReservationToDelete);
            }
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return this.ireservationsRepository.GetAll();
        }

        public Reservation GetReservation(int id)
        {
            Reservation ReservationToReturn = this.ireservationsRepository.GetOne(id);
            if (ReservationToReturn != null)
            {
                return ReservationToReturn;
            }
            else
            {
                throw new Exception("ID not founded for this reservation.");
            }
        }

        public void UpdateReservationDate(Reservation rsv)
        {
            this.ireservationsRepository.UpdateDate(rsv.Id, rsv.DateTime);
        }
    }
}
