using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tests.Models
{
    [PetaPoco.TableName("Customers")]
    [PetaPoco.PrimaryKey("CustomerId")]
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerTypeId { get; set; }

        [PetaPoco.Ignore]
        public string CustomerTypeName { get; set; }
    }
}
