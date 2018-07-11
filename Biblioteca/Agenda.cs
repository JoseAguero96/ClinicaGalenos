using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class Agenda
    {
        public int id { get; set; }
        public int medico_id { get; set; }
        public int atention_module_id { get; set; }
        public int user_id { get; set; }
        public string day { get; set; }

        public string estado_agenda { get; set; }
    }
}
