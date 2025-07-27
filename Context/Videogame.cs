using System;
using System.Collections.Generic;

namespace GameZard.Context;

public partial class Videogame
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public byte[]? Cover { get; set; }

    public string ExecutableLocation { get; set; } = null!;

    public int IsSelected { get; set; }

    public virtual PcSavedatum? PcSavedatum { get; set; }
}
