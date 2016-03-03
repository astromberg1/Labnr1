using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameoflife2
{
    class Grid
    {
        Random random = new Random();

        public static int columns = 20;
        public static int rows = 20;
        bool[,] mainGrid = new bool[rows, columns];
        bool[,] mainGrid1 = new bool[rows, columns];
        public Grid()
        {
            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    int randomNumber = random.Next(0, 2);
                    if (randomNumber == 1)
                    {
                        mainGrid1[x, y] = true;
                    }
                }
            }

                    //mainGrid[0, 4] = true;
                    //mainGrid[0, 5] = true;
                    //mainGrid[0, 6] = true;
                    //mainGrid[1, 5] = true;
                    //mainGrid[2, 5] = true;
                    //mainGrid[3, 5] = true;
                    //mainGrid[1, 4] = true;
                    //mainGrid[2, 5] = true;
                    //mainGrid[2, 7] = true;
                    //mainGrid[17, 19] = true;
                    //mainGrid[11, 13] = true;
                    //mainGrid[6, 6] = true;

                }
        public int CountNeighbours(int x, int y)
        {
            int countNeighbours = 0;

            for (int neighbourX = x - 1; neighbourX <= x + 1; neighbourX++)
            {
                for (int neighbourY = y - 1; neighbourY <= y + 1; neighbourY++)
                {
                    if (neighbourY < 0 || neighbourY >= columns || neighbourX < 0 || neighbourX >= rows)
                    {
                        continue;
                    }
                    if (neighbourX == x && neighbourY == y)
                    {
                        continue;
                    }
                    if(mainGrid[neighbourX,neighbourY])
                    {
                        countNeighbours++;
                    }
                }
            }
            return countNeighbours;
        }
        public void GenerateNew()
        {
            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    int numberofNeighbours = CountNeighbours(x, y);
                    bool isAlive = mainGrid[x, y];

                    if(isAlive)
                    {
                        if (numberofNeighbours < 2 || numberofNeighbours > 3)
                        {
                            mainGrid1[x, y] = false;
                        }
                    }

                    else
                    {
                        if (numberofNeighbours == 3)
                        {
                            mainGrid1[x,y] = true;
                        }
                    }

                }
            }
            //Todo Clona matris, lite snyggare än se nedan 

         // " ", backcolor som  

        }
        public void Print()
        {
            Console.Clear();
            for (int y = 0; y < columns; y++)
            {
                for (int x = 0; x < rows; x++)
                {
                    mainGrid[x, y]= mainGrid1[x, y]; // traditionell element kopiering fungerar alltid
                    bool cell = mainGrid[x, y];
                    if (cell)
                    {
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
