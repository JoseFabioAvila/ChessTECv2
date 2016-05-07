using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas
{
    class Caballo : Pieza
    {
        public Caballo(string jugador)
        {
            movilidad = new List<int[]>();
            piezasComibles = new List<int[]>();
            if (jugador == "player1")
            {
                this.imagen += "Caballo_blanco.PNG";
                this.color = "B";
            }
            else {
                this.imagen += "Caballo_negro.PNG";
                this.color = "N";
            }
            this.simbologia = "C";
            this.valor = (double)3;
        }

        public override void calcularValor(int fila, int columna, Tablero tablero)
        {
            this.valor = (double)3 +
                ((double)movilidad.Count * 0.1) +
                (defensa(fila, columna, tablero) * 0.05);
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
            // fila + 2 / col +-1
            if (fila + 2 < 8)
            {
                if (columna + 1 < 8)
                {
                    checkAdd(fila + 2, columna + 1, tablero);
                }
                if (columna - 1 >= 0)
                {
                    checkAdd(fila + 2, columna - 1, tablero);
                }
            }
            // fila - 2 / col +-1
            if (fila - 2 >= 0)
            {
                if (columna + 1 < 8)
                {
                    checkAdd(fila - 2, columna + 1, tablero);
                }
                if (columna - 1 >= 0)
                {
                    checkAdd(fila - 2, columna - 1, tablero);
                }
            }
            // colm + 2 / fila +-1
            if (columna + 2 < 8)
            {
                if (fila + 1 < 8)
                {
                    checkAdd(fila + 1, columna + 2, tablero);
                }
                if (fila - 1 >= 0)
                {
                    checkAdd(fila - 1, columna + 2, tablero);
                }
            }
            // colm - 2 / fila +-1 
            if (columna - 2 >= 0)
            {
                if (fila + 1 < 8)
                {
                    checkAdd(fila + 1, columna - 2, tablero);
                }
                if (fila - 1 >= 0)
                {
                    checkAdd(fila - 1, columna - 2, tablero);
                }
            }
        }
        private void checkAdd(int fila, int colnm, Tablero tablero)
        {
            int[] mov = new int[] { fila, colnm };

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
