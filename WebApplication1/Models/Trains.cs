using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Trains
{
    public int ID { get; set; }

    public int Train_Number { get; set; }

    public string Train_Name { get; set; } = null!;

    public string? Source { get; set; }

    public string? Destination { get; set; }

}
