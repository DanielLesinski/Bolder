using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash
{
    public abstract class Item
    {
        protected state item;

        public int PozX { get; set; }
        public int PozY { get; set; }

        public int ile = 0;

        public Item(int x, int y)
        {
            PozX = x;
            PozY = y;
        }

        public void Physics(Board m) //fizyka obiektów
        {
            if(m.board[PozX+1,PozY] == state.EMPTY)
            {
                m.board[PozX, PozY] = state.EMPTY;
                PozX += 1;
                m.board[PozX, PozY] = item;
                ile += 1;
            }
            else if(m.board[PozX, PozY+1] == state.EMPTY && m.board[PozX+1, PozY+1] == state.EMPTY)
            {
                if (m.board[PozX + 1, PozY] == state.BOULDER || m.board[PozX + 1, PozY] == state.DIAMOND || m.board[PozX + 1, PozY] == state.WALL)
                {
                    m.board[PozX, PozY] = state.EMPTY;
                    PozY += 1;
                    m.board[PozX, PozY] = item;
                    ile += 1;
                }
            }
            else if (m.board[PozX, PozY-1] == state.EMPTY && m.board[PozX+1, PozY-1] == state.EMPTY)
            {
                if (m.board[PozX + 1, PozY] == state.BOULDER || m.board[PozX + 1, PozY] == state.DIAMOND || m.board[PozX + 1, PozY] == state.WALL)
                {
                    m.board[PozX, PozY] = state.EMPTY;
                    PozY -= 1;
                    m.board[PozX, PozY] = item;
                    ile += 1;
                }
                
            }
            else if(m.board[PozX + 1, PozY] == state.BUTTERFLY && ile >= 2)
            {
                m.motyl.Explosion(m);
            }
            else if (m.board[PozX + 1, PozY] == state.GHOST && ile >= 2)
            {
                m.duch.Explosion(m);
            }
            else if(m.board[PozX + 1, PozY] == state.PLAYER && ile >= 2)
            {
                m.board[PozX, PozY] = state.EMPTY;
                PozX += 1;
                Explosion(m);
                m.PlayerDeadOnTheBoard();
            }
            else
            {
                ile = 0;
            }
        }

        

        public void Explosion(Board m)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if(PozX == PozX + i && PozY == PozY + j)
                    {
                        m.board[PozX + i, PozY + j] = state.EMPTY;
                    }
                    else if (m.board[PozX + i, PozY + j] == state.DIAMOND)
                    {
                        continue;
                    }
                    else if (m.board[PozX + i, PozY + j] == state.BOULDER)
                    {
                        continue;
                    }
                    else if (m.board[PozX + i, PozY + j] == state.BUTTERFLY)
                    {
                        m.motyl.Explosion(m);
                    }
                    else if (m.board[PozX + i, PozY + j] == state.GHOST)
                    {
                        m.duch.Explosion(m);
                    }
                    else if (m.board[PozX + i, PozY + j] == state.PLAYER)
                    {
                        m.board[PozX + i, PozY + j] = state.EMPTY;
                        m.PlayerDeadOnTheBoard();
                    }
                    else if (m.board[PozX + i, PozY + j] != state.SUPERWALL && m.board[PozX + i, PozY + j] != state.EXIT)
                    {
                        m.board[PozX + i, PozY + j] = state.EMPTY;
                    }

                }
            }
        }
    }
}
