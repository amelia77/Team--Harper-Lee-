namespace Game.Data
{
    using System;

    class PlayerOutOfHPException : Exception
    {
        public PlayerOutOfHPException(string msg)
            : base(msg)
        { }
    }
}
