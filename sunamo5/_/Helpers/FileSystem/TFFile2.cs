using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using sunamo.Essential;

/// <summary>
/// Methods which is calling with TFFIle.cs
/// </summary>
public partial class TF
{
    /// <summary>
    /// A1 cant be storagefile because its
    /// not in 
    /// </summary>
    /// <param name="file"></param>
    /// <param name="content"></param>
    public static void WriteAllText<StorageFolder, StorageFile>(StorageFile file, string content, Encoding enc, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            try
            {
                File.WriteAllText(file.ToString(), content, enc);
            }
            catch (Exception)
            {
                if (throwExcIfCantBeWrite)
                {
                    throw;
                }
            }
        }
        else
        {
            ac.tf.writeAllText.Invoke(file, content);
        }
    }

    public static void WriteAllBytes<StorageFolder, StorageFile>(StorageFile file, List<byte> b, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        if (ac == null)
        {
            var fileS = file.ToString();

            if (LockedByBitLocker(fileS))
            {
                return;
            }

            File.WriteAllBytes(fileS, b.ToArray());

        }
        else
        {
            ac.tf.writeAllBytes(file, b);
        }

    }

    public static void SaveLines(IList<string> list, string file)
    {
        File.WriteAllLines(file, list);
    }

    /// <summary>
    /// Create folder hiearchy and write
    /// </summary>
    /// <param name="path"></param>
    /// <param name="content"></param>
    public static void WriteAllText<StorageFolder, StorageFile>(StorageFile path, string content, AbstractCatalog<StorageFolder, StorageFile> ac)
    {
        FS.CreateUpfoldersPsysicallyUnlessThere(path, ac);

        TF.WriteAllText<StorageFolder, StorageFile>(path, content, Encoding.UTF8, ac);
    }

    /// <summary>
    /// Precte soubor a vrati jeho obsah. Pokud soubor neexistuje, vytvori ho a vrati SE. 
    /// </summary>
    /// <param name="s"></param>
    public static string ReadFile<StorageFolder, StorageFile>(StorageFile s, AbstractCatalog<StorageFolder, StorageFile> ac = null)
    {
        if (readFile)
        {
            if (!File.Exists(s.ToString()))
            {
                return string.Empty;
            }

            if (ac == null)
            {
                FS.MakeUncLongPath<StorageFolder, StorageFile>(ref s, ac);
            }

            var ss = s.ToString();

            if (isUsed != null)
            {
                if (isUsed.Invoke(ss))
                {
                    return string.Empty;
                }
            }

            if (ac == null)
            {
                var ss2 = s.ToString();

                // Způsobovalo mi chybu v asp.net Could not find file 'd:\Documents\sunamo\Common\Settings\CloudProviders'.
                //AppData.ci.GetCommonSettings("CloudProviders");
                if (LockedByBitLocker(ss2))
                {
                    return String.Empty;
                }
                
                //ThisApp.firstReadingFromCloudProvider = 

                //result = enc.GetString(bytesArray);
                return File.ReadAllText(ss2);
            }
            else
            {
                return ac.tf.readAllText(s);
            }
        }
        return string.Empty;

    }
}