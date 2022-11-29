using H3IA9Z_ADT_2022_23_1_Repository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Logic
{
    public class VisitorLogic : IVisitorLogic
    {
        protected IReservationsRepository iReservationsRepository;
        protected IVisitorRepository iVisitorRepository;
        public VisitorLogic(IReservationsRepository reservationsRepo, IVisitorRepository visiRepo)
        {
            iReservationsRepository = reservationsRepo;
            iVisitorRepository = visiRepo;
        }

        public Visitor AddNewVis(Visitor vis)
        {
            if (vis.Name == null)
            {
                throw new ArgumentException("ERROR : Name not founded");
            }
            else
            {

                this.iVisitorRepository.Insert(vis);
                return vis;
            }
        }

      

        public void DeleteVisitor(int id)
        {
            Visitor visToDelete = this.iVisitorRepository.GetOne(id);
            if (visToDelete != null)
            {
                this.iVisitorRepository.Remove(visToDelete);
            }
            else
            {
                throw new ArgumentException("Error : no name assigned to this Id founded.");
            }
        }

        public IEnumerable<Visitor> GetAllVisitors()
        {
            return this.iVisitorRepository.GetAll();

        }

        public Visitor GetVisitor(int id)
        {
            Visitor visToReturn = this.iVisitorRepository.GetOne(id);
            if (visToReturn != null)
            {
                return visToReturn;
            }
            else
            {
                throw new Exception("This ID not found.");
            }
        }


        public List<KeyValuePair<int, int>> BestVisitor()
        {
            var BestVis = from vis in this.iVisitorRepository.GetAll().ToList()
                          join Reservations in this.iReservationsRepository.GetAll().ToList()
                          on vis.Id equals Reservations.VisitorId
                          group Reservations by Reservations.VisitorId.Value into gr
                          select new
                          {
                              id = gr.Key,
                              c = gr.Count()
                          };
            int max = BestVis.AsEnumerable().Max(t => t.c);

            int[] maxNumOfReservationss = BestVis.Where(x => x.c == max).Select(x => x.id).ToArray();
            List<KeyValuePair<int, int>> re = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < maxNumOfReservationss.Length; i++)
            {
                re.Add(new KeyValuePair<int, int>(maxNumOfReservationss[i], max));
            }
            return re;
        }
        public int ReservationsNumber(int id)
        {
            if (GetVisitor(id) == null)
            {
                throw new Exception("This Vis id does not exist in our Database.");
            }
            else
            {
                var res = this.iReservationsRepository.GetAll().Count(x => x.VisitorId == id);
                return res;
            }
        }

        public void UpdateAddress(Visitor vis)
        {
            this.iVisitorRepository.UpdateAddress(vis.Id, vis.City);
        }
        public List<KeyValuePair<int, int>> WorstVisitor()
        {
            var WorstVis = from vis in this.iVisitorRepository.GetAll().ToList()
                           join Reservations in this.iReservationsRepository.GetAll().ToList()
                           on vis.Id equals Reservations.MovieId
                           group Reservations by Reservations.MovieId.Value into gr
                           select new
                           {
                               id = gr.Key,
                               c = gr.Count()
                           };
            int min = WorstVis.AsEnumerable().Min(t => t.c);
            int[] minNumOfReservationss = WorstVis.Where(x => x.c == min).Select(x => x.id).ToArray();
            List<KeyValuePair<int, int>> re = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < minNumOfReservationss.Length; i++)
            {
                re.Add(new KeyValuePair<int, int>(minNumOfReservationss[i], min));
            }

            return re;
        }
    }
}
