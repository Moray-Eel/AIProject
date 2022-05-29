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

    private readonly Random _randomizer = new Random();
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

    public double[][][] UpdatedWeights { get; set; }
    public double[][][] Gradient { get; set; }
    public double[][] Signals { get; set; }
    public double[] TargetValues { get; set; }




    public double LearningRate = 0.001;

    public NeuralNetwork(int[] layers)
    {
        Values = new double[layers.Length][];
        Biases = new double[layers.Length][];
        Weights = new double[layers.Length-1][][];
        UpdatedWeights = new double[layers.Length - 1][][];
        Gradient = new double[layers.Length - 1][][];


        for (int i = 0; i < layers.Length; i++)
        {
            Values[i] = new double[layers[i]];
            Biases[i] = new double[layers[i]];
        }
        for (int i = 0; i < layers.Length-1 ; i++)
        {
            Weights[i] = new double[layers[i]][];
            UpdatedWeights[i] = new double[layers[i]][];


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
            throw new ArgumentOutOfRangeException("input");

        for (int i = 0; i < input.Length; i++)
        {
            Values[0][i] = input[i];
        }
    }
    
    
    //Funkcja oblicza wartości dla neuronów dla warstw > 1 poprzez
    //mnożenie wektora wartości z poprzedniej warstw z wektorem wag 
    public void ComputeValues()
    {
        for (int i = 1; i < Values.GetLength(0); i++)
            for (int j = 0; j < Values[i].Length; j++)
            {
                Values[i][j] = Sum(Values[i - 1], Weights[i - 1], j) + Biases[i][j];
            }
    }

    public void Train()
    {
        for(int i=0; i < Values[Values.Length-1].Length; i++)
        {
            
        }
    }
    //Zwraca iloczyn wektorów tablicy values z tablicą weigths w (v,i), gdzie i to iterator tablicy values, a j to numer 
    //neuronu, dla którego obliczamy wagu
    public double Sum(double[] values, double[][] weights, int j) => values.Select((v, i) => v * weights[i][j]).Sum();
    public void TestMethod()
    {
        this.Values[0][0] = 1;
        this.Values[0][1] = 1;
        this.Values[0][2] = 1;
        this.Values[0][3] = 1;
        this.Values[0][4] = 1;
        this.Values[0][5] = 1;
        this.Values[0][6] = 1;

    }

}
