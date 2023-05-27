using System.Transactions;

namespace MnistRoomateCompetition;

public static class MnistLoader
{
    private const int LabelMagicNumber = 0x00000801;
    private const int ImageMagicNumber = 0x00000803;
    
    public static TrainingData[] LoadData(string imagePath, string labelPath)
    {
        using BinaryReader imageReader = new BinaryReader(File.Open(imagePath, FileMode.Open));
        using BinaryReader labelReader = new BinaryReader(File.Open(labelPath, FileMode.Open));

        int imageMagicNumber = imageReader.ReadInt32BE();
        if (imageMagicNumber != ImageMagicNumber)
        {
            throw new FormatException(
                $"Image file '{imagePath}' not correct format, magic number did not match. Got: 0x{imageMagicNumber:X}, Expected: 0x{ImageMagicNumber:X}");
        }

        int labelMagicNumber = labelReader.ReadInt32BE();
        if (labelMagicNumber != LabelMagicNumber)
        {
            throw new FormatException(
                $"Label file '{labelPath}' not correct format, magic number did not match. Got: 0x{labelMagicNumber:X}, Expected: 0x{LabelMagicNumber:X}");
        }

        int imageCount = imageReader.ReadInt32BE();
        int height = imageReader.ReadInt32BE();
        int width = imageReader.ReadInt32BE();

        if (height != Image.Rows || width != Image.Columns)
        {
            throw new FormatException(
                $"Image file '{imagePath}' did not have correct image size. Got: {height}x{width}, Expected {Image.Rows}x{Image.Columns}");
        }

        int labelCount = labelReader.ReadInt32BE();
        if (labelCount != imageCount)
        {
            throw new ArgumentException(
                $"Image file '{imagePath}' and label file '{labelPath}' do not appear to be from the same dataset. Image count {imageCount} does not match label count {labelCount}");
        }

        TrainingData[] trainingData = new TrainingData[imageCount];
        for (int i = 0; i < imageCount; i++)
        {
            byte[,] data = new byte[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    data[x, y] = imageReader.ReadByte();
                }
            }

            Image image = new Image(data);
            Byte label = labelReader.ReadByte();
            trainingData[i] = new TrainingData(image, label);
        }

        return trainingData;
    }
}