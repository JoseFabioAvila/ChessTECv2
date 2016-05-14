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

        public Arbol(Tablero tablero)
        {
            Nodo raiz = new Nodo(tablero, "", turnoAc(tablero),0);
            List<Nodo> hijosRaiz = raiz.expandir();
            Console.WriteLine(hijosRaiz.Count);
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
