using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class Modulo
    {
        public Modulo(int id, string hora, bool act)
        {
            this.id = id;
            this.start_time = hora;
            this.isActivo = act;
        }
        public int id { get; set; }
        public string start_time { get; set; }
        public string finish_time { get; set; }
        public bool isActivo { get; set; }
    }
}
