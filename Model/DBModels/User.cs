using System;
using System.Collections.Generic;

namespace Model.DBModels;

public partial class User
{
    public int Userid { get; set; }

    public string Name { get; set; } = null!;

    public string? MoblieNumber { get; set; }

    public string? City { get; set; }
}
