using System;
using System.Collections.Generic;

namespace Task_8
{
    class Program
    {

        class Edge
        {
            public int v1, v2;

            public Edge(int v1, int v2)
            {
                this.v1 = v1;
                this.v2 = v2;
            }

        }
        static Random rnd = new Random();
        static int K;
        static List<Edge> E = new List<Edge>();
        static List<string> chains = new List<string>();
        static int[] V = new int[] { 0, 1, 2, 3, 4 };
        static void GraphGenerator(int size)
        {
            Console.WriteLine("Список вершин: 1, 2, 3, 4, 5");
            Console.WriteLine("Список рёбер:");
            bool[,] Graph = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j) { Graph[i, j] = false; }
                    if (i < j)
                    {
                        int t = rnd.Next(2);
                        if (t == 0) { Graph[i, j] = false; }
                        else { Graph[i, j] = true; }
                        if (Graph[i, j] == true) { E.Add(new Edge(i, j)); Console.WriteLine("{0}-{1}", i + 1, j + 1); }
                    }
                    if (i > j) { Graph[i, j] = Graph[j, i]; }
                }
            }

        }//Генератор матриц смежности (он же генератор тестов) 
        static void chainsSearch()
        {
            int[] color = new int[V.Length];
            for (int i = 0; i < V.Length - 1; i++)
                for (int j = i + 1; j < V.Length; j++)
                {
                    for (int k = 0; k < V.Length; k++)
                        color[k] = 1;
                    DFSchain(i, j, E, color, (i + 1).ToString());
                    //поскольку в C# нумерация элементов начинается с нуля, то 
                    //для удобочитаемости результатов в строку передаем i + 1 
                }
        }
        static void DFSchain(int u, int endV, List<Edge> E, int[] color, string s)
        {
            //вершину не следует перекрашивать, если u == endV (возможно в endV есть несколько путей) 
            if (u != endV)
                color[u] = 2;
            else
            {
                chains.Add(s);
                return;
            }
            for (int w = 0; w < E.Count; w++)
            {
                if (color[E[w].v2] == 1 && E[w].v1 == u)
                {
                    DFSchain(E[w].v2, endV, E, color, s + "-" + (E[w].v2 + 1).ToString());
                    color[E[w].v2] = 1;
                }
                else if (color[E[w].v1] == 1 && E[w].v2 == u)
                {
                    DFSchain(E[w].v1, endV, E, color, s + "-" + (E[w].v1 + 1).ToString());
                    color[E[w].v1] = 1;
                }
            }
        }
        static void Main(string[] args)
        {
            bool ok = false;
            K = rnd.Next(1, 6);
            GraphGenerator(5);
            chainsSearch();
            Console.WriteLine("Список простых цепей с длиной {0}:", K);
            for (int i = 0; i < chains.Count; i++)
            {
                if (chains[i].Length % 2 == 1)
                {
                    if ((chains[i].Length) / 2 == K) { ok = true; Console.WriteLine(chains[i
                        ]); }
                }
                else
                {
                    if (((chains[i].Length)+1) / 2 == K) { ok = true; Console.WriteLine(chains[i]); }
                }
            }
            if (!ok) Console.WriteLine("В графе нет простых цепей длиной {0}", K);
            Console.ReadLine();
        }
    }
}
