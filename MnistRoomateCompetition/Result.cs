using System.Collections;

namespace MnistRoomateCompetition;

public readonly struct Result : IEnumerable<float>
{
    private readonly float[] _results;

    public Result(params float[] results)
    {
        if (results.Length != 10)
        {
            throw new ArgumentException("Must have 10 results", nameof(results));
        }
        _results = results;
    }

    public float this[Index i] => _results[i];
    public IEnumerator<float> GetEnumerator()
    {
        for (int i = 0; i < _results.Length; i++)
        {
            yield return _results[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}