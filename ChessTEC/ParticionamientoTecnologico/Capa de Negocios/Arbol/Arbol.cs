using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol
{
    class Arbol
    {
        private NodoArbol root;

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

        /// <summary>Constructs the tree</summary>
        /// <param name="value">the value of the root node</param>
        /// <param name="children">the children of the root
        /// node</param>
        public Arbol(Tablero value, params NodoArbol[] children)
        {
            foreach (NodoArbol child in children)
            {
                this.root.AddChild(child);
            }
        }

        /// <summary>
        /// The root node or null if the tree is empty
        /// </summary>
        public NodoArbol Root
        {
            get
            {
                return this.root;
            }
        }

        /// <summary>Traverses and prints tree in
        /// Depth-First Search (DFS) manner</summary>
        /// <param name="root">the root of the tree to be
        /// traversed</param>
        /// <param name="spaces">the spaces used for
        /// representation of the parent-child relation</param>
        private void PrintDFS(NodoArbol root, string spaces)
        {
            if (this.root == null)
            {
                return;
            }

            Console.WriteLine(spaces + root.Value);

            NodoArbol child = null;
            for (int i = 0; i < root.ChildrenCount; i++)
            {
                child = root.GetChild(i);
                PrintDFS(child, spaces + "   ");
            }
        }

        /// <summary>Traverses and prints the tree in
        /// Depth-First Search (DFS) manner</summary>
        public void TraverseDFS()
        {
            this.PrintDFS(this.root, string.Empty);
        }
    }
}
