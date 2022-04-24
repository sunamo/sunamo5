using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Když nemám očíslované, počítá od 0. tedy warning = 0, error = 1, fatal = 2, ve VS debuggeru při error | fatal vidím 3 
/// </summary>
public enum GitTypesOfMessages
    {

        warning,
        error,
        fatal
    }