using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;

//using System.Linq;

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

            var calc = logic.Method1Async(logic.Times, logic.Number);
            var load = logic.LoadingAsync();

            var tasks = new List<Task> { calc, load };

            await Task.WhenAll(tasks);
        }

        private async void Button_Click_Meth2(object sender, RoutedEventArgs e)
        {
            MethodBlock logic = Blocks[1];

            var calc = logic.Method2Async(logic.Number);
            var load = logic.LoadingAsync();

            var tasks = new List<Task> { calc, load };

            await Task.WhenAll(tasks);
        }

        private async void Button_Click_Meth3(object sender, RoutedEventArgs e)
        {
            MethodBlock logic = Blocks[2];

            var calc = logic.Method3Async(logic.Number);
            var load = logic.LoadingAsync();

            var tasks = new List<Task> { calc, load };

            await Task.WhenAll(tasks);
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

        public async Task Method1Async(int howTimes, int whatNumber)
        {
            IsFree = false;
            int primeNum = 0;

            if (howTimes == 1)
            {
                primeNum = (int)Math.Pow(10, howTimes - 1);
            }
            else
            {
                primeNum = (int)Math.Pow(10, howTimes);
            }
            bool right = true;

            await Task.Run(() =>
                {
                    while (right)
                    {
                        bool is_dividable = false;

                        for (int i = 2; i <= Math.Sqrt(primeNum); i++)
                        {
                            if (primeNum % i == 0)
                            {
                                is_dividable = true;
                                break;
                            }
                        }

                        if (!is_dividable && primeNum != 1)
                        {
                            int count = 0;
                            string strPrime = primeNum.ToString();

                            for (int i = 0; i < strPrime.Length; i++)
                            {
                                int toFind = Convert.ToInt32(strPrime[i].ToString());
                                if (toFind == whatNumber)
                                {
                                    count++;
                                    if (strPrime.Length - howTimes < i - count)
                                    {
                                        break;
                                    }
                                }
                            }

                            if (count >= howTimes)
                            {
                                break;
                            }
                        }

                        primeNum++;
                    }
                    IsFree = true;
                    Output = primeNum.ToString();
                });
        }

        public async Task Method2Async(int max)
        {
            IsFree = false;
            int count = 0;

            await Task.Run(() =>
                {
                    //bool[] primeNums = Enumerable.Repeat(true, max + 1).ToArray();
                    bool[] primeNums = new bool[max + 1];
                    for (int i = 0; i < primeNums.Length; i++)
                    {
                        primeNums[i] = true;
                    }

                    int prime = 2;
                    while(prime * prime <= max)
                    {
                        if (primeNums[prime])
                        {
                            for (int i = prime * prime; i < max + 1; i += prime)
                            {
                                primeNums[i] = false;
                            }
                        }

                        prime++;
                    }

                    for (int i = 2; i < max; i++)
                    {
                        if (primeNums[i])
                        {
                            count++;
                        }
                    }
                    IsFree = true;
                    Output = count.ToString();
                });

        }

        public async Task Method3Async(int numToFind)
        {
            IsFree = false;
            int count = 0;
            int max = numToFind.ToString().Length * 3 * numToFind;
            int result = 0;

            await Task.Run(() =>
            {
                bool[] primeNums = new bool[max + 1];
                for (int i = 0; i < primeNums.Length; i++)
                {
                    primeNums[i] = true;
                }

                int prime = 2;
                while (prime * prime <= max)
                {
                    if (primeNums[prime])
                    {
                        for (int i = prime * prime; i < max + 1; i += prime)
                        {
                            primeNums[i] = false;
                        }
                    }

                    prime++;
                }

                for (int i = 2; i < max; i++)
                {
                    if (primeNums[i])
                    {
                        count++;
                        if (count == numToFind)
                        {
                            result = i;
                            break;
                        }
                    }
                }
                IsFree = true;
                Output = result.ToString();
            });
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
