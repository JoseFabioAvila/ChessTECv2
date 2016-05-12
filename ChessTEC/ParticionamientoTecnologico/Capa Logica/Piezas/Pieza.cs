using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios
{
    abstract class Pieza
    {
        public string color { get; set; }
        public double valor { get; set; }
        public string simbologia { get; set; }
        public List<int[]> movilidad { get; set; }
        public List<int[]> piezasComibles { get; set; }


        public string imagen = @"C:\Users\sejol\Documents\GitHub\ChessTECv2\ChessTEC\Recursos\";
        //public string imagen = @"C:\Users\fabio\Desktop\ChessTEC\ChessTEC\Recursos\";

        public abstract void calcularValor(int fila, int columna, Tablero tablero);

        public abstract void actualizarMov(int fila, int columna, Tablero tablero);

        public bool verificarMovida(int fila, int columna)
        {
            foreach (int[] mov in movilidad)
            {
                if (mov[0] == fila && mov[1] == columna)
                    return true;
            }
            return false;
        }

        internal void hacerNulo()
        {
            throw new NotImplementedException();
        }
    }
}
