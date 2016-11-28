using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetaPoco;

namespace Data.UnitOfWork
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        public UnitOfWorkBase(string connectionStringName, bool useTransaction)
        {
            Context = new Database(connectionStringName);
            _useDispose = true;
            if (!useTransaction) return;
            
            _transaction = new Transaction(Context);
        }

        /// <summary>
        /// For use with other Database instances outside of Unit Of Work
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWorkBase(Database context)
        {
            Context = context;
            _useDispose = false;
        }
        
        public Database Context { get; private set; }
        private readonly bool _useDispose;
        private Transaction _transaction;

        public void Commit()
        {
            if (_transaction == null) return;

            _transaction.Complete();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Dispose()
        {
            var doThrowTransactionException = false;
            
            if (_transaction != null)
            {
                Context.AbortTransaction();
                _transaction.Dispose();
                doThrowTransactionException = true;
            }

            if (_useDispose && Context != null)
            {
                Context.Dispose();
                Context = null;
            }

            if (doThrowTransactionException)
                throw new DataException("Transaction was aborted");
        }
    }


}
