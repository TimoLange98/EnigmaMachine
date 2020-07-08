using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using Caliburn.Micro;

namespace EnigmaUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : MetroWindow
    {
        private readonly List<Button> Buttons;

        public ShellView()
        {
            InitializeComponent();
            Buttons = FindVisualChildren<Button>(Keyboard).ToList();
        }

        private void MetroWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            KeyConverter kc = new KeyConverter();
            var model = DataContext as ShellViewModel;

            var keyString = kc.ConvertToString(e.Key);

            if (keyString.Length > 1)
            {
                return;
            }

            if (!char.IsLetter(keyString.First()) && !char.IsWhiteSpace(keyString.First()))
            {
                return;
            }
            
            var outPutChar = model.Process(keyString.First());

            if (outPutChar == null)
            {
                return;
            }

            var button = Buttons.Find(b => b.Name == outPutChar.ToString());

            var t = new Thread(() => Blink(button));
            t.Start();
        }

        private void Blink(Button button)
        {
            Dispatcher.Invoke(() =>
            {
                button.IsEnabled = true;
                button.Background = Brushes.Red;
            });
            
            Thread.Sleep(100);

            Dispatcher.Invoke(() =>
            {
                button.IsEnabled = false;
                button.Background = Brushes.Transparent;
            });
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
