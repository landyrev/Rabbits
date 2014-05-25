using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Rabbits
{
    class Game
    {
        //Constants
        public const int CANVAS_HEIHT = 700;
        public const int CANVAS_WIDTH = 1200;
        public const int LEVEL_WIDTH = 3;
        public const int LEVEL_HEIGHT = 2;
        public const int TILE_SIDE_LENTH = 512;

        private Engine engine;

        public void loadLevel()
        {
            Level.initLevel();
        }

        public void startGraphics(Graphics g)
        {
            engine = new Engine(g);
            engine.init();
        }

        public void stopGame()
        {
            engine.stop();
        }

        public void onMouseClick(Point p)
        {
            engine.onMouseClick(p);
        }

        public void onButtonDown(object sender, KeyEventArgs e)
        {
            engine.keyPressed(e);
        }
    }

    public enum TextureID
    {
        grass
    }
}
