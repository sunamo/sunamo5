using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class TF
{
    static Type type = typeof(TF);
    public static Func<string, bool> isUsed = null;

    public static List<string> GetLines(string item)
    {
        return GetLines<string, string>(item, null);
    }

    public static List<string> GetLines<StorageFolder, StorageFile>(StorageFile item, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        return ReadAllLines<StorageFolder, StorageFile>(item, ac);
    }







    public static void SaveLinesIEnumerable(IEnumerable belowZero, string f)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in belowZero)
        {
            sb.AppendLine(item.ToString());
        }
        TF.SaveFile(sb.ToString(), f);
    }




    public static List<string> ReadAllLines<StorageFolder, StorageFile>(StorageFile file, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        return SH.GetLines(ReadFile<StorageFolder, StorageFile>(file, ac));
    }

    /// <summary>
    /// Just one command File.Write* can be wrapped with it
    /// </summary>
    public static bool throwExcIfCantBeWrite = false;

    public static void SaveFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, false);
    }

    private static void SaveFile(string obsah, string soubor, bool pripsat)
    {
        var dir = FS.GetDirectoryName(soubor);

        ThrowExceptions.DirectoryWasntFound(Exc.GetStackTrace(), type, "SaveFile", dir);

        if (soubor == null)
        {
            return;
        }
        if (pripsat)
        {
            File.AppendAllText(soubor, obsah);
        }
        else
        {
            TF.WriteAllText(soubor, obsah, Encoding.UTF8);
        }
    }


    public static void AppendToFile(string obsah, string soubor)
    {
        SaveFile(obsah, soubor, true);
    }

    /// <summary>
    /// Return string.Empty when file won't exists
    /// Use FileUtil.WhoIsLocking to avoid error The process cannot access the file because it is being used by another process
    /// </summary>
    /// <param name="s"></param>
    public static string ReadFile(string s)
    {
        return ReadFile<string, string>(s);
    }

    public static bool readFile = true;





    public static void CreateEmptyFileWhenDoesntExists(string path)
    {
        CreateEmptyFileWhenDoesntExists<string, string>(path, null);
    }

    public static void CreateEmptyFileWhenDoesntExists<StorageFolder, StorageFile>(StorageFile path, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (!FS.ExistsFile(path, ac))
        {
            FS.CreateUpfoldersPsysicallyUnlessThere<StorageFolder, StorageFile>(path, ac);
            TF.WriteAllText<StorageFolder, StorageFile>(path, "", Encoding.UTF8, ac);
        }
    }

}
