using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol
{
    class Arbol
    {
        public NodoArbol root { get; set; }
        

        /// <summary>Constructs the tree</summary>
        /// <param name="value">the value of the node</param>
        public Arbol(Tablero value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Cannot insert null value!");
            }

            this.root = new NodoArbol(value);
        }


        public void generarArbol(NodoArbol root, int profundidad) {
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
                                    Tablero ntablero = new Tablero();
                                    ntablero.matrizTablero = root.value.matrizTablero;
                                    ntablero.moverPieza(mov[1], mov[0], f, c);
                                    root.AddChild(new NodoArbol(ntablero));
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
        private void PrintDFS(NodoArbol root, string spaces, int nivel)
        {
            if (this.root == null)
            {
                return;
            }
            Console.WriteLine(spaces + "--> nivel " +nivel.ToString());
            Console.WriteLine(spaces + root.value.print2(spaces));

            NodoArbol child = null;
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
        }
    }
}
