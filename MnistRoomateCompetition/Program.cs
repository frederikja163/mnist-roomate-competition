using System.Diagnostics;
using MnistRoomateCompetition;

TrainingData[] data = MnistLoader.LoadData("./Data/train-images-idx3-ubyte", "./Data/train-labels-idx1-ubyte");

for (int i = 0; i < data.Length; i++)
{
    Console.Clear();
    AsciiMnistImageRenderer.RenderImage(data[i].Image);
    Console.WriteLine($"Data[{i}]: " + data[i].Label);
    Console.ReadKey();
}

