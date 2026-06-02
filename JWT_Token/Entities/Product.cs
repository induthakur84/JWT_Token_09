using System;
using System.Collections.Generic;

namespace JWT_Token.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
