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
            this.hijos = new List<Nodo2>();
        }

        public void agregarHijo()
        {
            this.hijos.Add(this);
        } 
    }
}