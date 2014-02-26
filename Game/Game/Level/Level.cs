namespace Game.Levels
{
    using Game.Data;
    using System.Collections.Generic;

    public abstract class Level
    {
        abstract public Player Player
        { get; }

        abstract public List<Enemy> Enemies
        { get; }
    }
}
