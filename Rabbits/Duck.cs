using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Rabbits
{
    class Duck : GameObject
    {
        private static Random rnd = new Random();
        private bool _isActive;
        private bool _isDead;
        private long _timeCreated;
        private long _timeDead;
        private Point _point;

        public Duck():
            base(Rabbits.Properties.Resources.duck, rnd.Next(1,Game.CANVAS_WIDTH-512), rnd.Next(1,Game.CANVAS_HEIHT-512))
        {
            _isActive = false;
            _timeCreated = Environment.TickCount;
            _isDead = false;
            _point.X = rnd.Next(1, Game.CANVAS_WIDTH - 512);
            _point.Y = rnd.Next(1, Game.CANVAS_HEIHT - 512);
        }

        public void Kill()
        {
            _isDead = true;
            Texture = Rabbits.Properties.Resources.duck_dead;
            _timeDead = Environment.TickCount;
        }

        public bool Dead
        {
            get { return _isDead; }
        }

        public bool Active
        {
            get { return _isActive; }
        }

        public void setActive(List<Duck> l)
        {
            l.ForEach(delegate(Duck d)
            {
                if (d.Active)
                    d.setNotActive();
            });
            _isActive = true;
            Texture = Rabbits.Properties.Resources.duck_selected;
        }

        public void setNotActive()
        {
            _isActive = false;
            if (_isDead)
            {
                Texture = Rabbits.Properties.Resources.duck_dead;
            }
            else
            {
                Texture = Rabbits.Properties.Resources.duck;
            }
        }

        public long TimeDead
        {
            get { return _timeDead; }
        }

        public void Update()
        {
            if (Environment.TickCount-_timeCreated > 30000)
            {
                Kill();
            } else
            {
                if (!_isActive && !_isDead)
                {
                    if (X == _point.X && Y == _point.Y)
                    {
                        _point.X = rnd.Next(1, Game.CANVAS_WIDTH - 512);
                        _point.Y = rnd.Next(1, Game.CANVAS_HEIHT - 512);
                    }
                    else
                    {
                        if (X != _point.X)
                            X += 1 * (_point.X - X) / Math.Abs(_point.X - X);

                        if (Y != _point.Y)
                            Y += 1 * (_point.Y - Y) / Math.Abs(_point.Y - Y);
                    }
                }
            }
        }
    }
}
