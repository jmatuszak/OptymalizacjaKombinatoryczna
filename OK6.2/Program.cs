/*Napisz program, który pobierze od użytkownika w pierwszym wierszu ilość maszyn oraz zadań, w kolejnym wierszu pobierze czasy zadań.

Wynikiem działania programu ma być uszeregowanie zadań za pomocą algorytmu McNaughtona.

Każdy wiersz we wyjściu jest odpowiednikiem maszyny mimi​, każdy opis zadania zawiera numer zadania oraz czas trwania.

Zadania szeregujemy malejąco po labelach.*/

using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{

    public struct Machine
    {
        public int TimeInWork { get; set; }
        public Dictionary<int, int> TaskTimes { get; internal set; }
    }
    public static void Main()
    {
        /*        var line1 = "3 5";
                var line2 = "2 2 1 1 3";*/
        //Read input
        var line1 = Console.ReadLine();
        var line2 = Console.ReadLine();
        var split1 = line1.Split(' ');
        var m = int.Parse(split1[0]);
        var n = int.Parse(split1[1]);

        var times = Array.ConvertAll(line2.Split(' '), s => int.Parse(s));

        var machines = LS(m, n, times);
        PrintOutput(machines);

    }
    public static Machine[] LS(int m, int n, int[] times)
    {
        var machines = new Machine[m];
        for (int i = 0; i < m; i++)
        {
            machines[i] = new Machine()
            {
                TimeInWork = 0,
                TaskTimes = new Dictionary<int, int>()
            };
        }
        var tasksLeft = new List<int>();
        for (int i = 0; i < n; i++)
        {
            tasksLeft.Add(i);
        }

        while (tasksLeft.Any())
        {
            var minTime = int.MaxValue;
            int minMachine = -1;
            //Finding available machine 
            for (int i = 0; i < m; i++)
            {
                if (machines[i].TimeInWork < minTime)
                {
                    minTime = machines[i].TimeInWork;
                    minMachine = i;
                }
            }
            machines[minMachine].TimeInWork += times[tasksLeft[0]];
            machines[minMachine].TaskTimes.Add(tasksLeft[0], times[tasksLeft[0]]);
            tasksLeft.RemoveAt(0);
        }
        return machines;
    }
    public static void PrintOutput(Machine[] machines)
    {
        var lastMachine = machines.Last();
        foreach (var machine in machines)
        {
            if (machine.TaskTimes.Any())
            {
                var lastTask = machine.TaskTimes.Last();
                foreach (var task in machine.TaskTimes)
                {
                    Console.Write((task.Key + 1) + ":" + task.Value);
                    if (!task.Equals(lastTask)) Console.Write(" ");
                }
                if (!machine.Equals(lastMachine)) Console.WriteLine();
            }

        }
    }
}