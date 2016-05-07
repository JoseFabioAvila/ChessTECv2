using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas
{
    class Torre : Pieza
    {
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

        public override void calcularValor(int fila, int columna, Tablero tablero)
        {
            this.valor = (double)5 +
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

        //private void avanzar(int fila, int tempF, int tempF2, int columna, int posIni, Tablero tablero)
        //{
        //    //if (tablero.matrizTablero[tempF][columna] == null) // Avanzar
        //    //{
        //    //    int[] mov = new int[] { tempF, columna };
        //    //    movilidad.Add(mov);

        //    //    if (fila == posIni)
        //    //    {
        //    //        if (tablero.matrizTablero[tempF2][columna] == null)
        //    //        {
        //    //            mov = new int[] { tempF2, columna };
        //    //            movilidad.Add(mov);
        //    //        }
        //    //    }
        //    //}
        //}


        private void comerDer(int tempF, int columna, Tablero tablero)
        {
            //if (columna + 1 < 8)
            //{
            //    if (tablero.matrizTablero[tempF][columna + 1] != null) // Comer der
            //    {
            //        if (!tablero.matrizTablero[tempF][columna + 1].color.Equals(this.color)) // Comer
            //        {
            //            int[] mov = new int[] { tempF, columna + 1 };
            //            movilidad.Add(mov);
            //            piezasComibles.Add(mov);
            //        }
            //    }
            //}
        }


        private void comerIzq(int tempF, int columna, Tablero tablero)
        {
            //if (columna - 1 >= 0)
            //{
            //    if (tablero.matrizTablero[tempF][columna - 1] != null) // Comer Izq
            //    {
            //        if (!tablero.matrizTablero[tempF][columna - 1].color.Equals(this.color)) // Comer
            //        {
            //            int[] mov = new int[] { tempF, columna - 1 };
            //            movilidad.Add(mov);
            //            piezasComibles.Add(mov);
            //        }
            //    }
            //}
        }



    }
}
