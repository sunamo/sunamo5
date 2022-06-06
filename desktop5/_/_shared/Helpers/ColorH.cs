using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using sunamo.Enums;

/// <summary>
/// Is shared between apps ColorH and shared's ColorH 
/// </summary>
public partial class ColorH
{
    

    public static bool IsColorSame(PixelColor first, PixelColor pxsi)
    {
        return first.Red == pxsi.Red && first.Green == pxsi.Green && first.Blue == pxsi.Blue;
    }
}