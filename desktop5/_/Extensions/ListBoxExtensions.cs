﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sunamo.Essential;

namespace System.Windows.Controls
{
    public static class ListBoxExtensions
    {
        public static bool validated
        {
            get => ValidationHelper.validated;
            set => ValidationHelper.validated = value;
        }

        /// <summary>
        /// Before first calling I have to set validated = true
        /// </summary>
        /// <param name="validated"></param>
        /// <param name="tb"></param>
        /// <param name="control"></param>
        /// <param name="trim"></param>
        public static void Validate(this ListBox control, object tb, ref ValidateData d)
        {
            if (!validated)
            {
                return;
            }
            if (d == null)
            {
                d = new ValidateData();
            }
            var count = control.SelectedItems.Count;

            if (count == 0)
            {
                InitApp.TemplateLogger.MustHaveValue(TextBlockHelper.TextOrToString(tb));
                validated = false;
            }
            else
            {
                validated = true;
            }
        }
    }
}