using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol;
using ChessTEC.ParticionamientoTecnologico.Capa_Logica.Utilidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol
{
    /// <summary>
    /// Clase del arbol del A* Limitado
    /// </summary>
    class ArbolAE
    {
        public List<NodoAE> variantes { get; set; }
        public NodoAE majorVariante { get; set; }
        public string turno { get; set; }
        public NodoAE raiz { get; set; }
        public Estadisticas estadisticas { get; set; }

        /// <summary>
        /// Cosntructor de clase
        /// </summary>
        /// <param name="tablero">tablero de juego</param>
        public ArbolAE(Tablero tablero)
        {
            this.estadisticas = new Estadisticas();
            turno = tablero.turno;
            raiz = new NodoAE(tablero, "", turnoAc(tablero), 0); // Crear nodo raiz
            variantes = raiz.expandir();
        }

        /// <summary>
        /// Analiza los nodos hijos del nodo atual para buscar su mejor euristica hasta la profundiad deseada
        /// </summary>
        /// <param name="profLimite">profundiada deseada</param>
        /// <returns>Nodo con el mejor camino</returns>
        public NodoAE analizar(int profLimite)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            NodoAE mejorJugada;
            do
            {
                mejorJugada = variantes.ElementAt(0);
                int posMejorJugada = 0;
                for (int i = 1; i < variantes.Count; i++)
                {
                    if (turno.Equals("B"))
                    {
                        if (variantes.ElementAt(i).tablero.valorT < mejorJugada.tablero.valorT)
                        {
                            mejorJugada = variantes.ElementAt(i);
                            posMejorJugada = i;
                        }
                    }
                    else
                    {
                        if (variantes.ElementAt(i).tablero.valorT >= mejorJugada.tablero.valorT)
                        {
                            mejorJugada = variantes.ElementAt(i);
                            posMejorJugada = i;
                        }
                    }
                }
                variantes.RemoveAt(posMejorJugada);
                List<NodoAE> nuevasVariantes = reemplazar(mejorJugada);
                variantes = variantes.Concat(nuevasVariantes)
                                    .ToList();
            } while (mejorJugada.profundidad < profLimite);

            sw.Stop();

            this.estadisticas.tiempo = sw.ElapsedMilliseconds;
            this.estadisticas.cantJugadasAnalizadas = this.variantes.Count();

            return mejorJugada;
        }

        /// <summary>
        /// Remplaza lalista de nodos hijos con los mejores hijos 
        /// </summary>
        /// <param name="nodoAExpandir">nodo al que se expande</param>
        /// <returns>Nueva lista de mejores hijos</returns>
        public List<NodoAE> reemplazar(NodoAE nodoAExpandir) {

            List<NodoAE> respuestas = nodoAExpandir.expandir();
            NodoAE mejorRespuesta = respuestas.ElementAt(0);
            if (turno.Equals("B"))
            {
                for (int i = 1; i < respuestas.Count; i++)
                {
                    if (respuestas.ElementAt(i).tablero.valorT >= mejorRespuesta.tablero.valorT)
                        mejorRespuesta = respuestas.ElementAt(i);
                }
            }
            else
            {
                for (int i = 1; i < respuestas.Count; i++)
                {
                    if (respuestas.ElementAt(i).tablero.valorT < mejorRespuesta.tablero.valorT)
                        mejorRespuesta = respuestas.ElementAt(i);
                }
            }
            return mejorRespuesta.expandir();
        }
        
        /// <summary>
        /// Cambia de turno
        /// </summary>
        /// <param name="t">tablero</param>
        /// <returns>nuevo turno</returns>
        private string turnoAc(Tablero t)
        {
            if (t.turno.Equals("B"))
            {
                return "N";
            }
            return "B";
        }

        /// <summary>
        /// Devuelve el recorrido de toda la operacion de expancion del nodo raiz
        /// </summary>
        /// <returns>Devuelve el recorrido de toda la operacion de expancion del nodo raiz</returns>
        public String PrintA()
        {
            NodoAE n = this.variantes.Last();
            return n.recorrido;
        }
    }
}
