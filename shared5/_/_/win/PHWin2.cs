using System;
using System.Collections.Generic;
using System.Text;

public class PHWin : IPHWin
{
    public static PHWin ci = new PHWin();
    public static IPHWin p = null;

    public void Code(string e)
    {
        p.Code(e);
    }
}