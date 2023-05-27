namespace MnistRoomateCompetition;

public struct Result
{
    private readonly int[] _result;

    public Result(params int[] result)
    {
        if (result.Length != 10)
        {
            throw new ArgumentException("Must have 10 results", nameof(result));
        }
        _result = result;
    }
}