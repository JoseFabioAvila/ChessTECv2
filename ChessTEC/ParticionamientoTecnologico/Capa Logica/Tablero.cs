using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios
{
    /// <summary>
    /// Clase del tablero
    /// </summary>
    class Tablero : ICloneable
    {
        /// <summary>
        /// Matriz del tablero.
        /// </summary>
        public Pieza[][] matrizTablero { get; set; }

        /// <summary>
        /// Valores de la heuristica
        /// </summary>
        public double valorS { get; set; }
        public double valorB { get; set; }
        public double valorN { get; set; }
        public double valorT { get; set; }
        /// <summary>
        /// Turno
        /// </summary>
        public string turno  { get; set; }

        /// <summary>
        /// Constructor de la clase tablero
        /// </summary>
        public Tablero() {
            matrizTablero = new Pieza[8][];
            for (int i = 0; i < matrizTablero.Length; i++)
            {
                //Console.WriteLine(i.ToString());
                
                matrizTablero[i] = new Pieza[8];

            }
            colocarFichas();
            valorS = 0;
            valorB = 0;
            valorN = 0;
            valorT = 0;
            turno = "B";
            calularTodo();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Tablero(Pieza[][] mt, string t)
        {
            //matrizTablero = (Pieza[][])mt.Clone();
            matrizTablero = new Pieza[8][];
            for (int i = 0; i < matrizTablero.Length; i++)
            {
                matrizTablero[i] = new Pieza[8];
                matrizTablero[i][0] = mt[i][0];
                matrizTablero[i][1] = mt[i][1];
                matrizTablero[i][2] = mt[i][2];
                matrizTablero[i][3] = mt[i][3];
                matrizTablero[i][4] = mt[i][4];
                matrizTablero[i][5] = mt[i][5];
                matrizTablero[i][6] = mt[i][6];
                matrizTablero[i][7] = mt[i][7];
            }

            //this.matrizTablero = mt;
            this.turno = t.ToString();
            valorS = 0;
            valorB = 0;
            valorN = 0;
            valorT = 0;

            for (int x = 0; x < matrizTablero.Length; x++)
            {
                for (int y = 0; y < matrizTablero[x].Length; y++)
                {
                    if (matrizTablero[x][y] != null && matrizTablero[x][y].color == turno)
                    {
                        matrizTablero[x][y].actualizarMov(x, y, this);
                    }
                }
            }

            calularTodo();
        }

        /// <summary>
        /// Colocar las fichas del estado inicial del tablero.
        /// </summary>
        private void colocarFichas()
        {
            
            //fichas player 1
            matrizTablero[0][0] = new Torre("player2");
            matrizTablero[0][1] = new Caballo("player2");
            matrizTablero[0][2] = new Alfil("player2");
            matrizTablero[0][3] = new Rey("player2");
            matrizTablero[0][4] = new Dama("player2");
            matrizTablero[0][5] = new Alfil("player2");
            matrizTablero[0][6] = new Caballo("player2");
            matrizTablero[0][7] = new Torre("player2");

            matrizTablero[1][0] = new Peon("player2");
            matrizTablero[1][1] = new Peon("player2");
            matrizTablero[1][2] = new Peon("player2");
            matrizTablero[1][3] = new Peon("player2");
            matrizTablero[1][4] = new Peon("player2");
            matrizTablero[1][5] = new Peon("player2");
            matrizTablero[1][6] = new Peon("player2");
            matrizTablero[1][7] = new Peon("player2");

            //fichas player 2
            matrizTablero[6][0] = new Peon("player1");
            matrizTablero[6][1] = new Peon("player1");
            matrizTablero[6][2] = new Peon("player1");
            matrizTablero[6][3] = new Peon("player1");
            matrizTablero[6][4] = new Peon("player1");
            matrizTablero[6][5] = new Peon("player1");
            matrizTablero[6][6] = new Peon("player1");
            matrizTablero[6][7] = new Peon("player1");
            
            matrizTablero[7][0] = new Torre("player1");
            matrizTablero[7][1] = new Caballo("player1");
            matrizTablero[7][2] = new Alfil("player1");
            matrizTablero[7][3] = new Dama("player1");
            matrizTablero[7][4] = new Rey("player1");
            matrizTablero[7][5] = new Alfil("player1");
            matrizTablero[7][6] = new Caballo("player1");
            matrizTablero[7][7] = new Torre("player1");
            
        }

        /// <summary>
        /// Busca una jugada para la ficha en la posicion f y c del turno actual. 
        /// </summary>
        /// <param name="f">fila</param>
        /// <param name="c">columna</param>
        /// <param name="turno">turno actual</param>
        /// <param name="bandera">no se usa</param>
        public void buscarJugada(int f, int c, string turno, bool bandera)
        {
            //aqui se genrara las cordenadas con los movimientos de la ficha.
            this.turno = turno;
            if (matrizTablero[f][c].color.Equals(turno))
            {
                matrizTablero[f][c].actualizarMov(f, c, this);
            }
        }

        /// <summary>
        /// Mueve una pieza a la posicion vacia a la que se le indique. Siempre y cuando sea valida
        /// </summary>
        /// <param name="fm">fila de la posicion vacia</param>
        /// <param name="cm">columna de la posicion vacia</param>
        /// <param name="f">fila de la pieza a mover</param>
        /// <param name="c">columna de la pieza a mover</param>
        public void moverPieza(int fm, int cm, int f, int c)
        {
            /*if (f >= 0 && f < 8)
            {
                if (c >= 0 && c < 8)
                {
                    if (fm >= 0 && fm < 8)
                    {
                        if (cm >= 0 && cm < 8)
                        {*/
            if (matrizTablero[f][c].verificarMovida(fm, cm))
            {
                Pieza piezaSeleccionada = matrizTablero[f][c];
                matrizTablero[f][c] = null;
                matrizTablero[fm][cm] = piezaSeleccionada;
            }
                        /*}
                    }
                }
            }*/               
        }

        /// <summary>
        /// Come una pieza a la posicion vacia a la que se le indique. Siempre y cuando sea valida
        /// </summary>
        /// <param name="fm">fila de la posicion de la comida</param>
        /// <param name="cm">columna de la posicion de la comida</param>
        /// <param name="f">fila de la pieza que come</param>
        /// <param name="c">columna de la pieza que come</param>
        public void comerPieza(int fm, int cm, int f, int c)
        {
            Pieza piezaSeleccionada = matrizTablero[f][c];
            matrizTablero[f][c] = null;
            matrizTablero[fm][cm] = piezaSeleccionada;
        }

        /// <summary>
        /// Print del tablero actual
        /// </summary>
        /// <returns>String con el tablero</returns>
        public string print() {
            string res = "\n";
            foreach (Pieza[] p in matrizTablero) {
                foreach (Pieza pi in p) {
                    if (pi == null)
                    {
                        res += "  00   |";
                    }
                    else {
                        if (pi.color.Equals("B"))
                        {
                            res += "  " + pi.simbologia + "B  |";
                        }
                        else {
                            res += "  " + pi.simbologia + "N  |";
                        }
                    }
                }
                res += "\n";
            }
            return res;
        }

        /// <summary>
        /// Print del tablero para los arboles
        /// </summary>
        /// <param name="spaces">tabs de impresion</param>
        /// <returns>String con el tablero</returns>
        public string print2(string spaces)
        {
            string res = "\n";
            foreach (Pieza[] p in matrizTablero)
            {
                res += spaces;
                foreach (Pieza pi in p)
                {
                    if (pi == null)
                    {
                        res += "  0   |";
                    }
                    else {
                        if (pi.color.Equals("B"))
                        {
                            res += "  "+pi.simbologia+ "B  |";
                        }
                        else {
                            res += "  " + pi.simbologia + "N  |";
                        }
                    }
                }
                res += "\n";
            }
            return res;
        }

        /// <summary>
        /// Calcular el valor de la Heuristica del tablero
        /// </summary>
        public void calularTodo()
        {
            valorB = 0;
            valorN = 0;
            valorT = 0;
            for (int i = 0; i < matrizTablero.Length; i++)
            {
                for (int j = 0; j < matrizTablero[i].Length; j++)
                {

                    if (matrizTablero[i][j] != null) {
                        matrizTablero[i][j].calcularValor(i, j, this);
                        if (matrizTablero[i][j].color == "B")
                        {
                            valorB += matrizTablero[i][j].valor;
                        }
                        else {
                            valorN += matrizTablero[i][j].valor;
                        }
                    }
                }
            }
            valorT = valorB - valorN;
        }

        /// <summary>
        /// Actualizar el valor de la Heuristica del tablero
        /// </summary>
        public void actualizarTodo()
        {
            valorB = 0;
            valorN = 0;
            valorT = 0;
            for (int i = 0; i < matrizTablero.Length; i++)
            {
                for (int j = 0; j < matrizTablero[i].Length; j++)
                {

                    if (matrizTablero[i][j] != null)
                    {
                        if (matrizTablero[i][j].color == "B")
                        {
                            valorB += matrizTablero[i][j].valor;
                        }
                        else {
                            valorN += matrizTablero[i][j].valor;
                        }
                    }
                }
            }
            valorT = valorB - valorN;
        }

        /// <summary>
        /// Calcula el valor de la pieza seleccionada en el tablero
        /// </summary>
        /// <param name="f">fila de la pieza seleccionada</param>
        /// <param name="c">columna de la pieza seleccionada</param>
        public void calularSeleccionada(int f, int c)
        {
            for(int i = 0; i < matrizTablero.Length; i++)
            {
                for (int j = 0; j < matrizTablero[i].Length; j++)
                {
                    if (i == f && c == j)
                    {
                        matrizTablero[i][j].calcularValor(f, c, this);
                        valorS = matrizTablero[i][j].valor;
                    }
                }
            }
        }
    }
}