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

    }


}
