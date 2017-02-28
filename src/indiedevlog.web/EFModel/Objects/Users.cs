using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace indiedevlog.web.EFModel.Objects
{
    public class Users : BaseTable
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public bool IsConfirmed { get; set; }
    }
}