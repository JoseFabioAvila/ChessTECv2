using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas
{
    /// <summary>
    /// Clase de la pieza Rey
    /// </summary>
    class Rey : Pieza
    {
        /// <summary>
        /// Constructor de la clase Rey
        /// </summary>
        /// <param name="jugador">Si es blanco(B) o si es negro(N)</param>
        public Rey(string jugador)
        {
            movilidad = new List<int[]>();
            piezasComibles = new List<int[]>();
            if (jugador == "player1")
            {
                this.imagen += "Rey_blanco.PNG";
                this.color = "B";
            }
            else {
                this.imagen += "Rey_negro.PNG";
                this.color = "N";
            }

            this.simbologia = "R";
            this.valor = (double)1000;
        }

        /// <summary>
        /// Calcula el valor del Rey
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="columna">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        public override void calcularValor(int fila, int columna, Tablero tablero)
        {
            this.valor = (double)1000+
                lineasDefendidas(fila, columna,tablero)*0.05;
        }

        /// <summary>
        /// Verifica si el Rey esta siendo defendido
        /// </summary>
        /// <param name="fila">fila del Rey</param>
        /// <param name="columna">clumna del Rey</param>
        /// <param name="tablero">Tablero de juego</param>
        /// <returns>valor de la defensa que se le hace al Rey</returns>
        private double lineasDefendidas(int fila, int columna, Tablero tablero)
        {
            // fila + 1 // columna +,-,=
            // fila // columna + , - 
            return 0.0;
        }

        /// <summary>
        /// Actualizar la listas de movilidad y piezasComibles.
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="columna">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        public override void actualizarMov(int fila, int columna, Tablero tablero)
        {
            this.movilidad.Clear();
            this.piezasComibles.Clear();
            avanzar(fila, columna, tablero);
        }

        /// <summary>
        /// Busca toda las movidas y comidas posibles del Rey.
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="columna">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        private void avanzar(int fila, int columna, Tablero tablero)
        {
            // fila + 1 / clnm -,=,+
            if (fila + 1 < 8)
            {
                if (columna + 1 < 8)
                {
                    checkAdd(fila + 1, columna + 1, tablero);
                }
                if (columna - 0 >= 0)
                {
                    checkAdd(fila + 1, columna - 1, tablero);
                }
                checkAdd(fila + 1, columna, tablero);
            }
            // fila - 1 / clnm -,=,+
            if (fila - 1 >= 0)
            {
                if (columna + 1 < 8)
                {
                    checkAdd(fila - 1, columna + 1, tablero);
                }
                if (columna - 0 >= 0)
                {
                    checkAdd(fila - 1, columna - 1, tablero);
                }
                checkAdd(fila - 1, columna, tablero);
            }
            // fila     / clnm -, +
            if (columna + 1 < 8)
            {
                checkAdd(fila, columna + 1, tablero);
            }
            if (columna - 1 >= 0)
            {
                checkAdd(fila, columna - 1, tablero);
            }
        }

        /// <summary>
        /// Auxiliar de avanzar 
        /// </summary>
        /// <param name="fila">fila de la pieza</param>
        /// <param name="colnm">columna de la pieza</param>
        /// <param name="tablero">Tablero de juego</param>
        private void checkAdd(int fila, int colnm, Tablero tablero)
        {
            int[] mov = new int[] { fila, colnm };

            if (fila >= 0 && fila <= 7)
            {
                if (colnm >= 0 && colnm <= 7)
                {
                    if (tablero.matrizTablero[fila][colnm] == null)
                    {
                        movilidad.Add(mov);
                    }
                    else if (!tablero.matrizTablero[fila][colnm].color.Equals(this.color))
                    {
                        movilidad.Add(mov);
                        piezasComibles.Add(mov);
                    }
                }
            }
        }
    }
}
