using System;
using System.Collections.Generic;
using System.Text;

public class DW : IDW
{
    public static IDW p = null;
    public static DW ci = new DW();

    public  string SelectOfFolder(string rootFolder)
    {
        return p.SelectOfFolder(rootFolder);
    }

    public  string SelectOfFolder(Environment.SpecialFolder rootFolder)
    {
        return p.SelectOfFolder(rootFolder);
    }
}