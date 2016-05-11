using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTEC.ParticionamientoTecnologico.Capa_de_Negocios.Arbol
{
    class NodoArbol
    {
        // Contains the value of the node
        private Tablero value;

        // Shows whether the current node has a parent or not
        private bool hasParent;

        // Contains the children of the node (zero or more)
        private List<NodoArbol> children;

        /// <summary>Constructs a tree node</summary>
        /// <param name="value">the value of the node</param>
        public NodoArbol(Tablero value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(
                    "Cannot insert null value!");
            }
            this.value = value;
            this.children = new List<NodoArbol>();
        }

        /// <summary>The value of the node</summary>
        public Tablero Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>The number of node's children</summary>
        public int ChildrenCount
        {
            get
            {
                return this.children.Count;
            }
        }

        /// <summary>Adds child to the node</summary>
        /// <param name="child">the child to be added</param>
        public void AddChild(NodoArbol child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(
                    "Cannot insert null value!");
            }

            if (child.hasParent)
            {
                throw new ArgumentException(
                    "The node already has a parent!");
            }

            child.hasParent = true;
            this.children.Add(child);
        }

        /// <summary>
        /// Gets the child of the node at given index
        /// </summary>
        /// <param name="index">the index of the desired child</param>
        /// <returns>the child on the given position</returns>
        public NodoArbol GetChild(int index)
        {
            return this.children[index];
        }
    }
}
