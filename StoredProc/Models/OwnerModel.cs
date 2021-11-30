using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoredProcedure.Models
{
    public class OwnerModel
    {
        public Guid id { get; set; }
        public string company_name { get; set; }
        public string first_name { get; set; }
        public int street_number { get; set; }
        public string race { get; set; }
    }
}