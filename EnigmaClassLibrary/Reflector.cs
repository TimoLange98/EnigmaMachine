using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnigmaClassLibrary
{
    public class Reflector
    {
        private readonly List<int[]> Connections;

        public Reflector(List<int[]> Connections)
            => this.Connections = Connections;

        public int Process(int index)
            => Connections.Find(con => con[0] == index)[1];

        //Creates a random connection-pattern and stores it in a textfile
        public static void Create(string name)
        {
            var r = new Random();

            var numbers = Enumerable.Range(0, 26).OrderBy(n => r.Next()).ToList();

            using (var writer = new StreamWriter($@"{new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}\EnigmaClassLibrary\Parts\{name}.txt"))
            {
                while (numbers.Count != 0)
                {
                    var first = numbers[0];
                    var second = numbers[1];

                    writer.WriteLine($"{first},{second}");
                    writer.WriteLine($"{second},{first}");

                    numbers.Remove(first);
                    numbers.Remove(second);
                }
            }
        }
    }
}