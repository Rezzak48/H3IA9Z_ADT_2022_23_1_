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
    public class VisitorRepository : Repository<Visitor>, IVisitorRepository
    {
        public VisitorRepository(ChooseYourMovieDbContext DbContext) : base(DbContext) { }

        public override Visitor GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(vis => vis.Id == id);
        }
        public void UpdateAddress(int id, string newAddress)
        {
            var vis = this.GetOne(id);
            if (vis == null)
            {
                throw new Exception("Visitor is not exist in Db");
            }
            else
            {
                vis.Address = newAddress;
                this.cntx.SaveChanges();
            }
        }
    }
}
