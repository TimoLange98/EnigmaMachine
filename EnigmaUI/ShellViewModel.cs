using Caliburn.Micro;
using System.Reactive.Linq;
using EnigmaClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnigmaUI
{
    internal class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel()
        {
           

            //ComboBoxItems = GetComboBoxItems();

            //RotorPaths = Directory.GetFiles(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
            //                                            "\\EnigmaClassLibrary\\Parts").Where(f => f.Contains("Rotor")).ToList();

            //ReflectorPaths = Directory.GetFiles(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
            //                                            "\\EnigmaClassLibrary\\Parts").Where(f => f.Contains("Reflector")).ToList();

            //PlugboardPaths = Directory.GetFiles(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
            //                                            "\\EnigmaClassLibrary\\Parts").Where(f => f.Contains("Plugboard")).ToList();
        }
        //
        //Frontend properties and methods 
        //
        public string HeaderLabelContent { get; set; } = "Enigma";
        public string LeftRotorLabelText { get; set; } = "Left Rotor";
        public string MiddleRotorLabelText { get; set; } = "Middle Rotor";
        public string RightRotorLabelText { get; set; } = "Right Rotor";
        public string ReflectorLabelText { get; set; } = "Reflector";
        public string PlugboardLabelText { get; set; } = "Plugboard";
        public string ApplyButtonContent { get; set; } = "Apply";
        public string FastInitButtonContent { get; set; } = "Fast init";

        public string ArrowUp { get; set; } = "↑";
        public string ArrowDown { get; set; } = "↓";
        public string ArrowRight { get; set; } = "→";

        public List<string> RotorPaths { get; set; } 
        public List<string> ReflectorPaths { get; set; } 
        public List<string> PlugboardPaths { get; set; }


        public List<ComboBoxItem> ComboBoxItems { get; set; }


        private List<ComboBoxItem> GetComboBoxItems()
        {
            var result = new List<ComboBoxItem>();

            foreach (var path in RotorPaths)
            {
                var text = Path.GetFileNameWithoutExtension(path);
                text = text.Replace($"{text.Last()}", $" {text.Last()}");

                result.Add(new ComboBoxItem() { Text = text, Value = path });
            }

            return result;
        }

        //
        //Backend properties and methods
        //
        public readonly char[] Alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

        public string LeftRotorPath;
        public string MiddleRotorPath;
        public string RightRotorPath;
        public string ReflectorPath;
        public string PlugboardPath;

        public int LeftRotorState { get; set; } = 1;
        public int MiddleRotorState { get; set; } = 1;
        public int RightRotorState { get; set; } = 1;

        public int LeftRotorStateAfterApplied;
        public int MiddleRotorStateAfterApplied;
        public int RightRotorStateAfterApplied;

        public Machine Machine;

        public bool AllPartsSelected;
        public bool Startable;
    }
}
