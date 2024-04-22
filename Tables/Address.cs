using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfomatikPizza.Tables
{
   
    public class Address
    {
        public int Id;
        public string FullAddress;
        public Address(
            int id,
            string address
            )
        {
            Id = id;
            FullAddress = address;
        }
    }

}
