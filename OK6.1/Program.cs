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
        public int AvailableTime { get; set; }
        public Dictionary<int, int> TaskTimes { get; internal set; }

    }
    public static void Main()
    {
        //Read input
        var line1 = Console.ReadLine();
        var split1 = line1.Split(' ');
        var m = int.Parse(split1[0]);
        var n = int.Parse(split1[1]);

        var line2 = Console.ReadLine();
        var times = Array.ConvertAll(line2.Split(' '), s => int.Parse(s));

        var machines = McNaughtonAlgorithm(m, n, times);
        PrintOutput(machines);

    }
    public static Machine[] McNaughtonAlgorithm(int m, int n, int[] times)
    {

        //Count optimal Cmax
        var sum = times.Sum();
        int Cmax = (int)Math.Ceiling((double)sum / m);

        var tasksLeft = n;
        var machines = new Machine[m];
        for (int i = 0; i < m; i++)
        {
            machines[i] = new Machine()
            {
                AvailableTime = Cmax,
                TaskTimes = new Dictionary<int, int>(),
            };
        }

        for (int i = 0; i < m; i++)
        {
            while (tasksLeft > 0 && machines[i].AvailableTime != 0)
            {
                for (int j = 0; j < n; j++)
                {
                    if (times[j] != 0)
                    {
                        if (times[j] <= machines[i].AvailableTime)
                        {
                            machines[i].TaskTimes.Add(j, times[j]);
                            machines[i].AvailableTime = machines[i].AvailableTime - times[j];
                            times[j] = 0;
                            tasksLeft--;
                        }
                        else
                        {
                            machines[i].TaskTimes.Add(j, machines[i].AvailableTime);
                            times[j] = times[j] - machines[i].AvailableTime;
                            machines[i].AvailableTime = 0;
                        }
                        if (machines[i].AvailableTime == 0) break;
                    }
                }
            }
        }
        return machines;
    }
    public static void PrintOutput(Machine[] machines)
    {
        var lastMachine = machines.Last();
        foreach (var machine in machines)
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