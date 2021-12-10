using sunamo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class ThrowExceptions
{
    #region For easy copy from ThrowExceptions.cs
    #region DifferentCountInLists
    public static void FolderCannotBeDeleted(string stacktrace, object type, string methodName, string repairedBlogPostsFolder, Exception ex)
    {
        ThrowIsNotNull(stacktrace, Exceptions.FolderCannotBeDeleted(FullNameOfExecutedCode(type, methodName, true), repairedBlogPostsFolder, ex));
    }
    #endregion
    #endregion
}