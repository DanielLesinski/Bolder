using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash
{
    public class Diamond:Item
    {
        public Diamond(int x,int y):base(x,y)
        {
            item = state.DIAMOND;
        }
    }
}
