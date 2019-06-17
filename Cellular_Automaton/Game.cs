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

        public Game(int height, int width, Grid gameGrid)
        {
            this.gameGrid = gameGrid;
            this.height = height;
            this.width = width;
            cells = new Cell[height, width];
            CreateGameField(height, width);
            timer = new DispatcherTimer();
            timer.Tick += (sender, e) => { Generate(); };
            timer.Interval = TimeSpan.FromMilliseconds(500);
            started = false;
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

        private void Generate()
        {
            

            foreach(Cell cell in cells)
            {
                int counter = 0;

                if (CheckNeighbor(cell.X - 1, cell.Y - 1)) counter++;
                if (CheckNeighbor(cell.X - 0, cell.Y - 1)) counter++;
                if (CheckNeighbor(cell.X + 1, cell.Y - 1)) counter++;
                if (CheckNeighbor(cell.X - 1, cell.Y - 0)) counter++;
                if (CheckNeighbor(cell.X - 0, cell.Y - 0)) counter++;
                if (CheckNeighbor(cell.X + 1, cell.Y - 0)) counter++;
                if (CheckNeighbor(cell.X - 1, cell.Y + 1)) counter++;
                if (CheckNeighbor(cell.X - 0, cell.Y + 1)) counter++;
                if (CheckNeighbor(cell.X + 1, cell.Y + 1)) counter++;

                if(cell.IsAlive)
                {
                    if (counter < 2) cell.IsAlive = false;
                    if (counter > 3) cell.IsAlive = false;
                    if (counter == 2 || counter == 3) cell.IsAlive = true;
                }
                if(!cell.IsAlive)
                {
                    if (counter == 3) cell.IsAlive = true;
                }
                
            }
        }

        

        private bool CheckNeighbor(int x, int y)
        {
            if(x>=0 && x<width)
            {
                if(y>=0 && y<height)
                {
                    return cells[x, y].IsAlive;
                }
            }

            return false;
        }
    }
}
