using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol
{
    class rAmplitudLimitado
    {
        public int MyProperty { get; set; }
        public Tablero raiz { get; set; }
        public string turno { get; set; }
        public List<Nodo2> movimientos { get; set; }
        public Nodo2 raiz2 { get; set; }

        public rAmplitudLimitado(Tablero r)
        {
            this.raiz = r;
            this.turno = r.turno;
            this.raiz2 = new Nodo2(r, "", "B");
        } 
    }
}
