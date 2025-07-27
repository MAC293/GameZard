using System;
using System.Collections.Generic;

namespace GameZard.Context;

public partial class PcSavedatum
{
    public string Id { get; set; } = null!;

    public string BackupMode { get; set; } = null!;

    public string? FromPath { get; set; }

    public string? ToPath { get; set; }

    public string? LastSave { get; set; }

    public string Videogame { get; set; } = null!;

    public virtual Videogame VideogameNavigation { get; set; } = null!;
}
