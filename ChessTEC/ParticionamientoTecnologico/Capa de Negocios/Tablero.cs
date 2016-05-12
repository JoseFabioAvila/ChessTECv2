using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Piezas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios
{
    class Tablero
    {
        public Pieza[][] matrizTablero { get; set; }

        public double valorS { get; set; }
        public double valorB { get; set; }
        public double valorN { get; set; }
        public double valorT { get; set; }
        public string turno  { get; set; }

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

        

        public void buscarJugada(int f, int c, string turno, bool bandera)
        {

            //aqui se genrara las cordenadas con los movimientos de la ficha.
            this.turno = turno;
            if (matrizTablero[f][c].color.Equals(turno))
            {
                matrizTablero[f][c].actualizarMov(f, c, this);
            }
        }

        public void moverPieza(int fm, int cm, int f, int c)
        {
            if (matrizTablero[f][c].verificarMovida(fm, cm))
            {
                Pieza piezaSeleccionada = matrizTablero[f][c];
                matrizTablero[f][c] = null;
                matrizTablero[fm][cm] = piezaSeleccionada;
            }
        }

        public void comerPieza(int fm, int cm, int f, int c)
        {
            Pieza piezaSeleccionada = matrizTablero[f][c];
            matrizTablero[f][c] = null;
            matrizTablero[fm][cm] = piezaSeleccionada;
        }

        public string print() {
            string res = "\n";
            foreach (Pieza[] p in matrizTablero) {
                foreach (Pieza pi in p) {
                    if (pi == null)
                    {
                        res += "  0   |";
                    }
                    else {
                        if (pi.color.Equals("B"))
                        {
                            res += "  B  |";
                        }
                        else {
                            res += "  N  |";
                        }
                    }
                }
                res += "\n";
            }
            return res;
        }

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
                            res += "  B  |";
                        }
                        else {
                            res += "  N  |";
                        }
                    }
                }
                res += "\n";
            }
            return res;
        }

        public void calularTodo()
        {
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
