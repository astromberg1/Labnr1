using System;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;


namespace ConsoleApplication26
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            int distans;

            InitializeComponent();
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Refresh();

            panel1.Left = this.Left;
            distans = panel1.Top - this.Top;
            panel1.Top = this.Top + distans;


            panel1.Width = this.Width;
            panel1.Height = this.Height - distans;

        }



        private void button2_Click(object sender, EventArgs e)
        {


            DateTime start_time = DateTime.Now;

            //   DateTime Td = DateTime.Now;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            System.Drawing.Graphics graphicsObj;

            graphicsObj = this.panel1.CreateGraphics();
            int storlek = 10;

            storlek = int.Parse(this.textBox4.Text);
            Application.DoEvents();

            Color col1 = new Color();
            Color col2 = new Color();


            foreach (Color color in new ColorConverter().GetStandardValues())
            {
                if (comboBox1.SelectedItem.Equals(color))
                    col1 = color;
                if (comboBox2.SelectedItem.Equals(color))
                    col2 = color;


            }

            Pen myPen = new Pen(col1, storlek);

            Pen myPen1 = new Pen(col2, storlek);


            Brush myB = new HatchBrush(HatchStyle.Plaid, Color.Blue);
            Brush myB1 = new HatchBrush(HatchStyle.Plaid, Color.Yellow);

            //Point myp = new Point(storlek);
            //Point myp1 = new Point(storlek);


            //Rectangle rectangleObj = new Rectangle (x, y, width, height); 

            checkBox1.CheckState = 0;


            int xlength = 50;
            int ylength = 50;
            xlength = int.Parse(this.textBox2.Text);
            ylength = int.Parse(this.textBox3.Text);
            double popandel = double.Parse(this.textBox6.Text);
            int noofGenerations = 0;

            char[,] world1 = new char[ylength + 2, xlength + 2];
            char[,] world2 = new char[ylength + 2, xlength + 2];
            Rectangle[,] rc = new Rectangle[ylength + 2, xlength + 2];

            // 1 Init first state, slumpar ut liv i en matris.
            //ramarna, ytterraderna/kolumnerna sätts till -, tecken
            // init ramar
            int slumptal = 0;
            //System.Random rnd = new Random( (int)DateTime.Now.Ticks);
            System.Random rnd = new Random();


            for (int i = 1; i < xlength + 1; i++)
            {
                for (int j = 1; j < ylength + 1; j++)
                {
                    // slumptal = rnd.Next(0, 2);
                    //Next(x) som ger ett heltal mellan 0 och x-1
                    if (rnd.NextDouble() > (popandel / 100))
                        slumptal = 0;
                    else
                        slumptal = 1;

                    if (slumptal == 1)
                        world1[j, i] = 'O';
                    else
                        world1[j, i] = ' ';

                }
            }

            for (int j = 0; j < ylength; j++)
            {
                for (int i = 0; i < xlength; i++)
                {


                    rc[j, i].X = i * storlek;
                    rc[j, i].Y = j * storlek;
                    rc[j, i].Width = storlek;
                    rc[j, i].Height = storlek;

                    if (world1[j + 1, i + 1] == 'O')
                        graphicsObj.DrawRectangle(myPen, rc[j, i]);
                    else
                        graphicsObj.DrawRectangle(myPen1, rc[j, i]);


                }

            }

            // visa värld

            // CTRL + F10
            // System.Windows.InputSendKeys.Send("{ENTER}");


            // generera nästa state
            // 1.Any live cell with fewer than two live neighbours dies, as if caused by under-population. 
            // 2.Any live cell with two or three live neighbours lives on to the next generation. //
            //3.Any live cell with more than three live neighbours dies, as if by over - population. 
            //4.Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction

            bool run = true;
            int rekn = 0;
            while (run)
            {
                //panel1.Clear;



                foreach (Color color in new ColorConverter().GetStandardValues())
                {
                    if (comboBox1.SelectedItem.Equals(color))
                        col1 = color;
                    if (comboBox2.SelectedItem.Equals(color))
                        col2 = color;


                }


                myPen.Color = col1;
                myPen1.Color = col2;
                



                noofGenerations = noofGenerations + 1;
                Application.DoEvents();
                textBox1.Refresh();
                this.textBox1.Text = noofGenerations.ToString();
                //   TimeSpan.FromSeconds(Td - DateTime.Now).Seconds

                TimeSpan ts = DateTime.Now - start_time;
                String tstring = ts.ToString();
                tstring = String.Format("{0:s ss}", tstring);
                
                this.textBox5.Text = tstring;

                Application.DoEvents();
                textBox1.Refresh();


                for (int j = 1; j < ylength + 1; j++)
                {
                    for (int i = 1; i < xlength + 1; i++)
                    {
                        //radvis check
                        rekn = 0;

                        if (world1[j, i - 1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j - 1, i] == 'O')
                            rekn = rekn + 1;
                        if (world1[j - 1, i - 1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j, i + 1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j + 1, i] == 'O')
                            rekn = rekn + 1;
                        if (world1[j + 1, i + 1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j - 1, i + 1] == 'O')
                            rekn = rekn + 1;
                        if (world1[j + 1, i - 1] == 'O')
                            rekn = rekn + 1;


                        if (world1[j, i] == 'O')
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

                Application.DoEvents();


                //   if (noofGenerations % 2==0)
                //       panel1.Hide();
                //   else
                //       panel1.Show();


                for (int j = 0; j < ylength; j++)
                {
                    Application.DoEvents();

                    for (int i = 0; i < xlength; i++)
                    {


                        rc[j, i].X = i * storlek;
                        rc[j, i].Y = j * storlek;
                        rc[j, i].Width = storlek;
                        rc[j, i].Height = storlek;
                        if (!(world2[j + 1, i + 1] == world1[j + 1, i + 1]))
                        {
                            if (world2[j + 1, i + 1] == 'O')
                                graphicsObj.DrawRectangle(myPen, rc[j, i]);
                            else
                                graphicsObj.DrawRectangle(myPen1, rc[j, i]);
                            world1[j + 1, i + 1] = world2[j + 1, i + 1];
                        }









                    }

                }

                Application.DoEvents();
                checkBox1.Refresh();
                if (this.checkBox1.Checked)
                {
                    run = false;
                    panel1.Invalidate();
                }
            }








        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i = 0;
            int lifeint = 0;
            int deadint = 0;
            foreach (Color color in new ColorConverter().GetStandardValues())

            {
                i++;
                comboBox1.Items.Add( color);

                comboBox2.Items.Add(color);
                if (color.ToString().Equals("Yellow"))
                    lifeint = i;
                if (color.ToString().Equals("Blue"))
                    deadint = i;

            }
            comboBox1.SelectedIndex = 13;
            comboBox2.SelectedIndex = i - 2;
        }

    }

}