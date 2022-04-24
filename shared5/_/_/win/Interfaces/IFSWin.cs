using System;
using System.Collections.Generic;
using System.Text;

public interface IFSWin
{
    void DeleteFileMaybeLocked(string s);
    void DeleteFileOrFolderMaybeLocked(string p);
}
