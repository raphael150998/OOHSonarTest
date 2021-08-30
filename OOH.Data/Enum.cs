using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OOH.Data
{
    public enum Direction
    {
        NorteSur = 1,

        SurNorte = 2,

        OrientePoniente,

        PonienteOriente
    }

    public enum ActionPermission
    {
        Read = 0,
        Create = 1,
        Delete = 2,
        Update = 3,
        Execute = 4,
        NoAction = 5
    }

    public enum Languages
    {
        [Description("Español")]
        es = 1,

        [Description("Inglés")]
        en = 2
    }
}
