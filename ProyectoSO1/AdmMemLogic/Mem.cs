using System;
using System.Collections.Generic;

namespace AdmMemLogic
{
    public class Mem
    {
        public int M { get; set; }//cantidad de bytes que tiene la memoria RAM
        public int S { get; set; }//tamaño del monitor
        public int MemAvail { get; set; }
        public int MaxAvail { get; set; }

        public NodoList L { get; set; }
        public Mem()
        {
            L = new NodoList();
        }
        public Mem(int tam)
        {
            M = tam;
            S = Convert.ToInt32(Math.Truncate(tam * 0.1));
            L = new NodoList();
           // L.AddOrdenedByDir(new Nodo { Dir = 0, Tam = S });
            L.AddOrdenedByDir(new Nodo { Dir = M-S, Tam = S });
            MemAvail = M - S;
            MaxAvail = M - S;
        }

        public void LoadProcess(ref Process process)
        {
            int dir = getMemFF(process.Tam);
            process.Dir = dir;
            if (dir == -1)
            {
                throw new Exception("no memory");
            }
            else
            {
                if (L.Length() == 0)
                {
                    Nodo nodo = new Nodo { Dir = 0, Tam = S + Convert.ToInt32(process.Tam) };
                    L.Add(nodo);
                    updateVariables();
                    return;
                }
                else if (L.Length() == 1)
                {
                    if (process.Dir == 0) {
                        Nodo nodo = new Nodo { Dir=0,Tam=process.Tam};
                        L.AddOrdenedByDir(nodo);
                    }
                    //L.GetNodo(0).Tam += Convert.ToInt32(process.Tam);
                    updateVariables();
                    return;
                }
                

                for (int i = 0; i < L.Length() - 1; i++)
                {
                    int dirFinCurrentProcess = process.Dir + process.Tam;
                    if (dirFinCurrentProcess == L.GetNodo(i + 1).Dir && (getFreeSize(process.Dir) == process.Tam))
                    {
                        Nodo NodoMerged = new Nodo { Dir = L.Dir(i), Tam = L.Tam(i) + L.Tam(i + 1) + process.Tam };
                        L.RemoveNodo(i);
                        L.RemoveNodo(i);
                        L.AddOrdenedByDir(NodoMerged);
                        updateVariables();
                        return;
                    }
                    if ((getFreeSize(process.Dir) >= process.Tam))
                    {
                        Nodo nodo = new Nodo { Dir = L.GetNodo(i).Dir, Tam = L.GetNodo(i).Tam + process.Tam };
                        L.RemoveNodo(i);
                        L.AddOrdenedByDir(nodo);
                        updateVariables();
                        return;
                    }
                }



                if (isTheLastFill())
                {
                    for (int i = 0; i < L.Length(); i++)
                    {
                        int dirFreeSpace = L.GetNodo(i).Dir + L.GetNodo(i).Tam;
                        if (dirFreeSpace == dir)
                        {
                            Nodo nodo = new Nodo { Dir = L.GetNodo(i).Dir, Tam = L.GetNodo(i).Tam + process.Tam };
                            L.RemoveNodo(i);
                            L.AddOrdenedByDir(nodo);
                            updateVariables();
                            return;
                        }
                    }
                }
            }
        }

        private void updateVariables()
        {
            UpdateMaxAvail();
            UpdateMemAvail();
        }

        public bool isTheLastFill()
        {
            return M == L.GetNodo(L.Length() - 1).Tam + L.GetNodo(L.Length() - 1).Dir;
        }
        public bool isThefirstFill()
        {
            if (L.GetNodo(0).Dir == 0 && L.GetNodo(0).Tam > S)
            {
                return true;
            }
            else { return false; }


        }
        public int getFreeSize(int dirFree)
        {
            for (int i = 0; i < L.Length() - 1; i++)
            {
                if (L.GetNodo(i).Dir + L.GetNodo(i).Tam == dirFree)
                {
                    return L.GetNodo(i + 1).Dir - dirFree;
                }
            }
            if (!isTheLastFill())
            {

                return M - dirFree;
            }
            return -1;
        }

