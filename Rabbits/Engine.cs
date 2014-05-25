using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace Rabbits
{
    class Engine
    {
        //Members
        private Graphics drawHandle;
        private Thread renderThread;

        public List<Duck> ducks = new List<Duck>();

        private Bitmap tex_duck;
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

        //Loads resources
        private void loadAssets()
        {
            tex_duck = Rabbits.Properties.Resources.duck;
            tex_grass = Rabbits.Properties.Resources.grass;
        }

        public void stop()
        {
            renderThread.Abort();
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

                ducks.ForEach(delegate(Duck d)
                {
                    d.Update();
                    frameGraphics.DrawImage(d.Texture, d.X, d.Y);
                });

                frameGraphics.DrawImage(tex_duck, move, move);
                drawHandle.DrawImage(frame, 0, 0);

                //Benchmarking
                framesRendered++;
                move++;
                if (Environment.TickCount >= startTime + 1000)
                {
                    Console.WriteLine("Engine: " + framesRendered + " fps");
                    framesRendered = 0;
                    startTime = Environment.TickCount;

                }
            }
        }
    }
}
