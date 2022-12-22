using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace sunamo.Essential
{
    public class SunamoLogger : LoggerBase
    {
        public static SunamoLogger Instance = new SunamoLogger(WriteLineWorker);

        private SunamoLogger()
        {
            // Here it must be without Instance =, otherwise write Instance again with writeLineHandler=null
            //new SunamoLogger(WriteLine);
        }

        public SunamoLogger(VoidStringParamsObjects writeLineHandler) : base(writeLineHandler)
        {
        }

        public static void WriteLineWorker(string text, params object[] args)
        {
            ThisApp.SetStatus(TypeOfMessage.Ordinal, text, args);
        }

        
    }
}