namespace Game.Interfaces
{
    using Game.Data;
    using System;
    using System.Collections.Generic;

    public interface IConsoleRenderer
    {
        int RenderFieldMatrixRows { get; }
        int RenderFieldMatrixCols { get; }

        void ReDraw(IObjectRenderable obj, bool something);

        void WriteOnPosition(string text,
                             Point topLeft,
                             int width,
                             ConsoleColor foregroundColor = ConsoleColor.White,
                             ConsoleColor backgroundColor = ConsoleColor.Black);
                             

        void DrawTextBoxTopLeft(string text,
                                int left = 0,
                                int top = 0,
                                ConsoleColor foregroundColor = ConsoleColor.White,
                                ConsoleColor backgroundColor = ConsoleColor.Black);
            

        void ClearDestroyedObjects(List<GameUnit> destroyedObjects);

        void WriteOnPosition(string text,
                             int left = 0,
                             int top = 0,
                             ConsoleColor foregroundColor = ConsoleColor.White,
                             ConsoleColor backgroundColor = ConsoleColor.Black);
                             

        void DrawImage(char[,] text,
                        int left = 0,
                        int top = 0,
                        ConsoleColor foregroundColor = ConsoleColor.White,
                        ConsoleColor backgroundColor = ConsoleColor.Black);
                        

        void ClearScreen();
    }
}
