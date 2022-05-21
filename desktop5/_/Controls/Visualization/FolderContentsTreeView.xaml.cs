﻿using sunamo.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace desktop.Controls
{
    /// <summary>
    /// I have 3 TreeViews:
    /// desktop.Controls.FolderContentsTreeView - used in AllProjectsSearch\Wins\DuplicateSolutionsWindow.xaml. With icons but without checkboxes
    /// SunamoTreeView - very nice, load whole fs structure, example in OptimalAllocationSizeUnit. Without icons but with checkboxes
    /// HostingManagerTreeView - Pracuje s DB a FolderEntryDB/FileInfoDB
    /// 
    /// </summary>
    public partial class FolderContentsTreeView : UserControl
    {

        #region Rewrite to pure cs. With xaml is often problems without building
        private object dummyNode = null;
        public event VoidT<FileSystemEntry> Selected;
        public Dictionary<string, TreeViewItem> folders = new Dictionary<string, TreeViewItem>();
        public Dictionary<string, TreeViewItem> files = new Dictionary<string, TreeViewItem>();

        public FolderContentsTreeViewArgs args = new FolderContentsTreeViewArgs();

        public FolderContentsTreeView()
        {
            InitializeComponent();
        }

        bool useDictionary = false;

        public bool UseDictionary
        {
            set
            {
                useDictionary = value;
            }
        }

        /// <summary>
        /// A1 can be null
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="args"></param>
        public void Initialize(string folder, FolderContentsTreeViewArgs args = null)
        {
            if (args != null)
            {
                this.args = args;
            }

            if (folder != null)
            {
                AddTviFolderTo(folder, tv);
            }


            tv.SelectedItemChanged += Tv_SelectedItemChanged;
        }



        private void Tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue != null)
            {
                var tv = (e.NewValue as TreeViewItem);
                var fse = tv.Tag as FileSystemEntry;
                if (Selected != null)
                {
                    Selected(fse);
                }
            }
        }

        public void ExpandAll()
        {
            var exp = tv.Items;
            Expand(exp);
        }

        private void Expand(ItemCollection ic)
        {
            foreach (TreeViewItem item in ic)
            {
                item.ExpandSubtree();
                Expand(item.Items);
            }
        }

        public void AddTviFolderTo(string s)
        {
            AddTviFolderTo(s, tv);
        }

        private void AddTviFolderTo(string s, ItemsControl to)
        {
            TreeViewItem subfolder = new TreeViewItem();
            s = s.TrimEnd(AllChars.bs);
            subfolder.Header = s.Substring(s.LastIndexOf(AllStrings.bs) + 1);
            subfolder.Tag = new FileSystemEntry { file = false, path = s }; ;
            subfolder.FontWeight = FontWeights.Normal;
            subfolder.Items.Add(dummyNode);
            subfolder.Expanded += new RoutedEventHandler(folder_Expanded);

            if (useDictionary)
            {
                folders.Add(s, subfolder);
            }
            to.Items.Add(subfolder);
        }

        private void AddTviFileTo(string s, ItemsControl to)
        {
            TreeViewItem subfiles = new TreeViewItem();
            subfiles.Header = s.Substring(s.LastIndexOf(AllStrings.bs) + 1);
            subfiles.Tag = new FileSystemEntry { file = true, path = s };
            subfiles.FontWeight = FontWeights.Normal;
            if (useDictionary)
            {
                files.Add(s, subfiles);
            }
            to.Items.Add(subfiles);
        }

        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    string folder = ((FileSystemEntry)item.Tag).path.ToString();
                    foreach (string s in FS.GetFolders(folder))
                    {
                        AddTviFolderTo(s, item);
                    }

                    if (args.addFiles)
                    {
                        foreach (string s in FS.GetFiles(folder))
                        {
                            AddTviFileTo(s, item);
                        }
                    }
                }
                catch (Exception ex) { }
            }
        }
        #endregion
    }
}