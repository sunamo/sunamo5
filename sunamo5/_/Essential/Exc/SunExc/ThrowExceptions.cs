using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public partial class ThrowEx
{
    #region For easy copy from ThrowEx.cs


    #region DifferentCountInLists
    public static void FolderCannotBeDeleted( string repairedBlogPostsFolder, Exception ex)
    {
        ThrowIsNotNull(Exceptions.FolderCannotBeDeleted(FullNameOfExecutedCode(t.Item1, t.Item2, true), repairedBlogPostsFolder, ex));
    }
    #endregion
    #endregion
}