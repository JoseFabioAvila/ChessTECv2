using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol
{
    class Arbol
    {
        public List<Nodo> variantes { get; set; }
        public Nodo majorVariante { get; set; }
        public string turno { get; set; }
        public Nodo raiz { get; set; }

        public Arbol(Tablero tablero)
        {
            turno = tablero.turno;
            raiz = new Nodo(tablero, "", turnoAc(tablero), 0); // Crear nodo raiz
            variantes = raiz.expandir();
        }

        public Nodo analizar(int profLimite)
        {
            Nodo mejorJugada;
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
                List<Nodo> nuevasVariantes = reemplazar(mejorJugada);
                variantes = variantes.Concat(nuevasVariantes)
                                    .ToList();
            } while (mejorJugada.profundidad < profLimite);

            return mejorJugada;
        }

        public List<Nodo> reemplazar(Nodo nodoAExpandir) {

            List<Nodo> respuestas = nodoAExpandir.expandir();
            Nodo mejorRespuesta = respuestas.ElementAt(0);
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
        
        private string turnoAc(Tablero t)
        {
            if (t.turno.Equals("B"))
            {
                return "N";
            }
            return "B";
        }

        public void PrintA()
        {
            foreach(Nodo n in this.variantes)
            {
                Console.WriteLine("\nProfundidad: " + n.profundidad);
                Console.WriteLine("Turno: " + n.turno);
                Console.WriteLine(n.recorrido);
                Console.WriteLine("/*************************************************************/" + "\n");        
            }
        }
    }
}
