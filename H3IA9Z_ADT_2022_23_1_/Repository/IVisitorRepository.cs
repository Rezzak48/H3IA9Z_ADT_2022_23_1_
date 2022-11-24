﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3IA9Z_ADT_2022_23_1_Repository
{
    internal interface IVisitor : IRepository<Visitor>
    {
        void UpdateAddress(int id, string newAddress);
    }
  
}