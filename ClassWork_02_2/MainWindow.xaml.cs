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

namespace ClassWork_02_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public Random rnd = new Random(DateTime.Now.Millisecond);

        private void StartThread1(object sender, RoutedEventArgs e)
        {
            Thread thread1 = new Thread(ThreadProc_1);
            thread1.SetApartmentState(ApartmentState.STA);
            thread1.IsBackground = (chbIsBckgrd.IsChecked == true);
            thread1.Name = $"Thread {rnd.Next(1, 100)}";
            thread1.Start(this);
        }

        void ThreadProc_1(object param)
        {
            MainWindow form = param as MainWindow;
            form.rnd.Next(1, 10);

            Window1 form1 = new Window1();

            int id = Thread.CurrentThread.ManagedThreadId;
            string name = Thread.CurrentThread.Name;

            form1.Title = $"Window in Thread {id}";
            form1.infoThread.Content = $"Thread ID = {id}, Thread Name = {name}";
            form1.txtOut.Text = $"{name} is started";
            form1.ShowDialog();
        }

        private void StartThread2(object sender, RoutedEventArgs e)
        {
            Window1 form1 = new Window1();
            Thread thread2 = new Thread(ThreadProc_2);
            thread2.Name = $"Thread {rnd.Next(200, 1000)}";
            thread2.IsBackground = (chbIsBckgrd.IsChecked == true);
            thread2.Start(form1);
        }

        void ThreadProc_2(object param)
        {
            Window1 form = param as Window1;

            int id = Thread.CurrentThread.ManagedThreadId;
            string name = Thread.CurrentThread.Name;

            //form.Title = $"Window in Thread {id}";
            form.Dispatcher.Invoke(new Action(() =>
            {
                form.Title = $"Window in Thread {id}";
                form.infoThread.Content = $"Thread ID = {id}, Thread Name = {name}";
                form.txtOut.Text = $"{name} is Started";
                form.Show();
            }));
        }

        private void Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
