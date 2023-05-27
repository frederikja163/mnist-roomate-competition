using System.Collections;

namespace MnistRoomateCompetition;

public class Frederik : IMnistRecogniser
{
    public class NeuralNetwork
    {
        private static Random _random = new Random();
        
        private record Layer(float[,] Weights, int InputNeurons, int OutputNeurons)
        {
            public Layer(int inputNeurons, int outputNeurons) : this(new float[inputNeurons, outputNeurons], inputNeurons, outputNeurons)
            {
            
            }

            public Layer(Layer layer)
            {
                InputNeurons = layer.InputNeurons;
                OutputNeurons = layer.OutputNeurons;
                float[,] clone = new float[InputNeurons, OutputNeurons];
                for (int i = 0; i < OutputNeurons; i++)
                {
                    for (int j = 0; j < InputNeurons; j++)
                    {
                        clone[j, i] = layer.Weights[j, i];
                    }
                }

                Weights = clone;
            }

            public void Randomize(float min, float max)
            {
                for (int i = 0; i < InputNeurons; i++)
                {
                    for (int j = 0; j < OutputNeurons; j++)
                    {
                        Weights[i, j] += _random.NextSingle() * (max - min) + min;
                    }
                }
            }

            public float[] Apply(float[] input)
            {
                float[] output = new float[OutputNeurons];
                for (int i = 0; i < output.Length; i++)
                {
                    float sum = 0;
                    for (int j = 0; j < input.Length; j++)
                    {
                        sum += input[j] * Weights[j, i];
                    }

                    output[i] = sum;
                }

                return output;
            }
        };

        private readonly Layer[] _layers;

        public NeuralNetwork(params int[] layerSizes)
        {
            _layers = new Layer[layerSizes.Length - 1];
            for (int i = 0; i < _layers.Length; i++)
            {
                _layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
                _layers[i].Randomize(-2, 2);
            }
        }

        public NeuralNetwork(NeuralNetwork nn)
        {
            _layers = new Layer[nn._layers.Length];
            _layers = nn._layers.Select(l => new Layer(l)).ToArray();
        }

        public float[] Test(float[] input)
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                input = _layers[i].Apply(input);
            }

            return input;
        }
        
        public IEnumerable Train(bool randomOrder, params (float[] input, float[] expected)[] trainingData)
        {
            Random random = new Random();
            for (int i = 0;
                 randomOrder || i < trainingData.Length;
                 i = randomOrder ? random.Next(trainingData.Length) : (i + 1))
            {
                // Implement gradient descent training here.
                yield return null;
            }
        }

        public void Evolve(float multiplier)
        {
            for (int i = 0; i < _layers.Length; i++)
            {
                _layers[i].Randomize(-multiplier, multiplier);
            }
        }
    }

    private readonly NeuralNetwork[] _neuralNetwork;
    private NeuralNetwork _best;

    public Frederik()
    {
        _neuralNetwork = new NeuralNetwork[100];
        for (int i = 0; i < _neuralNetwork.Length; i++)
        {
            _neuralNetwork[i] = new NeuralNetwork(Image.Rows * Image.Columns, 10);
        }

        _best = _neuralNetwork[0];
    }
    
    public void Train(TrainingData[] data)
    {
        foreach (TrainingData trainingData in data)
        {
            float[] input = trainingData.Image.Select(b => (float)b).ToArray();
            float[] expected = new float[10];
            expected[trainingData.Label] = 1;
            List<NeuralNetwork> survivors = _neuralNetwork
                .OrderBy(nn => CalculateFitness(nn.Test(input), expected))
                .Take(50)
                .ToList();
            for (var i = 0; i < survivors.Count; i++)
            {
                NeuralNetwork survivor = survivors[i];
                _neuralNetwork[i * 2] = survivor;
                NeuralNetwork newNn = new NeuralNetwork(survivor);
                _neuralNetwork[i * 2 + 1] = newNn;
                newNn.Evolve(0.1f);
            }
        }
    }

    private float CalculateFitness(float[] output, float[] expected)
    {
        return output.Select((f, i) => MathF.Abs(expected[i] - f)).Sum();
    }
    
    public Result Test(Image image)
    {
        float[] input = image.Select(b => b / 255f).ToArray();
        return new Result(_best.Test(input));
    }
}