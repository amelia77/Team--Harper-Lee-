using System.Collections.Generic;
using System.IO;
namespace Game
{
    public static class ImageProducer
    {
        //Get image from txt file and return a char[,] matrix of its image
        public static char[,] GetImage(string imageFile)
        {

            StreamReader reader = new StreamReader(imageFile);

            List<char[]> list = new List<char[]>();
            string line = reader.ReadLine();
            int maxCols = int.MinValue;
            while (line != null)
            {

                char[] symbols = line.ToCharArray();
                list.Add(symbols);
                if (symbols.Length > maxCols)
                {
                    maxCols = symbols.Length;
                }
                line = reader.ReadLine();
            }
            reader.Close();

            char[,] image = new char[list.Count, maxCols];
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Length; j++)
                {
                    image[i, j] = list[i][j];
                }
            }

            return image;
        }
    }
}