        public int getMemBF(int size)
        {
            return 1;
        }
        public int getMemFF(int size)
        {

            if (size > MaxAvail)
            {
                return -1;
            }
            if (L.Length() == 0)
            {
                return S;
            }
            int tamLibre = -1;
            int dirLibre = -1;
            for (int i = 0; i < L.Length() - 1; i++)
            {
                dirLibre = L.GetNodo(i).Dir + L.GetNodo(i).Tam;
                tamLibre = L.GetNodo(i + 1).Dir - dirLibre;
                if (tamLibre >= size)
                {
                     return dirLibre;
                }
            }
            if (M > L.GetNodo(L.Length() - 1).Dir + L.GetNodo(L.Length() - 1).Tam)
            {
                dirLibre = L.GetNodo(L.Length() - 1).Dir + L.GetNodo(L.Length() - 1).Tam;
            }else if(M == L.GetNodo(L.Length() - 1).Dir + L.GetNodo(L.Length() - 1).Tam)
            {
                return 0;
            }
            return dirLibre;


        }
        public bool EnoughMem(int tam)
        {
            return true;
        }
        public int LastDir()
        {
            return 1;
        }
        public void FreeMem(int dir) { }
        public void FreeMem(Process process)
        {
            
            if (process.Dir == S )
            {
                if (L.Length() == 1) {
                    if(L.GetNodo(0).Tam > S + process.Tam)
                    {
                Nodo newNodo = new Nodo { Dir = 0, Tam = S };
                Nodo newNodo2 = new Nodo { Dir = process.Tam+process.Dir, Tam = L.GetNodo(0).Tam-(process.Dir+process.Tam) };
                L.RemoveNodo(0);
                L.AddOrdenedByDir(newNodo);
                L.AddOrdenedByDir(newNodo2);
                        updateVariables();
                return;

                    }
                }
            }

            for (int i = 0;i < L.Length(); i++)
            {
                if (L.GetNodo(i).Dir==process.Dir)
                {
                    if (process.Dir + process.Tam == M)
                    {
                        L.RemoveNodo(L.Length()-1);
                        updateVariables();
                        return;
                    }
                    Nodo nodo = new Nodo { Dir = process.Dir + process.Tam ,Tam=(L.GetNodo(i).Tam + L.GetNodo(i).Dir)-(process.Dir+process.Tam) };
                    L.RemoveNodo(i);
                    if (nodo.Tam != 0)
                    {
                    L.AddOrdenedByDir(nodo);

                    }
                    updateVariables();
                    return;
                }
            }
            for(int i = 0; i < L.Length(); i++)
            {
                if (process.Dir<L.GetNodo(i).Dir+L.GetNodo(i).Tam  &&  process.Dir>L.GetNodo(i).Dir) {
                    Nodo nodo = new Nodo { Dir = L.GetNodo(i).Dir, Tam = L.GetNodo(i).Tam - process.Tam };
                    L.RemoveNodo(i);
                    L.AddOrdenedByDir(nodo);
                    updateVariables();
                    return;
                }
            }

            
        }
        public Nodo TwoProcessOneNodo(Process process1, Process process2)
        {

            if (process1.Dir + process1.Tam == process2.Dir)
            {
                return new Nodo { Dir = process1.Dir, Tam = process1.Tam + process2.Tam };
            }
            else
            {
                return null;
            }

        }

        public NodoList generateNodoList(List<Process> processes)
        {
            NodoList nodoList = new NodoList();
            Nodo nodo = new Nodo();
                    nodo.Dir = 0;
                    nodo.Tam = S;
            
            for (int i = 0; i < processes.Count - 1; i++)
            {
                
                if (processes[i].Dir + processes[i].Tam == processes[i + 1].Dir)
                {
                    nodo.Tam += processes[i].Tam;
                }
                else
                {
                    if (i ==0 ) {
                        
                    nodo.Tam += processes[i].Tam;
                    }
                    nodoList.AddOrdenedByDir(nodo);
                    nodo = new Nodo
                    {
                        Dir = processes[i + 1].Dir
                      , Tam = processes[i + 1].Tam
                    };
                }
            }
            if (processes[processes.Count - 2].Dir + processes[processes.Count - 2].Tam == processes[processes.Count - 1].Dir)
            {
                nodo.Tam += processes[processes.Count-1].Tam;
                nodoList.AddOrdenedByDir(nodo);
            }
            else
            {
               // nodoList.AddOrdenedByDir(nodo);
                nodo.Dir = processes[processes.Count - 1].Dir;
                nodo.Tam = processes[processes.Count - 1].Tam;
                nodoList.AddOrdenedByDir(nodo);
            }

            return nodoList;
        }

        public void FreeMem(string processName) { }
        public void UpdateMemAvail()
        {
            int memAvail = 0;
            if (L.GetNodo(0).Dir != 0) {
                memAvail += L.GetNodo(0).Dir;
            }
            for(int i = 0; i < L.Length()-1; i++)
            {
                memAvail += getFreeSize(L.GetNodo(i).Dir + L.GetNodo(i).Tam);
            }
            if (!isTheLastFill())
            {
                memAvail+= getFreeSize(L.GetNodo(L.Length()-1).Dir + L.GetNodo(L.Length()-1).Tam);
            }

            this.MemAvail = memAvail;
        }
        
        public void UpdateMaxAvail()
        {
            int maxAvail = 0;
            if (L.GetNodo(0).Dir != 0)
            {
                maxAvail += L.GetNodo(0).Dir;
            }
            for (int i = 0; i < L.Length()-1; i++)
            {
                if (getFreeSize(L.GetNodo(i).Dir + L.GetNodo(i).Tam)>=maxAvail) {
                maxAvail = getFreeSize(L.GetNodo(i).Dir + L.GetNodo(i).Tam);
                }
            }
            if (!isTheLastFill())
            {
                if (getFreeSize(  L.GetNodo( L.Length() - 1).Dir +  L.GetNodo( L.Length() - 1).Tam) >= maxAvail)
                {

                    maxAvail = getFreeSize(L.GetNodo(L.Length() - 1).Dir + L.GetNodo(L.Length() - 1).Tam);
                }
            }
            this.MaxAvail = maxAvail;
        }

    }
}
