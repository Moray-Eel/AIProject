using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIProject.Classes;

namespace AIProject.Classes;

public class NeuralNetwork
{

    //Values for every layer -> Values[i][j] where i -> a layer number,
    //j -> a node number
    public decimal[][] Values { get; set; }

    //Biases for every layer -> Biases[i][j] where i -> a layer number,
    //j -> a node number
    public decimal[][] Biases { get; set; }

    //Weigths for every layer -> Weights[i][j][z] where i-> a layer number, 
    //j -> a node number in a layer i, z-> a node number in a layer i+1
    //If N is a number of layers in our network then -> Weights[z-1][][]
    public decimal[][][] Weights { get; set; }

    public NeuralNetwork(int[] layers)
    {
        Values = new decimal[layers.Length][];
        Biases = new decimal[layers.Length][];
        Weights = new decimal[layers.Length][][];

        for (int i = 0; i < layers.Length; i++)
        {
            Values[i] = new decimal[layers[i]];
            Biases[i] = new decimal[layers[i]];
            Weights[i] = new decimal[layers[i]][];
        }
        for (int i = 0; i < layers.Length - 1; i++)
            for (int j = 0; j < layers.Length; j++)
            {
                Weights[i][j] = new decimal[layers[i+1]];
            }

    }
}
