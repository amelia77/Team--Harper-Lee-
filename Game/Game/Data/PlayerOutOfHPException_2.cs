using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Data
{
    class PlayerOutOfHPException : Exception
    {
        public PlayerOutOfHPException(string msg)
            : base(msg)
        { }
    }
}
