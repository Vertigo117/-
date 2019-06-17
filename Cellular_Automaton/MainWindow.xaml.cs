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

namespace Cellular_Automaton
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game game;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game(40, 40, gameField);
            startButton.Click += new RoutedEventHandler(Start_Button_Click);
            clearButton.Click += new RoutedEventHandler(Clear_Button_Click);
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            game.Clear();
        }

        private void Start_Button_Click(object sender, RoutedEventArgs e)
        {
            startButton.Content = game.Start();
        }
    }
}
