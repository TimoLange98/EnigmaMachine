using Caliburn.Micro;
using System.Reactive.Linq;
using EnigmaClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace EnigmaUI
{
    internal class ShellViewModel : PropertyChangedBase
    {
        public ShellViewModel()
        {
            LoadParts();

            this.HasChanged(() => SelectedLeftRotor)
                .Merge(this.HasChanged(() => SelectedMiddleRotor))
                .Merge(this.HasChanged(() => SelectedRightRotor))
                .Merge(this.HasChanged(() => SelectedReflector))
                .Merge(this.HasChanged(() => SelectedPlugboard))
                .Subscribe(_ => CheckAllParts());
        }

        private void CheckAllParts()
        {
            AllPartsSelected = (SelectedLeftRotor != null && SelectedMiddleRotor != null && SelectedRightRotor != null
                && SelectedReflector != null && SelectedPlugboard != null);

            if (Applied && (SelectedLeftRotorAfterApplied != SelectedLeftRotor
                || SelectedMiddleRotorAfterApplied != SelectedMiddleRotor
                || SelectedRightRotor != SelectedRightRotorAfterApplied
                || SelectedReflectorAfterApplied != SelectedReflector
                || SelectedPlugboardAfterApplied != SelectedPlugboard))
            {
                NeedToApply = true;
                Applied = false;
            }
        }

        //
        //Frontend properties
        //
        public string Output { get; set; } = "";
        public bool TextBoxReadonly { get; set; } = true;

        //
        //Backend properties and methods
        //
        public List<ComboBoxItem> RotorComboBoxItems { get; set; }
        public List<ComboBoxItem> ReflectorComboBoxItems { get; set; }
        public List<ComboBoxItem> PlugboardComboBoxItems { get; set; }

        
        public readonly char[] Alphabet = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();

        public ComboBoxItem SelectedLeftRotor { get; set; }
        public ComboBoxItem SelectedMiddleRotor { get; set; }
        public ComboBoxItem SelectedRightRotor { get; set; }
        public ComboBoxItem SelectedReflector { get; set; }
        public ComboBoxItem SelectedPlugboard { get; set; }

        public ComboBoxItem SelectedLeftRotorAfterApplied { get; set; }
        public ComboBoxItem SelectedMiddleRotorAfterApplied { get; set; }
        public ComboBoxItem SelectedRightRotorAfterApplied { get; set; }
        public ComboBoxItem SelectedReflectorAfterApplied { get; set; }
        public ComboBoxItem SelectedPlugboardAfterApplied { get; set; }

        public int LeftRotorState { get; set; } = 1;
        public int MiddleRotorState { get; set; } = 1;
        public int RightRotorState { get; set; } = 1;


        public int LeftRotorStateAfterApplied { get; set; }
        public int MiddleRotorStateAfterApplied { get; set; }
        public int RightRotorStateAfterApplied { get; set; }

        public Machine Machine { get; set; }

        public bool AllPartsSelected { get; set; } = false;
        public bool NeedToApply { get; set; } = false;
        public bool Applied { get; set; } = false;
        public bool NotStarted { get; set; } = true;

        public void LoadParts()
        {

            var rotorPaths = Directory.GetFiles(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                                                        "\\EnigmaClassLibrary\\Parts").Where(f => f.Contains("Rotor")).ToList();

            RotorComboBoxItems = LoadComboBoxItems(rotorPaths);

            var reflectorPaths = Directory.GetFiles(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                                                        "\\EnigmaClassLibrary\\Parts").Where(f => f.Contains("Reflector")).ToList();

            ReflectorComboBoxItems = LoadComboBoxItems(reflectorPaths);

            var plugboardPaths = Directory.GetFiles(new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName +
                                                        "\\EnigmaClassLibrary\\Parts").Where(f => f.Contains("Plugboard")).ToList();

            PlugboardComboBoxItems = LoadComboBoxItems(plugboardPaths);
        }

        private List<ComboBoxItem> LoadComboBoxItems(List<string> paths)
        {
            var result = new List<ComboBoxItem>();

            foreach (var path in paths)
            {
                var text = Path.GetFileNameWithoutExtension(path);
                text = text.Replace($"{text.Last()}", $" {text.Last()}");

                result.Add(new ComboBoxItem() { Text = text, Value = path });
            }

            return result;
        }

        public void LeftRotorStateUp()
        {
            if (SelectedLeftRotor == null)
            {
                return;
            }

            if (LeftRotorState == 26)
            {
                LeftRotorState = 1;
            }
            else
            {
                LeftRotorState++;
            }

            if (Applied == true && LeftRotorStateAfterApplied != LeftRotorState)
            {
                NeedToApply = true;
                Applied = false;
            }
        }
        public void LeftRotorStateDown()
        {
            if (SelectedLeftRotor == null)
            {
                return;
            }

            if (LeftRotorState == 1)
            {
                LeftRotorState = 26;
            }
            else
            {
                LeftRotorState--;
            }

            if (Applied == true && LeftRotorStateAfterApplied != LeftRotorState)
            {
                NeedToApply = true;
                Applied = false;
            }
        }

        public void MiddleRotorStateUp()
        {
            if (SelectedMiddleRotor == null)
            {
                return;
            }

            if (MiddleRotorState == 26)
            {
                MiddleRotorState = 1;
            }
            else
            {
                MiddleRotorState++;
            }

            if (Applied == true && MiddleRotorStateAfterApplied != MiddleRotorState)
            {
                NeedToApply = true;
                Applied = false;
            }
        }
        public void MiddleRotorStateDown()
        {
            if (SelectedMiddleRotor == null)
            {
                return;
            }

            if (MiddleRotorState == 1)
            {
                MiddleRotorState = 26;
            }
            else
            {
                MiddleRotorState--;
            }

            if (Applied == true && MiddleRotorStateAfterApplied != MiddleRotorState)
            {
                NeedToApply = true;
                Applied = false;
            }
        }

        public void RightRotorStateUp()
        {
            if (SelectedRightRotor == null)
            {
                return;
            }

            if (RightRotorState == 26)
            {
                RightRotorState = 1;
            }
            else
            {
                RightRotorState++;
            }

            if (Applied == true && RightRotorStateAfterApplied != RightRotorState)
            {
                NeedToApply = true;
                Applied = false;
            }
        }
        public void RightRotorStateDown()
        {
            if (SelectedRightRotor == null)
            {
                return;
            }

            if (RightRotorState == 1)
            {
                RightRotorState = 26;
            }
            else
            {
                RightRotorState--;
            }

            if (Applied == true && RightRotorStateAfterApplied != RightRotorState)
            {
                NeedToApply = true;
                Applied = false;
            }
        }

        public void RandomInit()
        {
            var r = new Random();

            SelectedLeftRotor = RotorComboBoxItems[r.Next(RotorComboBoxItems.Count)];
            SelectedMiddleRotor = RotorComboBoxItems[r.Next(RotorComboBoxItems.Count)];
            SelectedRightRotor = RotorComboBoxItems[r.Next(RotorComboBoxItems.Count)];

            LeftRotorState = r.Next(1, 27);
            MiddleRotorState = r.Next(1, 27);
            RightRotorState = r.Next(1, 27);

            SelectedReflector = ReflectorComboBoxItems[r.Next(ReflectorComboBoxItems.Count)];

            SelectedPlugboard = PlugboardComboBoxItems[r.Next(PlugboardComboBoxItems.Count)];

            NeedToApply = true;
        }

        public void Apply()
        {
            if (!AllPartsSelected)
            {
                System.Windows.MessageBox.Show("Please select all parts!", "Enigma");
                return;
            }

            Machine = new Machine(
                SelectedLeftRotor.Value, LeftRotorState - 1,
                SelectedMiddleRotor.Value, MiddleRotorState - 1,
                SelectedRightRotor.Value, RightRotorState - 1,
                SelectedReflector.Value,
                SelectedPlugboard.Value);

            SelectedLeftRotorAfterApplied = SelectedLeftRotor;
            SelectedMiddleRotorAfterApplied = SelectedMiddleRotor;
            SelectedRightRotorAfterApplied = SelectedRightRotor;

            SelectedReflectorAfterApplied = SelectedReflector;

            SelectedPlugboardAfterApplied = SelectedPlugboard;

            LeftRotorStateAfterApplied = LeftRotorState;
            MiddleRotorStateAfterApplied = MiddleRotorState;
            RightRotorStateAfterApplied = RightRotorState;

            Applied = true;
            NeedToApply = false;
        }

        public void Reset()
        {
            SelectedLeftRotor = null;
            SelectedMiddleRotor = null;
            SelectedRightRotor = null;

            LeftRotorState = 1;
            MiddleRotorState = 1;
            RightRotorState = 1;

            SelectedReflector = null;

            SelectedPlugboard = null;

            Machine = null;

            Output = "";

            Applied = false;
            NeedToApply = false;
            AllPartsSelected = false;
            NotStarted = true;
        }

        public char? Process(char input)
        {
            if (!AllPartsSelected)
            {
                return null;
            }

            if (NeedToApply)
            {
                System.Windows.MessageBox.Show("Please apply all changes!", "Enigma");
                return null;
            }

            NotStarted = false;

            var (outputChar, leftRotorState, middleRotorState, rightRotorState) = Machine.ProcessChar(input);

            LeftRotorState = leftRotorState + 1;
            MiddleRotorState = middleRotorState + 1;
            RightRotorState = rightRotorState + 1;

            Output += outputChar;

            return outputChar;
        }
    }
}
