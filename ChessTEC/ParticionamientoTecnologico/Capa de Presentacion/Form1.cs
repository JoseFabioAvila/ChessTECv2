﻿using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol;
using ChessTEC.ParticionamientoTecnologico.Capa_de_Presentacion;
using ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol;
using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas;
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
        
        /// <summary>
        /// Inicializador de la clase
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            tablero = new Tablero();
            cargarBotones();
            updateTablero();
            blanquear();
        }

        /// <summary>
        /// Cargar los botones en la matriz interna
        /// </summary>
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

        /// <summary>
        /// Hace update a la matriz de juego con las nuevas posiciones de las piezas
        /// </summary>
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
                        //matrizBotones[i][j].BackColor = Color.AliceBlue;
                    }
                    else {
                        matrizBotones[i][j].ImageLocation = "";
                        //matrizBotones[i][j].BackColor = Color.AliceBlue;
                    }
                }
            }
        }

        /// <summary>
        /// Accion click de cada ImageBox
        /// </summary>
        /// <param name="sender">ImageBox</param>
        /// <param name="e"></param>
        protected void btn_Click(object sender, EventArgs e)
        {
            //updateTablero();
            PictureBox btn = (PictureBox)sender;
            jugar(btn);            
        }

        /// <summary>
        /// Se llama al realizar click sobre el boton, por lo que se detecta si es un moviento una comida, 
        /// una selesccion de ficha o un cambio de seleccion de ficha.
        /// </summary>
        /// <param name="btn">boton seleccionado (posicion del tablero)</param>
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
                blanquear();
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

        /// <summary>
        /// Realiza el cambio de turno despues de cada movida o comida.
        /// </summary>
        /// <param name="jugador"> es el turno en cuestion "B" si son blancas y "N" si son negras</param>
        private void informarCambioTurno(string jugador)
        {
            turnoDe = jugador;
            if (turnoDe == "B") { turno.Text = "Turno del blanco"; }
            else { turno.Text = "Turno del negro"; }
        }

        /// <summary>
        /// Movimiento de una pieza sin realizar una comida
        /// </summary>
        /// <param name="btn">Boton a donde desea mover la pieza</param>
        /// <param name="turnoDe">Es el turno en cuaestion, B(Blancas) o N(Negras)</param>
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
                        //matrizBotones[i][j].BackColor = Color.AliceBlue;

                        fm = i;
                        cm = j;

                        tablero.moverPieza(fm, cm, f, c);
                        seleccionado = false;
                        richTextBox1.Text = tablero.print();
                        
                        if (turnoDe == "B" && fm == 0 && tablero.matrizTablero[fm][cm].simbologia == "P")
                        {
                            int opcion = CustomMessageBox.Show();

                            coronar(opcion, fm, cm, "player1");
                        }
                        else if (turnoDe == "N" && fm == 7 && tablero.matrizTablero[fm][cm].simbologia == "P")
                        {
                            int opcion = CustomMessageBox.Show();

                            coronar(opcion, fm, cm, "player2");                            
                        }

                        updateTablero();
                    }
                }
            }
            tablero.calularTodo();
            vHeuActual.Text = tablero.valorT.ToString();
            seleccionado = false;
        }

        /// <summary>
        /// Método que se encarga de coronar las piezas para ambos jugadores
        /// </summary>
        /// <param name="opcion">corona a utilizar</param>
        /// <param name="fm">fila</param>
        /// <param name="cm">columna</param>
        /// <param name="jugardor">jugador actual</param>
        public void coronar(int opcion, int fm, int cm, string jugardor)
        {
            if (opcion == 0)
            {
                tablero.matrizTablero[fm][cm] = new Alfil(jugardor);
            }
            else if (opcion == 1)
            {
                tablero.matrizTablero[fm][cm] = new Dama(jugardor);
            }
            else if (opcion == 2)
            {
                tablero.matrizTablero[fm][cm] = new Torre(jugardor);
            }
            else if (opcion == 3)
            {
                tablero.matrizTablero[fm][cm] = new Caballo(jugardor);
            }
        }

        /// <summary>
        /// Cuando se selecciona otra pieza distinta a la ya seleccionada
        /// </summary>
        /// <param name="btn">Boton de la nueva pieza seleccionada</param>
        /// <param name="turnoDe">Es el turno en cuaestion, B(Blancas) o N(Negras)</param>
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
                    vPieza.Text = tablero.valorS.ToString();
                    seleccionado = true;
                }
            }
        }

        /// <summary>
        /// Cuando se selecciona una pieza por pirmera vez en el tuno
        /// </summary>
        /// <param name="btn">Boton de la pieza seleccionada</param>
        /// <param name="turnoDe">Es el turno en cuaestion, B(Blancas) o N(Negras)</param>
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
                    vPieza.Text = tablero.valorS.ToString();
                    seleccionado = true;
                }
            }
        }

        /// <summary>
        /// Movimiento de una pieza realizando una comida
        /// </summary>
        /// <param name="btn">Boton de la pieza a comer</param>
        /// <param name="turnoDe">Es el turno en cuaestion, B(Blancas) o N(Negras)</param>
        public void realizarComida(PictureBox btn, string turnoDe)
        {
            for (int i = 0; i < matrizBotones.Length; i++)
            {
                for (int j = 0; j < matrizBotones[i].Length; j++)
                {
                    if (matrizBotones[i][j].Name == btn.Name)
                    {
                        //turno.Text = "| " + f.ToString() + " | " + c.ToString() + " |  comer";
                        //matrizBotones[i][j].BackColor = Color.AliceBlue;

                        fm = i;
                        cm = j;
                        tablero.comerPieza(fm, cm, f, c);
                        seleccionado = false;
                        richTextBox1.Text = tablero.print();

                        if (turnoDe == "B" && fm == 0 && tablero.matrizTablero[fm][cm].simbologia == "P")
                        {
                            int opcion = CustomMessageBox.Show();

                            coronar(opcion, fm, cm, "player1");
                        }
                        else if (turnoDe == "N" && fm == 7 && tablero.matrizTablero[fm][cm].simbologia == "P")
                        {
                            int opcion = CustomMessageBox.Show();

                            coronar(opcion, fm, cm, "player2");
                        }

                        updateTablero();
                    }
                }
            }
            tablero.calularTodo();
            vHeuActual.Text = tablero.valorT.ToString();
            seleccionado = false;
        }

        /// <summary>
        /// Buscar un boton en la matriz de botones
        /// </summary>
        /// <param name="nombre">Es el nombre del boton</param>
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
        
        /// <summary>
        /// Accion click de turno (no se usa)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void turno_Click(object sender, EventArgs e)
        {
            //turno.Text = "Turno";
        }

        /// <summary>
        /// Quita los colores amarillos y rojos de las posiciones en donde se puede mover o comer
        /// </summary>
        private void blanquear() {
            for (int i = 0; i < matrizBotones.Length; i++)
            {
                for (int j = 0; j < matrizBotones[i].Length; j++)
                {
                    matrizBotones[i][j].BackColor = Color.AliceBlue;
                }
            }
            matrizBotones[0][1].BackColor = Color.DarkGray;
            matrizBotones[0][3].BackColor = Color.DarkGray;
            matrizBotones[0][5].BackColor = Color.DarkGray;
            matrizBotones[0][7].BackColor = Color.DarkGray;

            matrizBotones[1][0].BackColor = Color.DarkGray;
            matrizBotones[1][2].BackColor = Color.DarkGray;
            matrizBotones[1][4].BackColor = Color.DarkGray;
            matrizBotones[1][6].BackColor = Color.DarkGray;

            matrizBotones[2][1].BackColor = Color.DarkGray;
            matrizBotones[2][3].BackColor = Color.DarkGray;
            matrizBotones[2][5].BackColor = Color.DarkGray;
            matrizBotones[2][7].BackColor = Color.DarkGray;

            matrizBotones[3][0].BackColor = Color.DarkGray;
            matrizBotones[3][2].BackColor = Color.DarkGray;
            matrizBotones[3][4].BackColor = Color.DarkGray;
            matrizBotones[3][6].BackColor = Color.DarkGray;

            matrizBotones[4][1].BackColor = Color.DarkGray;
            matrizBotones[4][3].BackColor = Color.DarkGray;
            matrizBotones[4][5].BackColor = Color.DarkGray;
            matrizBotones[4][7].BackColor = Color.DarkGray;

            matrizBotones[5][0].BackColor = Color.DarkGray;
            matrizBotones[5][2].BackColor = Color.DarkGray;
            matrizBotones[5][4].BackColor = Color.DarkGray;
            matrizBotones[5][6].BackColor = Color.DarkGray;

            matrizBotones[6][1].BackColor = Color.DarkGray;
            matrizBotones[6][3].BackColor = Color.DarkGray;
            matrizBotones[6][5].BackColor = Color.DarkGray;
            matrizBotones[6][7].BackColor = Color.DarkGray;

            matrizBotones[7][0].BackColor = Color.DarkGray;
            matrizBotones[7][2].BackColor = Color.DarkGray;
            matrizBotones[7][4].BackColor = Color.DarkGray;
            matrizBotones[7][6].BackColor = Color.DarkGray;
        }

        /// <summary>
        /// Click para ver el arbol en consola (Creo que ya no se usa)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ArbolPM x = new ArbolPM(tablero);
            x.expandir(x.raiz, int.Parse(maskedTextBox1.Text), 0);

            vRecorrido.Text = x.camino;
            vHeuFinal.Text = (x.valorT).ToString();
            this.lblTiempoA.Text = x.estadisticas.tiempo.ToString() + " ms" + ", " + x.estadisticas.cantJugadasAnalizadas + " jugadas";
        }

        /// <summary>
        /// Click para ver el arbol a* en consola
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAstrella_Click(object sender, EventArgs e)
        {
            ArbolAE x = new ArbolAE(tablero);
            NodoAE respuesta = x.analizar(int.Parse(maskedTextBox1.Text));
            vHeuFinal.Text = (respuesta.tablero.valorT).ToString();
            vRecorrido.Text = respuesta.recorrido;

            this.lblTiempoA.Text = x.estadisticas.tiempo.ToString() + " ms" + ", " + x.estadisticas.cantJugadasAnalizadas + " jugadas";
        }

        /// <summary>
        /// Your custom message box helper.
        /// </summary>
        public static class CustomMessageBox
        {


            public static int Show()
            {
                // using construct ensures the resources are freed when form is closed
                using (var form = new CustomMessageForm())
                {
                    form.ShowDialog();
                    return form.corona;
                }
            }
        }
    }
}
