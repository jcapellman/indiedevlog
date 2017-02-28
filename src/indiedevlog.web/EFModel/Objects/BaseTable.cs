using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indiedevlog.web.EFModel.Objects
{
    public class BaseTable
    {
        public int ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }
    }
}