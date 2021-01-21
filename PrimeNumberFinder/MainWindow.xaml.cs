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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static CancellationTokenSource cts;
        private CancellationToken ct;


        public event PropertyChangedEventHandler PropertyChanged;

        public delegate Task<string> FindNumber(int tis, int num);

        public List<FindNumber> Methods { get; set; }

        public FindNumber SelectedMethod { get; set; }

        private int times;
        public int Times
        {
            get { return times; }
            set
            {
                times = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Times"));
            }
        }

        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                number = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Number"));
            }
        }

        private string tb1;
        public string TB1
        {
            get { return tb1; }
            set
            {
                tb1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TB1"));
            }
        }

        private string tb2;
        public string TB2
        {
            get { return tb2; }
            set
            {
                tb2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TB2"));
            }
        }

        private string tb3;
        public string TB3
        {
            get { return tb3; }
            set
            {
                tb3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TB3"));
            }
        }

        private int taskId;
        public int TaskId
        {
            get { return taskId; }
            set
            {
                taskId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskId"));
            }
        }

        private bool isEnable;
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnable"));
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            Methods = new List<FindNumber>
            {
                FindNumNTimes
            };
            SelectedMethod = Methods[0];
            TaskId = 0;
            IsEnable = true;
            DataContext = this;
        }

        private async Task<string> FindNumNTimes(int tis, int num)
        {
            int primeNum = (int)Math.Pow(10, tis - 1);
            primeNum = 1;
            bool right = true;

            await Task.Run(() =>
            {
                while (right)
                {
                    int ctr = 0;

                    for (int i = 2; i <= primeNum / 2; i++)
                    {
                        if (primeNum % i == 0)
                        {
                            ctr++;
                        }
                    }

                    if (ctr == 0 && primeNum != 1)
                    {
                        int count = 0;
                        for (int i = 0; i < primeNum.ToString().Length; i++)
                        {
                            int some = Convert.ToInt32(primeNum.ToString()[i].ToString());
                            if (some == num)
                                count++;
                        }

                        if (count >= tis)
                        {
                            cts.Cancel();
                            right = false;
                            Console.WriteLine(primeNum);
                            break;
                        }
                    }
                    primeNum++;
                }
            });

            Thread.Sleep(3000);
            return primeNum.ToString();
        }

        private async Task<string> LoadingAsync(int id)
        {
            await Task.Run(() =>
            {
                while (ct.IsCancellationRequested == false)
                {
                    if (id == 1)
                    {
                        TB1 = "Načítání .";
                        Thread.Sleep(1000);
                        TB1 = "Načítání ..";
                        Thread.Sleep(1000);
                        TB1 = "Načítání ...";
                        Thread.Sleep(1000);
                    }
                    if (id == 2)
                    {
                        TB2 = "Načítání .";
                        Thread.Sleep(1000);
                        TB2 = "Načítání ..";
                        Thread.Sleep(1000);
                        TB2 = "Načítání ...";
                        Thread.Sleep(1000);
                    }
                    if (id == 3)
                    {
                        TB3 = "Načítání .";
                        Thread.Sleep(1000);
                        TB3 = "Načítání ..";
                        Thread.Sleep(1000);
                        TB3 = "Načítání ...";
                        Thread.Sleep(1000);
                    }
                }
            });
            TaskId--;
            return "";
        }

        private async void Button_Click_Find(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            ct = cts.Token;

            TaskId++;
            if (TaskId >= 3)
            {
                IsEnable = false;
                TaskId = 3;
            }
            else
            {
                isEnable = true;
                string output = await SelectedMethod(Times, Number);
                TB1 = output;
            }
        }

        private async void Button_Click_Loading(object sender, RoutedEventArgs e)
        {
            await LoadingAsync(TaskId);
        }
    }
}
