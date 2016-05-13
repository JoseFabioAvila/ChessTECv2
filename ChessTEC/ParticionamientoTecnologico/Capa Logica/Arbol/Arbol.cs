using ChessTEC.ParticionamientoTecnologico.Capa_Logica.Arbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol
{
    class Arbol
    {
        public Nodo2 raiz { get; set; }

        public Tablero tablero { get; set; }
        public string turno { get; set; }
        
        public Arbol(Tablero tab){
            this.tablero = tab;
            this.turno = tab.turno;
            this.raiz = new Nodo2(tab, "", tab.turno);
        }

        public void expandir(Nodo2 nodo, int nivel, int cont)
        {
            if (cont < nivel)
            {
                for (int x = 0; x < nodo.tablero.matrizTablero.Length; x++)
                {
                    for (int y = 0; y < nodo.tablero.matrizTablero[x].Length; y++)
                    {
                        if (nodo.tablero.matrizTablero[x][y] != null && nodo.tablero.matrizTablero[x][y].color == nodo.turno)
                        {
                            nodo.tablero.buscarJugada(x, y, turno, true);
                            List<int[]> movilidad = nodo.tablero.matrizTablero[x][y].movilidad;

                            for (int i = 0; i < movilidad.Count; i++)
                            {
                                Tablero t = new Tablero(nodo.tablero.matrizTablero, tablero.turno);
                                ////////////////////////////////////
                                //por aqui ira el proceso multicore
                                ////////////////////////////////////

                                t.moverPieza(movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1], x, y);

                                if (t.turno.Equals("B"))
                                {
                                    t.turno = "N";
                                }
                                else {
                                    t.turno = "B";
                                }
                                Nodo2 hijo = new Nodo2(t, t.matrizTablero[movilidad.ElementAt(i)[0]][movilidad.ElementAt(i)[1]].simbologia + x.ToString() + y.ToString(), t.turno);
                                nodo.hijos.Add(hijo);
                                expandir(nodo.hijos.ElementAt(i), nivel, cont);

                                //t = new Tablero(nodo.tablero.matrizTablero, tablero.turno);
                                ////////////////////////////////////
                            }
                        }
                    }
                    cont++;
                }
            }
        }

        private void PrintDFS(Nodo2 root, string spaces, int nivel)
        {
            if (raiz == null || nivel == 3)
            {
                Console.WriteLine("nulo");
                return;
            }
            Console.WriteLine(spaces + "--> nivel " + nivel.ToString());
            Console.WriteLine(spaces + root.tablero.print2(spaces));

            Nodo2 child = null;
            nivel++;
            for (int i = 0; i < root.hijos.Count; i++)
            {
                child = root.hijos.ElementAt(i);
                Console.WriteLine("--> Hijo: " + i.ToString());
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

        /*public Nodo root { get; set; }
        

        /// <summary>Constructs the tree</summary>
        /// <param name="value">the value of the node</param>
        public Arbol(Tablero value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Cannot insert null value!");
            }

            this.root = new Nodo(value);
        }


        public void generarArbol(Nodo root, int profundidad) {
            if (this.root == null)
            {
                return;
            }
            for (int f = 0; f < root.value.matrizTablero.Length; f++) {
                for (int c = 0; c < root.value.matrizTablero[f].Length; c++)
                {
                    if (root.value.matrizTablero[f][c] != null)
                    {
                        root.value.buscarJugada(f, c, root.value.turno, true);
                        //root.value.matrizTablero[f][c].actualizarMov(f, c, root.value);
                        if (root.value.turno == root.value.matrizTablero[f][c].color)
                        {
                            if (root.value.matrizTablero[f][c].movilidad != null)
                            {
                                foreach (int[] mov in root.value.matrizTablero[f][c].movilidad)
                                {
                                    Tablero ntablero = root.value;
                                    ntablero.matrizTablero = root.value.matrizTablero;
                                    ntablero.moverPieza(mov[0], mov[1], f, c);
                                    root.AddChild(new Nodo(ntablero));
                                }
                            }
                            //if (root.value.matrizTablero[f][c].piezasComibles != null)
                            //{
                            //    foreach (int[] mov in root.value.matrizTablero[f][c].piezasComibles)
                            //    {
                            //        Tablero ntablero = new Tablero();
                            //        ntablero.matrizTablero = root.value.matrizTablero;
                            //        ntablero.comerPieza(mov[1], mov[0], f, c);
                            //        root.AddChild(new NodoArbol(ntablero));
                            //    }
                            //}
                        }
                    }
                }
                for (int i = 0; i < root.ChildrenCount(); i++)
                {
                    generarArbol(root.GetChild(i), profundidad++);
                }
            }
        }

        /// <summary>Traverses and prints tree in
        /// Depth-First Search (DFS) manner</summary>
        /// <param name="root">the root of the tree to be
        /// traversed</param>
        /// <param name="spaces">the spaces used for
        /// representation of the parent-child relation</param>
        private void PrintDFS(Nodo root, string spaces, int nivel)
        {
            if (this.root == null)
            {
                return;
            }
            Console.WriteLine(spaces + "--> nivel " +nivel.ToString());
            Console.WriteLine(spaces + root.value.print2(spaces));

            Nodo child = null;
            for (int i = 0; i < root.ChildrenCount(); i++)
            {
                child = root.GetChild(i);
                Console.WriteLine("--> Hijo: " + i.ToString());
                PrintDFS(child, spaces + "   ", nivel++);
            }
        }


        /// <summary>Traverses and prints the tree in
        /// Depth-First Search (DFS) manner</summary>
        public void TraverseDFS()
        {
            Console.WriteLine("--> root");
            this.PrintDFS(this.root, string.Empty, 0);
        }*/
    }
}
