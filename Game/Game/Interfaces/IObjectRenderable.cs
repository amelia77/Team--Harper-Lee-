namespace Game.Interfaces
{
    using Game.Data;
    using System;
    public interface IObjectRenderable
    {
        Point GetTopLeftCoords();
        char[,] GetImage();
        ConsoleColor ImageColor { get; set; }
    }
}
