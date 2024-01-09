using System;
using System.Linq;
using System.Collections.Generic;

public class MainClass
{
    public struct Task
    {
        public char Name;
        public int Label;
        public List<char> Successors;
        public List<int> SuccessorsLabels;
    };
    public static void Main()
    {
        var names = ReadNames();
        var dependencies = ReadDependencies();
        //var m = int.Parse(Console.ReadLine());
        var m = 2;


        var tasks = CreateTaskList(names, dependencies);
        var order = CoffmanGraham(tasks, m);
        //GenerateOutput(dependencies1, order, m);
        GenerateGraphicOutput(dependencies, order, m);

    }

    static List<char> CoffmanGraham(List<Task> tasks, int m)
    {
        var order = new List<char>();
        var result = new List<(char, int)>();
        for (int i = 1; i <= tasks.Count; i++)
        {
            //Tworzenie Listy A
            var A = new List<char>();
            foreach (var task in tasks)
            {
                if (task.Successors.Count == task.SuccessorsLabels.Count && task.Label == 0)
                    A.Add(task.Name);
            }
            //Wybranie z listy A zadania do nadania Etykiety (lexiSort)
            var name = FindTaskToLabel(tasks, A);
            //przpisanie etykiety do zadania

            for (int j = 0; j < tasks.Count; j++)
            {
                if (tasks[j].Name.Equals(name))
                {
                    {
                        Task task = tasks[j];
                        task.Label = i;
                        tasks[j] = task;
                    }
                }
            }
            //Dodanie SuccessorLabels Update
            foreach (var task in tasks)
            {
                if (task.Successors.Contains(name))
                {
                    task.SuccessorsLabels.Add(i);
                }
            }
            order.Add(name);
        }
        return order;
    }
    static void GenerateGraphicOutput(List<(char, char)> dependencies, List<char> order, int m)
    {
        //Przypisanie zadań do maszyn
        var result = CreateResult(dependencies, order, m);
        List<(char, int)>[] machines = new List<(char, int)>[m];
        for (int i = 0; i < m; i++)
        {
            machines[i] = new List<(char, int)>();
        }
        var cMax = result.Last().Item2 + 1;
        var t = 0;
        while (result.Count > 0)
        {
            for (int i = 0; i < m; i++)
            {
                if (result.Count == 0) break;
                if (result[0].Item2 == t)
                {
                    machines[i].Add(result[0]);
                    result.RemoveAt(0);
                }
            }
            t++;
        }
        //Wypisanie

        Console.WriteLine("Cmax = " + cMax);

        for (int i = 0; i < m; i++)
        {
            t = 0;
            string text = "M" + i;
            int targetWidth = 3;
            int padding = targetWidth - text.Length;
            int leftPadding = padding / 2;
            int rightPadding = padding - leftPadding;
            string paddedText = text.PadLeft(text.Length + leftPadding).PadRight(targetWidth);
            Console.Write(paddedText + "|");
            while (t != machines[i].ElementAt(0).Item2)
            {
                text = " ";
                padding = targetWidth - text.Length;
                leftPadding = padding / 2;
                rightPadding = padding - leftPadding;
                paddedText = text.PadLeft(text.Length + leftPadding).PadRight(targetWidth);
                Console.Write(paddedText + "|");
                t++;
            }
            foreach (var task in machines[i])
            {
                text = task.Item1.ToString();
                padding = targetWidth - text.Length;
                leftPadding = padding / 2;
                rightPadding = padding - leftPadding;
                paddedText = text.PadLeft(text.Length + leftPadding).PadRight(targetWidth);
                Console.Write(paddedText + "|");
                t++;
            }
            Console.WriteLine();
        }
    }
    static void GenerateOutput(List<(char, char)> dependencies, List<char> order, int m)
    {
        var result = CreateResult(dependencies, order, m);
        result.Sort((tuple1, tuple2) => tuple1.Item1.CompareTo(tuple2.Item1));
        foreach (var item in result)
        {
            if (!item.Equals(result.Last()))
            {
                Console.Write("\'" + item.Item1 + "\'" + ": " + item.Item2 + ", ");
            }
            else
            {
                Console.Write("\'" + item.Item1 + "\'" + ": " + item.Item2);
            }
        }
    }
    static List<(char, int)> CreateResult(List<(char, char)> dependencies, List<char> order, int m)
    {
        var result = new List<(char, int)>();
        int t = 0;
        while (order.Count > 0)
        {
            var removed = new List<char>();
            for (int i = 0; i < m; i++)
            {
                int count = 0;
                foreach ((char item1, char item2) in dependencies)
                {
                    if (item2 == order.Last()) count++;
                }
                if (count == 0)
                {
                    result.Add((order.Last(), t));
                    removed.Add(order.Last());
                    order.RemoveAt(order.Count - 1);
                    if (order.Count == 0) break;
                }
            }
            dependencies.RemoveAll(a => removed.Contains(a.Item1));

            t++;
        }
        return result;
    }
    static char FindTaskToLabel(List<Task> tasks, List<char> A)
    {
        tasks.ForEach(task => task.SuccessorsLabels.OrderByDescending(x => x));

        char name = A[0];
        var tempList = new List<List<int>>();
        foreach (var a in A)
        {
            var tempTask = tasks.FirstOrDefault(x => x.Name == a);
            tempList.Add(tempTask.SuccessorsLabels);
        }
        var sorted = LexicographicSort(tempList);
        var task = tasks.FirstOrDefault(a => a.SuccessorsLabels.Equals(sorted[0]));

        return task.Name;
    }

