using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Database Context { get; }
        void Commit();
    }
}
