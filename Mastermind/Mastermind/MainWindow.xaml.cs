using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace Mastermind
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Globale variable

        Brush chosenColor;

        int attempts = 0;
        int chosenAttempts = 10; // Default

        int colorCode1 = 0;
        int colorCode2 = 0;
        int colorCode3 = 0;
        int colorCode4 = 0;
        int colorCode5 = 0;     //Bijgevoegd sprint 3
        int colorCode6 = 0;     //Bijgevoegd sprint 3

        int chosenColorCode1 = 0;
        int chosenColorCode2 = 0;
        int chosenColorCode3 = 0;
        int chosenColorCode4 = 0;
        int chosenColorCode5 = 0;   //Bijgevoegd sprint 3
        int chosenColorCode6 = 0;   //Bijgevoegd sprint 3

        bool dissolved = false;
        bool gameStarted = false;

        DispatcherTimer timer = new DispatcherTimer();

        int points;
        int penaltyPoints;

        List<StackPanel> list = new List<StackPanel>();

        string codeString = "";

        string[] highScores = new string[15]; //[naam speler] - [x pogingen] - [score/100]

        string player = "";

        public List<string> players = new List<string>();

        int playerCounter = 0;

        string namePlayer = "";
        string nameNextPlayer = "";

        public int amount = 4;  //Default = 4

        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mastermindWindow.Top = 40;
            mastermindWindow.Left = 40;

            timer.Interval = new TimeSpan(0, 0, 10); // interval van 10 seconden
            timer.Tick += Timer_Tick;

            debugStackPanel.Visibility = Visibility.Hidden;
            //debugStackPanel.HorizontalAlignment = HorizontalAlignment.Center;

            hintButton.Visibility = Visibility.Collapsed;

            foreach (var item in seriesStackPanel.Children)
            {
                if (item is StackPanel stack)
                {
                    list.Add(stack);
                }
            }

            generateLabels(amount);
        }


        private void generateLabels(int amount)
        {
            colorsStackPanel.Children.Clear();

            serie1StackPanel.Children.Clear();
            serie2StackPanel.Children.Clear();
            serie3StackPanel.Children.Clear();
            serie4StackPanel.Children.Clear();
            serie5StackPanel.Children.Clear();
            serie6StackPanel.Children.Clear();
            serie7StackPanel.Children.Clear();
            serie8StackPanel.Children.Clear();
            serie9StackPanel.Children.Clear();
            serie10StackPanel.Children.Clear();
            serie11StackPanel.Children.Clear();
            serie12StackPanel.Children.Clear();
            serie13StackPanel.Children.Clear();
            serie14StackPanel.Children.Clear();
            serie15StackPanel.Children.Clear();
            serie16StackPanel.Children.Clear();
            serie17StackPanel.Children.Clear();
            serie18StackPanel.Children.Clear();
            serie19StackPanel.Children.Clear();
            serie20StackPanel.Children.Clear();

            //Labels genereren
            for (int i = 1; i <= 22; i++)
            {
                if (i == 1)
                {
                    for (int j = 1; j <= 6; j++)
                    {
                        Label lbl = new Label();
                        lbl.Name = $"colorLabel{j}";
                        lbl.Width = 54;
                        lbl.Height = 54;
                        lbl.Margin = new Thickness(8);
                        lbl.BorderThickness = new Thickness(8);

                        lbl.MouseDown += Color_MouseDown;

                        Grid.SetRow(lbl, i);
                        Grid.SetColumn(lbl, j);

                        switch (j)
                        {
                            case 1:
                                lbl.Background = Brushes.Red;
                                lbl.BorderBrush = lbl.Background;
                                chosenColor = lbl.Background;
                                break;
                            case 2:
                                lbl.Background = Brushes.Yellow;
                                break;
                            case 3:
                                lbl.Background = Brushes.Orange;
                                break;
                            case 4:
                                lbl.Background = Brushes.White;
                                break;
                            case 5:
                                lbl.Background = Brushes.Green;
                                break;
                            case 6:
                                lbl.Background = Brushes.Blue;
                                break;
                        }

                        colorsStackPanel.Children.Add(lbl);
                    }
                }
                else if (i >= 3)
                {
                    for (int j = 1; j <= amount + 1; j++)
                    {
                        Label lbl = new Label();
                        lbl.Name = $"serie{i - 2}{j - 1}";
                        lbl.Width = 54;
                        lbl.Height = 54;
                        lbl.Background = Brushes.DarkGray;
                        lbl.BorderThickness = new Thickness(8);
                        lbl.BorderBrush = Brushes.Transparent;
                        lbl.ToolTip = null;

                        if (j == 1)
                        {
                            lbl.FontSize = 28;
                            lbl.Content = (i - 2).ToString();
                            lbl.Width = 60;
                            lbl.Height = 60;
                            lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                            lbl.VerticalContentAlignment = VerticalAlignment.Center;
                            lbl.Background = Brushes.White;

                            lbl.MouseDown += Series_MouseDown;
                            lbl.MouseDoubleClick += Series_MouseDoubleClick;
                        }
                        else
                        {
                            lbl.Margin = new Thickness(8);

                            lbl.MouseDown += Label_MouseDown;
                            lbl.MouseDoubleClick += Label_MouseDoubleClick;
                        }

                        Grid.SetRow(lbl, i);
                        Grid.SetColumn(lbl, j);

                        switch (i)
                        {
                            case 3:
                                serie1StackPanel.Children.Add(lbl);
                                break;
                            case 4:
                                serie2StackPanel.Children.Add(lbl);
                                break;
                            case 5:
                                serie3StackPanel.Children.Add(lbl);
                                break;
                            case 6:
                                serie4StackPanel.Children.Add(lbl);
                                break;
                            case 7:
                                serie5StackPanel.Children.Add(lbl);
                                break;
                            case 8:
                                serie6StackPanel.Children.Add(lbl);
                                break;
                            case 9:
                                serie7StackPanel.Children.Add(lbl);
                                break;
                            case 10:
                                serie8StackPanel.Children.Add(lbl);
                                break;
                            case 11:
                                serie9StackPanel.Children.Add(lbl);
                                break;
                            case 12:
                                serie10StackPanel.Children.Add(lbl);
                                break;
                            case 13:
                                serie11StackPanel.Children.Add(lbl);
                                break;
                            case 14:
                                serie12StackPanel.Children.Add(lbl);
                                break;
                            case 15:
                                serie13StackPanel.Children.Add(lbl);
                                break;
                            case 16:
                                serie14StackPanel.Children.Add(lbl);
                                break;
                            case 17:
                                serie15StackPanel.Children.Add(lbl);
                                break;
                            case 18:
                                serie16StackPanel.Children.Add(lbl);
                                break;
                            case 19:
                                serie17StackPanel.Children.Add(lbl);
                                break;
                            case 20:
                                serie18StackPanel.Children.Add(lbl);
                                break;
                            case 21:
                                serie19StackPanel.Children.Add(lbl);
                                break;
                            case 22:
                                serie20StackPanel.Children.Add(lbl);
                                break;
                        }
                    }
                }
            }

            if(amount < 5)
            {
                debugLabel5.Visibility = Visibility.Collapsed;
            }
            else
            {
                debugLabel5.Visibility = Visibility.Visible;
            }

            if(amount < 6)
            {
                debugLabel6.Visibility = Visibility.Collapsed;
            }
            else
            {
                debugLabel6.Visibility = Visibility.Visible;
            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            attempts++;

            points -= 8;
            scoreLabel.Content = $"{namePlayer} : Poging {attempts}/{chosenAttempts} Score = {points}";

            if (attempts < chosenAttempts)
            {
                // Als attempst aangepast wordt, moet ook de StackPanels aangepast worden
                // Hier en in timer_Tick
                makeStackPanelVisible();
            }
            else
            {
                // Spel einde na chosenAttempts beurten

                debugStackPanel.Visibility = Visibility.Visible;
                settingsMenuItem.IsEnabled = true;

                if (playerCounter < players.Count)
                {
                    MessageBox.Show($"You failed! De correcte code was {codeString}.\nNu is speler {nameNextPlayer} aan de beurt", $"{namePlayer}", MessageBoxButton.OK);

                    StartGame();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show($"You failed! De correcte code was {codeString}.\nSpelreeks ten einde\n\nWil je dezelfde spelreeks nog eens starten?", $"{namePlayer}", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        playerCounter = 0;
                        StartGame();
                    }
                    else
                    {
                        newGameButton.IsEnabled = true;
                        return;
                    }
                }
            }

            makeStackPanelVisible();

            // eerst kleuren overzetten
            Series_MouseDoubleClick(null, null);

            // Dan Resetten
            ResetLabels();

            StopCountdown();
        }

        private void ResetLabels()
        {
            // Kleuren verloren beurt DarkGray maken

            int counter = 0;

            for (int i = list.Count - 1; i >= 1; i--)
            {
                counter = 0;

                if (list[i].Visibility == Visibility.Visible)
                {
                    foreach (var item in list[i - 1].Children)
                    {
                        if (item is Label lbl)
                        {
                            if (counter > 0)
                            {
                                lbl.Background = Brushes.DarkGray;
                                lbl.BorderBrush = Brushes.Transparent;
                                lbl.ToolTip = null;
                                hintButton.IsEnabled = false ;
                            }
                        }
                        counter++;
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// The timer = stoped
        /// </summary>
        private void StopCountdown()
        {
            timer.Stop();
        }

        /// <summary>
        /// The timer = started
        /// from in generateButton_Click or
        /// from in controlButton_Click
        /// </summary>
        private void StartCountdown()
        {
            timer.Start();
        }


        private void Label_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!gameStarted)
            {
                MessageBoxResult result = MessageBox.Show("Wil je een spel starten?", "Spel starten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes); ;
                if (result == MessageBoxResult.Yes)
                {
                    StartGame();
                }
                else
                {
                    return;
                }
            }

            if (sender is Label lbl)
            {
                int counter;
                int counter2;
                string temp = lbl.Name;
                temp = temp.Substring(temp.Length - 1, 1);
                counter = Convert.ToInt32(temp);

                // ToDo

                for (int i = list.Count - 1; i >= 1; i--)
                {
                    if (list[i].Visibility == Visibility.Visible)
                    {
                        counter2 = 0;
                        foreach (var item2 in list[i - 1].Children)
                        {
                            if (item2 is Label lbl2 && counter2 > 0)
                            {
                                if (counter2 == counter)
                                {
                                    if (lbl2.Background != Brushes.DarkGray)
                                    {
                                        lbl.Background = lbl2.Background;
                                        chosenColor = lbl2.Background;
                                        GetColorCode(lbl.Name);
                                    }
                                }
                            }
                            counter2++;
                        }
                        return;
                    }
                }
            }
        }

        private void Series_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if (!gameStarted)
            {
                return;
            }

            if (serie2StackPanel.Visibility == Visibility.Hidden)
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
                    counter2 = 0;

                    foreach (var item in list[i].Children)
                    {
                        if (item is Label lbl && counter > 0)
                        {
                            counter2 = 0;
                            foreach (var item2 in list[i - 1].Children)
                            {
                                if (item2 is Label lbl2 && counter2 > 0)
                                {
                                    if (counter2 == counter)
                                    {
                                        lbl.Background = lbl2.Background;
                                        chosenColor = lbl2.Background;
                                        GetColorCode(lbl.Name);
                                    }
                                }
                                counter2++;
                            }
                        }
                        counter++;
                    }
                    return;
                }
            }
        }

        private void Series_MouseDown(object sender, RoutedEventArgs e)
        {
            if (!gameStarted)
            {
                return;
            }

            if (chosenColor == Brushes.Transparent)
            {
                // Standaard begin als er nog geen kleur gekozen is

                chosenColor = Brushes.Red;
                chosenColor_Changed();
            }

            int counter = 0;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].Visibility == Visibility.Visible)
                {
                    counter = 0;
                    foreach (var item in list[i].Children)
                    {
                        if (item is Label lbl && counter > 0)
                        {
                            if (lbl.Background == Brushes.DarkGray)
                            {
                                lbl.Background = chosenColor;
                                GetColorCode(lbl.Name);
                            }
                        }
                        counter++;
                    }
                    return;
                }
            }
        }

        private void Color_MouseDown(object sender, RoutedEventArgs e)
        {
            if (sender is Label lbl)
            {
                string temp = lbl.Name;
                temp = temp.Substring(temp.Length - 1, 1);
                foreach (var item in colorsStackPanel.Children)
                {
                    if (item is Label lbl2)
                    {
                        if (lbl2.Name.Contains(temp))
                        {
                            lbl2.BorderBrush = lbl.Background;
                        }
                        else
                        {
                            lbl2.BorderBrush = Brushes.Transparent;
                        }
                    }
                }
                chosenColor = lbl.Background;
                chosenColor_Changed();
            }
        }

        private void Label_MouseDown(object sender, RoutedEventArgs e)
        {
            if (!gameStarted)
            {
                MessageBoxResult result = MessageBox.Show("Wil je een spel starten?", "Spel starten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes); ;
                if (result == MessageBoxResult.Yes)
                {
                    StartGame();
                }
                else
                {
                    return;
                }
            }
            if (chosenColor == Brushes.Transparent)
            {
                chosenColor = Brushes.Red;
                chosenColor_Changed();
            }

            if (sender is Label lbl)
            {
                if (lbl.Name.Contains("serie"))
                {
                    string temp = lbl.Name;
                    temp = temp.Substring(temp.LastIndexOf("e") + 1);
                    int intTemp = Convert.ToInt32(temp);
                    intTemp = intTemp / 10;

                    if (attempts == intTemp - 1)
                    {
                        lbl.Background = chosenColor;
                        GetColorCode(lbl.Name);
                    }
                    else
                    {
                        if (lbl.Background != Brushes.DarkGray)
                        {
                            chosenColor = lbl.Background;
                            chosenColor_Changed();
                        }
                    }
                }
            }
        }

        private void chosenColor_Changed()
        {
            foreach (var item in colorsStackPanel.Children)
            {
                if (item is Label lbl)
                {
                    if (chosenColor == lbl.Background)
                    {
                        lbl.BorderBrush = lbl.Background;
                    }
                    else
                    {
                        lbl.BorderBrush = Brushes.Transparent;
                    }
                }
            }

            controlButton.IsDefault = true;
        }

        private void GetColorCode(string lblName)
        {
            char lastLetter = lblName.Last();
            switch (lastLetter)
            {
                case '1':
                    if (chosenColor == Brushes.Red)
                    {
                        chosenColorCode1 = 1;
                    }
                    else if (chosenColor == Brushes.Yellow)
                    {
                        chosenColorCode1 = 2;
                    }
                    else if (chosenColor == Brushes.Orange)
                    {
                        chosenColorCode1 = 3;
                    }
                    else if (chosenColor == Brushes.White)
                    {
                        chosenColorCode1 = 4;
                    }
                    else if (chosenColor == Brushes.Green)
                    {
                        chosenColorCode1 = 5;
                    }
                    else if (chosenColor == Brushes.Blue)
                    {
                        chosenColorCode1 = 6;
                    }
                    break;
                case '2':
                    if (chosenColor == Brushes.Red)
                    {
                        chosenColorCode2 = 1;
                    }
                    else if (chosenColor == Brushes.Yellow)
                    {
                        chosenColorCode2 = 2;
                    }
                    else if (chosenColor == Brushes.Orange)
                    {
                        chosenColorCode2 = 3;
                    }
                    else if (chosenColor == Brushes.White)
                    {
                        chosenColorCode2 = 4;
                    }
                    else if (chosenColor == Brushes.Green)
                    {
                        chosenColorCode2 = 5;
                    }
                    else if (chosenColor == Brushes.Blue)
                    {
                        chosenColorCode2 = 6;
                    }
                    break;
                case '3':
                    if (chosenColor == Brushes.Red)
                    {
                        chosenColorCode3 = 1;
                    }
                    else if (chosenColor == Brushes.Yellow)
                    {
                        chosenColorCode3 = 2;
                    }
                    else if (chosenColor == Brushes.Orange)
                    {
                        chosenColorCode3 = 3;
                    }
                    else if (chosenColor == Brushes.White)
                    {
                        chosenColorCode3 = 4;
                    }
                    else if (chosenColor == Brushes.Green)
                    {
                        chosenColorCode3 = 5;
                    }
                    else if (chosenColor == Brushes.Blue)
                    {
                        chosenColorCode3 = 6;
                    }
                    break;
                case '4':
                    if (chosenColor == Brushes.Red)
                    {
                        chosenColorCode4 = 1;
                    }
                    else if (chosenColor == Brushes.Yellow)
                    {
                        chosenColorCode4 = 2;
                    }
                    else if (chosenColor == Brushes.Orange)
                    {
                        chosenColorCode4 = 3;
                    }
                    else if (chosenColor == Brushes.White)
                    {
                        chosenColorCode4 = 4;
                    }
                    else if (chosenColor == Brushes.Green)
                    {
                        chosenColorCode4 = 5;
                    }
                    else if (chosenColor == Brushes.Blue)
                    {
                        chosenColorCode4 = 6;
                    }
                    break;
                case '5':
                    if (chosenColor == Brushes.Red)
                    {
                        chosenColorCode5 = 1;
                    }
                    else if (chosenColor == Brushes.Yellow)
                    {
                        chosenColorCode5 = 2;
                    }
                    else if (chosenColor == Brushes.Orange)
                    {
                        chosenColorCode5 = 3;
                    }
                    else if (chosenColor == Brushes.White)
                    {
                        chosenColorCode5 = 4;
                    }
                    else if (chosenColor == Brushes.Green)
                    {
                        chosenColorCode5 = 5;
                    }
                    else if (chosenColor == Brushes.Blue)
                    {
                        chosenColorCode5 = 6;
                    }
                    break;
                case '6':
                    if (chosenColor == Brushes.Red)
                    {
                        chosenColorCode6 = 1;
                    }
                    else if (chosenColor == Brushes.Yellow)
                    {
                        chosenColorCode6 = 2;
                    }
                    else if (chosenColor == Brushes.Orange)
                    {
                        chosenColorCode6 = 3;
                    }
                    else if (chosenColor == Brushes.White)
                    {
                        chosenColorCode6 = 4;
                    }
                    else if (chosenColor == Brushes.Green)
                    {
                        chosenColorCode6 = 5;
                    }
                    else if (chosenColor == Brushes.Blue)
                    {
                        chosenColorCode6 = 6;
                    }
                    break;
            }

            chosenColor_Changed();
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            controlButton.IsDefault = true;

            if (!gameStarted)
            {
                players.Clear();
                InputPlayers();

                StartGame();
            }
        }

        private void StartGame()
        {
            int counter = 0;

            hintButton.IsEnabled=false;

            foreach (var player in players)
            {
                if (counter == playerCounter)
                {
                    namePlayer = player;
                    if (playerCounter < players.Count)
                    {
                        MessageBox.Show($"{namePlayer}, klik op OK als je klaar bent voor je spel\n\nJe hebt {chosenAttempts} pogingen", "Ben je klaar?");
                    }
                }
                else if (counter > playerCounter)
                {
                    nameNextPlayer = player;
                    break;
                }
                counter++;
            }

            if (playerCounter < players.Count)
            {
                playerCounter++;
                newGameButton.IsEnabled = false;
            }
            else
            {
                //newGameButton.IsEnabled = true;
                //return;
                int X = 0;
            }

            gameStarted = true;
            dissolved = false;

            settingsMenuItem.IsEnabled = false;

            chosenColor = Brushes.Red;
            chosenColor_Changed();

            // Code genereren

            attempts = 0;
            points = 100;
            penaltyPoints = 0;
            scoreLabel.Content = "";

            StartCountdown();

            debugStackPanel.Visibility = Visibility.Hidden;

            Random rnd = new Random();

            colorCode1 = rnd.Next(0, 24);
            colorCode2 = rnd.Next(0, 24);
            colorCode3 = rnd.Next(0, 24);
            colorCode4 = rnd.Next(0, 24);
            colorCode5 = rnd.Next(0, 24);
            colorCode6 = rnd.Next(0, 24);

            chosenColorCode1 = 0;
            chosenColorCode2 = 0;
            chosenColorCode3 = 0;
            chosenColorCode4 = 0;
            chosenColorCode5 = 0;
            chosenColorCode6 = 0;

            //chosenColor = Brushes.Transparent;

            switch (colorCode1 % 6)
            {
                case 0:
                    debugLabel1.Background = Brushes.Red;
                    colorCode1 = 1;
                    codeString = "Red ";
                    break;
                case 1:
                    debugLabel1.Background = Brushes.Yellow;
                    colorCode1 = 2;
                    codeString = "Yellow ";
                    break;
                case 2:
                    debugLabel1.Background = Brushes.Orange;
                    colorCode1 = 3;
                    codeString = "Orange ";
                    break;
                case 3:
                    debugLabel1.Background = Brushes.White;
                    colorCode1 = 4;
                    codeString = "White ";
                    break;
                case 4:
                    debugLabel1.Background = Brushes.Green;
                    colorCode1 = 5;
                    codeString = "Green ";
                    break;
                case 5:
                    debugLabel1.Background = Brushes.Blue;
                    colorCode1 = 6;
                    codeString = "Blue ";
                    break;
            }

            switch (colorCode2 % 6)
            {
                case 0:
                    debugLabel2.Background = Brushes.Red;
                    colorCode2 = 1;
                    codeString += "Red ";
                    break;
                case 1:
                    debugLabel2.Background = Brushes.Yellow;
                    colorCode2 = 2;
                    codeString += "Yellow ";
                    break;
                case 2:
                    debugLabel2.Background = Brushes.Orange;
                    colorCode2 = 3;
                    codeString += "Orange ";
                    break;
                case 3:
                    debugLabel2.Background = Brushes.White;
                    colorCode2 = 4;
                    codeString += "White ";
                    break;
                case 4:
                    debugLabel2.Background = Brushes.Green;
                    colorCode2 = 5;
                    codeString += "Green ";
                    break;
                case 5:
                    debugLabel2.Background = Brushes.Blue;
                    colorCode2 = 6;
                    codeString += "Blue ";
                    break;
            }

            switch (colorCode3 % 6)
            {
                case 0:
                    debugLabel3.Background = Brushes.Red;
                    colorCode3 = 1;
                    codeString += "Red ";
                    break;
                case 1:
                    debugLabel3.Background = Brushes.Yellow;
                    colorCode3 = 2;
                    codeString += "Yellow ";
                    break;
                case 2:
                    debugLabel3.Background = Brushes.Orange;
                    colorCode3 = 3;
                    codeString += "Orange ";
                    break;
                case 3:
                    debugLabel3.Background = Brushes.White;
                    colorCode3 = 4;
                    codeString += "White ";
                    break;
                case 4:
                    debugLabel3.Background = Brushes.Green;
                    colorCode3 = 5;
                    codeString += "Green ";
                    break;
                case 5:
                    debugLabel3.Background = Brushes.Blue;
                    colorCode3 = 6;
                    codeString += "Blue ";
                    break;
            }

            switch (colorCode4 % 6)
            {
                case 0:
                    debugLabel4.Background = Brushes.Red;
                    colorCode4 = 1;
                    codeString += "Red";
                    break;
                case 1:
                    debugLabel4.Background = Brushes.Yellow;
                    colorCode4 = 2;
                    codeString += "Yellow";
                    break;
                case 2:
                    debugLabel4.Background = Brushes.Orange;
                    colorCode4 = 3;
                    codeString += "Orange";
                    break;
                case 3:
                    debugLabel4.Background = Brushes.White;
                    colorCode4 = 4;
                    codeString += "White";
                    break;
                case 4:
                    debugLabel4.Background = Brushes.Green;
                    colorCode4 = 5;
                    codeString += "Green";
                    break;
                case 5:
                    debugLabel4.Background = Brushes.Blue;
                    colorCode4 = 6;
                    codeString += "Blue";
                    break;
            }

            if(amount >= 5)
            {
                switch (colorCode5 % 6)
                {
                    case 0:
                        debugLabel5.Background = Brushes.Red;
                        colorCode5 = 1;
                        codeString += "Red";
                        break;
                    case 1:
                        debugLabel5.Background = Brushes.Yellow;
                        colorCode5 = 2;
                        codeString += "Yellow";
                        break;
                    case 2:
                        debugLabel5.Background = Brushes.Orange;
                        colorCode5 = 3;
                        codeString += "Orange";
                        break;
                    case 3:
                        debugLabel5.Background = Brushes.White;
                        colorCode5 = 4;
                        codeString += "White";
                        break;
                    case 4:
                        debugLabel5.Background = Brushes.Green;
                        colorCode5 = 5;
                        codeString += "Green";
                        break;
                    case 5:
                        debugLabel5.Background = Brushes.Blue;
                        colorCode5 = 6;
                        codeString += "Blue";
                        break;
                }
            }
            
            if(amount == 6)
            {
                switch (colorCode6 % 6)
                {
                    case 0:
                        debugLabel6.Background = Brushes.Red;
                        colorCode6 = 1;
                        codeString += "Red";
                        break;
                    case 1:
                        debugLabel6.Background = Brushes.Yellow;
                        colorCode6 = 2;
                        codeString += "Yellow";
                        break;
                    case 2:
                        debugLabel6.Background = Brushes.Orange;
                        colorCode6 = 3;
                        codeString += "Orange";
                        break;
                    case 3:
                        debugLabel6.Background = Brushes.White;
                        colorCode6 = 4;
                        codeString += "White";
                        break;
                    case 4:
                        debugLabel6.Background = Brushes.Green;
                        colorCode6 = 5;
                        codeString += "Green";
                        break;
                    case 5:
                        debugLabel6.Background = Brushes.Blue;
                        colorCode6 = 6;
                        codeString += "Blue";
                        break;
                }
            }
            

            counter = 0;

            foreach (var item in seriesStackPanel.Children)
            {
                if (item is StackPanel stack)
                {
                    counter = 0;

                    if (stack.Visibility == Visibility.Visible)
                    {
                        foreach (var item2 in stack.Children)
                        {
                            if (counter > 0)
                            {
                                if (item2 is Label lbl)
                                {
                                    lbl.Background = Brushes.DarkGray;
                                    lbl.BorderBrush = Brushes.Transparent;
                                    lbl.ToolTip = "Foute kleur";
                                }
                            }
                            counter++;
                        }
                    }
                }
            }

            controlButton.IsEnabled = true;

            // StackPanels Visible maken

            serie1StackPanel.Visibility = Visibility.Visible;
            serie2StackPanel.Visibility = Visibility.Hidden;
            serie3StackPanel.Visibility = Visibility.Hidden;
            serie4StackPanel.Visibility = Visibility.Hidden;
            serie5StackPanel.Visibility = Visibility.Hidden;
            serie6StackPanel.Visibility = Visibility.Hidden;
            serie7StackPanel.Visibility = Visibility.Hidden;
            serie8StackPanel.Visibility = Visibility.Hidden;
            serie9StackPanel.Visibility = Visibility.Hidden;
            serie10StackPanel.Visibility = Visibility.Hidden;
            serie11StackPanel.Visibility = Visibility.Collapsed;
            serie12StackPanel.Visibility = Visibility.Collapsed;
            serie13StackPanel.Visibility = Visibility.Collapsed;
            serie14StackPanel.Visibility = Visibility.Collapsed;
            serie15StackPanel.Visibility = Visibility.Collapsed;
            serie16StackPanel.Visibility = Visibility.Collapsed;
            serie17StackPanel.Visibility = Visibility.Collapsed;
            serie18StackPanel.Visibility = Visibility.Collapsed;
            serie19StackPanel.Visibility = Visibility.Collapsed;
            serie20StackPanel.Visibility = Visibility.Collapsed;
        }

        private void controlButton_Click(object sender, RoutedEventArgs e)
        {
            // Resetten booleans

            bool colorPosition1 = false;
            bool colorPosition2 = false;
            bool colorPosition3 = false;
            bool colorPosition4 = false;
            bool colorPosition5 = false;
            bool colorPosition6 = false;

            bool color1 = false;
            bool color2 = false;
            bool color3 = false;
            bool color4 = false;
            bool color5 = false;
            bool color6 = false;

            penaltyPoints = 0;

            if (!gameStarted)
            {
                return;
            }

            hintButton.IsEnabled = true;

            if (amount == 4)
            {
                chosenColorCode5 = 1;
                chosenColorCode6 = 1;
                
                debugLabel5.Visibility = Visibility.Collapsed;
                debugLabel6.Visibility = Visibility.Collapsed;
            }
            else if(amount == 5)
            {
                chosenColorCode6 = 1;

                debugLabel5.Visibility = Visibility.Visible;
                debugLabel6.Visibility = Visibility.Collapsed;
            }
            else if(amount == 6)
            {
                debugLabel5.Visibility = Visibility.Visible;
                debugLabel6.Visibility = Visibility.Visible;
            }

            //Controle of elke positie een kleur heeft

            if (chosenColorCode1 > 0 &&
                chosenColorCode2 > 0 &&
                chosenColorCode3 > 0 &&
                chosenColorCode4 > 0 &&
                chosenColorCode5 > 0 &&
                chosenColorCode6 > 0)
            {
                StopCountdown();

                //Controleren juiste kleur op juiste plaats

                if (colorCode1 == chosenColorCode1)
                {
                    colorPosition1 = true;
                    color1 = true;
                }

                if (colorCode2 == chosenColorCode2)
                {
                    colorPosition2 = true;
                    color2 = true;
                }

                if (colorCode3 == chosenColorCode3)
                {
                    colorPosition3 = true;
                    color3 = true;
                }

                if (colorCode4 == chosenColorCode4)
                {
                    colorPosition4 = true;
                    color4 = true;
                }

                if (colorCode5 == chosenColorCode5)
                {
                    colorPosition5 = true;
                    color5 = true;
                }
                if (colorCode6 == chosenColorCode6)
                {
                    colorPosition6 = true;
                    color6 = true;
                }

                if (colorPosition1 == true)
                {
                    ChangeBorder(attempts, 1, 1);
                }
                if (colorPosition2 == true)
                {
                    ChangeBorder(attempts, 2, 1);
                }
                if (colorPosition3 == true)
                {
                    ChangeBorder(attempts, 3, 1);
                }
                if (colorPosition4 == true)
                {
                    ChangeBorder(attempts, 4, 1);
                }
                if (colorPosition5 == true)
                {
                    ChangeBorder(attempts, 5, 1);
                }
                if (colorPosition6 == true)
                {
                    ChangeBorder(attempts, 6, 1);
                }

                if (amount >= 5)
                {
                    if (colorPosition5 == true)
                    {
                        ChangeBorder(attempts, 5, 1);
                    }
                }
                else
                {
                    colorPosition5 = true;
                    color5= true;
                }

                if(amount >= 6)
                {
                    if (colorPosition6 == true)
                    {
                        ChangeBorder(attempts, 6, 1);
                    }
                }
                else
                {
                    colorPosition6 = true;
                    color6 = true;
                }
                

                if (colorPosition1 && colorPosition2 && colorPosition3 && colorPosition4 && colorPosition5 && colorPosition6)
                {
                    // Gewonnen spel

                    hintButton.IsEnabled = false;

                    attempts++;
                    scoreLabel.Content = $"{namePlayer} : Poging {attempts}/{chosenAttempts} Score = {points}";
                    debugStackPanel.Visibility = Visibility.Visible;
                    gameStarted = false;
                    dissolved = true;

                    settingsMenuItem.IsEnabled = true;

                    for (int i = 0; i < highScores.Length; i++)
                    {
                        if (highScores[i] == null)
                        {
                            highScores[i] = $"{namePlayer} - {attempts} pogingen - {points}/100";
                            break;
                        }
                    }

                    if (playerCounter < players.Count)
                    {
                        MessageBox.Show($"Proficiat! {namePlayer}\n\nJe hebt de code gekraakt in {attempts} pogingen\n\tmet {points} op 100\n\nNu is {nameNextPlayer} aan de beurt", $"{namePlayer}", MessageBoxButton.OK, MessageBoxImage.Information);
                        StartGame();
                    }
                    else
                    {
                        MessageBox.Show($"Proficiat! {namePlayer}\n\nJe hebt de code gekraakt in {attempts} pogingen\n\tmet {points} op 100", $"{namePlayer}", MessageBoxButton.OK, MessageBoxImage.Information);
                        MessageBoxResult result = MessageBox.Show("Wil je dezelfde spelreeks nog eens starten?", "Spelreeks ten einde", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            result = MessageBoxResult.Cancel;
                            if(chosenAttempts != 10)
                            {
                                result = MessageBox.Show("Wil je het aantal pogingen wijzigen?","Aantal pogingen wijzigen?",MessageBoxButton.YesNo,MessageBoxImage.Question);

                                if(result == MessageBoxResult.Yes)
                                {
                                    ChangeAttempts();
                                }
                            }
                            
                            playerCounter = 0;

                            StartGame();
                        }
                        else
                        {
                            newGameButton.IsEnabled = true;
                            return;
                        }
                    }
                }
                else
                {
                    // Checken of de kleur ergens anders voorkomt

                    if (colorPosition1 == false)
                    {
                        if (chosenColorCode1 == colorCode2 && color2 == false)
                        {
                            ChangeBorder(attempts, 1);
                            color2 = true;
                        }
                        else if (chosenColorCode1 == colorCode3 && color3 == false)
                        {
                            ChangeBorder(attempts, 1);
                            color3 = true;
                        }
                        else if (chosenColorCode1 == colorCode4 && color4 == false)
                        {
                            ChangeBorder(attempts, 1);
                            color4 = true;
                        }
                        else if (chosenColorCode1 == colorCode5 && color5 == false)
                        {
                            ChangeBorder(attempts, 1);
                            color5 = true;
                        }
                        else if (chosenColorCode1 == colorCode6 && color6 == false)
                        {
                            ChangeBorder(attempts, 1);
                            color6 = true;
                        }
                    }

                    if (colorPosition2 == false)
                    {
                        if (chosenColorCode2 == colorCode1 && color1 == false)
                        {
                            ChangeBorder(attempts, 2);
                            color1 = true;
                        }
                        else if (chosenColorCode2 == colorCode3 && color3 == false)
                        {
                            ChangeBorder(attempts, 2);
                            color3 = true;
                        }
                        else if (chosenColorCode2 == colorCode4 && color4 == false)
                        {
                            ChangeBorder(attempts, 2);
                            color4 = true;
                        }
                        else if (chosenColorCode2 == colorCode5 && color5 == false)
                        {
                            ChangeBorder(attempts, 2);
                            color5 = true;
                        }
                        else if (chosenColorCode2 == colorCode6 && color6 == false)
                        {
                            ChangeBorder(attempts, 2);
                            color6 = true;
                        }
                    }

                    if (colorPosition3 == false)
                    {
                        if (chosenColorCode3 == colorCode1 && color1 == false)
                        {
                            ChangeBorder(attempts, 3);
                            color1 = true;
                        }
                        else if (chosenColorCode3 == colorCode2 && color2 == false)
                        {
                            ChangeBorder(attempts, 3);
                            color2 = true;
                        }
                        else if (chosenColorCode3 == colorCode4 && color4 == false)
                        {
                            ChangeBorder(attempts, 3);
                            color4 = true;
                        }
                        else if (chosenColorCode3 == colorCode5 && color5 == false)
                        {
                            ChangeBorder(attempts, 3);
                            color5 = true;
                        }
                        else if (chosenColorCode3 == colorCode6 && color6 == false)
                        {
                            ChangeBorder(attempts, 3);
                            color6 = true;
                        }
                    }

                    if (colorPosition4 == false)
                    {
                        if (chosenColorCode4 == colorCode1 && color1 == false)
                        {
                            ChangeBorder(attempts, 4);
                            color1 = true;
                        }
                        else if (chosenColorCode4 == colorCode2 && color2 == false)
                        {
                            ChangeBorder(attempts, 4);
                            color2 = true;
                        }
                        else if (chosenColorCode4 == colorCode3 && color3 == false)
                        {
                            ChangeBorder(attempts, 4);
                            color3 = true;
                        }
                        else if (chosenColorCode4 == colorCode5 && color5 == false)
                        {
                            ChangeBorder(attempts, 4);
                            color5 = true;
                        }
                        else if (chosenColorCode4 == colorCode6 && color6 == false)
                        {
                            ChangeBorder(attempts, 4);
                            color6 = true;
                        }
                    }

                    if (colorPosition5 == false)
                    {
                        if (chosenColorCode5 == colorCode1 && color1 == false)
                        {
                            ChangeBorder(attempts, 5);
                            color1 = true;
                        }
                        else if (chosenColorCode5 == colorCode2 && color2 == false)
                        {
                            ChangeBorder(attempts, 5);
                            color2 = true;
                        }
                        else if (chosenColorCode5 == colorCode3 && color3 == false)
                        {
                            ChangeBorder(attempts, 5);
                            color3 = true;
                        }
                        else if (chosenColorCode5 == colorCode4 && color4 == false)
                        {
                            ChangeBorder(attempts, 5);
                            color4 = true;
                        }
                        else if (chosenColorCode5 == colorCode6 && color6 == false)
                        {
                            ChangeBorder(attempts, 5);
                            color6 = true;
                        }
                    }

                    if (colorPosition6 == false)
                    {
                        if (chosenColorCode6 == colorCode1 && color1 == false)
                        {
                            ChangeBorder(attempts, 6);
                            color1 = true;
                        }
                        else if (chosenColorCode6 == colorCode2 && color2 == false)
                        {
                            ChangeBorder(attempts, 6);
                            color2 = true;
                        }
                        else if (chosenColorCode6 == colorCode3 && color3 == false)
                        {
                            ChangeBorder(attempts, 6);
                            color3 = true;
                        }
                        else if (chosenColorCode6 == colorCode4 && color4 == false)
                        {
                            ChangeBorder(attempts, 6);
                            color4 = true;
                        }
                        else if (chosenColorCode6 == colorCode5 && color5 == false)
                        {
                            ChangeBorder(attempts, 6);
                            color5 = true;
                        }
                    }

                    if (color1 && !colorPosition1)
                    {
                        penaltyPoints++;
                    }

                    if (color2 && !colorPosition2)
                    {
                        penaltyPoints++;
                    }

                    if (color3 && !colorPosition3)
                    {
                        penaltyPoints++;
                    }

                    if (color4 && !colorPosition4)
                    {
                        penaltyPoints++;
                    }

                    if (color5 && !colorPosition5)
                    {
                        penaltyPoints++;
                    }

                    if (color6 && !colorPosition6)
                    {
                        penaltyPoints++;
                    }

                    if (!color1)
                    {
                        penaltyPoints += 2;
                    }

                    if (!color2)
                    {
                        penaltyPoints += 2;
                    }

                    if (!color3)
                    {
                        penaltyPoints += 2;
                    }

                    if (!color4)
                    {
                        penaltyPoints += 2;
                    }

                    if (!color5)
                    {
                        penaltyPoints += 2;
                    }

                    if (!color6)
                    {
                        penaltyPoints += 2;
                    }

                    points -= penaltyPoints;

                    attempts++;

                    scoreLabel.Content = $"{namePlayer} : Poging {attempts}/{chosenAttempts} Score = {points}";

                    if (attempts < chosenAttempts)
                    {
                        // Als attempst aangepast wordt, moet ook de StackPanels aangepast worden
                        // Hier en in timer_Tick
                        makeStackPanelVisible();
                    }
                    else
                    {
                        // Spel einde na {chosenAttempts} beurten

                        gameStarted = false;

                        debugStackPanel.Visibility = Visibility.Visible;
                        settingsMenuItem.IsEnabled = true;

                        if (playerCounter < players.Count)
                        {
                            MessageBox.Show($"You failed! De correcte code was {codeString}.\nNu is speler {nameNextPlayer} aan de beurt", $"{namePlayer}", MessageBoxButton.OK);

                            StartGame();
                        }
                        else
                        {
                            MessageBoxResult result = MessageBox.Show($"You failed! De correcte code was {codeString}.\nSpelreeks ten einde\n\nWil je nog een reeks spelen?", $"{namePlayer}", MessageBoxButton.YesNo, MessageBoxImage.Question);
                            
                            if (result == MessageBoxResult.Yes)
                            {
                                result = MessageBoxResult.Cancel;
                                if (chosenAttempts != 10)
                                {
                                    result = MessageBox.Show("Wil je het aantal pogingen wijzigen?", "Aantal pogingen wijzigen?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                                    if (result == MessageBoxResult.Yes)
                                    {
                                        ChangeAttempts();
                                    }
                                }

                                playerCounter = 0;

                                StartGame();
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Maak een keuze voor alle vakken", "Keuze");
            }
        }

        private void ChangeAttempts()
        {
            string temp = "";
            do
            {
                amountWindow instAmountWindow = new amountWindow();

                instAmountWindow.amountTextBox.Focus();
                instAmountWindow.ShowDialog();

                temp = instAmountWindow.amountTextBox.Text;

            } while (!int.TryParse(temp, out chosenAttempts) || chosenAttempts < 3 || chosenAttempts > 20);

            //playerCounter = 0;
        }

        private void ChangeBorder(int attempst, int code, int colorPositie = 0)
        {
            int counter = 1;

            foreach (var item in seriesStackPanel.Children)
            {
                if (item is StackPanel stack)
                {
                    if (counter == attempst + 1)
                    {
                        int counter2 = 0;
                        foreach (var item2 in stack.Children)
                        {
                            if (counter2 == code)
                            {
                                if (item2 is Label lbl)
                                {
                                    if (colorPositie == 1)
                                    {
                                        lbl.BorderBrush = Brushes.DarkRed;
                                        lbl.ToolTip = "\"Juiste kleur, juiste positie\"";
                                    }
                                    else
                                    {
                                        lbl.BorderBrush = Brushes.Wheat;
                                        lbl.ToolTip = "\"Juiste kleur, foute positie\"";
                                    }
                                    return;
                                }

                            }
                            counter2++;
                        }
                    }
                    counter++;
                }
            }
        }

        private void makeStackPanelVisible()
        {
            chosenColorCode1 = 0;
            chosenColorCode2 = 0;
            chosenColorCode3 = 0;
            chosenColorCode4 = 0;
            chosenColorCode5 = 0;
            chosenColorCode6 = 0;

            switch (attempts)
            {
                case 1:
                    serie2StackPanel.Visibility = Visibility.Visible;
                    hintButton.Visibility = Visibility.Visible;
                    break;
                case 2:
                    serie3StackPanel.Visibility = Visibility.Visible;
                    break;
                case 3:
                    serie4StackPanel.Visibility = Visibility.Visible;
                    break;
                case 4:
                    serie5StackPanel.Visibility = Visibility.Visible;
                    break;
                case 5:
                    serie6StackPanel.Visibility = Visibility.Visible;
                    break;
                case 6:
                    serie7StackPanel.Visibility = Visibility.Visible;
                    break;
                case 7:
                    serie8StackPanel.Visibility = Visibility.Visible;
                    break;
                case 8:
                    serie9StackPanel.Visibility = Visibility.Visible;
                    break;
                case 9:
                    serie10StackPanel.Visibility = Visibility.Visible;
                    break;
                case 10:
                    serie1StackPanel.Visibility = Visibility.Collapsed;
                    serie11StackPanel.Visibility = Visibility.Visible;
                    break;
                case 11:
                    serie2StackPanel.Visibility = Visibility.Collapsed;
                    serie12StackPanel.Visibility = Visibility.Visible;
                    break;
                case 12:
                    serie3StackPanel.Visibility = Visibility.Collapsed;
                    serie13StackPanel.Visibility = Visibility.Visible;
                    break;
                case 13:
                    serie4StackPanel.Visibility = Visibility.Collapsed;
                    serie14StackPanel.Visibility = Visibility.Visible;
                    break;
                case 14:
                    serie5StackPanel.Visibility = Visibility.Collapsed;
                    serie15StackPanel.Visibility = Visibility.Visible;
                    break;
                case 15:
                    serie6StackPanel.Visibility = Visibility.Collapsed;
                    serie16StackPanel.Visibility = Visibility.Visible;
                    break;
                case 16:
                    serie7StackPanel.Visibility = Visibility.Collapsed;
                    serie17StackPanel.Visibility = Visibility.Visible;
                    break;
                case 17:
                    serie8StackPanel.Visibility = Visibility.Collapsed;
                    serie18StackPanel.Visibility = Visibility.Visible;
                    break;
                case 18:
                    serie9StackPanel.Visibility = Visibility.Collapsed;
                    serie19StackPanel.Visibility = Visibility.Visible;
                    break;
                case 19:
                    serie10StackPanel.Visibility = Visibility.Collapsed;
                    serie20StackPanel.Visibility = Visibility.Visible;
                    break;
            }

            if (debugStackPanel.Visibility == Visibility.Hidden)
            {
                StartCountdown();
            }
        }

        private void mastermindWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F12 && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                ToggleDebug();
            }
            else if (e.Key == Key.Enter)
            {
                controlButton_Click(null, null);
            }
        }

        ///<summary>
        ///Make the code visible or hidden
        /// </summary>
        private void ToggleDebug()
        {

            if (debugStackPanel.Visibility == Visibility.Visible)
            {
                debugStackPanel.Visibility = Visibility.Hidden;
                StartCountdown();
            }
            else
            {
                debugStackPanel.Visibility = Visibility.Visible;
                StopCountdown();
            }
        }

        private void mastermindWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (gameStarted == true)
            {
                StopCountdown();

                MessageBoxResult result = MessageBox.Show("Wilt u het spel vroegtijdig beëindigen?", $"Poging {attempts + 1}/10", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);

                if (result == MessageBoxResult.No)
                {
                    if (debugStackPanel.Visibility == Visibility.Hidden)
                    {
                        StartCountdown();
                    }

                    e.Cancel = true;
                }
            }
        }

        private void InputPlayers()
        {
            //string name = "";
            MessageBoxResult result;
            do
            {
                playerWindow instPlayerWindow = new playerWindow();

                instPlayerWindow.playerTextBox.Focus();
                instPlayerWindow.ShowDialog();

                if (instPlayerWindow.playerTextBox.Text.Length > 1)
                {
                    players.Add(instPlayerWindow.playerTextBox.Text);
                }

                if (players.Count < 1)
                {
                    MessageBox.Show("Je moet minimum 1 speler ingeven","Minimum 1 speler",MessageBoxButton.OK,MessageBoxImage.Warning);
                    result = MessageBoxResult.Yes;
                }
                else
                {
                    result = MessageBox.Show("Wil je nog een speler toevoegen?", "Speler toevoegen", MessageBoxButton.YesNo, MessageBoxImage.Question);
                }
                
            } while (result == MessageBoxResult.Yes);

            playerCounter = 0;
        }

        private void closeGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void highScoresMenuItem_Click(object sender, RoutedEventArgs e)
        {
            string highScoresString = "";

            for (int i = 0; i < highScores.Length; i++)
            {
                if (highScores[i] != null)
                {
                    highScoresString += highScores[i] + "\n";
                }
                else
                {
                    break;
                }
            }
            MessageBox.Show(highScoresString, "Mastermind highscores");
        }

        private void chosenAttemptsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // ToDo Enkel ingave van cijfers toestaan
            // Gaat niet met InputBox, Nieuw window met - ShowModal -

            ChangeAttempts();

        }

        private void newGameMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void hintButton_Click(object sender, RoutedEventArgs e)
        {
            StopCountdown();

            penaltyPoints = 0;

            hintWindow instHint = new hintWindow();
            instHint.ShowDialog();

            if (instHint.hintColorLabel.Background != Brushes.DarkGray && instHint.colorRadioButton.IsChecked == true)
            {
                penaltyPoints = 15;

                points -= penaltyPoints;

                scoreLabel.Content = $"{namePlayer} : Poging {attempts}/{chosenAttempts} Score = {points}";

                hintButton.IsEnabled = false;
            }

            if(instHint.positionRadioButton.IsChecked == true && (instHint.hint1Label.Background != Brushes.DarkGray ||
                                                                  instHint.hint2Label.Background != Brushes.DarkGray ||
                                                                  instHint.hint3Label.Background != Brushes.DarkGray ||
                                                                  instHint.hint4Label.Background != Brushes.DarkGray ||
                                                                  instHint.hint5Label.Background != Brushes.DarkGray ||
                                                                  instHint.hint6Label.Background != Brushes.DarkGray))
            {
                penaltyPoints = 25;

                points -= penaltyPoints;

                scoreLabel.Content = $"{namePlayer} : Poging {attempts}/{chosenAttempts} Score = {points}";

                hintButton.IsEnabled = false;
            }

            if (debugStackPanel.Visibility == Visibility.Hidden)
            {
                StartCountdown();
            }
        }

        private void choseCountCodeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            do
            {
                int.TryParse(Interaction.InputBox("Hoeveel code vakken wil je? 4, 5 of 6", "Code vakken", "4", 20, 20), out amount);
            } while (amount < 4 || amount > 6);

            generateLabels(amount);
        }
    }
}