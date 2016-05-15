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
            expandir(tablero);
        }

        private Nodo analizar(int profLimite)
        {
            Nodo mejorHeuristica;
            return null;
        }

        public Nodo expandir(Tablero tablero,Nodo nodoAExpandir) {

            nodoAExpandir = new Nodo(tablero, "", turnoAc(tablero), 0); // Crear nodo raiz
            List<Nodo> jugadas = nodoAExpandir.expandir(); // juega blanco
            Nodo mejorMovidaBlanca = jugadas.ElementAt(0);
            if (turno.Equals("B"))
            {
                for (int i = 1; i < jugadas.Count; i++)
                {
                    if (jugadas.ElementAt(i).tablero.valorT >= mejorMovidaBlanca.tablero.valorT)
                        mejorMovidaBlanca = jugadas.ElementAt(i);
                }
            }
            else
            {
                for (int i = 1; i < jugadas.Count; i++)
                {
                    if (jugadas.ElementAt(i).tablero.valorT < mejorMovidaBlanca.tablero.valorT)
                        mejorMovidaBlanca = jugadas.ElementAt(i);
                }
            }
            nodoAExpandir = new Nodo(tablero, "", tablero.turno, 0); // Crear nodo raiz
            List<Nodo> respuestas = nodoAExpandir.expandir(); // juega negro
            Nodo mejorMovidaNegra = respuestas.ElementAt(0);
            if (turno.Equals("B"))
            {
                for (int i = 1; i < jugadas.Count; i++)
                {
                    if (jugadas.ElementAt(i).tablero.valorT >= mejorMovidaBlanca.tablero.valorT)
                        mejorMovidaBlanca = jugadas.ElementAt(i);
                }
            }
            else
            {
                for (int i = 1; i < jugadas.Count; i++)
                {
                    if (jugadas.ElementAt(i).tablero.valorT < mejorMovidaBlanca.tablero.valorT)
                        mejorMovidaBlanca = jugadas.ElementAt(i);
                }
            }
            return mejorMovidaNegra;
        }
        
        private string turnoAc(Tablero t)
        {
            if (t.turno.Equals("B"))
            {
                return "N";
            }
            return "B";
        }
    }
}
