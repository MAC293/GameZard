using System;
using System.Collections.Generic;

namespace GameZard.Context;

public partial class EmulatorSavedatum
{
    public string Id { get; set; } = null!;

    public string BackupMode { get; set; } = null!;

    public string? FromPath { get; set; }

    public string? ToPath { get; set; }

    public string? LastSave { get; set; }

    public string Emulator { get; set; } = null!;

    public virtual Emulator EmulatorNavigation { get; set; } = null!;
}
