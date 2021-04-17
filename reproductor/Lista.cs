using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reproductor
{
    class Lista
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            ListaDoblementeEnlazada lista = new ListaDoblementeEnlazada();

            for (int k = 0; k < 20; k++) lista.Agregar(rnd.Next(0, 100));

            Nodo sig = lista.Primero;
            while (sig != null)
            {
                Console.Write(sig.Valor.ToString() + '\t');
                sig = sig.Siguiente;
            }

            Console.WriteLine('\n');
            Nodo ant = lista.Ultimo;
            while (ant != null)
            {
                Console.Write(ant.Valor.ToString() + '\t');
                ant = ant.Anterior;
            }

            Console.ReadKey();
        }
    }

    class ListaDoblementeEnlazada
    {
        private Nodo m_primero;
        private Nodo m_ultimo;

        public Nodo Primero { get { return m_primero; } }
        public Nodo Ultimo { get { return m_ultimo; } }

        public ListaDoblementeEnlazada()
        {
            m_primero = null;
            m_ultimo = null;
        }

        public virtual void Agregar(int valor)
        {
            Nodo nuevo = new Nodo();

            nuevo.Valor = valor;

            if (m_primero == null)
            {
                m_primero = nuevo;
                m_ultimo = nuevo;

                nuevo.Anterior = null;
                nuevo.Siguiente = null;
            }
            else
            {
                Nodo aux = m_primero;

                while (aux != null && aux.Valor < valor)
                {
                    aux = aux.Siguiente;
                }

                if (aux == null)
                {
                    nuevo.Anterior = m_ultimo;
                    Ultimo.Siguiente = nuevo;
                    nuevo.Siguiente = null;
                    m_ultimo = nuevo;
                }
                else
                {
                    if (aux.Anterior == null) m_primero = nuevo;
                    else aux.Anterior.Siguiente = nuevo;

                    nuevo.Anterior = aux.Anterior;
                    aux.Anterior = nuevo;
                    nuevo.Siguiente = aux;
                }
            }
        }

    }

    class Nodo
    {
        public Nodo Anterior { get; set; }
        public Nodo Siguiente { get; set; }
        public int Valor { get; set; }
    }
}

