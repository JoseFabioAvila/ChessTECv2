using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas
{
    /// <summary>
    /// Clase de Pieza Torre
    /// </summary>
    class Torre : Pieza
    {
        /// <summary>
        /// Constructor de la pieza torre
        /// </summary>
        /// <param name="jugador">Si es blanco(B) o si es negro(N)</param>
        public Torre(string jugador)
        {
            movilidad = new List<int[]>();
            piezasComibles = new List<int[]>();
            if (jugador == "player1")
            {
                this.imagen += "Torre_blanco.PNG";
                this.color = "B";
            }
            else {
                this.imagen += "Torre_negro.PNG";
                this.color = "N";
            }
            this.simbologia = "T";
            this.valor = (double)5;
        }

        /// <summary>
        /// Calcula el valor de la torre.
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="columna">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        public override void calcularValor(int fila, int columna, Tablero tablero)
        {
            this.valor = (double)5 +
                ((double)movilidad.Count * 0.1) +
                (defensa(fila, columna, tablero) * 0.05);
        }

        /// <summary>
        /// Aumenta el valor de la torre en virtud de la cantidad de piezas que la estan apoyando.
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="columna">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        /// <returns>valor de la defensa</returns>
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

        /// <summary>
        /// Actualizar la listas de movilidad y piezasComibles.
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="columna">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        public override void actualizarMov(int fila, int columna, Tablero tablero)
        {
            movilidad.Clear();
            piezasComibles.Clear();
            avanzar(fila, columna, tablero);
        }

        /// <summary>
        /// Busca toda las movidas y comidas posibles de la torre.
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="columna">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        private void avanzar(int fila, int columna, Tablero tablero)
        {
            bool arriba = false, abajo = false, derecha = false, izquierda = false, primera = true;

            int i = fila;
            int j = columna;
            for (int cont = 0; cont < 8; cont++) {
                if (primera == false)
                {
                    if (i - cont >= 0 && arriba == false) //ariba
                    {
                        if (tablero.matrizTablero[i - cont][j] == null)
                        {
                            int[] mov = new int[] { i - cont, j };
                            movilidad.Add(mov);
                        }
                        else
                        {
                            if (tablero.matrizTablero[fila][columna].color != tablero.matrizTablero[i - cont][j].color) {
                                int[] com = new int[] { i - cont, j };
                                piezasComibles.Add(com);
                            }
                            arriba = true;
                        }
                    }
                    if (i + cont < 8 && abajo == false) //abajo
                    {
                        if (tablero.matrizTablero[i + cont][j] == null)
                        {
                            int[] mov = new int[] { i + cont, j };
                            movilidad.Add(mov);
                        }
                        else
                        {
                            if (tablero.matrizTablero[fila][columna].color != tablero.matrizTablero[i + cont][j].color)
                            {
                                int[] com = new int[] { i + cont, j };
                                piezasComibles.Add(com);
                            }
                            abajo = true;
                        }
                    }
                    if (j - cont >= 0 && izquierda == false) //izq
                    {
                        if (tablero.matrizTablero[i][j - cont] == null)
                        {
                            int[] mov = new int[] { i, j - cont };
                            movilidad.Add(mov);
                        }
                        else
                        {
                            if (tablero.matrizTablero[fila][columna].color != tablero.matrizTablero[i][j - cont].color)
                            {
                                int[] com = new int[] { i, j - cont };
                                piezasComibles.Add(com);
                            }
                            izquierda = true;
                        }
                    }
                    if (j + cont < 8 && derecha == false) //der
                    {
                        if (tablero.matrizTablero[i][j + cont] == null)
                        {
                            int[] mov = new int[] { i, j + cont };
                            movilidad.Add(mov);
                        }
                        else
                        {
                            if (tablero.matrizTablero[fila][columna].color != tablero.matrizTablero[i][j + cont].color)
                            {
                                int[] com = new int[] { i, j + cont };
                                piezasComibles.Add(com);
                            }
                            derecha = true;
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
