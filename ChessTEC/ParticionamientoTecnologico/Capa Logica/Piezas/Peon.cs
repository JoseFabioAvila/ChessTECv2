using ChessTEC.ParticionamientoTecnologico.Capa_de_Presentacion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas
{
    class Peon : Pieza
    {

        int tempF = 0, tempF2 = 0, posIni = 0;

        public Peon(string jugador)
        {
            movilidad = new List<int[]>();
            piezasComibles = new List<int[]>();
            if (jugador == "player1")
            {
                this.imagen += "Peon_blanco.PNG";
                this.color = "B";
                posIni = 6;
            }
            else {
                this.imagen += "Peon_negro.PNG";
                this.color = "N";
                posIni = 1;
            }
            this.simbologia = "P";
            this.valor = (double)1;
        }

        public override void calcularValor(int fila, int columna, Tablero tablero)
        {
            valor = 1;
            peonCentrado(columna);
            peonAvanzado(fila);
            peonPasadoODoblado(fila, columna, tablero);
        }

        private void peonPasadoODoblado(int fila, int columna, Tablero tablero)
        {
            bool obstaculoLateral = false;
            bool obstaculoFrontal = false;

            if (this.color.Equals("B"))
            {
                for (int i = fila-1; i > 0; i--)
                {
                    if (tablero.matrizTablero[i][columna] != null) // Frente
                    {
                        if (tablero.matrizTablero[i][columna].color.Equals(this.color)) // peon doblado
                        {
                            valor = valor - (double)0.25;
                            obstaculoFrontal = true;
                        }
                        else
                        {
                            obstaculoFrontal = true;
                        }
                    }
                    if(columna+1<8){
                        if (tablero.matrizTablero[i][columna + 1] != null) //Derecha
                        {
                            if (!tablero.matrizTablero[i][columna + 1].color.Equals(this.color))
                            {
                                obstaculoLateral = true;
                            }
                        }
                    }
                    if (columna - 1 >= 0)
                    {
                        if (tablero.matrizTablero[i][columna - 1] != null)// Izquierda xSSS
                        {
                            if (!tablero.matrizTablero[i][columna - 1].color.Equals(this.color))
                            {
                                obstaculoLateral = true;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = fila+1; i < 7; i++)
                {
                    if (tablero.matrizTablero[i][columna] != null)
                    {
                        if (tablero.matrizTablero[i][columna].color.Equals(this.color)) // peon doblado
                        {
                            valor = valor - (double)0.25;
                            obstaculoFrontal = true;
                        }
                        else
                        {
                            obstaculoFrontal = true;
                        }
                    }
                    if (columna + 1 < 8)
                    {
                        if (tablero.matrizTablero[i][columna + 1] != null) //Derecha
                        {
                            if (!tablero.matrizTablero[i][columna + 1].color.Equals(this.color))
                            {
                                obstaculoLateral = true;
                            }
                        }
                    }
                    if (columna - 1 > 0)
                    {
                        if (tablero.matrizTablero[i][columna - 1] != null)// Izquierda
                        {
                            if (!tablero.matrizTablero[i][columna - 1].color.Equals(this.color))
                            {
                                obstaculoLateral = true;
                            }
                        }
                    }
                }
            }

            if (!obstaculoFrontal)
            {
                valor = valor + (double)0.25;
                if(!obstaculoLateral)
                    valor = valor + (double)0.25;
            }
        }

        private void peonAvanzado(int fila)
        {
            if (color.Equals("N"))
            {
                if (fila >= 5)
                {
                    valor = (valor - 1) + ((fila - 4) * 1.5);
                }
            }
            else
            {
                if (fila <=2)
                {
                    valor = (valor - 1) + (((fila - 3)*-1) * 1.5);
                }
            }
        }

        private void peonCentrado(int columna)
        {
            switch (columna)
            {
                case 0:
                    this.valor = valor - (double)0.25;
                    break;
                case 2:
                    this.valor = valor + (double)0.25;
                    break;
                case 3:
                    this.valor = valor + (double)0.5;
                    break;
                case 4:
                    this.valor = valor + (double)0.5;
                    break;
                case 5:
                    this.valor = valor + (double)0.25;
                    break;
                case 7:
                    this.valor = valor - (double)0.25;
                    break;
            }
        }

        public override void actualizarMov(int fila, int columna, Tablero tablero)
        {
            movilidad.Clear();
            piezasComibles.Clear();

            switch (this.color)
            {
                case "B":
                    tempF = fila - 1;
                    tempF2 = fila - 2;
                    posIni = 7;
                    break;
                case "N":
                    tempF = fila + 1;
                    tempF2 = fila + 2;
                    posIni = 1;
                    break;
            }
            
            avanzar(fila, tempF, tempF2, columna, posIni, tablero);

            comerIzq(tempF, columna, tablero);

            comerDer(tempF, columna, tablero);
        }
        
        private void avanzar(int fila, int tempF, int tempF2, int columna, int posIni, Tablero tablero) {
            if (tablero.matrizTablero[tempF][columna] == null) // Avanzar
            {
                int[] mov = new int[] { tempF, columna };
                movilidad.Add(mov);

                if (fila == posIni)
                {
                    if (tablero.matrizTablero[tempF2][columna] == null)
                    {
                        mov = new int[] { tempF2, columna };
                        movilidad.Add(mov);
                    }
                }
            }
        }
        
        private void comerDer(int tempF, int columna, Tablero tablero)
        {
            if (columna + 1 < 8)
            {
                if (tablero.matrizTablero[tempF][columna + 1] != null) // Comer der
                {
                    if (!tablero.matrizTablero[tempF][columna + 1].color.Equals(this.color)) // Comer
                    {
                        int[] mov = new int[] { tempF, columna + 1 };
                        movilidad.Add(mov);
                        piezasComibles.Add(mov);
                    }
                }
            }
        }
        
        private void comerIzq(int tempF, int columna, Tablero tablero)
        {
            if (columna - 1 >= 0)
            {
                if (tablero.matrizTablero[tempF][columna - 1] != null) // Comer Izq
                {
                    if (!tablero.matrizTablero[tempF][columna - 1].color.Equals(this.color)) // Comer
                    {
                        int[] mov = new int[] { tempF, columna - 1 };
                        movilidad.Add(mov);
                        piezasComibles.Add(mov);
                    }
                }
            }
        }
    }
}
