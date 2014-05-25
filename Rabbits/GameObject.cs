﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;   

namespace Rabbits
{
    class GameObject
    {
        private Bitmap _texture;
        private int _height;
        private int _width;
        private int _x;
        private int _y;

        public GameObject(Bitmap texture, int x, int y)
        {
            _texture = texture;
            _width = _texture.Width;
            _height = _texture.Height;
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Height
        {
            get { return _height; }
        }

        public int Width
        {
            get { return _width; }
        }

        public Bitmap Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
    }
}
