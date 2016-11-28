using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.UnitOfWork;
using Data.Tests.Models;
using PetaPoco;
using System.Collections.Generic;

namespace Data.Tests
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void CanJoin()
        {
            using (var uow = new SampleUnitOfWork())
            {
                var sql = PetaPoco.Sql.Builder
                .Append("SELECT c.*, t.CustomerTypeName")
                .Append("FROM Customers c")
                .Append("LEFT JOIN CustomerTypes t ON c.CustomerTypeId = t.CustomerTypeId");

                var results = uow.Context.Fetch<Customer, CustomerType, Customer>((c, t) => { c.CustomerTypeName = t.CustomerTypeName; return c; }, sql);

                Assert.IsNotNull(results);

            }
        }
    }
}
