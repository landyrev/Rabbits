﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Rabbits
{
    public partial class GameWindow : Form
    {
        private Game game = new Game();

        public GameWindow()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            game.loadLevel();
            Graphics g = canvas.CreateGraphics();
            game.startGraphics(g);
        }

        private void GameWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.stopGame();
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            AllocConsole();
            this.KeyDown += new KeyEventHandler(game.onButtonDown);
        }

        // Allows the command line to be seen during normal execution
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            game.onMouseClick(e.Location);
        }
    }
}
