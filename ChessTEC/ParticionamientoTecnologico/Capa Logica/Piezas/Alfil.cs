using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas
{
    /// <summary>
    /// Clase de pieza Alfil
    /// </summary>
    class Alfil : Pieza
    {
        /// <summary>
        /// Constructor de la pieza alfil
        /// </summary>
        /// <param name="jugador"></param>
        public Alfil(string jugador)
        {
            movilidad = new List<int[]>();
            piezasComibles = new List<int[]>();
            if (jugador == "player1")
            {
                this.imagen += "Alfil_blanco.PNG";
                this.color = "B";
            }
            else {
                this.imagen += "Alfil_negro.PNG";
                this.color = "N";
            }
            this.simbologia = "A";
            this.valor = (double)3;
        }

        /// <summary>
        /// Calcula el valor de la torre
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="columna">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        public override void calcularValor(int fila, int columna, Tablero tablero)
        {
            this.valor = (double)3 +
                ((double)movilidad.Count * 0.1) +
                (defensa(fila, columna, tablero) * 0.05)+
                parejaAlfiles(tablero);
        }

        private double parejaAlfiles(Tablero tablero)
        {
            double counter = 0.0;
            for (int f = 0; f < 8; f++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (tablero.matrizTablero[f][c] != null)
                    {
                        if (tablero.matrizTablero[f][c].color.Equals(this.color)
                            && tablero.matrizTablero[f][c].simbologia.Equals("A"))
                        {
                            counter = counter + (double)0.25;
                        }
                    }
                }
            }
            return counter;
        }

        private double defensa(int fila, int columna, Tablero tablero)
        {
            double counter = 0.0;
            for (int f = 0; f < 8; f++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (tablero.matrizTablero[f][c] != null)
                    {
                        if (tablero.matrizTablero[f][c].color.Equals(this.color)
                            && verificarMovida(f, c))
                        {
                            counter = counter + (double)1.0;
                        }
                    }
                }
            }
            return counter;
        }

        public override void actualizarMov(int fila, int columna, Tablero tablero)
        {
            movilidad.Clear();
            piezasComibles.Clear();
            avanzar(fila, columna, tablero);
        }

        private void avanzar(int fila, int columna, Tablero tablero)
        {
            bool noroeste = false, noreste = false, suroeste = false, sureste = false, primera = true;

            int i = fila;
            int j = columna;
            for (int cont = 0; cont < 8; cont++)
            {
                if (primera == false)
                {
                    if (i - cont >= 0 && j - cont >= 0 && noroeste == false) //noroeste
                    {
                        if (tablero.matrizTablero[i - cont][j - cont] == null)
                        {
                            int[] mov = new int[] { i - cont, j - cont };
                            movilidad.Add(mov);
                        }
                        else
                        {
                            if (tablero.matrizTablero[fila][columna].color != tablero.matrizTablero[i - cont][j - cont].color)
                            {
                                int[] com = new int[] { i - cont, j - cont };
                                piezasComibles.Add(com);
                            }
                            noroeste = true;
                        }
                    }
                    if (i - cont >= 0 && j + cont < 8 && noreste == false) //noreste
                    {
                        if (tablero.matrizTablero[i - cont][j + cont] == null)
                        {
                            int[] mov = new int[] { i - cont, j + cont };
                            movilidad.Add(mov);
                        }
                        else
                        {
                            if (tablero.matrizTablero[fila][columna].color != tablero.matrizTablero[i - cont][j + cont].color)
                            {
                                int[] com = new int[] { i - cont, j + cont };
                                piezasComibles.Add(com);
                            }
                            noreste = true;
                        }

                    }
                    if (j - cont >= 0 && i + cont < 8 && suroeste == false) //suroeste
                    {
                        if (tablero.matrizTablero[i + cont][j - cont] == null)
                        {
                            int[] mov = new int[] { i + cont, j - cont };
                            movilidad.Add(mov);
                        }
                        else
                        {
                            if (tablero.matrizTablero[fila][columna].color != tablero.matrizTablero[i + cont][j - cont].color)
                            {
                                int[] com = new int[] { i + cont, j - cont};
                                piezasComibles.Add(com);
                            }
                            suroeste = true;
                        }
                    }
                    if (j + cont < 8 && i + cont < 8 && sureste == false) //sureste
                    {
                        if (tablero.matrizTablero[i + cont][j + cont] == null)
                        {
                            int[] mov = new int[] { i + cont, j + cont };
                            movilidad.Add(mov);
                        }
                        else
                        {
                            if (tablero.matrizTablero[fila][columna].color != tablero.matrizTablero[i + cont][j + cont].color)
                            {
                                int[] com = new int[] { i + cont, j + cont};
                                piezasComibles.Add(com);
                            }
                            sureste = true;
                        }
                    }
                }
                else {
                    primera = false;
                }
            }
        }
    }
}
