using ChessTEC.ParticionamientoTecnologico.Capa_Logica.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol
{
    class Nodo
    {
        public Tablero tablero { get; set; }
        public string recorrido { get; set; }
        public int profundidad { get; set; }
        public string turno { get; set; }
        public List<Nodo> hijos { get; set; }

        Traductor traductor = new Traductor();

        public Nodo(Tablero tab, string recorrido, string turno,int prof)
        {
            this.tablero = tab;
            this.recorrido = recorrido;
            this.turno = turno;
            this.profundidad = prof;
        }

        public List<Nodo> expandir()
        {
            for (int x = 0; x < tablero.matrizTablero.Length; x++)
            {
                for (int y = 0; y < tablero.matrizTablero[x].Length; y++) // Recorrer todo el arbol
                {
                    if (tablero.matrizTablero[x][y] != null && 
                        tablero.matrizTablero[x][y].color == this.turno) // Pieza del bando jugando
                    {
                        tablero.buscarJugada(x, y, turno, true);
                        List<int[]> movilidad = tablero.matrizTablero[x][y].movilidad; // Movidas de la pieza

                        for (int i = 0; i < movilidad.Count; i++)
                        {
                            bool funcionono = true;
                            Tablero t = new Tablero(tablero.matrizTablero, tablero.turno);

                            try
                            {
                                t.moverPieza(movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1], x, y);
                            }
                            catch (Exception e)
                            {
                                funcionono = false;
                            }

                            if (t.turno.Equals("B"))
                                t.turno = "N";
                            else 
                                t.turno = "B";

                            if (funcionono)
                            {
                                Nodo hijo = new Nodo(
                                    t, 
                                    this.recorrido + " -> " + traductor.traducir(tablero.matrizTablero[x][y], movilidad.ElementAt(i)[1], movilidad.ElementAt(i)[0]), 
                                    t.turno,
                                    profundidad++);
                                hijos.Add(hijo);
                            }
                        }
                    }
                }
            }
            return hijos;
        }
    }
}
