using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    public class DefaultPaths
    {
        public const string BackupSunamosAppData = @"E:\Sync\Develop of Future\Backups\";
        public const string pathPa = @"D:\pa\";
        public const string pathPaSync = @"D:\paSync\";
        
        public const string capturedUris = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris.txt";
        public const string capturedUris_backup = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris_backup.txt";
        
        public const string rootVideos0Kb = @"D:\Documents\Videos0kb\";
        public static string Documents = @"D:\Documents\";
        public static string eDocuments = @"E:\Documents\";
        public static string Docs = @"D:\Docs\";
        public static string Downloads = @"D:\Downloads\";
        public static string Music2 = @"D:\Music2\";
        public static string Backup = @"D:\Documents\Backup\";



        public static string Streamline = @"D:\Pictures\Streamline_All_Icons_PNG\PNG Icons\";

        /// <summary>
        /// For all is here sczRootPath
        /// edn with bs
        /// </summary>
        public static string sczPath = FS.Combine(eDocuments, @"vs\Projects\sunamo.cz\sunamo.cz\");
        public static string sczOldPath = FS.Combine(eDocuments, @"vs\Projects\sunamo.cz\sunamo.cz-old\");
        public static string sczNsnPath = FS.Combine(eDocuments, @"vs\Projects\sunamo.cz\sunamo.cz-nsn\");
        /// <summary>
        /// Ended with backslash
        /// </summary>
        public static string sczRootPath = FS.Combine(eDocuments, @"vs\Projects\sunamo.cz\");
        public const string ProjectsFolderNameSlash = "Projects\\";

        #region vs

        public const string cRepos = @"C:\repos";

        public const string bpMb = @"E:\Documents\vs\";
        public const string bpQ = @"C:\repos\_\";
        public const string bpVps = @"C:\_\";

        public const string bpBb = @"D:\Documents\BitBucket\";

        public static string bp = null;

        static DefaultPaths()
        {
            bp = bpMb;

            if (VpsHelperSunamo.IsQ)
            {
                bp = bpQ;
            }

            sunamo = bp + @"Projects\sunamo\";
            sunamoProject = bp + @"Projects\sunamo\sunamo\";
            vsProjects = bp + @"Projects\";
            vs = bp + @"Projects\";
            KeysXlf = bp + @"Projects\sunamo\sunamo\Enums\KeysXlf.cs";
            DllSunamo = bp + @"Projects\sunamo\dll\";
            VisualStudio2017 = bp ;
            VisualStudio2017WoSlash = bp.Substring(0, bp.Length -1);

            AllPathsToProjects = CA.ToListString(Test_MoveClassElementIntoSharedFileUC, vs, vsDocuments, vs17 + ProjectsFolderNameSlash, vs17Documents + ProjectsFolderNameSlash, NormalizePathToFolder);
        }

        /// <summary>
        /// Solution, not project
        /// </summary>
        public static string sunamo = null;
        /// <summary>
        /// Cant be used also VpsHelperSunamo.SunamoProject()
        /// </summary>
        public static string sunamoProject = null;
        /// <summary>
        /// E:\Documents\vs\Projects\
        /// </summary>
        public static string vsProjects = null;
        /// <summary>
        /// E:\Documents\vs\Projects\
        /// </summary>
        public static string vs = null;
        public static string KeysXlf = null;
        public static string DllSunamo = null;
        public static string VisualStudio2017 = null;
        public static string VisualStudio2017WoSlash = null;

        
        #endregion




        public static string vsDocuments = FS.Combine(DefaultPaths.eDocuments, @"vs\");
        /// <summary>
        /// Use vs for non shortcuted folder
        /// D:\vs17\
        /// </summary>
        public static string vs17 = @"E:\vs17\";
        public static string vs17Documents = FS.Combine(DefaultPaths.eDocuments, @"vs17\");
        public static string NormalizePathToFolder = FS.Combine(DefaultPaths.eDocuments, @"vs\Projects\");
        public static string Test_MoveClassElementIntoSharedFileUC = "D:\\_Test\\AllProjectsSearch\\AllProjectsSearch\\MoveClassElementIntoSharedFileUC\\";

        public static List<string> AllPathsToProjects = null;

        public const string SyncArchived = @"E:\SyncArchived\";
        public const string SyncArchivedText = @"E:\SyncArchived\Text\";
        public const string SyncArchivedDrive = @"E:\SyncArchived\Drive\";

        public static List<string> All = new List<string> { Documents, Docs, Downloads, Music2 };
        public static string XnConvert = @"D:\Pictures\XnConvert\";
        public const string PhotosScz = @"D:\Pictures\photos.sunamo.cz\";
    }
}