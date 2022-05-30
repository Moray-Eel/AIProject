﻿using System;
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
    //Jeśli nasza sieć ma N warstw to -> Biases[N-1][][]
    public double[][] Biases { get; set; }

    //Nowe biasy dla kazdej warstwy -> Biases[i][j] gdzie i -> a numer warstwy,
    //j -> numer neuronu
    //Jeśli nasza sieć ma N warstw to -> Biases[N-1][][]
    //j -> numer neuronu
    public double[][] UpdatedBiases { get; set; }

    //Wagi dla każdej warstwy -> Weights[i][j][k] gdzie i-> numer warstwy, 
    //j -> numer neuronu w warstwie i, k-> numer neuronu w warstwie i+1
    //Jeśli nasza sieć ma N warstw to -> Weights[N-1][][]
    public double[][][] Weights { get; set; }

    //Nowe wagi dla każdej warstwy -> Weights[i][j][k] gdzie i-> numer warstwy, 
    //j -> numer neuronu w warstwie i, k-> numer neuronu w warstwie i+1
    //Jeśli nasza sieć ma N warstw to -> UpdatedWeights[N-1][][]
    public double[][][] UpdatedWeights { get; set; }

    //Wartości gradientów dla neuronów w warstwach gdzie i-> numer warstwy,
    //j -> numer neuronu w warstwie
    //Jeśli nasza sieć ma N warstw to -> Signals[N-1][][]
    public double[][] Signals { get; set; }

    //Wartości z poprawnymi wynikami dla poszczególnych danych wejściowych
    public double[] TargetValues { get; set; }

    //Współczynnik uczenia
    public double LearningRate = 0.05;

    public NeuralNetwork(int[] layers)
    {
        //Tablica wartości dla poszczególnych neuronów
        Values = new double[layers.Length][];
        
        //Tablice biasów 
        Biases = new double[layers.Length-1][];
        UpdatedBiases = new double[layers.Length-1][];
        
        //Tablice wag
        Weights = new double[layers.Length-1][][];
        UpdatedWeights = new double[layers.Length - 1][][];
        
        //Tablica gradientu
        Signals = new double[layers.Length - 1][];
        
        //Tablica spodziewanych wyników
        TargetValues = new double[layers[0]];


        for (int i = 0; i < layers.Length; i++)
        {
            Values[i] = new double[layers[i]];
        }

        for (int i = 0; i < layers.Length-1 ; i++)
        {

            Weights[i] = new double[layers[i]][];
            UpdatedWeights[i] = new double[layers[i]][];
            
            Signals[i] = new double[layers[i+1]];
            Biases[i] = new double[layers[i+1]];
            UpdatedBiases[i] = new double[layers[i+1]];

            for (int j = 0; j < Weights[i].Length; j++)
            {
                Weights[i][j] = new double[layers[i+1]];
                UpdatedWeights[i][j] = new double[layers[i+1]];
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
    /// Oblicza wartości dla neuronów dla warstw > 1 poprzez
    ///mnożenie wektora wartości z poprzedniej warstwy z wektorem wag pomiędzy warstwami oraz odjęciu biasu
    /// </summary>
   
    public void ComputeValues()
    {
        double sum;

        for (int i = 1; i < Values.GetLength(0); i++)
            for (int j = 0; j < Values[i].Length; j++)
            {
                sum = Sum(Values[i - 1], Weights[i - 1], j) + Biases[i-1][j];
                Values[i][j] = ComputeSigmoid(sum); 
            }
    }

    /// <summary>
    /// Oblicza wartość błędu sieci jako sumę połów kwadratów różnic pomiędzy
    /// wartością oczekiwaną a wynikiem
    /// </summary>
    /// <param name="targetValues"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public double ComputeTotalError(double[] targetValues)
    {
        double[] outputs = Values[^1];

        if (outputs.Length != targetValues.Length)
            throw new ArgumentOutOfRangeException(nameof(outputs.Length));

        return outputs.Select((v, i) => 0.5 * Math.Pow(targetValues[i] - v,2)).Sum();
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

    public void Train(double[] values, double[] target)
    {
        Values[0] = values;
        TargetValues = target;
        InitializeWeightsAndBiases(-1,1);
        ComputeValues();
        
        Console.WriteLine(ComputeTotalError(target));
        double[] outputs = Values[^1];
        
        for (int i=0; i < Signals[^1].Length; i++)
        {
            Signals[^1][i] = (TargetValues[i] - outputs[i]) * outputs[i] * (1 - outputs[i]);
        }

        //Updating values for the hidden layer
        for (int i = 0; i < Signals[^1].Length; i++)
        {
            UpdatedBiases[^1][i] = Biases[^1][i] - Signals[^1][i] * LearningRate;
            for(int j = 0; j < UpdatedWeights[^1].Length; j++)
            {
                
                //Od każdej wagi odejmujemy gradient przemnożony przez wartość learning rate
                UpdatedWeights[^1][j][i] = Weights[^1][j][i] - Signals[^1][i] * LearningRate * Values[^2][j];
            }
        }  
        for(int i = UpdatedWeights.Length-1; i>0 ;i--)
        {

            for(int j = 0; j < UpdatedWeights[i].Length; j++)
            {

                double sum = 0;
                for(int k = 0; k < Signals[i].Length; k++)
                {

                    sum += Signals[i][k] * Weights[i][j][k] * Values[i][j] * (1 - Values[i][j]);
                }

                Signals[i-1][j] = sum;
                UpdatedBiases[i - 1][j] = Biases[i-1][j] - Signals[i-1][j] * LearningRate;
                
            }

            for (int j = 0; j < UpdatedWeights[i - 1].Length; j++)
            {
                for (int k = 0; k < UpdatedWeights[i - 1][j].Length; k++)
                {
                    UpdatedWeights[i-1][j][k] = Weights[i-1][j][k]  - Signals[i-1][k] * Values[i-1][j] * LearningRate;
                }

            }

        }

        ChangeWeights();
    }

    public void ChangeWeights()
    {
        for (int i = 0; i < Weights.Length; i++)
            for (int j = 0; j < Weights[i].Length; j++)
                for (int k = 0; k < Weights[i][j].Length; k++)
                {
                    Weights[i][j][k] = UpdatedWeights[i][j][k];
                }
        for (int i = 0; i < Biases.Length; i++)
        { }

        }
    }

    //Zwraca iloczyn wektorów tablicy values z tablicą weigths w (v,i), gdzie i to iterator tablicy values, a j to numer 
    //neuronu, dla którego obliczamy wagu
    public static double Sum(double[] values, double[][] weights, int j) => values.Select((v, i) => v * weights[i][j]).Sum();
    public void TestMethod()
    {
        for (int i = 0; i < Values[0].Length; i++)
        {
            this.Values[0][i] = 1;
        }
    }
    public void TestMethod2()
    {

    }

    public double[][] ReadFromFile(string filePath) // funkcja czytająca dane z pliku, zwracająca je w postaci macierzy dwuwymiarowej typu double
    {
        int i = 0;

        int lineCount = File.ReadAllLines(filePath).Length;

        double[][] matrix = new double[lineCount][];

        foreach (string line in System.IO.File.ReadLines(filePath))
        {
            string[] split = line.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            double[] values = new double[split.Length];

            for (int n = 0; n < split.Length; n++)
                values[n] = double.Parse(split[n]);



            for (int j = 0; j < values.Length; j++)
                matrix[i][j] = values[j];

            i++;
        }
        return matrix;
    }

}
