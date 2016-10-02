using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using Color = System.Drawing.Color;
using System.Runtime.InteropServices;

namespace snake
{
    public partial class Snake : Form
    {
        enum Direction { UP, DOWN, RIGHT, LEFT };
        Direction currentDirection;
        bool oneDirectionChange = true;
        
        Pen pen = new Pen(Color.Black, 4);
        SolidBrush brush = new SolidBrush(Color.White);

        ushort offset = 15;

        SnakeObject mySnake;
        SnakeFood food;

        static Random rnd = new Random();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public Snake()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
            this.KeyDown += OnKeyPressing;
            //AllocConsole();

            //Threads.Add(new Thread(SnakeDrawer));
            
            Starter();
        }

        void Starter()
        {
            if (mySnake != null) return;
            mySnake = new SnakeObject(new Vector2(10, 10));
            currentDirection = Direction.RIGHT;
            oneDirectionChange = true;
            food = new SnakeFood(CalculateLocation());
            new Task(SnakeMovementProcessing).Start();
        }

        Vector2 CalculateLocation()
        {
            bool posFound = false;
            Vector2 pos = new Vector2();
            while (!posFound)
            {
                pos.X = rnd.Next(1, 19);
                pos.Y = rnd.Next(1, 19);
                foreach (SnakeSector sector in mySnake.GetObjects())
                    if (pos.X != sector.GetCoords().X && pos.Y != sector.GetCoords().Y)
                        posFound = true;
            }
            return pos;
        }

        void SnakeMovementProcessing()
        {
            List<SnakeSector> objs = mySnake.GetObjects();
            SnakeSector head = objs[0];

            for(int i = objs.Count-1; i>=0; i--)
            {
                SnakeSector part = objs[i];
                Vector2 coords = part.GetCoords();
                if (part.Head())
                {
                    switch (currentDirection)
                    {
                        case Direction.UP:
                            part.SetCoords(new Vector2(coords.X, coords.Y - 1));
                            break;
                        case Direction.DOWN:
                            part.SetCoords(new Vector2(coords.X, coords.Y + 1));
                            break;
                        case Direction.LEFT:
                            part.SetCoords(new Vector2(coords.X - 1, coords.Y));
                            break;
                        case Direction.RIGHT:
                            part.SetCoords(new Vector2(coords.X + 1, coords.Y));
                            break;
                    }
                    head = part;
                }
                else
                {
                    if (part.GetCoords() == head.GetCoords())
                    {
                        mySnake = null;
                        return;
                    }
                    if (part.GetCoords() == food.GetCoords())
                    {
                        mySnake.AddObject(new SnakeSector(part.GetCoords()));
                        food.SetCoords(CalculateLocation());
                    }
                    part.SetCoords(objs[i - 1].GetCoords());
                }
            }
            if (head.GetCoords().X < 1 || head.GetCoords().X > 19 || head.GetCoords().Y < 1 || head.GetCoords().Y > 19)
            {
                mySnake = null;
                return;
            }

            DrawCall();
            Thread.Sleep(100);
            oneDirectionChange = true;
            SnakeMovementProcessing();
        }

        void OnProcessExit(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        void DrawCall()
        {
            Bitmap screen = new Bitmap(300, 300);
            screen = DrawRectangle((int)(food.GetCoords().X * offset + 1 - offset + 1), (int)(food.GetCoords().Y * offset + 1 - offset + 1), screen, Color.Red);
            foreach (SnakeSector part in mySnake.GetObjects())
            {
                screen = DrawRectangle((int)(part.GetCoords().X * offset + 1 - offset + 1), (int)(part.GetCoords().Y * offset + 1 - offset + 1), screen, Color.Black, part.Head());
            }
            Console.WriteLine();
            pictureBox1.Image = screen;
            return;
        }

        Bitmap DrawRectangle(int X, int Y, Bitmap bitmap, Color borderColor, bool head = false, ushort offset = 15, ushort borderSize = 3)
        {
            for (int i = X-1, j = Y;;)
            {
                i++;
                if (i > X + offset)
                {
                    i = X;
                    j++;
                    if (j > Y + borderSize && j < Y + offset - borderSize)
                        j = Y + offset - borderSize;
                }
                if (j > Y + offset) break;
                bitmap.SetPixel(i, j, borderColor);
            }

            for (int i = X, j = Y-1;;)
            {
                j++;
                if (j > Y + offset)
                {
                    j = Y;
                    i++;
                    if (i > X + borderSize && i < X + offset - borderSize)
                        i = X + offset - borderSize;
                }
                if (i > X + offset) break;
                bitmap.SetPixel(i, j, borderColor);
            }

            if (head) bitmap = DrawRectangle(X + borderSize, Y + borderSize, bitmap, Color.LawnGreen, false, (ushort)(offset - borderSize*2), 2);
            return bitmap;
        }

        void OnKeyPressing(object sender, KeyEventArgs e)
        {
            if (!oneDirectionChange) return;
            oneDirectionChange = false;
            if (e.KeyCode == Keys.Space)
                Starter();
            switch (e.KeyCode)
            {
                case Keys.Down:
                    if(currentDirection != Direction.UP)
                        currentDirection = Direction.DOWN;
                    break;
                case Keys.Up:
                    if (currentDirection != Direction.DOWN)
                        currentDirection = Direction.UP;
                    break;
                case Keys.Left:
                    if (currentDirection != Direction.RIGHT)
                        currentDirection = Direction.LEFT;
                    break;
                case Keys.Right:
                    if (currentDirection != Direction.LEFT)
                        currentDirection = Direction.RIGHT;
                    break;
            }
        }
    }
}
