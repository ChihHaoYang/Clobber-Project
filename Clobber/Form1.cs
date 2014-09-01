using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Threading;


namespace ClobberGUI
{
    public partial class Form1 : Form
    {
        private int[,] Map = new int[10, 10];
        //0無子 1黑色 2白色
        private int[,] WP = new int[32,2];
        private int[,] BP = new int[32,2];
        private int Mycolor;
        private int _x1;
        //座標
        private int _y1;
        private int isStart;
        private int tempx,tempy;


        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseDown += new MouseEventHandler(pictureBox1_MouseDown);
            pictureBox1.MouseClick += new MouseEventHandler(pictureBox1_MouseClick);
            isStart = 0;            
        }
        double oldWidth;
        double oldHeight;

        //Not used now
        private void Form1_Load(object sender, EventArgs e)
        {
            oldWidth = this.Width;
            oldHeight = this.Height;
        }
        //

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Step2.計算比例 
            double x = (this.Width / oldWidth);
            double y = (this.Height / oldHeight);

            // Step3.控制項 Resize 
            button1.Width = Convert.ToInt32(x * button1.Width);
            button1.Height = Convert.ToInt32(y * button1.Height);
            button2.Width = Convert.ToInt32(x * button2.Width);
            button2.Height = Convert.ToInt32(y * button2.Height);
            listBox1.Width = Convert.ToInt32(x * listBox1.Width);
            listBox1.Height = Convert.ToInt32(y * listBox1.Height);
            listBox2.Width = Convert.ToInt32(x * listBox2.Width);
            listBox2.Height = Convert.ToInt32(y * listBox2.Height);
            pictureBox1.Width = Convert.ToInt32(x * pictureBox1.Width);
            pictureBox1.Height = Convert.ToInt32(y * pictureBox1.Height);

