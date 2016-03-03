using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;



namespace Lab1gameoflife
{

    class gameoflife
    {

        [DllImport("kernel32.dll", ExactSpelling = true)]  
      private static extern IntPtr GetConsoleWindow();  
      private static IntPtr ThisConsole = GetConsoleWindow();  
      [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]  
      private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);  
      private const int HIDE = 0;  
      private const int MAXIMIZE = 3;  
      private const int MINIMIZE = 6;  
      private const int RESTORE = 9;


        




        /*private int GetRandom(int max)
        {
            System.Security.Cryptography.RandomNumberGenerator rnd = System.Security.Cryptography.RandomNumberGenerator.Create();
            byte[] data = new byte[1];
            rnd.GetNonZeroBytes(data);
            int n = 1 + data[0] % max;
            return n;
        }*/


        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConsoleApplication26.Form1());


            int xlength = 150;
            int ylength = 45;
            int noofGenerations = 0;

            char[,] world1 = new char[ylength + 2, xlength + 2];
            char[,] world2 = new char[ylength + 2, xlength + 2];

            // 1 Init first state, slumpar ut liv i en matris.
            //ramarna, ytterraderna/kolumnerna sätts till -, tecken
            // init ramar
            for (int xrad = 0; xrad < (xlength + 2); xrad++)
            {
                world1[0, xrad] = '-';
                world2[0, xrad] = '-';
                world1[ylength + 1, xrad] = '-';
                world2[ylength + 1, xrad] = '-';
            }
            
            for (int ycol = 0; ycol < (ylength + 2); ycol++)
            {
                world1[ycol, 0] = '|';
                world2[ycol, 0] = '|';
                world1[ycol, xlength + 1] = '|';
                world2[ycol, xlength + 1] = '|';
            }
            int slumptal = 0;
            //System.Random rnd = new Random( (int)DateTime.Now.Ticks);
            System.Random rnd = new Random();


            for (int i = 1; i < xlength + 1; i++)
            {
                for (int j = 1; j < ylength+1; j++)
                {
                    slumptal = rnd.Next(0, 2);
                    //Next(x) som ger ett heltal mellan 0 och x-1
                 //   if (rnd.NextDouble() >= 0.5f)
                 //       slumptal = 1;
                 //   else
                  //      slumptal = 0;

                    if (slumptal == 1)
                        world1[j, i] = 'O';
                    else
                        world1[j, i] = ' ';

                }
            }

            // visa värld

            Console.Clear();
            // CTRL + F10
           // System.Windows.InputSendKeys.Send("{ENTER}");

            for (int j = 0; j < ylength + 2; j++)
            {
                for (int i = 0; i < xlength + 2; i++)
                {

                    Console.Write(world1[j, i]);
                    if (i == (xlength + 1))
                        Console.WriteLine();

                }
            }

            // generera nästa state
            // 1.Any live cell with fewer than two live neighbours dies, as if caused by under-population. 
            // 2.Any live cell with two or three live neighbours lives on to the next generation. //
            //3.Any live cell with more than three live neighbours dies, as if by over - population. 
            //4.Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction

            bool run = true;
            int rekn = 0;
            while (run)
            {
                noofGenerations = noofGenerations + 1;

                for (int j = 1; j < ylength + 1; j++)
                {
                    for (int i = 1; i < xlength+1; i++)
                    {
                        //radvis check
                        rekn = 0;

                        if (world1[j, i - 1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j - 1,i] == 'O')
                            rekn = rekn + 1;
                        if (world1[ j - 1,i-1] == 'O')
                            rekn = rekn + 1;
                        if (world1[ j,i+1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j + 1,i] == 'O')
                            rekn = rekn + 1;
                        if (world1[j + 1, i + 1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j - 1, i + 1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j + 1, i - 1] == 'O')
                            rekn = rekn + 1;


                        if (world1[j,i] == 'O')
                        {


                            if (rekn < 2)
                                world2[j, i] = ' ';
                            else if (rekn < 4)
                                world2[j, i] = 'O';
                            else
                                world2[j, i] = ' ';
                        }
                        else
                        {
                            if (rekn == 3)
                                world2[j, i] = 'O';
                            else
                                world2[j, i] = ' ';
                        }


                    }
                }







                Console.Clear();


                for (int j = 0; j < ylength + 2; j++)
                {
                    for (int i = 0; i < xlength + 2; i++)
                    {
                       



                        Console.Write(world2[j, i]);

                        world1[j, i] = world2[j, i];
                        if (i == (xlength + 1))
                        {
                            Console.WriteLine();

                            
                        }

                    }
                }








                if (Console.ReadKey().KeyChar == 'q')
                    run = false;
            }




            //skriv ut spelplan




        }
    }
}
