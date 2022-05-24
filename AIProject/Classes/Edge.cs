using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIProject.Classes;

public class Edge
{
    public decimal Weight { get; set; }
    public Edge(decimal weight)
    {
        Weight = weight;
    }
}