            // Step4.把Form本來大小值設為目前大小值 
            oldWidth = this.Width;
            oldHeight = this.Height; 
        }

        private bool IsMyStone(int x, int y)
        {
            if (Map[x, y] == Mycolor)
                return true;
            else
                return false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        // Init Game//
        private void button1_Click(object sender, EventArgs e)
        {
            int tpw = 0;
            int tpb = 0;
            //SoundPlayer Mymusic = new SoundPlayer("BGM.wav");
            //Mymusic.Play();
            int x, y;
            //Graphics g = pictureBox1.CreateGraphics();
           // Bitmap bitmap = new Bitmap("WhiteStone.png");
           // Bitmap bitmap2 = new Bitmap("BlackStone.png");
           // pictureBox1.Enabled = true;
           // pictureBox1.Refresh();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Map[i, j] = 2;
                        WP[tpw, 0] = i;
                        WP[tpw, 1] = j;
                        tpw++;
                        //g.DrawImage(bitmap, i * 100 + 10, j * 100 + 10, 85, 85);
                    }
                    else
                    {
                        Map[i, j] = 1;
                        //g.DrawImage(bitmap2, i * 100 + 10, j * 100 + 10, 85, 85);
                        BP[tpb, 0] = i;
                        BP[tpb, 1] = j;
                        tpb++;
                    }
                }
            }
            Print();
            listBox1.Items.Clear();
            Mycolor = 1;
            isStart = 1;
            listBox2.Items.Clear();
            listBox2.Items.Add("Black Moves.");


        }


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int x, y;
            int n = 0;
            //x1 = (e.X + 10)/100;
            //y1 = (e.Y + 10)/100;
            //listBox1.Items.Add(x1 +" "+y1);

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
 
            //Graphics g = pictureBox1.CreateGraphics(); 
            //Bitmap bitmap = new Bitmap("select.png");
            if (isStart == 1)
            {
                if (_x1 != (e.X + 10) / 100 || _y1 != (e.Y + 10) / 100)
                {
                    if (Can_Move(_x1, _y1, (e.X + 10) / 100, (e.Y + 10) / 100)&&IsMyStone(_x1,_y1))
                    {
                        MoveStone(_x1, _y1, (e.X + 10) / 100, (e.Y + 10) / 100);
                        Print();
                        RM(Mycolor);
                        Thread.Sleep(2000);
                        Print();
                    }
                    else
                    {
                        //listBox1.Items.Add("Invalid Move!!");
                        _x1 = (e.X + 10) / 100;
                        _y1 = (e.Y + 10) / 100;
                        listBox1.Items.Add(_x1 + " " + _y1);
                        
                        if (Map[_x1, _y1] == Mycolor)
                        {
                           // listBox1.Items.Add("This is your stone");

                        }
                        else
                        {
                           // listBox1.Items.Add("This is not your stone");
                        }
                    }
                    
                }
                
                else
                {
                    _x1 = (e.X + 10) / 100;
                    _y1 = (e.Y + 10) / 100;
                    listBox1.Items.Add(_x1 + " " + _y1);


                    if (Map[_x1, _y1] == Mycolor)
                    {
                      //  listBox1.Items.Add("This is your stone");

                    }
                    else
                    {
                      //  listBox1.Items.Add("This is not your stone");
                    }
                }
            }
        }

        private void Print()
        {
            if (Mycolor == 1)
            {
                listBox2.Items.Clear();
                listBox2.Items.Add("Black Moves");
            }
            else
            {
                listBox2.Items.Clear();
                listBox2.Items.Add("White Moves");
            }
            Graphics g = pictureBox1.CreateGraphics();
            Bitmap bitmap = new Bitmap("WhiteStone.png");
            Bitmap bitmap2 = new Bitmap("BlackStone.png");
            pictureBox1.Enabled = true;
            pictureBox1.Refresh();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Map[i, j] == 2)
                    {
                        g.DrawImage(bitmap, i * 100 + 10, j * 100 + 10, 85, 85);
                    }
                    else if(Map[i, j] == 1)
                    {
                        g.DrawImage(bitmap2, i * 100 + 10, j * 100 + 10, 85, 85);

                    }
                }
            }
            if (!NotGameover())
            {
                MessageBox.Show("Game Over");
                if (Mycolor == 1)
                {
                    MessageBox.Show("White Wins");
                }
                else MessageBox.Show("Black Wins");
                //Application.Exit();
            }

        }

        private void RM(int color)
        {
            tt:
            int count;
            Random u = new Random();
            Random rnd = new Random();
            int index;
            int dir;
            if (color == 1)
            {
                count = 0;
                start1:
                index=rnd.Next()%32;
                st1:
                dir = u.Next();
                if (BP[index, 0] != -1)
                {
                    count++;
                    if (count > 10) goto tt;
                    switch(dir%4)
                    {
                        case 0:
                            if (BP[index, 0] > 0)
                            {
                                if (Can_Move(BP[index, 0], BP[index, 1], BP[index, 0] - 1, BP[index, 1]))
                                {
                                    MoveStone(BP[index, 0], BP[index, 1], BP[index, 0] - 1, BP[index, 1]);
                                }
                                else goto st1;
                            }
                            else goto st1;
                                break;
                            
                        case 1:
                                if (BP[index, 0] < 7)
                                {
                                    if (Can_Move(BP[index, 0], BP[index, 1], BP[index, 0] + 1, BP[index, 1]))
                                    {
                                        MoveStone(BP[index, 0], BP[index, 1], BP[index, 0] + 1, BP[index, 1]);
                                    }
                                    else goto st1;
                                }
                                else goto st1;  
                                break;
                        case 2:
                                if (BP[index, 1] > 0)
                                {
                                    if (Can_Move(BP[index, 0], BP[index, 1], BP[index, 0], BP[index, 1] - 1))
                                    {
                                        MoveStone(BP[index, 0], BP[index, 1], BP[index, 0], BP[index, 1] - 1);
                                    }
                                    else goto st1;
                                }
                                else goto st1;
                                break;
                        case 3:
                                if (BP[index, 1] < 7)
                                {
                                    if (Can_Move(BP[index, 0], BP[index, 1], BP[index, 0], BP[index, 1] + 1))
                                    {
                                        MoveStone(BP[index, 0], BP[index, 1], BP[index, 0], BP[index, 1] + 1);
                                    }
                                    else goto st1;
                                }
                                else goto st1;
                            break;
                    }
                }
                else goto start1;

            }
            else
            {
                count = 0;
                start2:
                index = rnd.Next()%32;
                st2:
                dir = u.Next();
                if (WP[index, 0] != -1)
                {
                    count++;
                    if (count > 5) goto tt; 
                    switch (dir % 4)
                    {
                        case 0:
                            if (WP[index, 0] > 0)
                            {
                                if (Can_Move(WP[index, 0], WP[index, 1], WP[index, 0] - 1, WP[index, 1]))
                                {
                                    MoveStone(WP[index, 0], WP[index, 1], WP[index, 0] - 1, WP[index, 1]);
                                }
                                else goto st2;
                            }
                            else goto st2;
                                break;
                            
                        case 1:
                                if (WP[index, 0] < 7)
                                {
                                    if (Can_Move(WP[index, 0], WP[index, 1], WP[index, 0] + 1, WP[index, 1]))
                                    {
                                        MoveStone(WP[index, 0], WP[index, 1], WP[index, 0] + 1, WP[index, 1]);
                                    }
                                    else goto st2;
                                }
                                else goto st2;
                                break;
                            
                        case 2:
                                if (WP[index, 1] > 0)
                                {
                                    if (Can_Move(WP[index, 0], WP[index, 1], WP[index, 0], WP[index, 1] - 1))
                                    {
                                        MoveStone(WP[index, 0], WP[index, 1], WP[index, 0], WP[index, 1] - 1);
                                    }
                                    else goto st2;
                                }
                                else goto st2;  
                                break;
                        case 3:
                                if (WP[index, 1] < 7)
                                {
                                    if (Can_Move(WP[index, 0], WP[index, 1], WP[index, 0], WP[index, 1] + 1))
                                    {
                                        MoveStone(WP[index, 0], WP[index, 1], WP[index, 0], WP[index, 1] + 1);
                                    }
                                    else goto st2;
                                }
                                else goto st2;
                        break;
                    }
                }
                else goto start2;
            }
        }

        private void MoveStone(int x1, int y1, int x2, int y2)
        {
            if (Mycolor == 1)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (BP[i, 0] == x1 && BP[i, 1] == y1)
                    {
                        BP[i, 0] = x2;
                        BP[i, 1] = y2;
                    }
                }
                for (int i = 0; i < 32; i++)
                {
                    if (WP[i, 0] == x2 && WP[i, 1] == y2)
                    {
                        WP[i, 0] = -1;
                        WP[i, 1] = -1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 32; i++)
                {
                    if (WP[i, 0] == x1 && WP[i, 1] == y1)
                    {
                        WP[i, 0] = x2;
                        WP[i, 1] = y2;
                    }
                }
                for (int i = 0; i < 32; i++)
                {
                    if (BP[i, 0] == x2 && BP[i, 1] == y2)
                    {
                        BP[i, 0] = -1;
                        BP[i, 1] = -1;
                    }
                }
            }
            listBox1.Items.Add("From " + x1 + "," + y1 + " to " + x2 + "," + y2);
            listBox1.Items.Add(Mycolor);
            Map[x2, y2] = Map[x1, y1];
            Map[x1, y1] = 0;
            Mycolor = Mycolor % 2 + 1;
            
            
        }

        private bool Can_Move(int x1, int y1, int x2, int y2)
        {
            if (Map[x1,y1]!=0&&Math.Abs(x1+y1-x2-y2)==1&&Map[x1, y1] != Map[x2, y2] && Map[x2, y2] != 0)
                return true;
            else
                return false;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool NotGameover()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //listBox1.Items.Add(Map[i, j]);

                    if (Can_Move(i, j, i + 1, j) || Can_Move(i, j, i, j + 1))
                    {
                        tempx = i;
                        tempy = j;
                        return true;
                    }
                    else if (i > 0 && j > 0)
                    {
                        if (Can_Move(i, j, i, j - 1) || Can_Move(i, j, i - 1, j))
                        {
                            tempx = i;
                            tempy = j;
                            return true;
                        }
                    }            
                }
            }
            return false;
        }



    }
}
