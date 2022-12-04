﻿using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Constants
{
    public class TemporaryPaths
    {
        /// <summary>
        /// 
        /// </summary>
        public const string easeusSavedSession = @"C:\Program Files\EaseUS\EaseUS Data Recovery Wizard\SaveScan";
        public const string diskDrillSavedSession = @"D:\_\DiskDrill\";
        /// <summary>
        /// In binary, but name of files are correct in Code
        /// </summary>
        public const string diskDrillExtension = @".ddwscan";
        /// <summary>
        /// In binary
        /// </summary>
        public const string easeUsExt = ".rsf";
        public const string _RecoveredPhotos = @"D:\_C\_RecoveredPhotos\";
        public const string _BadImages = @"D:\_C\_BadImages\";
        public const string fileWithAllPhotos = CloudProvidersHelper.GDriveFolder + @"Todo\Restore photos\Restore photos-Drive\AllRestoredPhotoFiles.txt";
        public const string fileWithAllPhotosLowerExt = CloudProvidersHelper.GDriveFolder + @"Todo\Restore photos\Restore photos-Drive\AllRestoredPhotoFiles-lowerExt.txt";
    }
}