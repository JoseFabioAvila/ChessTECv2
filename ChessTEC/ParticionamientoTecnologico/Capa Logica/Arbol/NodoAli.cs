using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol
{
    class NodoAli
    {
        public List<NodoAli> hijos { get; set; }

        public string recorrido { get; set; }

        public Tablero tablero {get; set; }

        public string turno { get; set; }

        public string hoja { get; set; }
        
        public NodoAli(Tablero tab, string recorrido, string turno)
        {
            this.tablero = tab;
            this.recorrido = recorrido;
            this.turno = turno;
            this.hijos = new List<NodoAli>();
        }

        public void agregarHijo()
        {
            this.hijos.Add(this);
        } 
    }
}