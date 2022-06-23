using System;
using System.Collections.Generic;
using System.Text;

namespace Boulder_Dash
{
    public class Ghost:LiveObject
    {
        public Ghost(int x, int y) : base(x, y)
        {
            liveobject = state.GHOST;
        }
    }
}
