using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace AACOA
{
    /// <summary>
    /// Логика взаимодействия для LogS.xaml
    /// </summary>
    public partial class LogS : Page
    {
        DispatcherTimer timer = new DispatcherTimer();
        string ca = "";
        public LogS()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += new EventHandler(Timer_Trick);
            
        }
        private void tbnumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int n = 0;
            LogIn logIn=Base.EA.LogIn.FirstOrDefault(z=>z.number == n);
            if (e.Key == Key.Enter)
            {
                if (tbnumber.Text != "")
                {
                    if (int.TryParse(tbnumber.Text, out n))
                    {
                        if (logIn != null)
                        {
                            MessageBox.Show("Not Number");
                        }
                        else
                        {
                            tbpassword.IsEnabled = true;
                            tbpassword.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Number");
                    }
                }
                else 
                {
                    MessageBox.Show("Empty Number");
                }
            }
        }

        private void tbpassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            Random random = new Random();
            int code = random.Next(10000, 90000);
            string ca = "";
            string[] capcha = new string[9];
            for (int i = 0; ca.Length < 8; i++)
            {
                if (random.Next(1, 3) == 1)
                {
                    capcha[i] = Convert.ToString(random.Next(0, 9)) + (char)(random.Next('A', 'Z'));
                    ca = ca + capcha[i];
                }
                else
                {
                    capcha[i] = Convert.ToString(random.Next(0, 9)) + (char)(random.Next('a', 'z'));
                    ca = ca + capcha[i];
                }
            }

            int pasGegCode = tbpassword.Password.GetHashCode();
            LogIn logIn = Base.EA.LogIn.FirstOrDefault(z => z.password == pasGegCode);
            if (e.Key == Key.Enter)
            {
                if (tbpassword.Password != "")
                {
                    
                        if (logIn != null)
                        {
                            MessageBox.Show("Not Password");
                        }
                        else
                        {
                        if (MessageBox.Show(ca.ToString(), "Cгенерированный код доступа", MessageBoxButton.OK) == MessageBoxResult.OK) 
                        {
                            tbcode.IsEnabled = true;
                            tbcode.Focus();
                            timer.Start();
                        }
                        }
                  
                }
                else
                {
                    MessageBox.Show("Empty Password");
                }
            }
        }

        private void btcancel_Click(object sender, RoutedEventArgs e)
        {
            tbnumber.Text = "";
            tbpassword.Password = "";
            tbcode.Text = "";
            tbpassword.IsEnabled = false;
            tbcode.IsEnabled = false;
        }

        private void tbcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (tbcode.Text != "")
                {

                    if (ca == tbcode.Text)
                    {
                        MessageBox.Show("Not code");
                    }
                    else
                    {
                        MessageBox.Show("Вы успешно авторизовались!");
                    }

                }
                else
                {
                    MessageBox.Show("Empty code");
                }
            }
        }

        private void Timer_Trick(object sender, EventArgs e)
        {
            MessageBox.Show("Вы не успели ввести код доступа!");
            tbcode.IsEnabled = false;
            btrefresh.IsEnabled = true;
            btlogin.IsEnabled = false;
        }

        private void btrefresh_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int code = random.Next(10000, 90000);
            string ca = "";
            string[] capcha = new string[9];
            for (int i = 0; ca.Length < 8; i++)
            {
                if (random.Next(1, 3) == 1)
                {
                    capcha[i] = Convert.ToString(random.Next(0, 9)) + (char)(random.Next('A', 'Z'));
                    ca = ca + capcha[i];
                }
                else
                {
                    capcha[i] = Convert.ToString(random.Next(0, 9)) + (char)(random.Next('a', 'z'));
                    ca = ca + capcha[i];
                }
            }
            if (MessageBox.Show(ca.ToString(), "Cгенерированный код доступа", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                btrefresh.IsEnabled = false;
                tbcode.IsEnabled = true;
                tbcode.Focus();
                timer.Start();
                btlogin.IsEnabled = true;
            }
        }

        private void btlogin_Click(object sender, RoutedEventArgs e)
        {
            if (tbcode.Text != "")
            {

                if (ca == tbcode.Text)
                {
                    MessageBox.Show("Not code");
                }
                else
                {
                    MessageBox.Show("Вы успешно авторизовались!");
                }

            }
            else
            {
                MessageBox.Show("Empty code");
            }
        }
    }

}
