using PersonDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Domain.Entities
{
    public class PhoneNumber
    {
        public int Id { get; set; }

        public PhoneType Type { get; set; }

        public string  Number { get; set; }
    }
}
