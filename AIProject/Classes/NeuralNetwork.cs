using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIProject.Classes;

namespace AIProject.Classes;

public class NeuralNetwork
{
    public decimal Bias { get; set; }
    public List<Layer> Layers { get; set; }
    public NeuralNetwork(int layers, int[] neuronInLayers)
    {
        Layers = new List<Layer>(layers);

        for (int i = 0; i < layers; i++)
        {
            if (i == layers - 1)
                Layers[i] = new Layer(neuronInLayers[i], null);
            else
                Layers[i] = new Layer(neuronInLayers[i], neuronInLayers[i + 1]);
        }
    }
}
