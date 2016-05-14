using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol
{
    class Arbol2
    {
        public List<Nodo2> variables { get; set; }
        public string turno { get; set; }
        public Nodo2 mejorVariante { get; set; }

        public Arbol2(Tablero t)
        {
            Nodo2 raiz = new Nodo2(t,"",t.turno);
        }
    }
}
