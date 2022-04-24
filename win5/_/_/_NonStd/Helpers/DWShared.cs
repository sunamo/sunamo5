﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


    public partial class DW
    {
        /// <summary>
        /// Filter is set do default - PP filterDefault
        /// InitialFolder is MyDocuments
        /// G null při nenalezení
        /// </summary>
        public static string SelectOfFile()
        {
            return SelectOfFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }
        /// <summary>
        /// G null při nenalezení
        /// Filter is set do default - PP filterDefault
        /// </summary>
        /// <param name = "initialFolder"></param>
        public static string SelectOfFile(Environment.SpecialFolder initialFolder)
        {
            return SelectOfFile(Environment.GetFolderPath(initialFolder));
        }

        /// <summary>
        /// As filter is set filterDefault, multiselect is enabled.
        /// </summary>
        /// <param name = "sf"></param>
        public static List<string> SelectOfFiles(Environment.SpecialFolder sf)
        {
            return SelectOfFiles(Environment.GetFolderPath(sf));
        }



        /// <summary>
        /// Default is All Files|*.*
        /// </summary>
        public static string filterDefault = sess.i18n(XlfKeys.AllFiles) + AllStrings.verbar + "*.*";

        /// <summary>
        /// G null při nenalezení
        /// Filter is set do default - PP filterDefault
        /// </summary>
        /// <param name = "initialFolder"></param>
        public static string SelectOfFile(string initialFolder)
        {
            return SelectOfFile(filterDefault, initialFolder);
        }

        /// <summary>
        /// G null při nenalezení
        /// </summary>
        /// <param name = "initialFolder"></param>
        public static string SelectOfFile(AppFolders initialFolder)
        {
            return SelectOfFile(AppData.ci.GetFolder(initialFolder));
        }

        /// <summary>
        /// As filter is set filterDefault, multiselect is enabled.
        /// </summary>
        /// <param name = "initialFolder"></param>
        public static List<string> SelectOfFiles(string initialFolder)
        {
            return SelectOfFiles(filterDefault, initialFolder, true);
        }

        /// <summary>
        /// G null při nenalezení
        /// </summary>
        /// <param name = "filter"></param>
        /// <param name = "initialFolder"></param>
        public static string SelectOfFile(string filter, string initialFolder)
        {
            List<string> d = SelectOfFiles(filter, initialFolder, false);
            if (CA.HasAtLeastOneElementInArray(d))
            {
                return d[0];
            }

            return null;
        }

        /// <summary>
        /// Vrati seznam vybranych souboru nebo null
        /// 
        /// </summary>
        /// <param name = "filtr"></param>
        /// <param name = "slozka"></param>
        /// <param name = "multiselect"></param>
        private static List<string> SelectOfFiles(string filter, string initialDirectory, bool multiselect)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.CustomPlaces.Add(new FileDialogCustomPlace(FS.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Consts.@sunamo)));
            ofd.AddExtension = false;
            ofd.InitialDirectory = initialDirectory;
            //ofd.AutoUpgradeEnabled = true;
            ofd.DereferenceLinks = false;
            ofd.Filter = FS.RepairFilter(filter);
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            ofd.RestoreDirectory = true;
            ofd.SupportMultiDottedExtensions = true;
            ofd.ValidateNames = true;
            ofd.Multiselect = multiselect;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                return ofd.FileNames.ToList();
            }

            return null;
        }
    }
