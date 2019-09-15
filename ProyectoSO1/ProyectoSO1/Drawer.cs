using AdmMemLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoSO1
{
    public static class Drawer
    {
        public static Font DefaultFont { get; private set; }

        public static void DrawMem(ref Panel panel,List<Process> processes,int sizeMonitor,int sizeMem)
        {
                
            Graphics g = panel.CreateGraphics();
            
            Rectangle MemSimulator = new Rectangle(0, 0, 99, sizeMem - 1);
            Rectangle monitor = new Rectangle(0, 0, 99, sizeMonitor);
            Pen pen = new Pen(Color.Black);
            g.FillRectangle(Brushes.LightGreen, MemSimulator);
            g.DrawRectangle(pen, MemSimulator);

            g.FillRectangle(Brushes.CadetBlue, monitor);
            g.DrawRectangle(pen, monitor);
            g.DrawString("monitor", new Font(FontFamily.GenericMonospace, 12), Brushes.Black, 15,Convert.ToInt32(sizeMonitor/2)-6);

            for (int i = 0; i < processes.Count; i++)
            {
                Rectangle microProcessRect = new Rectangle(0, processes[i].Dir, 99, processes[i].Tam);

                g.FillRectangle(Brushes.OrangeRed, microProcessRect);
                g.DrawRectangle(pen, microProcessRect);
                g.DrawString("P" + processes[i].Id.ToString(), new Font(FontFamily.GenericSerif, 10), Brushes.Black, 40, processes[i].Dir - 8 + Convert.ToInt32(Math.Truncate(Convert.ToDecimal(processes[i].Tam / 2))));


            }
        }
        public static void DrawMem(ref Panel panel,List<Process> processes,Mem mem)
        {
                
            Graphics g = panel.CreateGraphics();
            
            Rectangle MemSimulator = new Rectangle(0, 0, 99, mem.M - 1);
            Rectangle monitor = new Rectangle(0, mem.M-mem.S, 99, mem.S);
            Pen pen = new Pen(Color.Black);
            g.FillRectangle(Brushes.LightGreen, MemSimulator);
            g.DrawRectangle(pen, MemSimulator);

            g.FillRectangle(Brushes.CadetBlue, monitor);
            g.DrawRectangle(pen, monitor);
            g.DrawString("monitor", new Font(FontFamily.GenericMonospace, 12), Brushes.Black, 15,mem.M-mem.S+6);

            for (int i = 0; i < processes.Count; i++)
            {
                Rectangle microProcessRect = new Rectangle(0, processes[i].Dir, 99, processes[i].Tam);

                g.FillRectangle(Brushes.OrangeRed, microProcessRect);
                g.DrawRectangle(pen, microProcessRect);
                g.DrawString("P" + processes[i].Id.ToString(), new Font(FontFamily.GenericSerif, 10), Brushes.Black, 40, processes[i].Dir - 8 + Convert.ToInt32(Math.Truncate(Convert.ToDecimal(processes[i].Tam / 2))));


            }
        }
        public static void DrawList(ref Panel panel, NodoList nodoList) {
            for (int i = 0; i < nodoList.Length(); i++)
            {
               
                Graphics g2 = panel.CreateGraphics();
                Rectangle nodoRectangle = new Rectangle(i * 110, 0, 100, 40);
                Pen pen = new Pen(Color.Black);
                g2.FillRectangle(Brushes.WhiteSmoke, nodoRectangle);
                g2.DrawRectangle(pen, nodoRectangle);
                g2.DrawLine(pen, i * 110 + 40, 0, i * 110 + 40, 40);
                g2.DrawLine(pen, i * 110 + 80, 0, i * 110 + 80, 40);

                g2.DrawString(nodoList.Dir(i).ToString(), new Font(FontFamily.GenericSerif, 10), Brushes.Black, i * 110 + 15, 15);
                g2.DrawString(nodoList.Tam(i).ToString(), new Font(FontFamily.GenericSerif, 10), Brushes.Black, i * 110 + 55, 15);
                g2.DrawLine(pen, i * 110 + 90, 20, i * 110 + 110, 20);


            }
        }
    }
}
