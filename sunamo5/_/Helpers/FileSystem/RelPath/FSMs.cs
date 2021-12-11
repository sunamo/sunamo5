using System;
using System.Collections.Generic;
using System.Text;


public partial class FS
{
    #region For easy copy
    /// <summary>
    /// Create a relative path from one path to another. Paths will be resolved before calculating the difference.
    /// Default path comparison for the active platform will be used (OrdinalIgnoreCase for Windows or Mac, Ordinal for Unix).
    /// </summary>
    /// <param name="relativeTo">The source path the output should be relative to. This path is always considered to be a directory.</param>
    /// <param name="path">The destination path.</param>
    /// <returns>The relative path or <paramref name="path"/> if the paths don't share the same root.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="relativeTo"/> or <paramref name="path"/> is <c>null</c> or an empty string.</exception>
    public static string GetRelativePath(string relativeTo, string path)
    {
        return GetRelativePath(relativeTo, path, PathInternal.StringComparison);
    }

    public static readonly char DirectorySeparatorChar = PathInternal.DirectorySeparatorChar;

    private static string GetRelativePath(string relativeTo, string path, StringComparison comparisonType)
    {
        if (relativeTo == null)
            throw new ArgumentNullException(nameof(relativeTo));

        //if (PathInternal.IsEffectivelyEmpty(relativeTo.AsSpan()))
        //    throw new ArgumentException(SR.Arg_PathEmpty, nameof(relativeTo));

        if (path == null)
            throw new ArgumentNullException(nameof(path));

        //if (PathInternal.IsEffectivelyEmpty(path.AsSpan()))
        //    throw new ArgumentException(SR.Arg_PathEmpty, nameof(path));

        //Debug.Assert(comparisonType == StringComparison.Ordinal || comparisonType == StringComparison.OrdinalIgnoreCase);

        relativeTo = FS.GetFullPath(relativeTo);
        path = FS.GetFullPath(path);

        // Need to check if the roots are different- if they are we need to return the "to" path.
        if (!PathInternal.AreRootsEqual(relativeTo, path, comparisonType))
            return path;

        int commonLength = PathInternal.GetCommonPathLength(relativeTo, path, ignoreCase: comparisonType == StringComparison.OrdinalIgnoreCase);

        // If there is nothing in common they can't share the same root, return the "to" path as is.
        if (commonLength == 0)
            return path;

        // Trailing separators aren't significant for comparison
        int relativeToLength = relativeTo.Length;
        if (EndsInDirectorySeparator(relativeTo.AsSpan()))
            relativeToLength--;

        bool pathEndsInSeparator = EndsInDirectorySeparator(path.AsSpan());
        int pathLength = path.Length;
        if (pathEndsInSeparator)
            pathLength--;

        // If we have effectively the same path, return "."
        if (relativeToLength == pathLength && commonLength >= relativeToLength) return ".";

        // We have the same root, we need to calculate the difference now using the
        // common Length and Segment count past the length.
        //
        // Some examples:
        //
        //  C:\Foo C:\Bar L3, S1 -> ..\Bar
        //  C:\Foo C:\Foo\Bar L6, S0 -> Bar
        //  C:\Foo\Bar C:\Bar\Bar L3, S2 -> ..\..\Bar\Bar
        //  C:\Foo\Foo C:\Foo\Bar L7, S1 -> ..\Bar

        StringBuilder sb = new StringBuilder();
        //var sb = new ValueStringBuilder(stackalloc char[260]);
        //sb.EnsureCapacity(Math.Max(relativeTo.Length, path.Length));

        // Add parent segments for segments past the common on the "from" path
        if (commonLength < relativeToLength)
        {
            sb.Append("..");

            for (int i = commonLength + 1; i < relativeToLength; i++)
            {
                if (PathInternal.IsDirectorySeparator(relativeTo[i]))
                {
                    sb.Append(DirectorySeparatorChar);
                    sb.Append("..");
                }
            }
        }
        else if (PathInternal.IsDirectorySeparator(path[commonLength]))
        {
            // No parent segments and we need to eat the initial separator
            //  (C:\Foo C:\Foo\Bar case)
            commonLength++;
        }

        // Now add the rest of the "to" path, adding back the trailing separator
        int differenceLength = pathLength - commonLength;
        if (pathEndsInSeparator)
            differenceLength++;

        if (differenceLength > 0)
        {
            if (sb.Length > 0)
            {
                sb.Append(DirectorySeparatorChar);
            }

            sb.Append(path.AsSpan(commonLength, differenceLength).ToString());
        }

        return sb.ToString();
    }

    /// <summary>
    /// Returns true if the path ends in a directory separator.
    /// </summary>
    public static bool EndsInDirectorySeparator(ReadOnlySpan<char> path) => PathInternal.EndsInDirectorySeparator(path);

    public static string Postfix(string arg1, string v)
    {
        arg1 = arg1.TrimEnd(AllChars.bs) + v + AllStrings.bs;
        return arg1;
    }
    #endregion
}
