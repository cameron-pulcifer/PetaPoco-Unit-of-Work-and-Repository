using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Tests.Models
{
    [PetaPoco.TableName("CustomerTypes")]
    [PetaPoco.PrimaryKey("CustomerTypeId")]
    public class CustomerType
    {
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
    }
}
