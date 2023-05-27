using System.Diagnostics;
using MnistRoomateCompetition;

// Training data.
TrainingData[] data = MnistLoader.LoadData("./Data/train-images-idx3-ubyte", "./Data/train-labels-idx1-ubyte");
// Test data, only used on competition day.
// TrainingData[] data = MnistLoader.LoadData("./Data/t10k-images-idx3-ubyte", "./Data/t10k-labels-idx1-ubyte");
Frederik recogniser = new Frederik();

Train();
// ViewData(true);
Test();

void Train()
{
    recogniser.Train(data);
}

void Test()
{
    int successes = 0;
    Stopwatch sw = new Stopwatch();
    for (int i = 0; i < data.Length; i++)
    {
        sw.Start();
        Result result = recogniser.Test(data[i].Image);
        sw.Stop();
        int guess = result.Select((r, i) => (r, i)).MaxBy(t => t.r).i;
        if (data[i].Label == guess)
        {
            successes += 1;
        }
    }

    float perf = data.Length * Stopwatch.Frequency / (float)sw.ElapsedTicks;
    float accuracy = successes / (float)data.Length;
    Console.WriteLine($"Performance: {perf:E2}Images/Second");
    Console.WriteLine($"Accuracy: {100 * accuracy:F3}%");
}

void ViewData(bool randomized)
{
    Random random = new Random();
    for (int i = 0; randomized || i < data.Length; i = randomized ? random.Next(data.Length) : (i + 1))
    {
        Console.Clear();
        AsciiMnistImageRenderer.RenderImage(data[i].Image);
        Console.WriteLine();
        Console.WriteLine($"Data[{i}]");
        Console.WriteLine($"Label: {data[i].Label}");
        Result result = recogniser.Test(data[i].Image);
        Console.WriteLine($"Result:{result.Select((r, i) => (r, i)).MaxBy(t => t.r).i}");
        Console.WriteLine($"{{{string.Join(", ", result.Select((r, i) => $"{i}: {r:F2}"))}");
        Console.WriteLine("Press any key for the next image.");
        Console.ReadKey();
    }
}

