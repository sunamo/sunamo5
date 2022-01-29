﻿using sunamo;
using sunamo.Essential;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AppData : AppDataAbstractBase<string, string>
{
    public static AppData ci = new AppData();
    static Type type = typeof(AppData);
    private AppData()
    {
        
    }

    

    public override string GetFileInSubfolder(AppFolders output, string subfolder, string file, string ext)
    {
        return AppData.ci.GetFile(AppFolders.Output, subfolder + @"\" + file + ext);
    }

    /// <summary>
    /// Return always in User's AppData
    /// </summary>
    /// <param name="inFolderCommon"></param>
    public override string RootFolderCommon(bool inFolderCommon)
    {
        //string appDataFolder = SpecialFO
        string sunamo2 = FS.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
        var redirect = GetSunamoFolder();
        if (!string.IsNullOrEmpty(redirect))
        {
            sunamo2 = redirect;
        }
        if (inFolderCommon)
        {
            return FS.Combine(sunamo2, XlfKeys.Common);
        }

        return sunamo2;
    }

    

    public override string GetFileString(string af, string file, bool pa = false)
    {
        string slozka2, soubor;

        if (Exc.aspnet)
        {
            slozka2 = FS.Combine(basePath, af.ToString());
            soubor = FS.Combine(slozka2, file);
            return soubor;
        }
        else
        {
            var rf = RootFolder;
            if (pa)
            {
                rf = RootFolderPa;
            }

            slozka2 = FS.Combine(rf, af.ToString());
            soubor = FS.Combine(slozka2, file);
            return soubor;
        }
    }

    public string GetFileString(string af, string file)
    {
        return GetFileString(af, file, false);
    }

    public override string GetFile(AppFolders af, string file)
    {
        return GetFileString(af.ToString(), file);
    }

      public string GetFileAppTypeAgnostic(AppFolders af, string file)
    {
        return GetFileString(af.ToString(), file, true);
    }

    public override string GetFolder(AppFolders af)
    {
        var f = RootFolder;
        if (Exc.aspnet)
        {
            f = basePath;
        }

        string vr = FS.Combine(f, af.ToString());
        FS.WithEndSlash(ref vr);
        return vr;
    }

    public override bool IsRootFolderOk()
    {
        if (string.IsNullOrEmpty(rootFolder))
        {
            return false;
        }

        return FS.ExistsDirectory(rootFolder);
    }



    public List<string> ReadFileOfSettingsList(string path)
    {
        return SH.GetLines(ReadFileOfSettingsOther(path));
    }

    public string ReadFolderWithAppsFilesOrDefault(string s)
    {
        var content = TF.ReadFile(s);
        if (content == string.Empty)
        {
            return RootFolderCommon(false);
        }
        return content;
    }

    public override bool IsRootFolderNull()
    {
        var def = default(string);
        if (!EqualityComparer<string>.Default.Equals(rootFolder, def))
        {
            // is not null
            return rootFolder == string.Empty;
        }
        return true;
    }

    public override void AppendToFile(string content, string sf)
    {
        TF.AppendToFile(content, sf);
    }





     public override string GetRootFolder()
    {
        rootFolder = GetSunamoFolder();
        

        //pa ? SH.RemoveAfterFirst(ThisApp.Name, AllChars.dot) :
        RootFolder = FS.Combine(rootFolder,  ThisApp.Name);
        RootFolderPa = FS.Combine(FS.GetDirectoryName( rootFolder), SH.RemoveAfterFirst( ThisApp.Name, AllChars.dot));
        FS.CreateDirectory(RootFolder);
        FS.CreateDirectory(RootFolderPa);
        return RootFolder;
    }

    protected override void SaveFile(string content, string sf)
    {
        TF.SaveFile(content, sf);
    }

    public override void AppendToFile(AppFolders af, string file, string value)
    {
        ThrowExceptions.NotImplementedMethod(Exc.GetStackTrace(),type, Exc.CallingMethod());
    }

    public override  string GetSunamoFolder()
    {

            string r = AppData.ci.GetFolderWithAppsFiles();
            string sunamoFolder = TF.ReadFile(r);

            if (string.IsNullOrWhiteSpace(sunamoFolder))
            {
                sunamoFolder = FS.Combine(SpecialFoldersHelper.AppDataRoaming(), Consts.@sunamo);
            }
            return sunamoFolder;

    }

    /// <summary>
    /// Dont use - instead of this GetCommonSettings
    /// Without ext because all is crypted and in bytes
    /// Folder is possible to obtain A1 = null
    /// </summary>
    /// <param name="filename"></param>
    public override string GetFileCommonSettings(string filename)
    {
        var fc = RootFolderCommon(true);
        var vr = FS.Combine(fc, AppFolders.Settings.ToString(), filename);
        return vr;
    }

    public static Func<List<byte>, List<byte>> RijndaelBytesDecrypt;
    public static Func<List<byte>, List<byte>> RijndaelBytesEncrypt;

    public override string GetCommonSettings(string key)
    {
        var file = GetFileCommonSettings(key);
        var b = TF.ReadAllBytes(file);
        var b2 = RijndaelBytesDecrypt(b);
        var b3 = b2.ToArray();
        var vr = Encoding.UTF8.GetString(b3);
        vr = vr.Replace("\0", "");
        return vr;
    }

    public override void SetCommonSettings(string key, string value)
    {
        var file = GetFileCommonSettings(key);
        TF.WriteAllBytes(file, RijndaelBytesEncrypt(Encoding.UTF8.GetBytes(value).ToList()));
    }

    
}