using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitConsoleAppTest
{
    class Program
    {

        public struct Point {
            public int x, y;
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public Boolean isValid()
            {
                return Program.isValid(this.x, this.y);
            }

            public List<Point> adjacent()
            {
                List<Point> lp = new List<Point>();
                lp.Add(new Point(this.x - 1, this.y));
                lp.Add(new Point(this.x + 1, this.y));
                lp.Add(new Point(this.x, this.y - 1));
                lp.Add(new Point(this.x, this.y + 1));
                return lp;
            }

        }


        private const int MAZE_SIZE_X = 20, MAZE_SIZE_Y = 20;
        
        public static bool isValid(int x, int y)
        {
            if(x >= 0 && y >= 0)
            {
                if(x < MAZE_SIZE_X && y < MAZE_SIZE_Y)
                {
                    return true;
                }
            }
            return false;
        }

        

        public static void wall(ref bool[,] discovered, Point p, Point lp)
        {
            List<Point> edges = p.adjacent();
            edges.Shuffle();
            foreach(Point pp in edges)
            {
                if(pp.isValid())
                {
                    bool a = discovered[pp.x, pp.y];
                    if(a)
                    {
                        if(lp.x == -1 && lp.y == -1)
                        {
                            return;
                        } else if(pp.x == lp.x && pp.y == lp.y)
                        {
                            continue;
                        } else
                        {
                            return;
                        }
                    }
                }
            }

            discovered[p.x, p.y] = true;

            foreach(Point pa in edges)
            {
                if(pa.isValid())
                {
                    if(!discovered[pa.x, pa.y])
                    {
                        wall(ref discovered, pa, p);
                    }
                }
            }
        }



        static void Main(string[] args)
        {
            bool[,] discovered = new bool[MAZE_SIZE_X,MAZE_SIZE_Y];
            
            for(int i = 0; i < MAZE_SIZE_X; i++)
            {
                for(int j = 0; j < MAZE_SIZE_Y; j++)
                {
                    discovered[i, j] = false;
                }
            }

            wall(ref discovered, new Point(0, 0), new Point(-1, -1));

            for (int i = 0; i < MAZE_SIZE_X; i++)
            {
                for (int j = 0; j < MAZE_SIZE_Y; j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(discovered[i,j] ? "." : "#");
                }
            }


            Console.ReadLine();
        }



    }
}
