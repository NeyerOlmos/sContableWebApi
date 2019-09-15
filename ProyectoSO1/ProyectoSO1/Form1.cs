using AdmMemLogic;
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
namespace ProyectoSO1
{
    public partial class Form1 : Form
    {
        Mem mem;
        int processCounter;
        NodoList NodoList = new NodoList();
        List<Process> processes = new List<Process>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            processCounter++;
            int amount = Convert.ToInt32(Interaction.InputBox("amount", "getMem"));
            
            Process process = new Process { Id = processCounter, Tam = amount };
            try
            {

                mem.LoadProcess(ref process);
                processes.Add(process);
            }
            catch{
                if (process.Dir==-1) {
                    processCounter--;
                    MessageBox.Show("No hay memoria suficiente");
                }
            }
            rePaint();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.CheckState = CheckState.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.CheckState = CheckState.Unchecked;
            }
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.CheckState = CheckState.Unchecked;
            }
          

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int size = Convert.ToInt32(textBox1.Text);
            if (size > 700) {
                MessageBox.Show("la memoria mas grande que puedes crear es de 700bytes");
            } else if (size <10) {
                MessageBox.Show("la memoria mas pequeña que puedes crear es de 10bytes");
            }
            else if (size <= 700 && size >= 10)
            {
                mem = new Mem(size);

                rePaint();

                
            }
            

            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Process> processes = new List<Process>();
            Process process1 = new Process();
            Process process2 = new Process();
            Process process3 = new Process();
            Process process4 = new Process();
            process1.Id = 1;
            process1.Dir = 100;
            process1.Tam = 100;

            process2.Id = 2;
            process2.Dir = 250;
            process2.Tam = 150;

            process3.Id = 3;
            process3.Dir = 400;
            process3.Tam = 100;

            process4.Id = 4;
            process4.Dir = 600;
            process4.Tam = 400;
            processes.Add(process1);
            processes.Add(process2);
            processes.Add(process3);
            processes.Add(process4);
            



            //-------------------------------------------------------------------
            NodoList nodoList = new NodoList();
            Nodo nodo1 = new Nodo();
            nodo1.Dir = 0;
            nodo1.Tam = 200;
            Nodo nodo2 = new Nodo();
            nodo2.Dir =250;
            nodo2.Tam = 250;

            Nodo nodo3 = new Nodo();
            nodo3.Dir = 600;
            nodo3.Tam = 400;
            
            nodoList.Add(nodo1);
            nodoList.Add(nodo2);
            nodoList.Add(nodo3);
            mem = new Mem(1000);
            // Drawer.DrawList(ref panel2, nodoList);

            mem.L = nodoList;
            // Process process5 = new Process { Id = 5, Tam = 20 };
            //mem.LoadProcess(ref process5);

            //processes.Insert(processes.FindIndex(p => p.Dir < process5.Dir), process5);

            //int s = mem.getFreeSize(200);         
            //int s1 = mem.getFreeSize(500);         
            //   mem.L.AddOrdenedByDir(nodo3);

            //mem.L = mem.generateNodoList(processes);
            //processes.Remove(process1);
            //mem.L = mem.generateNodoList(processes);
            mem.FreeMem(process4);
            Drawer.DrawMem(ref panel1, processes,mem.S,mem.M);
            Drawer.DrawList(ref panel2, mem.L);
            
           




        }

        private void button2_Click(object sender, EventArgs e)
        {
            int processId = Convert.ToInt32(Interaction.InputBox("P:", "ProcessName"));
            Process process = processes.Find(p => p.Id == processId);
            mem.FreeMem(process);
            processes.Remove(process);
            rePaint();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            rePaint();
        }
        private void rePaint() {
            label8.Text = mem.M.ToString();
            label9.Text = mem.S.ToString();

            label4.Text = mem.MemAvail.ToString();
            label5.Text = mem.MaxAvail.ToString();

            panel1.Refresh();
            panel2.Refresh();

            Drawer.DrawList(ref panel2, mem.L);
           // Drawer.DrawMem(ref panel1, processes, mem.S, mem.M);
            Drawer.DrawMem(ref panel1, processes,mem);
        }
    }
}
