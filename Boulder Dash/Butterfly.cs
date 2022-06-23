using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash
{
    public class Butterfly:LiveObject
    {
        public Butterfly(int x, int y) : base(x, y)
        {
            liveobject = state.BUTTERFLY;
        }

        override public void Explosion(Board m)
        {
            base.Explosion(m);
            for(int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (m.board[PozX + i, PozY + j] != state.SUPERWALL && m.board[PozX + i, PozY + j] != state.EXIT)
                    {
                        if (m.board[PozX + i, PozY + j] == state.DIAMOND || m.board[PozX + i, PozY + j] == state.BOULDER)
                            continue;
                        m.board[PozX + i, PozY + j] = state.DIAMOND;
                        m.Diamonds.Add(new Diamond(PozX + i, PozY + j));

                    }
                }
            }
                
        }
    }
}
