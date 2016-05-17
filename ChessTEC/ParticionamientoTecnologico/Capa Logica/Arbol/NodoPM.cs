using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol
{
    /// <summary>
    /// Calse del Nodo de primero mejor
    /// </summary>
    class NodoPM
    {
        public List<NodoPM> hijos { get; set; }

        public string recorrido { get; set; }

        public Tablero tablero {get; set; }

        public string turno { get; set; }

        public string hoja { get; set; }

        public int correccion { get; set; }

        /// <summary>
        /// Constructor de clase
        /// </summary>
        /// <param name="tab">tablero de juego</param>
        /// <param name="recorrido">recorrido recomendado al usuario</param>
        /// <param name="turno">la ficha en turno</param>
        public NodoPM(Tablero tab, string recorrido, string turno)
        {
            this.tablero = tab;
            this.recorrido = recorrido;
            this.turno = turno;
            this.hijos = new List<NodoPM>();
            if (turno.Equals("B"))
            {
                correccion = 1;
            }
            else{
                correccion = -1;
            }
        }

        /// <summary>
        /// agrega hijos a la lista de hijos
        /// </summary>
        public void agregarHijo()
        {
            this.hijos.Add(this);
        } 
    }
}