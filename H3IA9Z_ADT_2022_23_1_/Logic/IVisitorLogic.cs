using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Logic
{
    public interface IVisitorLogic
    {
        void UpdateAddress(Visitor vis);

        public Visitor AddNewVis(Visitor vis);
        public void DeleteVisitor(int id);
        Visitor GetVisitor(int id);
        IEnumerable<Visitor> GetAllVisitors();

        List<KeyValuePair<int, int>> BestVisitor();
        List<KeyValuePair<int, int>> WorstVisitor();
        int ReservationsNumber(int id);
}
}
