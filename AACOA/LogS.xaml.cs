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

namespace AACOA
{
    /// <summary>
    /// Логика взаимодействия для LogS.xaml
    /// </summary>
    public partial class LogS : Page
    {
        public LogS()
        {
            InitializeComponent();
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
            int pasGegCode = tbpassword.Password.GetHashCode();
            LogIn logIn = Base.EA.LogIn.FirstOrDefault(z => z.password == pasGegCode);
            if (e.Key == Key.Enter)
            {
                if (tbnumber.Text != "")
                {
                    
                        if (logIn != null)
                        {
                            MessageBox.Show("Not Password");
                        }
                        else
                        {
                         
                        }
                  
                }
                else
                {
                    MessageBox.Show("Empty Password");
                }
            }
        }
    }

}
