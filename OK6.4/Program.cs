/*Napisz program, który pobierze od użytkownika w pierwszym wierszu ilość maszyn oraz zadań, w kolejnym wierszu podajemy "ujście" drzewa zależności, a następnie w kolejnych ii-tych wierszach wstawiane będą krawędzie digrafu (a,b)(a,b), gdzie aa oznacza numer wierzchnołka, a bb oznacza czas wykonywania zadania.

Wynikiem działania programu ma być uszeregowanie zadań za pomocą algorytmu HU

Każdy wiersz we wyjściu jest odpowiednikiem maszyny mimi​, każdy opis zadania zawiera numer zadania.

Czas trwania każdego zadania wynosi 1.

Zadania wyświetlamy rosnąco po labelach. To oznacza że zadanie 1 oraz 2 o czasach odpowiednio 2,2 będą miały kolejność 1 zadanie, następnie 2 zadanie.*/

using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{

    public struct Pair
    {
        public int V { get; set; }
        public int U { get; set; }

    }
    public static void Main()
    {
        /*        var m = 3;
                var n = 12;
                var pairs = new List<Pair>() 
                {
                    new Pair{ V=1, U=7,},
                    new Pair{ V=2, U=7,},
                    new Pair{ V=3, U=8,},
                    new Pair{ V=4, U=9,},
                    new Pair{ V=5, U=9,},
                    new Pair{ V=6, U=10,},
                    new Pair{ V=7, U=10,},
                    new Pair{ V=8, U=11,},
                    new Pair{ V=9, U=11,},
                    new Pair{ V=10, U=12,},
                    new Pair{ V=11, U=12,},
                    new Pair{ V=12},
                };*/



        //Read input
        var line1 = Console.ReadLine();
        var line2 = Console.ReadLine();
        var split1 = line1.Split(' ');
        var m = int.Parse(split1[0]);
        var n = int.Parse(split1[1]);
        var pairs = new List<Pair>();
        var ujscie = int.Parse(line2);
        for (int i = 0; i < n; i++)
        {

            if (i == n - 1)
            {
                pairs.Add(new Pair
                {
                    V = int.Parse(Console.ReadLine().Trim(':'))
                });
            }
            else
            {
                var splited = Console.ReadLine().Split(": ");
                pairs.Add(new Pair
                {
                    V = int.Parse(splited[0]),
                    U = int.Parse(splited[1]),
                });
            }

        }
        var x = HuAlgorithm(pairs, m, n, n);
        PrintOutput(x);


    }
    public static List<int>[] HuAlgorithm(List<Pair> pairs, int m, int n, int ujscie)
    {

        var machines = new List<int>[m];
        for (int i = 0; i < m; i++)
        {
            machines[i] = new List<int>();
        }
        while (pairs.Count > 0)
        {
            var Us = pairs.Select(x => x.U).ToList();
            var Vs = pairs.Select(x => x.V).ToList();
            var availableTasks = Vs.Except(Us).ToList();
            var pairsToRemove = new List<Pair>();
            for (int i = 0; i < m; i++)
            {
                if (availableTasks.Any())
                {
                    var pairToRemove = pairs.FirstOrDefault(x => x.V == availableTasks[0]);
                    machines[i].Add(availableTasks[0]);
                    availableTasks.RemoveAt(0);
                    pairs.Remove(pairToRemove);
                    if (pairs.Count > 1)
                    {
                        Vs.RemoveAt(0);
                        Us.RemoveAt(0);
                    }

                }

            }

        }
        return machines;
    }

    public static void PrintOutput(List<int>[] machines)
    {
        var lastMachine = machines.Last();
        foreach (var machine in machines)
        {
            if (machine.Any())
            {
                var lastTask = machine.Last();
                foreach (var task in machine)
                {
                    Console.Write((task) + ":" + 1);
                    if (!task.Equals(lastTask)) Console.Write(" ");
                }
                if (!machine.Equals(lastMachine)) Console.WriteLine();
            }

        }
    }
}