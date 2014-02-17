using System;
namespace Game
{
    public interface IObjectRenderable
    {
        Point GetTopLeftCoords();
        char[,] GetImage();
        ConsoleColor ImageColor { get; set; }
    }
}
