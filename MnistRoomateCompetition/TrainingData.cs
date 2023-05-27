namespace MnistRoomateCompetition;

public struct TrainingData
{
    public Image Image { get; }
    public byte Label { get; }

    public TrainingData(Image image, byte label)
    {
        Image = image;
        Label = label;
    }
}