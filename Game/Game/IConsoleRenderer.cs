using System;
using System.Collections.Generic;
namespace Game
{
    public interface IConsoleRenderer
    {     
        void ReDraw(IObjectRenderable obj, bool something);
        void WriteOnPosition(
            string text,
            Point topLeft,
            int width,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black);

        void DrawTextBoxTopLeft(
            string text,
            int left = 0,
            int top = 0,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black);

        void ClearDestroyedObjects(List<GameUnit> destroyedObjects);

        void WriteOnPosition(
            string text,
            int left = 0,
            int top = 0,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black);

        void ClearScreen();
    }
}
