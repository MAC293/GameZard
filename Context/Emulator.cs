using System;
using System.Collections.Generic;

namespace GameZard.Context;

public partial class Emulator
{
    public string Name { get; set; } = null!;

    public byte[]? Icon { get; set; }

    public string Console { get; set; } = null!;

    public string? ExecutableLocation { get; set; }

    public bool IsSelected { get; set; }

    public virtual EmulatorSavedatum? EmulatorSavedatum { get; set; }
}
