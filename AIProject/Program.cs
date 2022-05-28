using AIProject.Classes;

NeuralNetwork xd = new(new int[] { 7, 7, 4 });
xd.InitializeWeightsAndBiases(-1, 1);
xd.TestMethod();
xd.Train();

Console.WriteLine(xd);