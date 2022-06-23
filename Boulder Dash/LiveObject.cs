using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash
{
    public abstract class LiveObject
    {
        public state liveobject;
        public int PozX { get; set; }
        public int PozY { get; set; }

        int OldX = 0;
        int OldY = 0;

        Random los = new Random();
        int ok = 0;

        public LiveObject(int x, int y)
        {
            PozX = x;
            PozY = y;
        }

        public void Ai(Board m) // Nie jest idelane, ale przynajmniej się cały czas porusza
        {
            if ((m.board[PozX + 1, PozY] == state.EMPTY || m.board[PozX + 1, PozY] == state.PLAYER) && PozX + 1 != OldX && (ok == 0 || ok == 1)) //dół
            {
                if (m.board[PozX + 1, PozY + 1] != state.EMPTY || m.board[PozX + 1, PozY - 1] != state.EMPTY)
                {
                    if (m.board[PozX+1, PozY] == state.PLAYER)
                    {
                        MoveDown(m);
                        Explosion(m);
                        m.PlayerDeadOnTheBoard();
                    }
                    else
                    {
                        MoveDown(m);
                        ok = 0;
                    }

                }
                else if (m.board[PozX + 1, PozY + 1] == state.EMPTY || m.board[PozX + 1, PozY - 1] == state.EMPTY)
                {
                    if (m.board[PozX + 1, PozY] == state.PLAYER)
                    {
                        MoveDown(m);
                        Explosion(m);
                        m.PlayerDeadOnTheBoard();
                    }
                    else
                    {
                        MoveDown(m);
                        ok = 1;
                        while (ok == 1)
                            ok = los.Next(1, 4);
                    }  
                }

            }
            else if ((m.board[PozX, PozY + 1] == state.EMPTY || m.board[PozX, PozY + 1] == state.PLAYER) && PozY + 1 != OldY && (ok == 0 || ok == 2)) //prawo
            {
                if (m.board[PozX + 1, PozY + 1] != state.EMPTY || m.board[PozX - 1, PozY + 1] != state.EMPTY)
                {
                    if (m.board[PozX, PozY+1] == state.PLAYER)
                    {
                        MoveRight(m);
                        Explosion(m);
                        m.PlayerDeadOnTheBoard();
                    }
                    else
                    {
                        MoveRight(m);
                        ok = 0;
                    }
                }
                else if (m.board[PozX + 1, PozY + 1] == state.EMPTY || m.board[PozX - 1, PozY + 1] == state.EMPTY)
                {
                    if (m.board[PozX, PozY + 1] == state.PLAYER)
                    {
                        MoveRight(m);
                        Explosion(m);
                        m.PlayerDeadOnTheBoard();
                    }
                    else
                    {
                        MoveRight(m);
                        ok = 2;
                        while (ok == 2)
                            ok = los.Next(1, 4);
                    }
                }

            }
            else if ((m.board[PozX - 1, PozY] == state.EMPTY || m.board[PozX - 1, PozY] == state.PLAYER) && PozX - 1 != OldX && (ok == 0 || ok == 3)) //góra
            {
                if (m.board[PozX - 1, PozY + 1] != state.EMPTY || m.board[PozX - 1, PozY - 1] != state.EMPTY)
                {
                    if (m.board[PozX-1, PozY] == state.PLAYER)
                    {
                        MoveUp(m);
                        Explosion(m);
                        m.PlayerDeadOnTheBoard();
                    }
                    else
                    {
                        MoveUp(m);
                        ok = 0;
                    }
                }
                else if (m.board[PozX - 1, PozY + 1] == state.EMPTY || m.board[PozX - 1, PozY - 1] == state.EMPTY)
                {
                    if (m.board[PozX - 1, PozY] == state.PLAYER)
                    {
                        MoveUp(m);
                        Explosion(m);
                        m.PlayerDeadOnTheBoard();
                    }
                    else
                    {
                        MoveUp(m);
                        ok = 3;
                        while (ok == 3)
                            ok = los.Next(1, 4);
                    }
                }
                
            }
            else if ((m.board[PozX, PozY - 1] == state.EMPTY || m.board[PozX, PozY - 1] == state.PLAYER) && PozY - 1 != OldY && (ok == 0 || ok == 4)) //lewo
            {
                if (m.board[PozX + 1, PozY - 1] != state.EMPTY || m.board[PozX - 1, PozY - 1] != state.EMPTY)
                {
                    if (m.board[PozX, PozY-1] == state.PLAYER)
                    {
                        MoveLeft(m);
                        Explosion(m);
                        m.PlayerDeadOnTheBoard();
                    }
                    else
                    {
                        MoveLeft(m);
                        ok = 0;
                    }
                }
                else if (m.board[PozX + 1, PozY - 1] == state.EMPTY || m.board[PozX - 1, PozY - 1] == state.EMPTY)
                {
                    if (m.board[PozX, PozY - 1] == state.PLAYER)
                    {
                        MoveLeft(m);
                        Explosion(m);
                        m.PlayerDeadOnTheBoard();
                    }
                    else
                    {
                        MoveLeft(m);
                        ok = 4;
                        while (ok == 4)
                            ok = los.Next(1, 4);
                    }
                    
                }
                
            }
           else
            {
                OldX = 0;
                OldY = 0;
                ok = 0;
            }
        }

        void MoveDown(Board m)
        {
            OldX = PozX;
            OldY = PozY;
            m.board[PozX, PozY] = state.EMPTY;
            PozX += 1;
            m.board[PozX, PozY] = liveobject;
        }

        void MoveUp(Board m)
        {
            OldX = PozX;
            OldY = PozY;
            m.board[PozX, PozY] = state.EMPTY;
            PozX -= 1;
            m.board[PozX, PozY] = liveobject;
        }

        void MoveLeft(Board m)
        {
            OldX = PozX;
            OldY = PozY;
            m.board[PozX, PozY] = state.EMPTY;
            PozY -= 1;
            m.board[PozX, PozY] = liveobject;
        }

        void MoveRight(Board m)
        {
            OldX = PozX;
            OldY = PozY;
            m.board[PozX, PozY] = state.EMPTY;
            PozY += 1;
            m.board[PozX, PozY] = liveobject;
        }


        public virtual void Explosion(Board m)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if(PozX == PozX + i && PozY == PozY + j)
                    {
                        m.board[PozX + i, PozY + j] = state.EMPTY;
                        liveobject = state.EMPTY;
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
