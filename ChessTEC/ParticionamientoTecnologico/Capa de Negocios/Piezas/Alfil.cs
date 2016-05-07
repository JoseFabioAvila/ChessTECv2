using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas
{
    class Alfil : Pieza
    {
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

        public override void calcularValor(int fila, int columna, Tablero tablero)
        {
            
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
