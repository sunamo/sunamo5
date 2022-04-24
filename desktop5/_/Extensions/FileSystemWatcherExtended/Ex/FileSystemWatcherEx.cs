﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using System.Text;
using System.Threading;

    public class FileSystemWatcherEx : FileSystemWatcher
    {
        // set a reasonable maximum interval time
        public readonly int MaxInterval = 60000;

        public event PathAvailabilityHandler EventPathAvailability = delegate { };

        private bool IsNetworkAvailable = true;
        private int Interval = 100;
        public Thread thread = null;
        public string Name = "FileSystemWatcherEx";
        public bool Run = false;

        #region Constructors
        // -------------------------------------------------------------------------------
        public FileSystemWatcherEx()
            : base()
        {
            CreateThread();
        }

        // -------------------------------------------------------------------------------
        public FileSystemWatcherEx(string path)
            : base(path)
        {
            CreateThread();
        }

        // -------------------------------------------------------------------------------
        public FileSystemWatcherEx(int interval)
            : base()
        {
            this.Interval = interval;
            CreateThread();
        }

        // -------------------------------------------------------------------------------
        public FileSystemWatcherEx(string path, int interval)
            : base(path)
        {
            this.Interval = interval;
            CreateThread();
        }

        // -------------------------------------------------------------------------------
        public FileSystemWatcherEx(int interval, string name)
            : base()
        {
            this.Interval = interval;
            this.Name = name;
            CreateThread();
        }

        // -------------------------------------------------------------------------------
        public FileSystemWatcherEx(string path, int interval, string name)
            : base(path)
        {
            this.Interval = interval;
            this.Name = name;
            CreateThread();
        }
        #endregion Constructors

        #region Helper Methods
        // -------------------------------------------------------------------------------
        /// <summary>
        /// Creates the thread if the interval is greater than 0 milliseconds 
        /// </summary>
        private void CreateThread()
        {
            // Normalize  the interval
            this.Interval = Math.Max(0, Math.Min(this.Interval, this.MaxInterval));
            if (this.Interval > 0)
            {
                this.thread = new Thread(new ThreadStart(MonitorFolderAvailability));
                this.thread.Name = this.Name;
                this.thread.IsBackground = true;
            }
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Attempts to start the monitoring thread
        /// </summary>
        public void StartFolderMonitor()
        {
            this.Run = true;
            if (this.thread != null)
            {
                if (!thread.IsAlive)
                {
                    this.thread.Start();
                }
            }
        }

        // -------------------------------------------------------------------------------
        /// <summary>
        /// Attempts to start the monitoring thread
        /// </summary>
        public void StopFolderMonitor()
        {
            this.Run = false;
        }
        #endregion Helper Methods

        // -------------------------------------------------------------------------------
        /// <summary>
        /// The thread method. It sits and spins making sure the folder exists
        /// </summary>
        public void MonitorFolderAvailability()
        {
            while (this.Run)
            {
                if (this.IsNetworkAvailable)
                {
                    if (!FS.ExistsDirectory(base.Path))
                    {
                        this.IsNetworkAvailable = false;
                        RaiseEventNetworkPathAvailablity();
                    }
                }
                else
                {
                    if (FS.ExistsDirectory(base.Path))
                    {
                        this.IsNetworkAvailable = true;
                        RaiseEventNetworkPathAvailablity();
                    }
                }
                Thread.Sleep(this.Interval);
            }
        }

        // -------------------------------------------------------------------------------
        private void RaiseEventNetworkPathAvailablity()
        {
            EventPathAvailability(this, new PathAvailablitiyEventArgs(this.IsNetworkAvailable));
        }
    }