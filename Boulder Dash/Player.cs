using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Boulder_Dash
{
    public class Player
    {
        public Board mapa = new Board(20, 20);

        int pozX=1, pozY=1;

        public bool Dead = false;
        public int points = 0;

        public void Play(Graphics g)
        {
            if (mapa.board[1, 1] == state.PLAYER)
            {
                pozX = 1;
                pozY = 1;
            }
            DeadPlayer();
            mapa.PlayerOnTheBoard(pozX, pozY);
            foreach (var item in mapa.Diamonds)
                item.Physics(mapa);
            foreach (var item in mapa.Boulders)
                item.Physics(mapa);
            if (mapa.motyl.liveobject != state.EMPTY)
                mapa.motyl.Ai(mapa);
            if (mapa.duch.liveobject != state.EMPTY)
                mapa.duch.Ai(mapa);
            mapa.ShowBoard(g);
        }


        public void PlayPlayer()
        {
           if (mapa.board[1, 1] == state.PLAYER)
           {
               pozX = 1;
               pozY = 1;
           }
           DeadPlayer();
           mapa.PlayerOnTheBoard(pozX, pozY);
        }

        public void PlayEnvirnoment()
        {
            foreach (var item in mapa.Diamonds)
                item.Physics(mapa);
            foreach (var item in mapa.Boulders)
                item.Physics(mapa);
            if (mapa.motyl.liveobject != state.EMPTY)
                mapa.motyl.Ai(mapa);
            if (mapa.duch.liveobject != state.EMPTY)
                mapa.duch.Ai(mapa);
            
        }



        void DeadPlayer()
        {
            if (mapa.life <= 0)
                Dead = true;
        }

        public void DeadByTime()
        {
            mapa.board[pozX, pozY] = state.EMPTY;
            mapa.PlayerDeadOnTheBoard();
            mapa.time = 60;
        }

        void moveLeft()
        {
            mapa.board[pozX, pozY] = state.EMPTY;
            int oldX = pozX;
            int oldY = pozY;
            pozY -= 1;
            if (PlayerCollision(oldX,oldY))
                pozY += 1;
        }

        void moveRight()
        {
            mapa.board[pozX, pozY] = state.EMPTY;
            int oldX = pozX;
            int oldY = pozY;
            pozY += 1;
            if (PlayerCollision(oldX,oldY))
                pozY -= 1;
        }

        void moveDown()
        {
            mapa.board[pozX, pozY] = state.EMPTY;
            int oldX = pozX;
            int oldY = pozY;
            pozX += 1;
            if (PlayerCollision(oldX,oldY))
                pozX -= 1;
        }

        void moveUp()
        {
            mapa.board[pozX, pozY] = state.EMPTY;
            int oldX = pozX;
            int oldY = pozY;
            pozX -= 1;
            if (PlayerCollision(oldX,oldY))
                pozX += 1;
        }

        bool PlayerCollision(int oldX, int oldY) //kolizje gracza
        {
            if (mapa.board[pozX, pozY] == state.EMPTY || mapa.board[pozX, pozY] == state.GROUND)
                return false;
            if (mapa.board[pozX,pozY] == state.DIAMOND)
            {
                foreach(var item in mapa.Diamonds)
                {
                    if (item.PozX == pozX && item.PozY == pozY && item.ile < 2)
                    {
                        mapa.Diamonds.Remove(item);
                        mapa.ileDiamentów += 1;
                        break;
                    }   
                }
                points += 10;
                return false;
            }
            if(mapa.board[pozX, pozY] == state.BOULDER && pozX == oldX)
            {
                if(mapa.board[pozX,pozY+1] == state.EMPTY && oldY < pozY)
                {
                    foreach(var item in mapa.Boulders)
                    {
                        if (item.PozX == pozX && item.PozY == pozY)
                        {
                            item.move(pozX, pozY + 1, mapa);
                            return false;
                        }
                            
                    }
                }
                else if (mapa.board[pozX, pozY - 1] == state.EMPTY && oldY > pozY)
                {
                    foreach (var item in mapa.Boulders)
                    {
                        if (item.PozX == pozX && item.PozY == pozY)
                        {
                            item.move(pozX, pozY - 1, mapa);
                            return false;
                        }
                            
                    }
                }
                
            }
            if(mapa.board[pozX, pozY] == state.EXIT && mapa.ileDiamentów>=10)
            {
                pozX = 1;
                pozY = 1;
                points += mapa.time;
                mapa.generate();
                
                return false;
            }
            
            return true;
        }

        public void MovePlayer(KeyEventArgs e)
        {
            
            switch(e.KeyCode)
            {
                case Keys.D:
                    moveRight();
                    break;
                case Keys.A:
                    moveLeft();
                    break;
                case Keys.S:
                    moveDown();
                    break;
                case Keys.W:
                    moveUp();
                    break;
            }
        }

        
    }
}
