using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca
{
    public class MetodoPago
    {
        public MetodoPago(int id, string metodo)
        {
            this.id = id;
            this.metodo = metodo;
        }

        public int id { get; set; }
        public string metodo { get; set; }
    }
}
