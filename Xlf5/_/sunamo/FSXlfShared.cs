using SunamoExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class FSXlf
{
    #region For easy shared
    public static void FileToDirectory(ref string dir)
    {
        if (!dir.EndsWith(AllStrings.bs))
        {
            dir = FS.GetDirectoryName(dir);
        }
    }

    #endregion
}
