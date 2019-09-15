using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmMemLogic
{
   public class NodoList
    {
        private List<Nodo> Nodos = new List<Nodo>();

        public int Length()
        {
            return Nodos.Count;
        }
        public int Dir(int nroNodo)
        {
            Nodo temp = Nodos.ElementAt(nroNodo);
            return temp.Dir;
        }
        public int Tam(int nroNodo)
        {
            Nodo temp = Nodos.ElementAt(nroNodo);
            return temp.Tam;
        }
        public void Add(Nodo nodo) {
            Nodos.Add(nodo);
        }
        public void AddOrdenedByDir(Nodo nodo)
        {
           // Nodos.Add(nodo);

            int pos = -1;
            for (int i = 0; i < Nodos.Count; i++)
            {
                if (nodo.Dir + nodo.Tam < Nodos.ElementAt(i).Dir)
                {
                    Nodos.Insert(i, nodo);
                    return;
                }
                
            }
            if (pos == -1)
            {
                pos = Nodos.Count ;
            }
            
            Nodos.Insert(pos, nodo);

        }
        public Nodo GetNodo(int index) {
            return Nodos.ElementAt(index);
        }
        public void RemoveNodo(int nroNodo)
        {
            Nodos.RemoveAt(nroNodo);
        }

    }
}
