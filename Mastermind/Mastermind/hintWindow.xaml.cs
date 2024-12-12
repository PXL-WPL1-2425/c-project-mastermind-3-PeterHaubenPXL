using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for hintWindow.xaml
    /// </summary>
    public partial class hintWindow : Window
    {
        // Global variables

        MainWindow instMW;

        int[] colorCodes = new int[6];

        List<StackPanel> list = new List<StackPanel>();

        public hintWindow()
        {
            InitializeComponent();

            instMW = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault();
        }

        private void okHintButton_Click(object sender, RoutedEventArgs e)
        {
            if (instMW.serie2StackPanel.Visibility == Visibility.Hidden)
            {
                return;
            }

            int counter;
            int counter2;


            for (int i = list.Count - 1; i >= 1; i--)
            {
                if (list[i].Visibility == Visibility.Visible)
                {
                    counter = 0;
                    foreach (var item in list[i - 1].Children)
                    {
                        if (counter >= 1) // counter 0 = serieLabel
                        {
                            if (item is Label lbl)
                            {
                                if (lbl.BorderBrush == Brushes.DarkRed)
                                {
                                    colorCodes[counter - 1] = 0;
                                }
                            }
                        }
                        counter++;
                    }

                    if (colorRadioButton.IsChecked == true)
                    {
                        // Onderstaande geeft FOUT als alle vakken Ofwel darkred of wheat rand hebben
                        // Zonder bestaat de mogelijkheid dat een kleur die een wheat rand heeft, 
                        // gekozen wordt als mogelijke kleur, zonder positie.
                        // Maar het programma loopt niet meer vast, als dit uitgeschakeld is

                        //counter = 0;
                        //foreach (var item in list[i - 1].Children)
                        //{
                        //    if (counter >= 1) // counter 0 = serieLabel
                        //    {
                        //        if (item is Label lbl)
                        //        {
                        //            if (lbl.BorderBrush == Brushes.Wheat)
                        //            {
                        //                if (lbl.Background == Brushes.Red)
                        //                {
                        //                    if (colorCodes.Contains(1))
                        //                    {
                        //                        for (int j = 0; j < colorCodes.Length; j++)
                        //                        {
                        //                            if (colorCodes[j] == 1)
                        //                            {
                        //                                colorCodes[j] = 0;
                        //                                break;
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //                else if (lbl.Background == Brushes.Yellow)
                        //                {
                        //                    if (colorCodes.Contains(2))
                        //                    {
                        //                        for (int j = 0; j < colorCodes.Length; j++)
                        //                        {
                        //                            if (colorCodes[j] == 2)
                        //                            {
                        //                                colorCodes[j] = 0;
                        //                                break;
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //                else if (lbl.Background == Brushes.Orange)
                        //                {
                        //                    if (colorCodes.Contains(3))
                        //                    {
                        //                        for (int j = 0; j < colorCodes.Length; j++)
                        //                        {
                        //                            if (colorCodes[j] == 3)
                        //                            {
                        //                                colorCodes[j] = 0;
                        //                                break;
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //                else if (lbl.Background == Brushes.White)
                        //                {
                        //                    if (colorCodes.Contains(4))
                        //                    {
                        //                        for (int j = 0; j < colorCodes.Length; j++)
                        //                        {
                        //                            if (colorCodes[j] == 4)
                        //                            {
                        //                                colorCodes[j] = 0;
                        //                                break;
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //                else if (lbl.Background == Brushes.Green)
                        //                {
                        //                    if (colorCodes.Contains(5))
                        //                    {
                        //                        for (int j = 0; j < colorCodes.Length; j++)
                        //                        {
                        //                            if (colorCodes[j] == 5)
                        //                            {
                        //                                colorCodes[j] = 0;
                        //                                break;
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //                else if (lbl.Background == Brushes.Blue)
                        //                {
                        //                    if (colorCodes.Contains(6))
                        //                    {
                        //                        for (int j = 0; j < colorCodes.Length; j++)
                        //                        {
                        //                            if (colorCodes[j] == 6)
                        //                            {
                        //                                colorCodes[j] = 0;
                        //                                break;
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //    counter++;
                        //}
                    }

                }
            }

            bool seen = false;

            Random rnd = new Random();
            int random;

            do
            {
                random = rnd.Next(0, colorCodes.Length);
            } while (colorCodes[random] == 0);


            switch (colorCodes[random])
            {
                case 1:
                    if (colorRadioButton.IsChecked == true)
                    {
                        hintColorLabel.Background = Brushes.Red;
                        seen = true;
                        break;
                    }
                    else   // positionRadioButton.IsChecked = true;
                    {
                        switch (random)
                        {
                            case 0:
                                hint1Label.Background = Brushes.Red;
                                seen = true; break;
                            case 1:
                                hint2Label.Background = Brushes.Red;
                                seen = true; break;
                            case 2:
                                hint3Label.Background = Brushes.Red;
                                seen = true; break;
                            case 3:
                                hint4Label.Background = Brushes.Red;
                                seen = true; break;
                            case 4:
                                hint5Label.Background = Brushes.Red;
                                seen = true; break;
                            case 5:
                                hint6Label.Background = Brushes.Red;
                                seen = true; break;
                        }
                    }
                    break;

                case 2:
                    if (colorRadioButton.IsChecked == true)
                    {
                        hintColorLabel.Background = Brushes.Yellow;
                    }
                    else
                    {
                        switch (random)
                        {
                            case 0:
                                hint1Label.Background = Brushes.Yellow;
                                seen = true; break;
                            case 1:
                                hint2Label.Background = Brushes.Yellow;
                                seen = true; break;
                            case 2:
                                hint3Label.Background = Brushes.Yellow;
                                seen = true; break;
                            case 3:
                                hint4Label.Background = Brushes.Yellow;
                                seen = true; break;
                            case 4:
                                hint5Label.Background = Brushes.Yellow;
                                seen = true; break;
                            case 5:
                                hint6Label.Background = Brushes.Yellow;
                                seen = true; break;
                        }
                    }
                    seen = true;
                    break;
                case 3:
                    if (colorRadioButton.IsChecked == true)
                    {
                        hintColorLabel.Background = Brushes.Orange;
                    }
                    else
                    {
                        switch (random)
                        {
                            case 0:
                                hint1Label.Background = Brushes.Orange;
                                seen = true; break;
                            case 1:
                                hint2Label.Background = Brushes.Orange;
                                seen = true; break;
                            case 2:
                                hint3Label.Background = Brushes.Orange;
                                seen = true; break;
                            case 3:
                                hint4Label.Background = Brushes.Orange;
                                seen = true; break;
                            case 4:
                                hint5Label.Background = Brushes.Orange;
                                seen = true; break;
                            case 5:
                                hint6Label.Background = Brushes.Orange;
                                seen = true; break;
                        }
                    }
                    seen = true;
                    break;
                case 4:
                    if (colorRadioButton.IsChecked == true)
                    {
                        hintColorLabel.Background = Brushes.White;
                    }
                    else
                    {
                        switch (random)
                        {
                            case 0:
                                hint1Label.Background = Brushes.White;
                                seen = true; break;
                            case 1:
                                hint2Label.Background = Brushes.White;
                                seen = true; break;
                            case 2:
                                hint3Label.Background = Brushes.White;
                                seen = true; break;
                            case 3:
                                hint4Label.Background = Brushes.White;
                                seen = true; break;
                            case 4:
                                hint5Label.Background = Brushes.White;
                                seen = true; break;
                            case 5:
                                hint6Label.Background = Brushes.White;
                                seen = true; break;
                        }
                    }
                    seen = true;
                    break;
                case 5:
                    if (colorRadioButton.IsChecked == true)
                    {
                        hintColorLabel.Background = Brushes.Green;
                    }
                    else
                    {
                        switch (random)
                        {
                            case 0:
                                hint1Label.Background = Brushes.Green;
                                seen = true; break;
                            case 1:
                                hint2Label.Background = Brushes.Green;
                                seen = true; break;
                            case 2:
                                hint3Label.Background = Brushes.Green;
                                seen = true; break;
                            case 3:
                                hint4Label.Background = Brushes.Green;
                                seen = true; break;
                            case 4:
                                hint5Label.Background = Brushes.Green;
                                seen = true; break;
                            case 5:
                                hint6Label.Background = Brushes.Green;
                                seen = true; break;
                        }
                    }
                    seen = true;
                    break;
                case 6:
                    if (colorRadioButton.IsChecked == true)
                    {
                        hintColorLabel.Background = Brushes.Blue;
                    }
                    else
                    {
                        switch (random)
                        {
                            case 0:
                                hint1Label.Background = Brushes.Blue;
                                seen = true; break;
                            case 1:
                                hint2Label.Background = Brushes.Blue;
                                seen = true; break;
                            case 2:
                                hint3Label.Background = Brushes.Blue;
                                seen = true; break;
                            case 3:
                                hint4Label.Background = Brushes.Blue;
                                seen = true; break;
                            case 4:
                                hint5Label.Background = Brushes.Blue;
                                seen = true; break;
                            case 5:
                                hint6Label.Background = Brushes.Blue;
                                seen = true; break;
                        }
                    }
                    seen = true;
                    break;
            }

            if (seen == true)
            {
                colorCodes = colorCodes;
                okHintButton.IsEnabled = false;
                return;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            okHintButton.IsEnabled = true;
        }

        private void closelHintButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in instMW.seriesStackPanel.Children)
            {
                if (item is StackPanel stack)
                {
                    list.Add(stack);
                }
            }

            int counter = 0;
            foreach (var item2 in instMW.debugStackPanel.Children)
            {
                if (item2 is Label lbl)
                {
                    if (lbl.Visibility == Visibility.Visible)
                    {
                        if (lbl.Background == Brushes.Red)
                        {
                            colorCodes[counter] = 1;
                        }
                        else if (lbl.Background == Brushes.Yellow)
                        {
                            colorCodes[counter] = 2;
                        }
                        else if (lbl.Background == Brushes.Orange)
                        {
                            colorCodes[counter] = 3;
                        }
                        else if (lbl.Background == Brushes.White)
                        {
                            colorCodes[counter] = 4;
                        }
                        else if (lbl.Background == Brushes.Green)
                        {
                            colorCodes[counter] = 5;
                        }
                        else if (lbl.Background == Brushes.Blue)
                        {
                            colorCodes[counter] = 6;
                        }
                    }
                    else
                    {
                        colorCodes[counter] = 0;
                    }
                }
                counter++;
            }
            okHintButton.IsEnabled = false;

            if(instMW.amount < 5)
            {
                hint5Label.Visibility = Visibility.Collapsed;
            }
            else
            {
                hint5Label.Visibility = Visibility.Visible;
            }

            if (instMW.amount < 6)
            {
                hint6Label.Visibility = Visibility.Collapsed;
            }
            else
            {
                hint6Label.Visibility = Visibility.Visible;
            }


        }

    }

}
