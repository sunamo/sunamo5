using sunamo.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static partial class SunamoSizeExtensions
{
    public static System.Windows.Size ToSystemWindows(this SunamoSize ss)
    {
        return new System.Windows.Size(ss.Width, ss.Height);
    }

    #region Musí být zde páč je vyžadovaná v PicturesHelperFw
    public static System.Drawing.Size ToSystemDrawing(this SunamoSize ss)
    {
        return new System.Drawing.Size((int)ss.Width, (int)ss.Height);
    } 
    #endregion
}