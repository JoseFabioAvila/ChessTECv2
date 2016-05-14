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
            Nodo raiz = new Nodo(tablero, "", tablero.turno);
            List<Nodo> hijosRaiz = raiz.expandir();
        }
    }
}
