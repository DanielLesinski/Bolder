using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash
{
    public class Boulder:Item
    {
        public Boulder(int x,int y):base(x,y)
        {
            item = state.BOULDER;
        }

        public void move(int x, int y, Board mapa)
        {
            PozX = x;
            PozY = y;
            mapa.board[PozX, PozY] = item;
        }
    }
}
