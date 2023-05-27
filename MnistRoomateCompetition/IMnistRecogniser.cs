namespace MnistRoomateCompetition;

public interface IMnistRecogniser
{
    Result Test(Image image);
}