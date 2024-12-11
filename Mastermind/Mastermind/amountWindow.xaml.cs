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
    /// Interaction logic for amountWindow.xaml
    /// </summary>
    public partial class amountWindow : Window
    {
        public amountWindow()
        {
            InitializeComponent();
        }

        private void cancelAmountButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aantal pogingen wordt op de standaard waarde van 10 gezet", "Standaard waarde", MessageBoxButton.OK, MessageBoxImage.Information);
            amountTextBox.Text = "10";
            this.Close();
        }

        private void okeAmountButton_Click(object sender, RoutedEventArgs e)
        {
            if (amountTextBox.Text.Length >= 1)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Aantal pogingen moet tussen 3 en 20 liggen", "Foutieve ingave", MessageBoxButton.OK, MessageBoxImage.Error);
                amountTextBox.Focus();
            }
        }
    }
}
