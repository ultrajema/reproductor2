using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reproductor.listadobleenla
{
    class iterlistadoble
    {
        private Nodo actual;

        public iterlistadoble(Listadoble id)
        {
            actual = id.cabeza;
        }

        public Nodo siguiente()
        {
            Nodo a;
            a = actual;
            if (actual != null)
            {
                actual = actual.adelante;
            }
            return a;
        }

    }
}
