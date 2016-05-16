using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Utilidades
{
    class Estadisticas
    {
        public int cantProcesos { get; set; }
        public int cantJugadasAnalizadas { get; set; }
        public double calJugada { get; set; }
        public long tiempo { get; set; }

        public Estadisticas()
        {
            this.cantProcesos = 0;
            this.cantJugadasAnalizadas = 0;
            this.calJugada = 0;
            this.tiempo = 0;
        } 
    }
}