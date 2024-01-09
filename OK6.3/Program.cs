/*Napisz program, który pobierze od użytkownika w pierwszym wierszu ilość maszyn oraz zadań, w kolejnym wierszu pobierze czasy zadań.

Wynikiem działania programu ma być uszeregowanie zadań za pomocą algorytmu McNaughtona.

Każdy wiersz we wyjściu jest odpowiednikiem maszyny mimi​, każdy opis zadania zawiera numer zadania oraz czas trwania.

Zadania szeregujemy malejąco po labelach.*/

using System;
using System.Collections.Generic;
using System.Linq;

public class MainClass
{
    public struct Task
    {
        public int Id { get; set; }
        public int Time { get; set; }
    }
    public struct Machine
    {
        public int TimeInWork { get; set; }
        public List<Task> Tasks { get; set; }
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
        //Sorting tasks by descending time
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < n; i++)
        {
            tasks.Add(new Task
            {
                Id = i,
                Time = times[i]
            });
        }
        var sortedTasks = tasks.OrderByDescending(x => x.Time).ToList();
        var machines = new Machine[m]; for (int i = 0; i < m; i++)
        {
            machines[i] = new Machine()
            {
                TimeInWork = 0,
                Tasks = new List<Task>(),
            };
        }
        while (sortedTasks.Any())
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
            machines[minMachine].TimeInWork += sortedTasks[0].Time;
            machines[minMachine].Tasks.Add(new Task
            {
                Id = sortedTasks[0].Id,
                Time = sortedTasks[0].Time
            });
            sortedTasks.RemoveAt(0);
        }
        return machines;
    }
    public static void PrintOutput(Machine[] machines)
    {
        var lastMachine = machines.Last();
        foreach (var machine in machines)
        {
            if (machine.Tasks.Any())
            {
                var lastTask = machine.Tasks.Last();
                foreach (var task in machine.Tasks)
                {
                    Console.Write((task.Id + 1) + ":" + task.Time);
                    if (!task.Equals(lastTask)) Console.Write(" ");
                }
                if (!machine.Equals(lastMachine)) Console.WriteLine();
            }

        }
    }
}