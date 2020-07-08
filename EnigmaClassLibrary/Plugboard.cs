using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnigmaClassLibrary
{
    public class Plugboard
    {
        private readonly List<int[]> Connections;

        public Plugboard(List<int[]> Connections)
            => this.Connections = Connections;

        public int Process(int input)
        {
            var connection = Connections.Find(c => c[0] == input);
            return connection == default ? input : connection[1];
        }

        public static void Create(string name)
        {
            Random r = new Random();
            var numbers = Enumerable.Range(0, 26).ToList();

            using (var writer = new StreamWriter($@"{new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}\EnigmaClassLibrary\Parts\{name}.txt"))
            {
                var pairs = r.Next(0, 13);
                while (pairs > 0)
                {
                    var first = numbers[r.Next(0, numbers.Count)];
                    numbers.Remove(first);
                    var second = numbers[r.Next(0, numbers.Count)];
                    numbers.Remove(second);

                    writer.WriteLine($"{first},{second}");
                    writer.WriteLine($"{second},{first}");

                    pairs--;
                }
            }
        }
    }
}