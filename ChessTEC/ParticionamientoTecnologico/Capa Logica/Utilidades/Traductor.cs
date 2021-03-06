﻿using ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_Logica.Utilidades
{
    /// <summary>
    /// Clase que traduce la jugada para que sea mas lejible para el usuario
    /// </summary>
    class Traductor
    {
        public Pieza pieza { get; set; }
        public int fila { get; set; }
        public int columna { get; set; }

        /// <summary>
        /// Constructor de clase
        /// </summary>
        public Traductor(){}

        /// <summary>
        /// Traduce una posicion de ficha en tablero a simbologia mas entendible para el usaurio
        /// </summary>
        /// <param name="pieza">pieza de jugada</param>
        /// <param name="fila">fila</param>
        /// <param name="columna">columna</param>
        /// <returns>String producto de la traduccion</returns>
        public string traducir(Pieza pieza, int fila,int columna)
        {
            return pieza.simbologia+getCol(columna)+getFil(fila);
        }

        /// <summary>
        /// Traduce la columna
        /// </summary>
        /// <param name="c">columna</param>
        /// <returns>Columna traducida</returns>
        private string getCol(int c)
        {
            string col = "";

            switch (c)
            {
                case 0:
                    col = "a";
                    break;
                case 1:
                    col = "b";
                    break;
                case 2:
                    col = "c";
                    break;
                case 3:
                    col = "d";
                    break;
                case 4:
                    col = "e";
                    break;
                case 5:
                    col = "f";
                    break;
                case 6:
                    col = "g";
                    break;
                case 7:
                    col = "h";
                    break;
                case 8:
                    break;
            }
            return col;
        }

        /// <summary>
        /// Traduce la fila
        /// </summary>
        /// <param name="f">fila</param>
        /// <returns>Fila traducida</returns>
        private string getFil(int f)
        {
            string fil = "";

            switch (f)
            {
                case 0:
                    fil = "8";
                    break;
                case 1:
                    fil = "7";
                    break;
                case 2:
                    fil = "6";
                    break;
                case 3:
                    fil = "5";
                    break;
                case 4:
                    fil = "4";
                    break;
                case 5:
                    fil = "3";
                    break;
                case 6:
                    fil = "2";
                    break;
                case 7:
                    fil = "1";
                    break;
                case 8:
                    break;
            }
            return fil;
        }
    }
}
