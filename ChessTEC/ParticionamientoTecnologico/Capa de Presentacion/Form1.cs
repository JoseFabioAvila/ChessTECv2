using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessTEC
{
    /// <summary>
    /// clase de la interfaz
    /// </summary>
    public partial class Form1 : Form
    {
        PictureBox[][] matrizBotones;
        Tablero tablero;
        string turnoDe = "B";

        int f = 0, c = 0, fm = 0, cm = 0;
        bool seleccionado = false;
        
        public Form1()
        {
            InitializeComponent();
            tablero = new Tablero();
            cargarBotones();
            updateTablero();
        }

        public void cargarBotones()
        {
            matrizBotones = new PictureBox[8][];
            for (int i = 0; i < matrizBotones.Length; i++)
            {
                matrizBotones[i] = new PictureBox[8];
            }

            matrizBotones[0][0] = p1A;
            matrizBotones[0][1] = p1B;
            matrizBotones[0][2] = p1C;
            matrizBotones[0][3] = p1D;
            matrizBotones[0][4] = p1E;
            matrizBotones[0][5] = p1F;
            matrizBotones[0][6] = p1G;
            matrizBotones[0][7] = p1H;

            matrizBotones[1][0] = p2A;
            matrizBotones[1][1] = p2B;
            matrizBotones[1][2] = p2C;
            matrizBotones[1][3] = p2D;
            matrizBotones[1][4] = p2E;
            matrizBotones[1][5] = p2F;
            matrizBotones[1][6] = p2G;
            matrizBotones[1][7] = p2H;

            matrizBotones[2][0] = p3A;
            matrizBotones[2][1] = p3B;
            matrizBotones[2][2] = p3C;
            matrizBotones[2][3] = p3D;
            matrizBotones[2][4] = p3E;
            matrizBotones[2][5] = p3F;
            matrizBotones[2][6] = p3G;
            matrizBotones[2][7] = p3H;

            matrizBotones[3][0] = p4A;
            matrizBotones[3][1] = p4B;
            matrizBotones[3][2] = p4C;
            matrizBotones[3][3] = p4D;
            matrizBotones[3][4] = p4E;
            matrizBotones[3][5] = p4F;
            matrizBotones[3][6] = p4G;
            matrizBotones[3][7] = p4H;
            
            matrizBotones[4][0] = p5A;
            matrizBotones[4][1] = p5B;
            matrizBotones[4][2] = p5C;
            matrizBotones[4][3] = p5D;
            matrizBotones[4][4] = p5E;
            matrizBotones[4][5] = p5F;
            matrizBotones[4][6] = p5G;
            matrizBotones[4][7] = p5H;

            matrizBotones[5][0] = p6A;
            matrizBotones[5][1] = p6B;
            matrizBotones[5][2] = p6C;
            matrizBotones[5][3] = p6D;
            matrizBotones[5][4] = p6E;
            matrizBotones[5][5] = p6F;
            matrizBotones[5][6] = p6G;
            matrizBotones[5][7] = p6H;

            matrizBotones[6][0] = p7A;
            matrizBotones[6][1] = p7B;
            matrizBotones[6][2] = p7C;
            matrizBotones[6][3] = p7D;
            matrizBotones[6][4] = p7E;
            matrizBotones[6][5] = p7F;
            matrizBotones[6][6] = p7G;
            matrizBotones[6][7] = p7H;

            matrizBotones[7][0] = p8A;
            matrizBotones[7][1] = p8B;
            matrizBotones[7][2] = p8C;
            matrizBotones[7][3] = p8D;
            matrizBotones[7][4] = p8E;
            matrizBotones[7][5] = p8F;
            matrizBotones[7][6] = p8G;
            matrizBotones[7][7] = p8H;            
        }

        private void updateTablero()
        {
            for (int i = 0; i < tablero.matrizTablero.Length; i++)
            {
                for (int j = 0; j < tablero.matrizTablero[i].Length; j++)
                {
                    if (tablero.matrizTablero[i][j] != null)
                    {
                        //Console.WriteLine(tablero.matrizTablero[i][j].imagen);
                        matrizBotones[i][j].ImageLocation = tablero.matrizTablero[i][j].imagen;
                        matrizBotones[i][j].BackColor = Color.AliceBlue;
                    }
                    else {
                        matrizBotones[i][j].ImageLocation = "";
                        matrizBotones[i][j].BackColor = Color.AliceBlue;
                    }
                }
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            //updateTablero();
            PictureBox btn = (PictureBox)sender;
            jugar(btn);
        }

        private void jugar(PictureBox btn)
        {
            if (seleccionado == true && btn.BackColor == Color.Red)
            {
                if (turnoDe == "B")
                {
                    realizarComida(btn, turnoDe);
                    informarCambioTurno("N");
                }
                else {
                    realizarComida(btn, turnoDe);
                    informarCambioTurno("B");
                }
            }
            else if (seleccionado == false && btn.ImageLocation != "")//btn.BackColor != Color.Yellow)
            {
                blanquear();
                if (turnoDe == "B")
                {
                    primerMovida(btn, turnoDe);
                }
                else {
                    primerMovida(btn, turnoDe);
                }
            }
            else if (seleccionado == true && btn.ImageLocation != "")//btn.BackColor != Color.Yellow)
            {
                blanquear();
                //hacer busqueda de jugada en otra ficha
                if (turnoDe == "B")
                {
                    cambioDePieza(btn, turnoDe);
                }
                else {
                    cambioDePieza(btn, turnoDe);
                }
            }
            else if (seleccionado == true && btn.BackColor == Color.Yellow)
            {
                blanquear();
                if (turnoDe == "B")
                {
                    moverPieza(btn, turnoDe);
                    informarCambioTurno("N");
                }
                else {
                    moverPieza(btn, turnoDe);
                    informarCambioTurno("B");
                }
            }
            else {
                /////////////////////////////////////////////////////////////////////
                //turno.Text = "no valida";
                /////////////////////////////////////////////////////////////////////
            };
        }

        private void informarCambioTurno(string jugador)
        {
            turnoDe = jugador;
            if (turnoDe == "B") { turno.Text = "Turno del blanco"; }
            else { turno.Text = "Turno del negro"; }
        }

        private void moverPieza(PictureBox btn, string turnoDe)
        {
            /////////////////////////////////////////////////////////////////////
            //turno.Text = "jugada";
            /////////////////////////////////////////////////////////////////////

            for (int i = 0; i < matrizBotones.Length; i++)
            {
                for (int j = 0; j < matrizBotones[i].Length; j++)
                {
                    if (matrizBotones[i][j].Name == btn.Name)
                    {
                        //turno.Text += "| " + i.ToString() + " | " + j.ToString() + " |";
                        matrizBotones[i][j].BackColor = Color.AliceBlue;

                        fm = i;
                        cm = j;
                        tablero.moverPieza(fm, cm, f, c);
                        seleccionado = false;
                        richTextBox1.Text = tablero.print();
                        updateTablero();
                    }
                }
            }
            tablero.calularTodo();
            textBox1.Text = tablero.valorT.ToString();
            seleccionado = false;
        }

        private void cambioDePieza(PictureBox btn, string turnoDe)
        {
            buscarBtn(btn.Name);
            if (tablero.matrizTablero[f][c].color.Equals(turnoDe))
            {
                List<int[]> movidas = tablero.matrizTablero[f][c].movilidad;
                foreach (int[] x in movidas)
                {
                    matrizBotones[x[0]][x[1]].BackColor = Color.AliceBlue;
                }
                List<int[]> comidas = tablero.matrizTablero[f][c].piezasComibles;
                foreach (int[] x in comidas)
                {
                    matrizBotones[x[0]][x[1]].BackColor = Color.AliceBlue;
                }
                if (btn.ImageLocation == "")
                {
                    //turno.Text = "nulo";
                    //btn.BackColor = Color.Red;
                }
                else
                {
                    //buscarBtn(btn.Name);
                    tablero.buscarJugada(f, c, turnoDe, seleccionado);
                    List<int[]> movidas2 = tablero.matrizTablero[f][c].movilidad;
                    foreach (int[] x in movidas2)
                    {
                        matrizBotones[x[0]][x[1]].BackColor = Color.Yellow;
                    }
                    List<int[]> comidas2 = tablero.matrizTablero[f][c].piezasComibles;
                    foreach (int[] x in comidas2)
                    {
                        matrizBotones[x[0]][x[1]].BackColor = Color.Red;
                    }
                    tablero.calularSeleccionada(f, c);
                    textBox1.Text = tablero.valorS.ToString();
                    seleccionado = true;
                }
            }
        }

        private void primerMovida(PictureBox btn, string turnoDe)
        {
            if (btn.ImageLocation == null)
            {
                //turno.Text = "nulo";
                //btn.BackColor = Color.Red;
            }
            else
            {
                buscarBtn(btn.Name);
                if (tablero.matrizTablero[f][c].color.Equals(turnoDe))
                {
                    tablero.buscarJugada(f, c, turnoDe, seleccionado);
                    List<int[]> movidas = tablero.matrizTablero[f][c].movilidad;
                    foreach (int[] x in movidas)
                    {
                        matrizBotones[x[0]][x[1]].BackColor = Color.Yellow;
                    }
                    List<int[]> comidas = tablero.matrizTablero[f][c].piezasComibles;
                    foreach (int[] x in comidas)
                    {
                        matrizBotones[x[0]][x[1]].BackColor = Color.Red;
                    }
                    tablero.calularSeleccionada(f, c);
                    textBox1.Text = tablero.valorS.ToString();
                    seleccionado = true;
                }
            }
        }

        public void realizarComida(PictureBox btn, string turnoDe)
        {
            for (int i = 0; i < matrizBotones.Length; i++)
            {
                for (int j = 0; j < matrizBotones[i].Length; j++)
                {
                    if (matrizBotones[i][j].Name == btn.Name)
                    {
                        //turno.Text = "| " + f.ToString() + " | " + c.ToString() + " |  comer";
                        matrizBotones[i][j].BackColor = Color.AliceBlue;

                        fm = i;
                        cm = j;
                        tablero.comerPieza(fm, cm, f, c);
                        seleccionado = false;
                        richTextBox1.Text = tablero.print();
                        updateTablero();
                    }
                }
            }
            tablero.calularTodo();
            textBox1.Text = tablero.valorT.ToString();
            seleccionado = false;
        }

        private void buscarBtn(string nombre)
        {
            for (int i = 0; i < matrizBotones.Length; i++)
            {
                for (int j = 0; j < matrizBotones[i].Length; j++)
                {
                    if (matrizBotones[i][j].Name == nombre)
                    {
                        f = i;
                        c = j;
                    }
                }
            }
        }

        private void turno_Click(object sender, EventArgs e)
        {
            turno.Text = "Turno";
        }

        private void blanquear() {
            for (int i = 0; i < matrizBotones.Length; i++)
            {
                for (int j = 0; j < matrizBotones[i].Length; j++)
                {
                    matrizBotones[i][j].BackColor = Color.AliceBlue;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Arbol x = new Arbol(tablero);
            x.generarArbol(x.root, 3);
            x.TraverseDFS();
        }
    }
}
