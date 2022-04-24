using System;
using System.Collections.Generic;
using System.Text;

public class FSWin : IFSWin
{
    public static IFSWin p = null;
    public static FSWin ci = new FSWin();

    private FSWin()
    {
            
    }

    public void DeleteFileMaybeLocked(string s)
    {
        p.DeleteFileMaybeLocked(s);
    }

    public void DeleteFileOrFolderMaybeLocked(string p2)
    {
        p.DeleteFileOrFolderMaybeLocked(p2);
    }
}