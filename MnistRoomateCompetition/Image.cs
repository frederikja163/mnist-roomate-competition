using System.Collections;

namespace MnistRoomateCompetition;

public struct Image : IEnumerable<byte>
{
    private readonly byte[,] _data;

    public byte this[int x, int y] => _data[x, y];

    public Image(byte[,] data)
    {
        _data = data;
    }

    public const int Rows = 28;
    public const int Columns = 28;
    public IEnumerator<byte> GetEnumerator()
    {
        for (int y = 0; y < Image.Columns; y++)
        {
            for (int x = 0; x < Image.Rows; x++)
            {
                yield return _data[x, y];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}