using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Microservice.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Category { get; set; }
    }
}
