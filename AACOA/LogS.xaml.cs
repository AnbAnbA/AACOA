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
        int num;
        DispatcherTimer timer = new DispatcherTimer();
        string ca = "";
        public LogS()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 20);
            timer.Tick += new EventHandler(Timer_Trick);
            tbnumber.Focus();
        }
        private void tbnumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            
            if (e.Key == Key.Enter)
            {
                int n = 0;
                num = Convert.ToInt32(tbnumber.Text);
                User logIn = Base.EA.User.FirstOrDefault(z => z.number == num);
                if (tbnumber.Text != "")
                {
                    if (int.TryParse(tbnumber.Text, out n))
                    {
                        if (logIn == null)
                        {
                            MessageBox.Show("Такого номера не существует в базе!");
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
            if (e.Key == Key.Enter)
            {
                Random random = new Random();
                int code = random.Next(10000, 90000);
                ca = "";
                string sp = "!@#$%^&*()";
                string cac = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
                string c="";
                string[] capcha = new string[9];
                for (int i = 0; ca.Length < 6; i++)
                {
                    if (random.Next(1, 3) == 1)
                    {
                        //capcha[i] = Convert.ToString(random.Next(0, 9)) + (char)(random.Next('A', 'Z')) ;
                        ca = ca + (char)(random.Next('A', 'Z'));
                    }
                    else
                    {
                        //capcha[i] = (char)(random.Next('a', 'z')) ;
                        ca = ca + (char)(random.Next('a', 'z'));
                    }
                    
                }
                ca = ca + Convert.ToString(random.Next(0, 9));
                ca = ca + sp[random.Next(sp.Length)];


                for (int i = 0; c.Length < 8; i++) 
                {
                    c = c + Convert.ToString(cac[random.Next(cac.Length)]);
                }

                    //int pasGegCode = tbpassword.Password.GetHashCode();
                    int n = Convert.ToInt32(tbpassword.Password);
                User logIn = Base.EA.User.FirstOrDefault(z => z.password == n);
                if (tbpassword.Password != "")
                {
                    
                        if (logIn == null)
                        {
                            MessageBox.Show("Неправильно введен пароль!");
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
            timer.Stop();
        }

        private void tbcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
           

            if (e.Key == Key.Enter)
            {
                User user = Base.EA.User.FirstOrDefault(z => z.number == num);
                if (tbcode.Text != "")
                {

                    if (ca.ToString() != tbcode.Text.ToString())
                    {
                        MessageBox.Show("Not code");
                        tbcode.IsEnabled = false;
                        btrefresh.IsEnabled = true;
                        btlogin.IsEnabled = false;
                        tbcode.Text = "";
                        timer.Stop();
                    }
                    else
                    {
                        timer.Stop();
                        MessageBox.Show("Вы успешно авторизовались! Ваша роль "+ user.Role1.role1);
                    }

                }
                else
                {
                    timer.Stop();
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
            tbcode.Text = "";
            timer.Stop();
        }

        private void btrefresh_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int code = random.Next(10000, 90000);
            ca = "";
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
            User user = Base.EA.User.FirstOrDefault(z => z.number == num);
            if (tbcode.Text != "")
            {

                if (ca.ToString() != tbcode.Text.ToString())
                {
                    MessageBox.Show("Not code");
                    tbcode.IsEnabled = false;
                    btrefresh.IsEnabled = true;
                    btlogin.IsEnabled = false;
                    tbcode.Text = "";
                    timer.Stop();
                }
                else
                {
                    MessageBox.Show("Вы успешно авторизовались! Ваша роль " + user.Role1.role1);
                    timer.Stop();
                }

            }
            else
            {
                MessageBox.Show("Empty code");
            }
        }
    }

}
