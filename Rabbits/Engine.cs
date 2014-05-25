using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Rabbits
{
    class Engine
    {
        //Members
        private Graphics drawHandle;
        private Thread renderThread;
        private const int moveSpeed = 5;

        public List<Duck> ducks = new List<Duck>();

        private Bitmap tex_grass;

        //Functions
        public Engine(Graphics g)
        {
            drawHandle = g;
        }

        public void init()
        {
            //Load assets
            loadAssets();
            ducks.Add(new Duck());
            //Start the render thead
            renderThread = new Thread(new ThreadStart(render));
            renderThread.Start();
        }

        public void KillAll()
        {
            ducks.ForEach(delegate(Duck d)
            {
                d.Kill();
            });
        }

        public void keyPressed(KeyEventArgs k)
        {
            bool flag=true;
            Console.WriteLine("Key pressed");
            if (k.KeyCode==Keys.C)
            {
                lock(ducks)
                {
                    ducks.Add(new Duck());
                }
                return;
            }
            if (k.Control && k.KeyCode==Keys.K)
            {
                KillAll();
                return;
            }
            ducks.ForEach(delegate(Duck d)
            {
                if (d.Active && flag)
                {
                    switch (k.KeyCode)
                    {
                        case Keys.Up:
                            d.Y -= moveSpeed;
                            break;
                        case Keys.Down:
                            d.Y += moveSpeed;
                            break;
                        case Keys.Left:
                            d.X -= moveSpeed;
                            break;
                        case Keys.Right:
                            d.X += moveSpeed;
                            break;
                        case Keys.K:
                            d.Kill();
                            break;
                    }
                }
            });
        }

        //Loads resources
        private void loadAssets()
        {
            tex_grass = Rabbits.Properties.Resources.grass;
        }

        public void stop()
        {
            renderThread.Abort();
        }

        public void onMouseClick(Point p)
        {
            Console.WriteLine("X=" + p.X.ToString() + " Y=" + p.Y.ToString());
            bool flag = true;
            ducks.ForEach(delegate(Duck d)
            {
                if (p.X>=d.X && p.X<(d.X+d.Height) && flag && !d.Active && !d.Dead)
                    if (p.Y>=d.Y && p.Y<(d.Y+d.Width))
                    {
                        Console.WriteLine("dX=" + d.X + " dY=" + d.Y);
                        Console.WriteLine("dX=" + d.Width + " dY=" + d.Height);
                        d.setActive(ducks);
                        flag = false;
                    }
            });
        }

        private void render()
        {
            int move = 0;
            int framesRendered = 0;
            long startTime = Environment.TickCount;

            Bitmap frame = new Bitmap(Game.CANVAS_WIDTH, Game.CANVAS_HEIHT);
            Graphics frameGraphics = Graphics.FromImage(frame);

            while (true)
            {
                //Main action
                for (int x = 0; x < Game.LEVEL_WIDTH; x++)
                    for (int y = 0; y < Game.LEVEL_HEIGHT; y++ )
                    {
                        frameGraphics.DrawImage(tex_grass, x * Game.TILE_SIDE_LENTH, y * Game.TILE_SIDE_LENTH);
                    }
                lock(ducks)
                {
                    ducks.ForEach(delegate(Duck d)
                    {
                        d.Update();
                        frameGraphics.DrawImage(d.Texture, new Rectangle(d.X, d.Y, d.Width, d.Height));
                    });
                }

                drawHandle.DrawImage(frame, 0, 0);

                //Benchmarking
                framesRendered++;
                move++;
                if (Environment.TickCount >= startTime + 1000)
                {
                    Console.WriteLine("Engine: " + framesRendered + " fps");
                    framesRendered = 0;
                    startTime = Environment.TickCount;
                    garbageCollect();
                }
            }
        }

        private void garbageCollect()
        {
            for (int i=ducks.Count - 1; i>=0; i--)
            {
                if ((Environment.TickCount - ducks[i].TimeDead) > 1000 && ducks[i].Dead)
                {
                    lock(ducks)
                    {
                        ducks.RemoveAt(i);
                    }
                }
            }
        }
    }
}
