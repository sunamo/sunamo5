using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public partial class TF
{


    #region For easy copy


    static bool LockedByBitLocker(string path)
    {
        return ThrowEx.LockedByBitLocker(path);
    }

    #region Array
    public static void WriteAllLinesArray(string path, String[] c)
    {
        WriteAllLines(path, c.ToList());
    }

    public static void WriteAllBytesArray(string path, Byte[] c)
    {
        WriteAllBytes(path, c.ToList());
    }

    public static Byte[] ReadAllBytesArray(string path)
    {
        return TF.ReadAllBytes(path).ToArray();
    }
    #endregion

    #region Bytes
    /// <summary>
    /// Only one method where could be TF.ReadAllBytes
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static List<byte> ReadAllBytes(string file)
    {
        if (LockedByBitLocker(file))
        {
            return new List<byte>();
        }

        // Must be File
        return File.ReadAllBytes(file).ToList();
    }
    public static void WriteAllBytes(string file, List<byte> b)
    {


        WriteAllBytes<string, string>(file, b, null);
    }
    #endregion

    #region Lines
    public static void WriteAllLines(string file, IList<string> lines)
    {
        if (LockedByBitLocker(file))
        {
            return;
        }

        SaveLines(lines, file);
    }

    public static List<string> ReadAllLines(string file)
    {


        return ReadAllLines<string, string>(file, null);
    }
    #endregion

    #region Text
    public static void WriteAllText(string path, string content)
    {
#if MB
        TranslateDictionary.ShowMb("WriteAllText ThrowEx.reallyThrow2: " + ThrowEx.reallyThrow2);
#endif

        if (LockedByBitLocker(path))
        {
            return;
        }

        WriteAllText<string, string>(path, content, null);
    }

    public static string ReadAllText(string f)
    {
        if (LockedByBitLocker(f))
        {
            return string.Empty;
        }

        return File.ReadAllText(f);
    }

    public static string ReadAllText(string path, Encoding enc)
    {
        if (isUsed != null)
        {
            if (isUsed.Invoke(path))
            {
                return string.Empty;
            }
        }

        if (enc == null)
        {
            return File.ReadAllText(path);
        }
        else
        {
            return File.ReadAllText(path, enc);
        }
    }
    #endregion 
    #endregion
}