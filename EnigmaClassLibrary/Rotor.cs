using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnigmaClassLibrary
{
    public class Rotor
    {
        public readonly List<int[]> Wheel;

        public Rotor(List<int[]> Wheel)
            => this.Wheel = Wheel.ToList();


        public int Process(int index, bool reflected = false)
            => !reflected ? Wheel.Find(connection => connection[0] == index)[1] :
                            Wheel.Find(connection => connection[1] == index)[0];


        public void Rotate(int times = 1)
        {
            foreach (var connection in Wheel)
            {
                if (connection[0] + 1 > Wheel.Count - 1)
                    connection[0] = 0;
                else
                    connection[0]++;

                if (connection[1] + 1 > Wheel.Count - 1)
                    connection[1] = 0;
                else
                    connection[1]++;
            }
            times--;
            if (times > 0)
                Rotate(times);
        }

        public static void CreateWheel(string name)
        {
            var r = new Random();
            var numbers = Enumerable.Range(0, 26).OrderBy(n => r.Next()).ToList();

            using (var writer = new StreamWriter($@"{new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName}\EnigmaClassLibrary\Parts\{name}.txt"))
                for (int i = 0; i < 26; i++)
                {
                    writer.WriteLine($"{i},{numbers[i]}");
                }
        }
    }
}
