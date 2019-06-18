using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Threading;

namespace Cellular_Automaton
{
    class Game
    {
        private Grid gameGrid;
        private Cell[,] cells;
        private DispatcherTimer timer;
        private int height;
        private int width;
        private bool started;
        public event EventHandler GenerationsCount;

        public Game(int height, int width, Grid gameGrid)
        {
            this.gameGrid = gameGrid;
            this.height = height;
            this.width = width;
            cells = new Cell[height, width];
            CreateGameField(height, width);
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(On_Timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(500);
            started = false;
        }

        private void On_Timer_Tick(object sender, EventArgs e)
        {
            CheckGeneration();
            ChangeState();
            GenerationsCount?.Invoke(this, new EventArgs());
        }

        private void ChangeState()
        {
            foreach(Cell cell in cells)
            {
                if (cell.DeathStranding)
                {
                    cell.IsAlive = false;
                    cell.DeathStranding = false;
                }

                if (cell.ComeAlive)
                {
                    cell.IsAlive = true;
                    cell.ComeAlive = false;
                }
            }
        }

        private void CreateGameField(int h, int w)
        {
            for(int i=0;i<h;i++)
            {
                gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
                gameGrid.RowDefinitions.Add(new RowDefinition());

                for(int j=0;j<w;j++)
                {
                    Cell cell = new Cell(i, j);
                    //cell.IsMouseDirectlyOverChanged += new DependencyPropertyChangedEventHandler(Cell_Click);
                    cell.Click += new RoutedEventHandler(Cell_Click);
                    Grid.SetColumn(cell, i);
                    Grid.SetRow(cell, j);
                    gameGrid.Children.Add(cell);
                    cells[i, j] = cell;
                }
            }
        }

        private void Cell_Click(object sender, RoutedEventArgs e)
        {
            Cell cell = (Cell)sender;

            
                cell.IsAlive = true;
            
            
        }

        public void Clear()
        {
            foreach(Cell cell in cells)
            {
                cell.IsAlive = false;
                cell.DeathStranding = false;
                cell.ComeAlive = false;
            }
        }

        public string Start()
        {
            if(!started)
            {
                timer.Start();
                started = true;
                return "Stop";
            }
            else
            {
                timer.Stop();
                started = false;
                return "Start";
            }
            
        }

        private void CheckGeneration()
        {
            

            foreach(Cell cell in cells)
            {
                int counter = 0;

                if (CheckNeighbor(cell.X - 1, cell.Y - 1)) counter++;
                if (CheckNeighbor(cell.X - 0, cell.Y - 1)) counter++;
                if (CheckNeighbor(cell.X + 1, cell.Y - 1)) counter++;
                if (CheckNeighbor(cell.X - 1, cell.Y - 0)) counter++;
                if (CheckNeighbor(cell.X + 1, cell.Y - 0)) counter++;
                if (CheckNeighbor(cell.X - 1, cell.Y + 1)) counter++;
                if (CheckNeighbor(cell.X - 0, cell.Y + 1)) counter++;
                if (CheckNeighbor(cell.X + 1, cell.Y + 1)) counter++;

                if(cell.IsAlive)
                {
                    if (counter < 2) cell.DeathStranding = true;
                    if (counter > 3) cell.DeathStranding = true;
                    if (counter == 2 || counter == 3) cell.DeathStranding = false;
                }
                if(!cell.IsAlive)
                {
                    if (counter == 3) cell.ComeAlive = true;
                }
                
            }
        }

        

        private bool CheckNeighbor(int x, int y)
        {
            if(x>=0 && x<height)
            {
                if(y>=0 && y<width)
                {
                    return cells[x, y].IsAlive;
                }
            }

            return false;
        }
    }
}
