using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol
{
    class Nodo2
    {
        public List<Nodo2> hijos { get; set; }

        public string recorrido { get; set; }

        public Tablero tablero {get; set; }

        public string turno { get; set; }
        
        public Nodo2(Tablero tab, string recorrido, string turno)
        {
            this.tablero = tab;
            this.recorrido = recorrido;
            this.turno = turno;
        }

        public void agregarHijo()
        {
            this.hijos.Add(this);
        }

        public void expandir()
        {
            for (int x = 0; x < tablero.matrizTablero.Length; x++)
            {
                for (int y = 0; y < tablero.matrizTablero[x].Length; y++)
                {
                    if (tablero.matrizTablero[x][y] != null && tablero.matrizTablero[x][y].color == turno)
                    {                       
                        for (int i = 0; i < tablero.matrizTablero[x][y].movilidad.Count; i++)
                        {
                            Tablero t = new Tablero(tablero.matrizTablero, tablero.turno);
                            int[] movilidad = t.matrizTablero[x][y].movilidad.ElementAt(i);

                            t.moverPieza(movilidad[0], movilidad[1], x, y);

                            if (t.turno.Equals("B"))
                            {
                                t.turno = "N";
                            }
                            else {
                                t.turno = "B";
                            }
                            this.hijos.Add(new Nodo2( t, t.matrizTablero[x][y].simbologia+x.ToString()+y.ToString(), t.turno));
                        }                        
                    }
                }
            }
        }
    }
}
