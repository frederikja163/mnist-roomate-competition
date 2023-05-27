namespace MnistRoomateCompetition;

public sealed class RandomRecogniser : IMnistRecogniser
{
    private readonly Random _random = new Random();
    
    public Result Test(Image image)
    {
        return new Result(
            _random.NextSingle(), // Result 0
            _random.NextSingle(), // Result 1
            _random.NextSingle(), // Result 2
            _random.NextSingle(), // Result 3
            _random.NextSingle(), // Result 4
            _random.NextSingle(), // Result 5
            _random.NextSingle(), // Result 6
            _random.NextSingle(), // Result 7
            _random.NextSingle(), // Result 8
            _random.NextSingle()  // Result 9
        );
    }
}