﻿
using desktop.AwesomeFont;
using desktop.Controls;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace desktop.Controls.Input
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SelectMoreFolders : UserControl
    {

        //        public event VoidVoid SaveSetAsTemplate;
        //        /// <summary>
        //        /// Only adding folder with empty path
        //        /// </summary>
        //        public event Action<object, List<string>> FolderAdded;
        //        public event Action<object, List<string>> FolderChanged;
        //        public event Action<object, List<string>> FolderRemoved;

        //        public SelectMoreFolders()
        //        {
        //            InitializeComponent();

        //            Loaded += SelectMoreFolders_Loaded;
        //        }

        //        private void SelectMoreFolders_Loaded(object sender, RoutedEventArgs e)
        //        {
        //            SetAwesomeIcons();

        //            AddFolder(string.Empty);
        //        }

        //        public void RemoveAllFolders()
        //        {
        //            for (int i = spFolders.Children.Count - 1; i >= 0; i--)
        //            {
        //                Sf_FolderRemoved((SelectFolder)spFolders.Children[i]);
        //            }
        //        }

        //        public void AddFolder(string folder)
        //        {
        //            //TextBox sf = new TextBox();
        //            //sf.Text = folder;

        //            SelectFolder sf = new SelectFolder();
        //            sf.SelectedFolder = folder;
        //            sf.btnRemoveFolder.Visibility = Visibility.Visible;
        //            sf.FolderRemoved += Sf_FolderRemoved;
        //            sf.FolderChanged += Sf_FolderChanged;

        //            spFolders.Children.Add(sf);
        //            if (FolderAdded != null)
        //            {
        //                FolderAdded(this, SelectedFolders());
        //            }
        //            // Must be called after sf is on panel and has registered Sf_FolderChanged, because control for FolderChanged != null
        //            Sf_FolderChanged(null, folder);
        //        }

        //        private void Sf_FolderChanged(object o, string s)
        //        {
        //            if (FolderChanged != null)
        //            {
        //                FolderChanged(this, SelectedFolders());
        //            }
        //        }

        //        public void Sf_FolderRemoved(SelectFolder t)
        //        {
        //            spFolders.Children.Remove(t);
        //            if (FolderRemoved != null)
        //            {
        //                FolderRemoved(this, SelectedFolders());
        //            }
        //        }

        //        async void SetAwesomeIcons()
        //        {
        //            await AwesomeFontControls.SetAwesomeFontSymbol(btnAddFolder, "\uf07c " + sess.i18n(XlfKeys.New2));
        //            await AwesomeFontControls.SetAwesomeFontSymbol(btnAddAsTemplate, "\uf022 " + sess.i18n(XlfKeys.SaveSetAsTemplate));

        //        }

        //        private void BtnAddFolder_Click(object sender, RoutedEventArgs e)
        //        {
        //            AddFolder(string.Empty);
        //        }



        //        /// <summary>
        //        /// Validate before call
        //        /// </summary>
        //        public List<string> SelectedFolders()
        //        {
        //            List<string> result = new List<string>();
        //            foreach (SelectFolder item in spFolders.Children)
        //            {
        //                // Here I can eliminate empty strings, during Validate is calling Validate on every control, not use this method
        //                if (item.SelectedFolder != string.Empty)
        //                {
        //                    result.Add(item.SelectedFolder);
        //                }

        //            }
        //            return result;
        //        }

        //        private void BtnAddAsTemplate_Click(object sender, RoutedEventArgs e)
        //        {
        //            SaveSetAsTemplate();
        //        }

        //        public static Type type = typeof(SelectMoreFolders);
        //        public static bool validated
        //        {
        //            get
        //            {
        //                return SelectFolder.validated;
        //            }
        //            set
        //            {
        //                SelectFolder.validated = value;
        //            }
        //        }

        //        public static void Validate(object tb, SelectMoreFolders control, ref ValidateData d)
        //        {
        //            var controls = ControlFinder.StackPanel(control, "spFolders").Children;
        //            foreach (SelectFolder item in controls)
        //            {
        //                item.Validate(tb, ref d);
        //            }
        //        }

        //        public void Validate(object tbFolder, ref ValidateData d)
        //        {
        //            Validate(tbFolder, this, ref d);
        //        }

        //        private void btnSaveChangesToTemplate_Click(object sender, RoutedEventArgs e)
        //        {

        //        }
    }
}