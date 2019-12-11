using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode1Trampolines;

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
            var fuel = CalculateFuel(Weight);
            // Mit Trampoline berechnen. Dadurch wird Tail-Recursion simuliert und der Stack wächst nicht so stark
            return Trampoline.Start(BounceFuel, fuel, CalculateFuel(fuel));
        }

        private static int CalculateFuel(int weight)
        {
            return Math.Max(0, weight / 3 - 2);
        }

        private static Trampoline.Bounce<int, int, int> BounceFuel(int currentFuel, int additionalFuel)
        {
            if (additionalFuel == 0)
                return Trampoline.Bounce<int, int, int>.End(currentFuel);
            else
                return Trampoline.Bounce<int, int, int>.Continue(currentFuel + additionalFuel,
                    CalculateFuel(additionalFuel));
        }

        public Module(int weight)
        {
            Weight = weight;
        }
    }
}
