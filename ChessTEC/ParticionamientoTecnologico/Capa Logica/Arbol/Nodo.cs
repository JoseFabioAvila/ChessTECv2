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
            hijos = new List<Nodo>();
        }

        public List<Nodo> expandir()
        {
            profundidad ++;
            for (int x = 0; x < tablero.matrizTablero.Length; x++)
            {
                for (int y = 0; y < tablero.matrizTablero[x].Length; y++) // Recorrer todo el arbol
                {
                    if (tablero.matrizTablero[x][y] != null && 
                        tablero.matrizTablero[x][y].color == this.turno) // Pieza del bando jugando
                    {
                        //tablero.buscarJugada(x, y, turno, true);
                        
                        List<Nodo> nodosMovilidad = new List<Nodo>();
                        List<int[]> movilidad = new List<int[]>(tablero.matrizTablero[x][y].movilidad);
                        int c = (tablero.matrizTablero[x][y].movilidad.Count);
                        for (int i = 0; i < c; i++)
                        {
                            bool funcionono = true;
                            Tablero t = new Tablero(tablero.matrizTablero, tablero.turno);
                            //Tablero t = (Tablero)tablero.Clone();
                            try
                            {
                                t.moverPieza(movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1], x, y);
                                t.calularTodo();
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
                                Nodo nuevoNodo = new Nodo(t, this.recorrido + " "+ profundidad + ". " + traductor.traducir(tablero.matrizTablero[x][y], movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1]), t.turno, profundidad);
                                nodosMovilidad.Add(nuevoNodo);
                            }
                        }
                        if(tablero.matrizTablero[x][y].movilidad.Count != 0)
                            hijos.Add(poda(nodosMovilidad));
                    }
                }
            }
            return hijos;
        }

        private Nodo poda(List<Nodo> nodosMovilidad)
        {
            Nodo mejorMovida = nodosMovilidad.ElementAt(0);
            for (int i = 1; i < nodosMovilidad.Count; i++)
            {
                if (nodosMovilidad.ElementAt(i).tablero.valorT >= mejorMovida.tablero.valorT)
                    mejorMovida = nodosMovilidad.ElementAt(i);
            }
            return mejorMovida;
        }
    }
}
