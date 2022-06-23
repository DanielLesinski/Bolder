using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Boulder_Dash;

namespace Boulder_Dash_Win
{
    public partial class Form1 : Form
    {
        Game BoulderDash;
        bool speed = false;
        SoundPlayer BDTheme = new SoundPlayer();

        public Form1()
        {
            InitializeComponent();
            BoulderDash = new Game();
            BDTheme.SoundLocation = "boulder-dash.wav";


            if (MessageBox.Show("Sterowanie:\nGóra - W\nDół - S\nLewo - A\nPrawo - D\n" +
                "\nGdy zbierzesz wystarczającą ilość diamentów,\notworzy się przejście" +
                " w prawym dolnym rogu mapy.\n" +
                "Resztę infromacji odkryj sam Drogi Graczu.", "Instrukcja", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
            {
                timer1.Start();
                timer2.Start();
                timer3.Start();
                BDTheme.PlayLooping();
            }

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.Clear(BackColor);
            BoulderDash.gracz.mapa.ShowBoard(g);
            label2.Text = BoulderDash.gracz.mapa.life.ToString();
            label4.Text = BoulderDash.gracz.points.ToString();
            if (BoulderDash.gracz.Dead)
                BoulderDash.GameOver(timer1,timer2,BDTheme);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        { 
            if(speed)
            {
                BoulderDash.gracz.MovePlayer(e);
                BoulderDash.gracz.PlayPlayer();
                speed = false;
                pictureBox1.Refresh();
            }
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BoulderDash.gracz.PlayEnvirnoment();
            pictureBox1.Refresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            BoulderDash.gracz.mapa.time -= 1;
            if (BoulderDash.gracz.mapa.time < 0)
                BoulderDash.gracz.DeadByTime();
            label6.Text = BoulderDash.gracz.mapa.time.ToString();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            speed = true;
        }
    }
}
