using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Data.UnitOfWork
{
    public class SampleUnitOfWork : UnitOfWorkBase
    {
        public SampleUnitOfWork(bool useTransaction = false)
            : base("SampleConnection", useTransaction)
        {

        }

        public SampleUnitOfWork(Database context)
            :base(context)
        {
            
        }
    }
}
