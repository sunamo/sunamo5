using System;
using System.Collections.Generic;
using System.Text;

// Cant be DEBUG, in dependent assembly often dont see this classes even if all projects is Debug
//#if DEBUG
namespace sunamo.Essential
{
#if DEBUG //2
    public class DebugTemplateLogger : TemplateLoggerBase
    {
        public static Type type = typeof(DebugTemplateLogger);
        static DebugTemplateLogger instance = 

#if DEBUG2
        new DebugTemplateLogger();
#elif !DEBUG2
        //new DebugLogger(DebugWriteLine);
        null;
#endif

        public static TemplateLoggerBase Instance
        {
            get
            {
                if (Instance == null)
                {
                    ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(), "Dont use DebugLogger without #if DEBUG!!", false);
                    return DummyTemplateLogger.Instance;
                }
                return instance;
            }
        }

        private DebugTemplateLogger() : base(DebugLogger.DebugWriteLine)
        {
        }
    }
#endif
}
//#endif