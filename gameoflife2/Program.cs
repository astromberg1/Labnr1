﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameoflife2
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid myGrid = new Grid();
            while (true)
            {
                myGrid.Print();
                Console.ReadKey();
                myGrid.GenerateNew();
                
            }
                
        }
    }
}
