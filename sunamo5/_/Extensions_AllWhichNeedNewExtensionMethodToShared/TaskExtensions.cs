using sunamo.Essential;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace sunamo
{
    public static class TaskExtensions
    {
        #region For easy copy from TaskExtensionsSunamo.cs
        public static ConfiguredTaskAwaitable Conf(this Task t)
        {
            return t.ConfigureAwait(true);
        }

        public static ConfiguredTaskAwaitable<T> Conf<T>(this Task<T> t)
        {
            return t.ConfigureAwait(true);
        }

        public static void LogExceptions(this Task task)
        {
            task.ContinueWith(t =>
            {
                var aggException = t.Exception.Flatten();
                foreach (var exception in aggException.InnerExceptions)
                {
                    ThisApp.SetStatus(TypeOfMessage.Error, Exceptions.TextOfExceptions(exception));
                }
            },
            TaskContinuationOptions.OnlyOnFaulted);
        }
        #endregion
    }
}