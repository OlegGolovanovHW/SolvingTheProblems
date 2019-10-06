using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace SolvingTheProblems
{

    class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            int M = int.Parse(Console.ReadLine());
            //int N = 6;
            //int M = 6;
            string[,] Labyrinth = new string[N, M];
            Random rand = new Random();
            int n = 0;
            int I = rand.Next(0, N);
            int J = rand.Next(0, M);
            /*int I = 2;
            int J = 1;
            string[,] Labyrinth =
            {
                {"X","X","X","X","X","X"},
                {"O","X","O","O","O","X"},
                {"X","*","O","X","O","X"},
                {"X","X","X","X","O","X"},
                {"O","O","O","O","O","X"},
                {"O","X","X","X","O","X"}
            };*/
            Labyrinth[I, J] = "*";
            Console.WriteLine("I = {0}, J = {1}", I, J);
            
            for (int i = 0; i < Labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j < Labyrinth.GetLength(1); j++)
                {
                    
                    int k = rand.Next(0, 2);
                    if (k == 0 || Labyrinth[i, j] == "*")
                    {
                        Labyrinth[i, j] = n.ToString(); 
                        n++;                          
                    }
                    else if (Labyrinth[i, j] != "*")
                    {
                        Labyrinth[i, j] = "X";
                    }
                    /*
                    if (Labyrinth[i, j] != "X")
                    {
                        Labyrinth[i, j] = n.ToString(); - тестирование изначального примера
                        n++;
                    }*/
                }
            }

            int[,] G = new int[n, n];
            for (int i = 0; i<Labyrinth.GetLength(0); i++)
            {
                for (int j = 0; j<Labyrinth.GetLength(1); j++)
                {
                    if (Labyrinth[i, j] != "X")
                    {
                        if (i > 0)
                        {
                            if (Labyrinth[i - 1, j] != "X")
                            {
                                int k = int.Parse(Labyrinth[i, j]);
                                int l = int.Parse(Labyrinth[i - 1, j]);
                                G[k, l] = 1;
                                G[l, k] = 1;
                            }
                        }
                        if (j > 0)
                        {
                            if (Labyrinth[i, j - 1] != "X")
                            {
                                int k = int.Parse(Labyrinth[i, j]);
                                int l = int.Parse(Labyrinth[i, j - 1]);
                                G[k, l] = 1;
                                G[l, k] = 1;
                            }
                        }
                        if (i<Labyrinth.GetLength(0)-1)
                        {
                            if (Labyrinth[i + 1, j] != "X")
                            {
                                int k = int.Parse(Labyrinth[i, j]);
                                int l = int.Parse(Labyrinth[i + 1, j]);
                                G[k, l] = 1;
                                G[l, k] = 1;
                            }
                        }
                        if (j < Labyrinth.GetLength(1) - 1)
                        {
                            if (Labyrinth[i, j + 1] != "X")
                            {
                                int k = int.Parse(Labyrinth[i, j]);
                                int l = int.Parse(Labyrinth[i, j + 1]);
                                G[k, l] = 1;
                                G[l, k] = 1;
                            }
                        }
                    } 
                }
            }
            /* //выводим матрицу смежности 
            for (int i = 0; i<G.GetLength(0); i++)
            {
                for (int j = 0; j<G.GetLength(1); j++)
                {
                    Console.Write(G[i, j] + " ");
                }
                Console.WriteLine();
            }
            */
            //реализация алгоритма поиска в ширину
            int s = int.Parse(Labyrinth[I, J]); //стартовая вершина
            Queue<int> q = new Queue<int>(); //создаем очередь
            q.Enqueue(s); //помещаем в очередь стартовую вершину
            bool[] used = new bool[n]; //массив "подожжёных" вершин
            int[] d = new int[n]; //массив длин кратчайших путей из стартовой вершины в данные
            int[] p = new int[n]; //массив предков
            used[s] = true; //поджигаем стартовую вершину
            p[s] = -1; //у начального элемента предков нету
            while (q.Count() > 0) //пока очередь не пустая
            {
                int v = q.Dequeue(); //извлекаем элемент из начала очереди (выбираем вершину)
                //Console.Write(v + " ");
                for (int j = 0; j<G.GetLength(1); j++) //для этой вершины просмотрим все связанные с ней
                {

                    if (G[v, j] == 1) //если нашли такую вершину
                    {
                        int to = j; 
                        if (!used[to]) //если вершина ещё не горит
                        {
                            used[to] = true; //подожгем её
                            q.Enqueue(to); //поместим в очередь
                            d[to] = d[v] + 1; //запишем значение кратчайшего пути в эту вершину из вершины S
                            p[to] = v; //для данной вершины to запишем предка v
                        }
                    }
                }
            }
            /*Console.WriteLine();
            for (int i = 0; i<n; i++)
            {
                Console.Write(d[i] + " ");
            }
            Console.WriteLine();
            */
            int MinWay = int.MaxValue;
            int n0 = int.Parse(Labyrinth[I, J]); //номер вершины, из которой мы выходим из лабиринта
            for (int i = 0; i<Labyrinth.GetLength(0); i++) //просмотрим первый и последний столбцы
            {
                if (Labyrinth[i, 0] != "X")
                {
                    int k = int.Parse(Labyrinth[i, 0]);
                    if (d[k] <= MinWay && d[k] != 0)
                    {
                        MinWay = d[k];
                        n0 = k;
                    }
                }
                if (Labyrinth[i, Labyrinth.GetLength(1) - 1] != "X")
                {
                    int k = int.Parse(Labyrinth[i, Labyrinth.GetLength(1) - 1]);
                    if (d[k] <= MinWay && d[k] != 0)
                    {
                        MinWay = d[k];
                        n0 = k;
                    }
                }
            }
            for (int j = 0; j < Labyrinth.GetLength(1); j++) //просмотрим первую и последнюю строчки
            {
                if (Labyrinth[0, j] != "X")
                {
                    int k = int.Parse(Labyrinth[0, j]);
                    if (d[k] <= MinWay && d[k] != 0)
                    {
                        MinWay = d[k];
                        n0 = k;
                    }
                }
                if (Labyrinth[Labyrinth.GetLength(0) - 1, j] != "X")
                {
                    int k = int.Parse(Labyrinth[Labyrinth.GetLength(0) - 1, j]);
                    if (d[k] <= MinWay && d[k] != 0)
                    {
                        MinWay = d[k];
                        n0 = k;
                    }
                }
            }
            //выводим ответ
            int Ans;
            if (I == 0 || J == 0 || I == Labyrinth.GetLength(0) - 1 || J == Labyrinth.GetLength(1) - 1)
            {
                Ans = 1;
                Console.WriteLine("Shortest way out - " + Ans);
            }
            else if (MinWay == int.MaxValue)
            {
                Console.WriteLine("C’est la vie...");
                Ans = -1;
            }
            else
            {
                Ans = MinWay + 1;
                Console.WriteLine("Shortest way out - " + Ans);
            }
            //восстановим путь
            int cnt = 0;
            const int Size = 10000; //Величина, больше которой длина пути быть не может (теоретически)
            int[] Path = new int[Size];
            if (used[n0])
            {
                for (int v = n0; v != -1; v = p[v])
                {
                    Path[cnt] = v;
                    cnt++;
                }
                for (int j = cnt-1; j >= 0; j--)
                {
                    for (int k = 0; k<Labyrinth.GetLength(0); k++)
                        for(int m = 0; m<Labyrinth.GetLength(1); m++)
                        {
                            if (Labyrinth[k, m] == Path[j].ToString())
                            {
                                Console.Write("({0},{1}) ",k, m);
                            }
                        }
                }
            }
            Console.WriteLine();
            //визуализация
            if (Ans > 1)
            {
                int k = cnt - 1;
                while (Ans > 0)
                {
                    for (int i = 0; i < Labyrinth.GetLength(0); i++) //выведем лабиринт
                    {
                        for (int j = 0; j < Labyrinth.GetLength(1); j++)
                        {

                            if (Labyrinth[i, j] == Path[k].ToString())
                            {
                                Console.Write("*" + " ");
                            }
                            else if (Labyrinth[i, j] != "X")
                            {
                                Console.Write("O" + " ");
                            }
                            else
                            {
                                Console.Write(Labyrinth[i, j] + " ");
                            }
                        }
                        Console.WriteLine();
                    }
                    for (int i = 0; i<Labyrinth.GetLength(1); i++)
                    {
                        Console.Write("- ");
                    }
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(1000); //one second
                    Ans--;
                    k--;
                }
            }
            else
            {
                for (int i = 0; i < Labyrinth.GetLength(0); i++) //выведем лабиринт
                {
                    for (int j = 0; j < Labyrinth.GetLength(1); j++)
                    {

                        if (i == I && j == J)
                        {
                            Console.Write("*" + " ");
                        }
                        else if (Labyrinth[i, j] != "X")
                        {
                            Console.Write("O" + " ");
                        }
                        else
                        {
                            Console.Write(Labyrinth[i, j] + " ");
                        }
                    }
                    Console.WriteLine();
                }
            }

        }
    }
}
