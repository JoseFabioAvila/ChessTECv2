using ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol
{
    class ArbolAli
    {
        public NodoAli raiz { get; set; }

        public Tablero tablero { get; set; }
        public string turno { get; set; }
        
        public ArbolAli(Tablero tab){
            this.tablero = tab;
            this.turno = tab.turno;
            this.raiz = new NodoAli(tab, "", tab.turno);
        }

        public void expandir(NodoAli nodo, int nivel, int cont)
        {
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

                                List<int[]> movilidad = new List<int[]>(tablero.matrizTablero[x][y].movilidad);

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
                                        funciono = false;
                                    }

                                    if (t.turno.Equals("B"))
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
                                            if (crearHijo(t, movilidad, i, x, y, nodo, nivel, cont))
                                            {
                                                tasks[i] = Task.Run(() => crearHijo(t, movilidad, i, x, y, nodo, nivel, cont));
                                            }
                                        }
                                        catch (Exception e)
                                        {

                                        }
                                    }
                                }
                                try
                                {
                                    Task.WaitAll(tasks);
                                }
                                catch (Exception e)
                                {
                                }
                            }
                            catch (Exception e)
                            {

                            }
                        }
                    }
                }
                //hacer poda
                nodo.hijos = poda(nodo.hijos);
                expandir(nodo.hijos.ElementAt(0), nivel, cont+1);
            }
            else 
            {
                nodo.hoja = "Soy hoja";
            }
        }

        public bool crearHijo(Tablero t, List<int[]> movilidad, int i, int x, int y, NodoAli nodo, int nivel, int cont)
        {
            try
            {
                NodoAli hijo = new NodoAli(t, t.matrizTablero[movilidad.ElementAt(i)[0]][movilidad.ElementAt(i)[1]].simbologia + x.ToString() + y.ToString(), t.turno);
                hijo.hoja = "No soy Hoja";
                //Console.WriteLine(hijo.tablero.print());

                nodo.hijos.Add(hijo);

                Console.WriteLine("Hilo --> " + i + " en NIVEL " + cont);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        private List<NodoAli> poda(List<NodoAli> nodosMovilidad)
        {
            NodoAli mejorMovida = nodosMovilidad.ElementAt(0);
            for (int i = 1; i < nodosMovilidad.Count; i++)
            {
                if (nodosMovilidad.ElementAt(i).tablero.valorT >= mejorMovida.tablero.valorT)
                    mejorMovida = nodosMovilidad.ElementAt(i);
            }

            List<NodoAli> na = new List<NodoAli>();
            na.Add(mejorMovida);

            return na;
        }

        private void PrintDFS(NodoAli root, string spaces, int nivel)
        {
            if (raiz == null || nivel == 3)
            {
                Console.WriteLine("nulo");
                return;
            }
            Console.WriteLine(spaces + "--> nivel " + nivel.ToString());
            Console.WriteLine(root.hoja);
            Console.WriteLine(spaces + root.tablero.print2(spaces));

            NodoAli child = null;
            nivel++;
            for (int i = 0; i < root.hijos.Count; i++)
            {
                child = root.hijos.ElementAt(i);
                Console.WriteLine(spaces + "--> Hijo: " + i.ToString());
                PrintDFS(child, spaces + "   ", nivel);
            }            
        }
        
        /// <summary>Traverses and prints the tree in
        /// Depth-First Search (DFS) manner</summary>
        public void TraverseDFS()
        {
            Console.WriteLine("--> root");
            this.PrintDFS(raiz, string.Empty, 0);
        }
    }
}