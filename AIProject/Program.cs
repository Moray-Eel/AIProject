using AIProject.Classes;


NeuralNetwork xd = new(new int[] { 4, 3, 2 }, new string[] { @"C:\Users\slikm\Pulpit\IrisInput.tsv", @"C:\Users\slikm\Pulpit\IrisTarget.tsv" });
xd.run();
Console.ReadKey();


