namespace MnistRoomateCompetition;

public static class AsciiMnistImageRenderer
{
    public static void RenderImage(Image image, int posX = 0, int posY = 0)
    {
        for (int y = posY; y < posY + Image.Rows; y++)
        {
            Console.SetCursorPosition(posX, y);
            for (int x = posX; x < posX + Image.Columns; x++)
            {
                byte pixel = image[x, y];
                ConsoleColor color = (MathF.Round(pixel / (Byte.MaxValue / 3f))) switch
                {
                    0 => ConsoleColor.Black,
                    1 => ConsoleColor.DarkGray,
                    2 => ConsoleColor.Gray,
                    3 => ConsoleColor.White,
                    _ => throw new Exception()
                };
                Console.BackgroundColor = color;
                Console.Write("  ");
            }
        }
        Console.ResetColor();
    }
}