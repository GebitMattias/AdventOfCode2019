using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode1
{
    class Program
    {
        static void Main(string[] args)
        {
            using var fs = File.OpenRead("Input.txt");
            using var sr = new StreamReader(fs);

            var modules = new List<Module>();

            var line = sr.ReadLine();

            while (line != null)
            {
                if (int.TryParse(line, out var weight))
                {
                    modules.Add(new Module(weight));
                }
                
                line = sr.ReadLine();
            }
            
            var result = modules.Sum(x => x.GetFuel());

            Console.WriteLine(result);
        }
    }

    public class Module
    {
        public int Weight { get; }

        public int GetFuel()
        {
            var fuel = Math.Max(0, Weight / 3 - 2);
            return fuel + GetFuel(fuel);
        }

        private int GetFuel(int fuel)
        {
            if (fuel <= 0) return 0;

            var fuel1 = Math.Max(0, fuel / 3 - 2);
            return fuel1 + GetFuel(fuel1);
        }

        public Module(int weight)
        {
            Weight = weight;
        }
    }
}
