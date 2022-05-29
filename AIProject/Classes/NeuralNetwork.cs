using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AIProject.Classes;
using AIProject.Classes.Extensions;


namespace AIProject.Classes;


public class NeuralNetwork
{

    private readonly Random _randomizer = new ();
    //Wartości dla każdej warstwy -> Values[i][j] gdzie i -> numer warstwy,
    //j -> numer neuronu
    public double[][] Values { get; set; }

    //Biasy dla kazdej warstwy -> Biases[i][j] gdzie i -> a numer warstwy,
    //j -> numer neuronu
    public double[][] Biases { get; set; }

    //Waga dla każdej warstwy -> Weights[i][j][k] gdzie i-> numer warstwy, 
    //j -> numer neuronu w warstwie i, k-> numer neuronu w warstwie i+1
    //Jeśli nasza sieć ma N warstw to -> Weights[N-1][][]
    public double[][][] Weights { get; set; }

    //Nowa waga dla każdej warstwy -> Weights[i][j][k] gdzie i-> numer warstwy, 
    //j -> numer neuronu w warstwie i, k-> numer neuronu w warstwie i+1
    //Jeśli nasza sieć ma N warstw to -> Weights[N-1][][]
    public double[][][] UpdatedWeights { get; set; }
    
    //Nowe biasy dla kazdej warstwy -> Biases[i][j] gdzie i -> a numer warstwy,
    //j -> numer neuronu
    public double[][] UpdatedBiases { get; set; }

    //Gradienty dla każdej warstwy -> Values[i][j] gdzie i -> numer warstwy,
    //j -> numer neuronu
    public double[][] Gradient { get; set; }
    public double[][] Signals { get; set; }
    public double[] TargetValues { get; set; }




    public double LearningRate = 0.001;

    public NeuralNetwork(int[] layers)
    {
        Values = new double[layers.Length][];
        Biases = new double[layers.Length-1][];
        Weights = new double[layers.Length-1][][];
        UpdatedWeights = new double[layers.Length - 1][][];
        Gradient = new double[layers.Length - 1][];
        Signals = new double[layers.Length - 1][];


        for (int i = 0; i < layers.Length; i++)
        {
            Values[i] = new double[layers[i]];
        }
        for (int i = 0; i < layers.Length-1 ; i++)
        {
            Weights[i] = new double[layers[i]][];
            UpdatedWeights[i] = new double[layers[i]][];
            Biases[i] = new double[layers[i]];


            for (int j = 0; j < Weights[i].Length; j++)
            {
                Weights[i][j] = new double[layers[i+1]];
                UpdatedWeights[i] = new double[layers[i]][];
            }
        }
    }    /// <summary>
    /// Inicjalizuje wagi i biasy z przedziału od min do max
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    public void InitializeWeightsAndBiases(int min, int max)
    {

        //Inicjalizacja losowych wag z przedziału min do max
        for (int i = 0; i < Weights.GetLength(0); i++)
            for (int j = 0; j < Weights[i].Length; j++)
            {
                for (int k = 0; k < Weights[i][j].Length; k++)
                {
                    Weights[i][j][k] = _randomizer.NextDouble(min, max);
                }
            }

        //Inicjalizacja losowych biasów z przedziału min do max
        for (int i = 0; i < Biases.GetLength(0); i++)
            for (int j = 0; j < Biases[i].Length; j++)
                Biases[i][j] = _randomizer.NextDouble(min, max);
    }

    /// <summary>
    /// Przyjmuje tablicę wartości dla pierwszej warstwy
    /// </summary>
    /// <param name="input"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void ReadInput(int[] input)
    {
        if (input.Length != Values.GetLength(0))
            throw new ArgumentOutOfRangeException(nameof(input));

        for (int i = 0; i < input.Length; i++)
        {
            Values[0][i] = input[i];
        }
    }

    /// <summary>
    /// Funkcja oblicza wartości dla neuronów dla warstw > 1 poprzez
    ///mnożenie wektora wartości z poprzedniej warstwy z wektorem wag pomiędzy warstwami
    /// </summary>
   
    public void ComputeValues()
    {
        double sum;

        for (int i = 1; i < Values.GetLength(0); i++)
            for (int j = 0; j < Values[i].Length; j++)
            {
                sum = Sum(Values[i - 1], Weights[i - 1], j) + Biases[i][j];
                Values[i][j] = ComputeSigmoid(sum); 
            }
    }

    
    public double ComputeTotalError(double[] targetValues)
    {
        double[] outputs = Values[^1];

        if (outputs.Length != targetValues.Length)
            throw new ArgumentOutOfRangeException(nameof(outputs.Length));

        return outputs.Select((v, i) => 0.5 * Math.Pow(v - targetValues[i],2)).Sum();
    }

    /// <summary>
    /// Oblicza wartość funkcji sigmoidalnej 1/(1+e^(-x))
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static double ComputeSigmoid(double x)
    {
        double eToPowerMinusX = Math.Exp(-x);
        return 1 / (1 + eToPowerMinusX);
    }

    public void Train()
    {
        double[] outputs = Values[^1];
        double []outputGradients = Gradient[^1];
        for (int i=0; i < Signals.Length; i++)
        {
            Signals[^1][i] = (TargetValues[i] - outputs[i]) * (1 - outputs[i]);
        }

        for (int i = 0; i < UpdatedWeights[^2].Length; i++)
            for(int j = 0; j < UpdatedWeights[^1].Length; j++)
            {
                UpdatedBiases[^1][j] -= Signals[^1][j] * LearningRate;

                UpdatedWeights[^1][i][j] -= Signals[^1][j] * LearningRate;
            }
    }
    //Zwraca iloczyn wektorów tablicy values z tablicą weigths w (v,i), gdzie i to iterator tablicy values, a j to numer 
    //neuronu, dla którego obliczamy wagu
    public static double Sum(double[] values, double[][] weights, int j) => values.Select((v, i) => v * weights[i][j]).Sum();
    public void TestMethod()
    {
        for (int i = 0; i < Values.Length; i++)
        {
            this.Values[0][i] = 1;
        }
    }

}
