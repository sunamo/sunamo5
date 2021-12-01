using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    public class DefaultPaths
    {
        public const string BackupSunamosAppData = @"e:\Sync\Develop of Future\Backups\";
        public const string pathPa = @"d:\pa\";
        public const string pathPaSync = @"d:\paSync\";
        
        public const string capturedUris = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris.txt";
        public const string capturedUris_backup = @"C:\Users\Administrator\AppData\Roaming\sunamo\SunamoCzAdmin\Data\SubsSignalR\CapturedUris_backup.txt";
        
        public const string rootVideos0Kb = @"d:\Documents\Videos0kb\";
        public static string Documents = @"d:\Documents\";
        public static string eDocuments = @"e:\Documents\";
        public static string Docs = @"d:\Docs\";
        public static string Downloads = @"d:\Downloads\";
        public static string Music2 = @"d:\Music2\";
        public static string Backup = @"d:\Documents\Backup\";



        public static string Streamline = @"d:\Pictures\Streamline_All_Icons_PNG\PNG Icons\";

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

        public const string cRepos = @"c:\repos";

        static DefaultPaths()
        {
            string bp = @"e:\Documents\vs\";

            if (VpsHelperSunamo.IsQ)
            {
                bp = @"c:\repos\_\";
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
        /// e:\Documents\vs\Projects\
        /// </summary>
        public static string vsProjects = null;
        /// <summary>
        /// e:\Documents\vs\Projects\
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
        /// d:\vs17\
        /// </summary>
        public static string vs17 = @"e:\vs17\";
        public static string vs17Documents = FS.Combine(DefaultPaths.eDocuments, @"vs17\");
        public static string NormalizePathToFolder = FS.Combine(DefaultPaths.eDocuments, @"vs\Projects\");
        public static string Test_MoveClassElementIntoSharedFileUC = "d:\\_Test\\AllProjectsSearch\\AllProjectsSearch\\MoveClassElementIntoSharedFileUC\\";

        public static List<string> AllPathsToProjects = null;

        public const string SyncArchived = @"e:\SyncArchived\";
        public const string SyncArchivedText = @"e:\SyncArchived\Text\";
        public const string SyncArchivedDrive = @"e:\SyncArchived\Drive\";

        public static List<string> All = new List<string> { Documents, Docs, Downloads, Music2 };
        public static string XnConvert = @"d:\Pictures\XnConvert\";
        public const string PhotosScz = @"d:\Pictures\photos.sunamo.cz\";
    }
}