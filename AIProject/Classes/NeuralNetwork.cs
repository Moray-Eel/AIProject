using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIProject.Classes;

namespace AIProject.Classes;

public class NeuralNetwork
{

    private readonly Random _randomizer = new Random();
    //Values for every layer -> Values[i][j] where i -> a layer number,
    //j -> a node number
    public double[][] Values { get; set; }

    //Biases for every layer -> Biases[i][j] where i -> a layer number,
    //j -> a node number
    public double[][] Biases { get; set; }

    //Weigths for every layer -> Weights[i][j][k] where i-> a layer number, 
    //j -> a node number in a layer i, k-> a node number in a layer i+1
    //If N is a number of layers in our network then -> Weights[N-1][][]
    public double[][][] Weights { get; set; }

    public NeuralNetwork(int[] layers)
    {

        Values = new double[layers.Length][];
        Biases = new double[layers.Length][];
        Weights = new double[layers.Length][][];

        for (int i = 0; i < layers.Length; i++)
        {
            Values[i] = new double[layers[i]];
            Biases[i] = new double[layers[i]];
            Weights[i] = new double[layers[i]][];
        }
        for (int i = 0; i < layers.Length - 1; i++)
            for (int j = 0; j < layers.Length; j++)
            {
                Weights[i][j] = new double[layers[i+1]];
            }

    }
}
