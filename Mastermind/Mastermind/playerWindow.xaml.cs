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
using System.Windows.Shapes;


namespace Mastermind
{
    /// <summary>
    /// Interaction logic for playerWindow.xaml
    /// </summary>
    public partial class playerWindow : Window
    {   
        List<string> players = new List<string>();

        public playerWindow()
        {
            InitializeComponent();
        }

        private void okeButton_Click(object sender, RoutedEventArgs e)
        {
            if (playerTextBox.Text.Length > 1)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("De naam moet uit minimum 2 karakters bestaan","Foutieve ingave",MessageBoxButton.OK,MessageBoxImage.Error);
                playerTextBox.Focus();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
