using System;
using System.Collections.Generic;
using System.Text;
namespace Game
{
    class ConsoleRenderer: IConsoleRenderer
    {
        int renderFieldMatrixRows;
        int renderFieldMatrixCols;
        char[,] renderGameFieldMatrix;

        public void WriteOnPosition(
            string text,
            Point topLeft,
            int width,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(topLeft.Col, topLeft.Row);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            int countEmptySpaces = width - text.Length;
            Console.Write(text + new string(' ', countEmptySpaces>=0?countEmptySpaces:0));
        }

        public static void WriteOnPosition(
            char character,
            int left = 0,
            int top = 0,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(character);
        }

        public void DrawTextBoxTopLeft(
            string text,
            int left = 0,
            int top = 0,
            ConsoleColor foregroundColor = ConsoleColor.White,
            ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            // ╔═════════╗
            // ║Some Text║
            // ╚═════════╝
            WriteOnPosition('╔', left, top, foregroundColor, backgroundColor);
            WriteOnPosition('╗', left + text.Length + 1, top, foregroundColor, backgroundColor);

            WriteOnPosition('╚', left, top + 2, foregroundColor, backgroundColor);
            WriteOnPosition('╝', left + text.Length + 1, top + 2, foregroundColor, backgroundColor);

            WriteOnPosition('║', left, top + 1, foregroundColor, backgroundColor);
            WriteOnPosition('║', left + text.Length + 1, top + 1, foregroundColor, backgroundColor);

            WriteOnPosition(new string('═', text.Length), left + 1, top, foregroundColor, backgroundColor);
            WriteOnPosition(new string('═', text.Length), left + 1, top + 2, foregroundColor, backgroundColor);

            WriteOnPosition(text, left + 1, top + 1, foregroundColor, backgroundColor);
        }


        public ConsoleRenderer(int visibleConsoleRows, int visibleConsoleCols)
        {
            renderGameFieldMatrix = new char[visibleConsoleRows, visibleConsoleCols];

            this.renderFieldMatrixRows = renderGameFieldMatrix.GetLength(0);
            this.renderFieldMatrixCols = renderGameFieldMatrix.GetLength(1);
        }

        public void ReDraw(IObjectRenderable obj, bool clear)
        {
            Console.CursorVisible = false;
            Console.ResetColor();
            Console.ForegroundColor = obj.ImageColor;
            Point objTopLeftCoords = obj.GetTopLeftCoords();
            int imageRows = obj.GetImage().GetLength(0);
            int imageCols = obj.GetImage().GetLength(1);

            int lastRow = Math.Min(objTopLeftCoords.Row + imageRows, this.renderFieldMatrixRows);
            int lastCol = Math.Min(objTopLeftCoords.Col + imageCols, this.renderFieldMatrixCols);


            for (int row = obj.GetTopLeftCoords().Row; row < lastRow; row++)
            {

                for (int col = obj.GetTopLeftCoords().Col; col < lastCol; col++)
                {
                    Console.SetCursorPosition(col,row);
                    if (clear)
                    {
                        Console.Write(' ');
                    }
                    else
                    {

                        Console.Write(obj.GetImage()[row - obj.GetTopLeftCoords().Row, col - obj.GetTopLeftCoords().Col]);
                    }
                }

            }
        }

        public void ClearDestroyedObjects(List<GameUnit> destroyedObjects)
        {
            foreach (var obj in destroyedObjects)
            {
                Point objTopLeftCoords = obj.GetTopLeftCoords();
            int imageRows = obj.GetImage().GetLength(0);
            int imageCols = obj.GetImage().GetLength(1);

            int lastRow = Math.Min(objTopLeftCoords.Row + imageRows, this.renderFieldMatrixRows);
            int lastCol = Math.Min(objTopLeftCoords.Col + imageCols, this.renderFieldMatrixCols);


            for (int row = obj.GetTopLeftCoords().Row; row < lastRow; row++)
            {

                for (int col = obj.GetTopLeftCoords().Col; col < lastCol; col++)
                {
                    Console.SetCursorPosition(col,row);
                        Console.Write(' ');
                }
            }
            }
            }

        private static void WriteOnPosition(
           string text,
           int left = 0,
           int top = 0,
           ConsoleColor foregroundColor = ConsoleColor.White,
           ConsoleColor backgroundColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
        }
    }
}
