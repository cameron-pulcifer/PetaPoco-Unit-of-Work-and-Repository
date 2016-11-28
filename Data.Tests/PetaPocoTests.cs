using System;
using System.Linq;
using Data.Repositories;
using Data.Repositories.Helpers;
using Data.Tests.Models;
using Data.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Data.Tests
{
    [TestClass]
    public class PetaPocoTests
    {
        [TestMethod]
        public void CanGetAll()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var repo = new RepositoryBase<Customer>(uow);
                var list = repo.GetAll().ToList();
                Assert.IsNotNull(list);
            }
        }

        [TestMethod]
        public void CanGet()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var repo = new RepositoryBase<Customer>(uow);
                var customer = repo.Get<int>(1);
                Assert.IsNotNull(customer);
            }
        }

        [TestMethod]
        public void CanGetByForeignKey()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var repo = new ForeignKeyRepositoryBase<Customer>(uow);
                var list = repo.GetByForeignKey("customertypeid", 1).ToList();
                Assert.IsNotNull(list);
            }
        }

        [TestMethod]
        public void CanAdd()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var repo = new RepositoryBase<Customer>(uow);
                var customer = new Customer
                {
                    CustomerName = "Peter Parker",
                    CustomerTypeId = 1
                };

                var id = repo.Add<int>(customer);
                Assert.IsTrue(id > 0);
            }
        }


        [TestMethod]
        public void CanModify()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var repo = new RepositoryBase<Customer>(uow);
                var customer = repo.Get(1);
                customer.CustomerName = "Penelope Cruz";
                repo.Modify(customer);

                var repo2 = new RepositoryBase<Customer>(uow);
                var updatedCustomer = repo2.Get(1);
                Assert.AreEqual("Penelope Cruz", updatedCustomer.CustomerName);
            }
        }

        [TestMethod]
        public void CanRemove()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var repo = new RepositoryBase<Customer>(uow);
                var customer = repo.GetAll().OrderByDescending(x => x.CustomerId).First();
                var id = customer.CustomerId;
                repo.Remove(customer);

                var removedCustomer = repo.Get(id);
                Assert.IsNull(removedCustomer);
            }
        }

        [TestMethod]
        public void CanInsertOrUpdate()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var repo = new RepositoryBase<Customer>(uow);
                var customer = repo.Get(1);
                customer.CustomerName = "Penelope Cruz - insert or updated";
                repo.InsertOrUpdate<int>(customer);

                var repo2 = new RepositoryBase<Customer>(uow);
                var updatedCustomer = repo2.Get(1);
                Assert.AreEqual("Penelope Cruz - insert or updated", updatedCustomer.CustomerName);
            }
        }

        [TestMethod]
        public void CanVoidTransaction()
        {
            var customer1 = new Customer();
            var customer2 = new Customer();
            var unmodified1 = new Customer();
            var unmodified2 = new Customer();
            var newCustomer = new Customer();
            var newCustomerId = 0;
            const string name1 = "Tom Cruise";
            const string name2 = "Frank Sinatra";

            try
            {
                // This one is using a transaction
                using (var uow = new SampleUnitOfWork(true))
                {
                    var repo = new RepositoryBase<Customer>(uow);
                    customer1 = repo.Get(1);
                    customer1.CustomerName = name1;
                    repo.Modify(customer1);

                    customer2 = repo.Get(2);
                    customer2.CustomerName = name2;
                    repo.Modify(customer2);

                    newCustomer.CustomerTypeId = 3;
                    newCustomer.CustomerName = "Tom Selleck";

                    newCustomerId = repo.Add<int>(newCustomer);
                    //newCustomerId = newCustomer.CustomerId;
                    
                    // Forget to call uow.Commit() on purpose here to roll back transaction
                }
            }
            catch (DataException ex)
            {
                // swallow exception for this case
            }

            using (var uow = new SampleUnitOfWork())
            {
                var repo = new RepositoryBase<Customer>(uow);
                unmodified1 = repo.Get(1);
                unmodified2 = repo.Get(2);
                newCustomer = repo.Get(newCustomerId);
            }

            // These were not reverted by the Abort
            Assert.IsTrue(customer1.CustomerName == name1); 
            Assert.IsTrue(customer2.CustomerName == name2);

            Assert.IsTrue(unmodified1.CustomerName != name1);
            Assert.IsTrue(unmodified2.CustomerName != name2);

            Assert.IsTrue(unmodified1.CustomerName != customer1.CustomerName);
            Assert.IsTrue(unmodified2.CustomerName != customer2.CustomerName);

            Assert.IsNull(newCustomer);
        }

        [TestMethod]
        public void CanCommitTransaction()
        {
            var customer1 = new Customer();
            var customer2 = new Customer();
            var unmodified1 = new Customer();
            var unmodified2 = new Customer();
            var newCustomer = new Customer();
            var newCustomerId = 0;
            const string name1 = "Bob Hope - updated";
            const string name2 = "Peter Parker - updated";

            // This one is using a transaction with Commit()
            using (var uow = new SampleUnitOfWork(true))
            {
                var repo = new RepositoryBase<Customer>(uow);
                customer1 = repo.Get(1);
                customer1.CustomerName = name1;
                repo.Modify(customer1);

                customer2 = repo.Get(2);
                customer2.CustomerName = name2;
                repo.Modify(customer2);

                newCustomer.CustomerTypeId = 3;
                newCustomer.CustomerName = "Betty Davis";

                newCustomerId = repo.Add<int>(newCustomer);

                // must call commit
                uow.Commit();
            }

            using (var uow = new SampleUnitOfWork())
            {
                var repo = new RepositoryBase<Customer>(uow);
                unmodified1 = repo.Get(1);
                unmodified2 = repo.Get(2);
                newCustomer = repo.Get(newCustomerId);
            }

            Assert.IsTrue(customer1.CustomerName == name1); 
            Assert.IsTrue(customer2.CustomerName == name2);

            Assert.IsTrue(unmodified1.CustomerName == name1);
            Assert.IsTrue(unmodified2.CustomerName == name2);

            Assert.IsTrue(unmodified1.CustomerName == customer1.CustomerName);
            Assert.IsTrue(unmodified2.CustomerName == customer2.CustomerName);

            Assert.IsTrue(newCustomer.CustomerId == newCustomerId);
        }

        [TestMethod]
        public void CanGetFirstMatchWithMultipleColumnLookupsUsingDynamicObject()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var repo = new ColumnLookupRepositoryBase<Customer>(uow);
                var customer = repo.First(new { CustomerName = "Penelope Cruz", CustomerTypeId = 2 });
                var customer2 = repo.FirstOrDefault(new { CustomerName = "Penelope Cruz", CustomerTypeId = 1 });

                var queryBuilderResult = repo.FindByColumns(new {CustomerName = "Penelope Cruz", CustomerTypeId = 2}).First();

                Assert.IsNotNull(customer);
                Assert.IsNotNull(queryBuilderResult);
                Assert.IsNull(customer2);
            }
        }
    
    }
}
