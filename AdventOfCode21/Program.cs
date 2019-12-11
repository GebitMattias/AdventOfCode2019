using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace AdventOfCode2
{
    /// <summary>
    /// Day2 Puzzle1
    /// Author: Kevin
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            using var fs = File.OpenRead("Input.txt");
            using var sr = new StreamReader(fs);

            var pgm = new ElfProgram();

            pgm.Values.AddRange(sr.ReadToEnd().Split(",").Select(int.Parse));
            pgm.Values[1] = 12;
            pgm.Values[2] = 2;

            Operation operation;
            while ((operation = pgm.GetNextOperation()).OpCode != 99)
            {
                if (operation.OpCode == 1)
                    pgm.Values[operation.Pos3] = pgm.Values[operation.Pos1] + pgm.Values[operation.Pos2];
                else if (operation.OpCode == 2)
                    pgm.Values[operation.Pos3] = pgm.Values[operation.Pos1] * pgm.Values[operation.Pos2];
                else
                    throw new Exception();
            }

            var result = pgm.Values[0];
            
            Console.WriteLine(result);
        }
    }

    public class Operation
    {
        public int OpCode;
        public int Pos1, Pos2, Pos3;
    }

    public class ElfProgram
    {
        public List<int> Values { get; } = new  List<int>();
        private int pos = 0;

        public Operation GetNextOperation()
        {
            var result = new Operation()
            {
                OpCode = Values[pos], Pos1 = Values[pos+1], Pos2 = Values[pos+2], Pos3 = Values[pos+3]
            };
            pos += 4;
            return result;
        }
    }
}