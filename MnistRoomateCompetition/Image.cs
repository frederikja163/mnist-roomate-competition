namespace MnistRoomateCompetition;

public struct Image
{
    private readonly byte[,] _data;

    public byte this[int x, int y] => _data[x, y];

    public Image(byte[,] data)
    {
        _data = data;
    }

    public const int Rows = 28;
    public const int Columns = 28;
}