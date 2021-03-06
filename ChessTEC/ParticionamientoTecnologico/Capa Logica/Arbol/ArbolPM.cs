﻿using ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol;
using ChessTEC.ParticionamientoTecnologico.Capa_Logica.Utilidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol
{
    /// <summary>
    /// Calse de Arbol del primero mejor
    /// </summary>
    class ArbolPM
    {
        public NodoPM raiz { get; set; }

        public Tablero tablero { get; set; }

        public string turno { get; set; }

        public Estadisticas estadisticas { get; set; }

        public string camino { get; set; }

        public double valorT { get; set; }

        Traductor traductor = new Traductor();
        
        /// <summary>
        /// Constructor de clase
        /// </summary>
        /// <param name="tab">tablero</param>
        public ArbolPM(Tablero tab)
        {
            this.estadisticas = new Estadisticas();

            this.tablero = tab;
            this.turno = tab.turno;
            
            this.raiz = new NodoPM(tab, "", tab.turno);
        }

        /// <summary>
        /// Expande el nodo raiz
        /// </summary>
        /// <param name="nodo">nodo raiz</param>
        /// <param name="nivel">nivel de profundiad deseado</param>
        /// <param name="cont">contador que inicia en 0</param>
        public void expandir(NodoPM nodo, int nivel, int cont)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (cont < nivel)
            {
                for (int x = 0; x < nodo.tablero.matrizTablero.Length; x++)
                {
                    for (int y = 0; y < nodo.tablero.matrizTablero[x].Length; y++)
                    {
                        if (nodo.tablero.matrizTablero[x][y] != null && nodo.tablero.matrizTablero[x][y].color == nodo.turno)
                        {
                            try
                            {
                                nodo.tablero.buscarJugada(x, y, nodo.turno, true);

                                List<int[]> movilidad = new List<int[]>(nodo.tablero.matrizTablero[x][y].movilidad);

                                Task[] tasks = new Task[movilidad.Count];

                                int pa = movilidad.Count;

                                for (int i = 0; i < pa; i++)
                                {
                                    bool funciono = true;
                                    Tablero t = new Tablero(nodo.tablero.matrizTablero, tablero.turno);

                                    try
                                    {
                                        t.moverPieza(movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1], x, y);
                                        t.calularTodo();
                                    }
                                    catch (Exception e)
                                    {
                                        e.Message.ToString();
                                        funciono = false;
                                    }

                                    if (nodo.tablero.turno.Equals("B"))
                                    {
                                        t.turno = "N";
                                    }
                                    else
                                    {
                                        t.turno = "B";
                                    }
                                    if (funciono)
                                    {
                                        try
                                        {
                                            //if (crearHijo(t, movilidad, i, x, y, nodo, nivel, cont)){
                                                tasks[i] = Task.Run(() => crearHijo(t, movilidad, i, x, y, nodo, nivel, cont));
                                            
                                            if (!tasks[i].IsFaulted && !tasks[i].IsCanceled)
                                            {
                                                this.estadisticas.cantJugadasAnalizadas += tasks.Length;
                                                tasks[i].Wait();
                                            }
                                        }
                                        catch (AggregateException)
                                        {
                                            //e.Message.ToString();
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                e.Message.ToString();
                            }
                        }
                    }
                }
                //hacer poda
                nodo.hijos = poda(nodo.hijos);
                //Console.WriteLine("------------Turno : " + nodo.hijos.ElementAt(0).turno + "------------");
                //Console.WriteLine("Nivel del profundidad = "+cont);
                //Console.WriteLine(nodo.hijos.ElementAt(0).tablero.print());
                expandir(nodo.hijos.ElementAt(0), nivel, cont+1);
            }
            else 
            {
                
                nodo.hoja = "Soy hoja";
                camino = nodo.recorrido;
                nodo.tablero.calularTodo();
                valorT = nodo.tablero.valorT;
                
            }
            sw.Stop();
            this.estadisticas.tiempo = sw.ElapsedMilliseconds;

        }

        /// <summary>
        /// Crear hijo que es el mejor de toda las lista segun heuritica
        /// </summary>
        /// <param name="t">tablero</param>
        /// <param name="movilidad">Lista de movilidad</param>
        /// <param name="i">posicion de la lista de movilidad</param>
        /// <param name="x">fila del tablero</param>
        /// <param name="y">columna del tablero</param>
        /// <param name="nodo">nodo actual</param>
        /// <param name="nivel">nivel de profundiad deseado</param>
        /// <param name="cont">contador que inicia en 0</param>
        /// <returns>booleano que indica si operacion fue exitosa</returns>
        public bool crearHijo(Tablero t, List<int[]> movilidad, int i, int x, int y, NodoPM nodo, int nivel, int cont)
        {
            try
            {
                NodoPM hijo = new NodoPM(t, nodo.recorrido +" " + cont + ". " + nodo.tablero.turno + traductor.traducir(tablero.matrizTablero[x][y], movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1]) + " | ", t.turno);
                hijo.hoja = "No soy Hoja";
                //Console.WriteLine(hijo.tablero.print());

                nodo.hijos.Add(hijo);

                //Console.WriteLine("Hilo --> " + i + " en NIVEL " + cont);

                return true;
            }
            catch(Exception e)
            {
                e.Message.ToString();
                return false;
            }
        }

        /// <summary>
        /// Poda los hijos de un nodo para sacar el mejor
        /// </summary>
        /// <param name="nodosMovilidad">Lista de nodos hijos del nodo actual</param>
        /// <returns>Lista con los mejores hijos</returns>
        private List<NodoPM> poda(List<NodoPM> nodosMovilidad)
        {
            NodoPM mejorMovida = nodosMovilidad.ElementAt(0);
            if(mejorMovida.tablero.turno == "N") {
                for (int i = 1; i < nodosMovilidad.Count; i++)
                {
                    //if ((nodosMovilidad.ElementAt(i).tablero.valorT* nodosMovilidad.ElementAt(i).correccion) >= (mejorMovida.tablero.valorT * mejorMovida.correccion))
                    if (nodosMovilidad.ElementAt(i).tablero.valorT  >= mejorMovida.tablero.valorT)
                        mejorMovida = nodosMovilidad.ElementAt(i);
                }
            }
            else
            {
                for (int i = 1; i < nodosMovilidad.Count; i++)
                {
                    //if ((nodosMovilidad.ElementAt(i).tablero.valorT* nodosMovilidad.ElementAt(i).correccion) >= (mejorMovida.tablero.valorT * mejorMovida.correccion))
                    if ((nodosMovilidad.ElementAt(i).tablero.valorT) <= (mejorMovida.tablero.valorT))
                        mejorMovida = nodosMovilidad.ElementAt(i);
                }
            }

            List<NodoPM> na = new List<NodoPM>();
            na.Add(mejorMovida);

            return na;
        }
    }
}