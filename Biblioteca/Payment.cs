using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class Payment
    {
        public int user_id { get; set; }
        public int monto { get; set; }
        public int payment_type_id { get; set; }
        public int agenda_id { get; set; }

    }
}
