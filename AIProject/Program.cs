using AIProject.Classes;

static void SplitData(double[][] allData, double splitPercent, int seed, out double[][] trainData, out double[][] testData)
{
    Random rnd = new Random(seed);
    int totalRows = allData.Length;
    int numTrainRows = (int)(totalRows * splitPercent);
    int numTestRows = totalRows - numTrainRows;
    trainData = new double[numTrainRows][];
    testData = new double[numTestRows][];

    double[][] copy = new double[allData.Length][];
    for (int i = 0; i < copy.Length; ++i)
        copy[i] = allData[i];

    for (int i = 0; i < copy.Length; ++i)
    {
        int r = rnd.Next(i, copy.Length); // Fisher-Yates
        double[] tmp = copy[r];
        copy[r] = copy[i];
        copy[i] = tmp;
    }

    for (int i = 0; i < numTrainRows; ++i)
        trainData[i] = copy[i];

    for (int i = 0; i < numTestRows; ++i)
        testData[i] = copy[i + numTrainRows];
}

NeuralNetwork xd = new(new int[] { 2,1});


Console.WriteLine(xd);

