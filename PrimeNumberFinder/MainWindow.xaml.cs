using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace PrimeNumberFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<MethodBlock> Blocks { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Blocks = new List<MethodBlock>
            {
                new MethodBlock(),
                new MethodBlock(),
                new MethodBlock()
            };

            DataContext = Blocks;
        }

        private async void Button_Click_Meth1(object sender, RoutedEventArgs e)
        {
            MethodBlock logic = Blocks[0];

            var calc = logic.Method1Async();
            var load = logic.LoadingAsync();
            logic.IsFree = false;

            var tasks = new List<Task> { calc, load };

            await Task.WhenAll(tasks);
            logic.IsFree = true;
        }

        private async void Button_Click_Meth2(object sender, RoutedEventArgs e)
        {
            MethodBlock logic = Blocks[1];

            var calc = logic.Method1Async();
            var load = logic.LoadingAsync();
            logic.IsFree = false;

            var tasks = new List<Task> { calc, load };

            await Task.WhenAll(tasks);
        }

        private async void Button_Click_Meth3(object sender, RoutedEventArgs e)
        {
            MethodBlock logic = Blocks[2];

            var calc = logic.Method1Async();
            var load = logic.LoadingAsync();
            logic.IsFree = false;

            var tasks = new List<Task> { calc, load };

            await Task.WhenAll(tasks);
            logic.IsFree = true;
        }
    }

    public class MethodBlock : INotifyPropertyChanged
    {
        #region Properties

        private int times;
        public int Times 
        { 
            get => times;
            set 
            {
                times = value;
                OnPropertyChanged("Times");
            }
        }

        private int number;
        public int Number
        { 
            get => number;
            set
            {
                number = value;
                OnPropertyChanged("Number");
            }
        }

        private bool isFree;
        public bool IsFree 
        { 
            get => isFree;
            set
            {
                isFree = value;
                OnPropertyChanged("IsFree");
            }
        }

        private string output;
        public string Output
        {
            get => output;
            set
            {
                output = value;
                OnPropertyChanged("Output");
            }
        }

        #endregion

        public MethodBlock()
        {
            Times = 0;
            Number = 0;
            IsFree = true;
            Output = "";
        }

        public async Task Method1Async()
        {
            isFree = false;
            await Task.Delay(3117);
            isFree = true;
            Output = "10";
        }

        public async Task LoadingAsync()
        {
            await Task.Run(() =>
            {
                while (IsFree == false)
                {
                    if (IsFree == false)
                    {
                        Output = "Počítám.";
                        Thread.Sleep(1000);
                    }

                    if (IsFree == false)
                    {
                        Output = "Počítám..";
                        Thread.Sleep(1000);
                    }

                    if (IsFree == false)

                    {
                        Output = "Počítám...";
                        Thread.Sleep(1000);
                    }
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
