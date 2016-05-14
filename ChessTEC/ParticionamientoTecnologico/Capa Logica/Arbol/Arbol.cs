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
            if (cont <= nivel)
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
                                bool funcionono = true;
                                Tablero t = new Tablero(nodo.tablero.matrizTablero, tablero.turno);
                                ////////////////////////////////////
                                //por aqui ira el proceso multicore
                                ////////////////////////////////////
                                try {
                                    t.moverPieza(movilidad.ElementAt(i)[0], movilidad.ElementAt(i)[1], x, y);
                                }
                                catch (Exception e){
                                    funcionono = false;
                                }

                                if (t.turno.Equals("B"))
                                {
                                    t.turno = "N";
                                }
                                else {
                                    t.turno = "B";
                                }
                                if (funcionono)
                                {
                                    Nodo2 hijo = new Nodo2(t, t.matrizTablero[movilidad.ElementAt(i)[0]][movilidad.ElementAt(i)[1]].simbologia + x.ToString() + y.ToString(), t.turno);
                                    hijo.hoja = "No soy Hoja";
                                    //Console.WriteLine(hijo.tablero.print());

                                    nodo.hijos.Add(hijo);
                                    expandir(nodo.hijos.ElementAt(i), nivel, cont);

                                    //t = new Tablero(nodo.tablero.matrizTablero, tablero.turno);
                                    ////////////////////////////////////
                                }
                            }
                        }
                    }
                    cont++;
                }
            }
            else {
                nodo.hoja = "Soy hoja";
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
            Console.WriteLine(root.hoja);
            Console.WriteLine(spaces + root.tablero.print2(spaces));

            Nodo2 child = null;
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
