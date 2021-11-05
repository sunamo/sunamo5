using System;
using System.Collections.Generic;
using System.Text;

namespace sunamo.Essential
{
    public class TypedSunamoLogger : TypedLoggerBase
    {
        public static TypedSunamoLogger Instance = new TypedSunamoLogger();

        private TypedSunamoLogger() : base(WriteLineWorker)
        {
        }

        public static void WriteLineWorker(TypeOfMessage tz, string text, params object[] args)
        {
            ThisApp.SetStatus(tz, text, args);
        }

        
    }
}