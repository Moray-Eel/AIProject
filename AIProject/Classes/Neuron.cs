using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject.Classes;

public class Neuron
{
    public decimal Weight;
    public List<Edge> Edges { get; set; } = null;

    public Neuron(int? edges)
    {
        if (edges is not null)
            Edges = new List<Edge>(); 
    }

}


