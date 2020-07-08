using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnigmaClassLibrary
{
    public class Machine
    {
        readonly char[] Alphabet = Enumerable.Range('A', 26).Select(c => (char)c).ToArray();

        private readonly Rotor LeftRotor;
        private readonly Rotor MiddleRotor;
        private readonly Rotor RightRotor;

        private readonly Reflector Reflector;

        private readonly Plugboard Plugboard;

        private int LeftRotorRotations;
        private int MiddleRotorRotations;
        private int RightRotorRotations;


        public Machine(string LeftRotorPath, int LeftRotorState, string MiddleRotorPath, int MiddleRotorState,
            string RightRotorPath, int RightRotorState, string ReflectorPath, string PlugboardPath)
        {
            LeftRotor = new Rotor(GetConnections(LeftRotorPath));
            LeftRotor.Rotate(LeftRotorState);

            MiddleRotor = new Rotor(GetConnections(MiddleRotorPath));
            MiddleRotor.Rotate(MiddleRotorState);

            RightRotor = new Rotor(GetConnections(RightRotorPath));
            RightRotor.Rotate(RightRotorState);


            Reflector = new Reflector(GetConnections(ReflectorPath));

            Plugboard = new Plugboard(GetConnections(PlugboardPath));

            LeftRotorRotations = LeftRotorState;
            MiddleRotorRotations = MiddleRotorState;
            RightRotorRotations = RightRotorState;
        }

        public (char, int, int, int) ProcessChar(char input)
        {
            char output;

            if (char.IsLetter(input))
                output = Process(input);
            else
                output = input;

            return (output, LeftRotorRotations, MiddleRotorRotations, RightRotorRotations);
        }

        public (string, int, int, int) ProcessText(string input)
        {
            var output = "";

            foreach (var character in input.ToUpper().ToCharArray())
            {
                if (char.IsLetter(character))
                    output += Process(character);
                else
                    output += character;
            }
            return (output, LeftRotorRotations, MiddleRotorRotations, RightRotorRotations);
        }

        private char Process(char inputCharacter)
        {
            var newIndex = Array.FindIndex(Alphabet, c => c == inputCharacter);

            newIndex = Plugboard.Process(newIndex);
            newIndex = LeftRotor.Process(newIndex);
            newIndex = MiddleRotor.Process(newIndex);
            newIndex = RightRotor.Process(newIndex);
            newIndex = Reflector.Process(newIndex);
            newIndex = RightRotor.Process(newIndex, true);
            newIndex = MiddleRotor.Process(newIndex, true);
            newIndex = LeftRotor.Process(newIndex, true);
            newIndex = Plugboard.Process(newIndex);

            LeftRotor.Rotate();
            LeftRotorRotations++;

            if (LeftRotorRotations >= 27)
            {
                MiddleRotor.Rotate();
                MiddleRotorRotations++;

                LeftRotorRotations = 0;

                if (MiddleRotorRotations >= 27)
                {
                    RightRotor.Rotate();
                    RightRotorRotations++;

                    MiddleRotorRotations = 0;

                    if (RightRotorRotations >= 27)
                    {
                        RightRotorRotations = 0;
                    }
                }
            }

            return Alphabet[newIndex];
        }

        private List<int[]> GetConnections(string path)
        {
            var connections = new List<int[]>();

            foreach (var connection in File.ReadAllLines(path))
            {
                var splitted = connection.Trim().Split(',').Select(int.Parse).ToArray();
                connections.Add(new int[] { splitted[0], splitted[1] });
            }
            return connections;
        }
    }
}
