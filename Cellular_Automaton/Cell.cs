using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cellular_Automaton
{
    class Cell:Button
    {
        public int X { get; }
        public int Y { get; }
        private bool isAlive;

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }

            set
            {
                isAlive = value;
                if(isAlive==true)
                {
                    Background = Brushes.Gold;
                }
                else
                {
                    Background = Brushes.DarkGray;
                }
            }
        }

        public bool DeathStranding { get; set; }
        public bool ComeAlive { get; set; }
        

        public Cell(int x,int y)
        {
            X = x;
            Y = y;

            Background = Brushes.DarkGray;
        }
    }
}
