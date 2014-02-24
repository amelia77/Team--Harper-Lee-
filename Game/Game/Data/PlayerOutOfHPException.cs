using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Data
{
    public class PlayerOutOfHPException : ApplicationException
    {
        public PlayerOutOfHPException(string msg)
            : base(msg)
        { }
    }
}
