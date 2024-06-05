using System;
using System.IO;
using System.Drawing;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the path to the folder containing the tiles:");
        string folderPath = Console.ReadLine();

        Console.WriteLine("Enter the width of the tiles:");
        int tileWidth = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the height of the tiles:");
        int tileHeight = int.Parse(Console.ReadLine());

        // Get all PNG files in the directory
        string[] files = Directory.GetFiles(folderPath, "*.png");

        // Sort the files by Y and X coordinates
        Array.Sort(files, (a, b) =>
        {
            int aX = int.Parse(Path.GetFileNameWithoutExtension(a).Split('_')[0]);
            int aY = int.Parse(Path.GetFileNameWithoutExtension(a).Split('_')[1]);

            int bX = int.Parse(Path.GetFileNameWithoutExtension(b).Split('_')[0]);
            int bY = int.Parse(Path.GetFileNameWithoutExtension(b).Split('_')[1]);

            if (aY == bY)
            {
                return aX.CompareTo(bX);
            }
            return aY.CompareTo(bY);
        });

        // Calculate the size of the final image
        int maxX = int.Parse(Path.GetFileNameWithoutExtension(files[files.Length - 1]).Split('_')[0]);
        int maxY = int.Parse(Path.GetFileNameWithoutExtension(files[files.Length - 1]).Split('_')[1]);
        int finalWidth = (maxY + 1) * tileWidth;
        int finalHeight = (maxX + 1) * tileHeight;

        Bitmap finalImage = new Bitmap(finalWidth, finalHeight);
        using (Graphics g = Graphics.FromImage(finalImage))
        {
            foreach (string file in files)
            {
                int x = int.Parse(Path.GetFileNameWithoutExtension(file).Split('_')[0]);
                int y = int.Parse(Path.GetFileNameWithoutExtension(file).Split('_')[1]);
                Bitmap tile = new Bitmap(file);
                g.DrawImage(tile, y * tileWidth, x * tileHeight, tileWidth, tileHeight);
            }
        }

        finalImage.Save("finalImage.png");
        Console.WriteLine("Final image created and saved as finalImage.png");
    }
}