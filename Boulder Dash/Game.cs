using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Media;

namespace Boulder_Dash
{
    public class Game
    {
        public Player gracz = new Player();

        public void GameOver(Timer t, Timer m, SoundPlayer BD)
        {
            t.Stop();
            m.Stop();
            BD.Stop();
            if (MessageBox.Show("Przegrałeś\nZdobyłeś: " + gracz.points + " punktów", "Game Over!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry)
            {
                gracz.points = 0;
                gracz.mapa.life = 3;
                gracz.Dead = false;
                gracz.mapa.generate();
                t.Start();
                m.Start();
                BD.PlayLooping();
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
