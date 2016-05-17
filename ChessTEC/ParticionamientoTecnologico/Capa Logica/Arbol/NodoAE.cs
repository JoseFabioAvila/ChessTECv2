using ChessTEC.ParticionamientoTecnologico.Capa_Logica.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol
{
    /// <summary>
    /// Clase nodo del A* limitado
    /// </summary>
    class NodoAE   
    {
        public Tablero tablero { get; set; }
        public string recorrido { get; set; }
        public int profundidad { get; set; }
        public string turno { get; set; }
        public List<NodoAE> hijos { get; set; }

        Traductor traductor = new Traductor();

        /// <summary>
        /// Constructor de clase
        /// </summary>
        /// <param name="tab">tablero</param>
        /// <param name="recorrido">recorrido recomendado al usuario</param>
        /// <param name="turno">turno de la partida</param>
        /// <param name="prof">profundiadad</param>
        public NodoAE(Tablero tab, string recorrido, string turno,int prof)
        {
            this.tablero = tab;
            this.recorrido = recorrido;
            this.turno = turno;
            this.profundidad = prof;
            hijos = new List<NodoAE>();
        }

        /// <summary>
        /// Cosntructor de clase
        /// </summary>
        /// <param name="nodo">Nodo de A* limitado</param>
        public NodoAE(NodoAE nodo)
        {
            this.tablero = nodo.tablero;
            this.recorrido = nodo.recorrido;
            this.turno = nodo.turno;
            this.profundidad = nodo.profundidad;

        }

        /// <summary>
        /// Metodo de expancion de nodos
        /// </summary>
        /// <returns>Lista de nodos expandidos</returns>
        public List<NodoAE> expandir()
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
                        
                        List<NodoAE> nodosMovilidad = new List<NodoAE>();
                        List<int[]> movilidad = new List<int[]>(tablero.matrizTablero[x][y].movilidad);

                        Task[] tasks = new Task[movilidad.Count];
                        int c = (tablero.matrizTablero[x][y].movilidad.Count);
                        string fc;

                        for (int i = 0; i < c; i++)
                        {
                            bool funcionono = true;
                            Tablero t = new Tablero(tablero.matrizTablero, turno);
                            //Tablero t = (Tablero)tablero.Clone();
                            try
                            {
                                t.moverPieza(movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1], x, y);
                                t.calularTodo();
                            }
                            catch (Exception e)
                            {
                                e.Message.ToString();
                                funcionono = false;
                            }
                            
                            if (turno.Equals("B"))
                                fc = "N";
                            else
                                fc = "B";

                            if (funcionono)
                            {
                                NodoAE nuevoNodo = new NodoAE(t, this.recorrido + " "+ profundidad + ". " + traductor.traducir(tablero.matrizTablero[x][y], movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1]), fc, profundidad);
                                nodosMovilidad.Add(nuevoNodo);
                            }
                        }
                        if(tablero.matrizTablero[x][y].movilidad.Count != 0)
                            hijos.Add(poda(nodosMovilidad,turno));
                    }
                }
            }
            return hijos;
        }

        /// <summary>
        /// Metod encargado de podar los nodos que no son buena heuristica
        /// </summary>
        /// <param name="nodosMovilidad">Lista de nodos a podar</param>
        /// <param name="turno">turno</param>
        /// <returns>Mejor nodo segun heuristica</returns>
        private NodoAE poda(List<NodoAE> nodosMovilidad, string turno)
        {
            NodoAE mejorMovida = nodosMovilidad.ElementAt(0);
            if (turno.Equals("B"))
            {
                for (int i = 1; i < nodosMovilidad.Count; i++)
                {
                    if (nodosMovilidad.ElementAt(i).tablero.valorT >= mejorMovida.tablero.valorT)
                        mejorMovida = nodosMovilidad.ElementAt(i);
                }
            }
            else
            {
                for (int i = 1; i < nodosMovilidad.Count; i++)
                {
                    if (nodosMovilidad.ElementAt(i).tablero.valorT < mejorMovida.tablero.valorT)
                        mejorMovida = nodosMovilidad.ElementAt(i);
                }
            }
            return mejorMovida;
        }
    }
}
