using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject.Classes;

public class Layer
{
    public List<Neuron> Neurons { get; set; }
    public Layer(int neurons, int? neuronsNextLayer = null)
    {
        Neurons = new List<Neuron>(neurons);

        for (int i = 0; i < neurons; i++)
            Neurons[i] = new Neuron(neuronsNextLayer);

    }
}