    static List<List<int>> LexicographicSort(List<List<int>> SuccesorsLabels)
    {
        for (int i = 0; i < SuccesorsLabels.Count; i++)
        {
            SuccesorsLabels[i].Sort((x, y) => y.CompareTo(x));
        }
        SuccesorsLabels.Sort((list1, list2) =>
        {
            for (int i = 0; i < list1.Count; i++)
            {
                if (i >= list2.Count)
                    return 1;
                int comparisonResult = list1[i].CompareTo(list2[i]);
                if (comparisonResult != 0)
                    return comparisonResult;
            }
            return list1.Count.CompareTo(list2.Count);
        });
        /*foreach (List<int> list in SuccesorsLabels)
		{
			Console.WriteLine(string.Join(", ", list));
		}*/
        return SuccesorsLabels;
    }

    static List<Task> CreateTaskList(string names, List<(char, char)> dependencies)
    {
        var tasks = new List<Task>();
        foreach (var c in names)
        {
            var task = new Task();
            task.Name = c;
            task.Label = 0;
            task.Successors = new List<char>();
            task.SuccessorsLabels = new List<int>();
            foreach ((char item1, char item2) in dependencies)
            {
                if (item1 == c) task.Successors.Add(item2);
            }
            tasks.Add(task);
        }
        return tasks;
    }



    static string ReadNames()
    {
        var txt = "'A', 'B', 'C', 'D', 'E', 'F'";
        //var txt = Console.ReadLine();
        txt = txt.Replace("\'", "").Replace(" ", "").Replace(",", "");

        return txt;
    }
    static List<(char, char)> ReadDependencies()
    {
        var txt = "('A', 'B'), ('B', 'C'), ('C', 'D'), ('A', 'E'), ('E', 'F')";
        //var txt = Console.ReadLine();
        txt = txt.Replace("(", "").Replace(" ", "").Replace("\'", "");
        var pairs = txt.Split("),").ToList();
        pairs.Select(s => s.Replace(")", "").Replace(",", ""));
        var dependencies = new List<(char, char)>();
        foreach (var pair in pairs)
        {
            dependencies.Add((pair[0], pair[2]));
        }
        return dependencies;
    }
}