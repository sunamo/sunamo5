namespace sunamo.Essential
{


    public partial class ThisApp
    {
        public static Langs l = Langs.en;
        public static bool useShortAsDt = true;
        public static bool runInDebug = true;

        // Everywhere is used just ThisApp.cd. 
        //public static Dispatcher cd = null;
        //public static DispatcherPriority cdp = DispatcherPriority.Normal;

        public static TypedLoggerBase NopeOrDebugTyped()
        {
#if DEBUG2
            return TypedDebugLogger.Instance;
#elif !DEBUG2
        // Is possible also use CmdApp.ConsoleOrDebugTyped
        return TypedDummyLogger.Instance;
        //return TypedConsoleLogger.Instance;
#endif

        }


        public static bool check = false;

        /// <summary>
        /// Name = Solution
        /// Project = Project
        /// In selling is without spaces
        /// </summary>
        public static string Name;
        static string project = null;
        /// <summary>
        /// Name = Solution
        /// Project = Project
        /// </summary>
        public static string Project
        {
            get
            {
                if (project == null)
                {
                    return Name;
                }
                return project;
            }
            set
            {
                project = value;
            }
        }
        public static string _Name
        {
            get
            {
                return AllStrings.lowbar + Name;
            }
        }

        public static readonly bool initialized = false;
        public static string Namespace = "";
        /// <summary>
        /// Use everywhere is method without / with Async. Its needed due to avoid deadlock https://stackoverflow.com/a/65820543/9327173
        /// Good for example in ClipboardHelperWinStd. But when I'm changing headers of methods, I have to use #if ASYNC. Otherwise program can "froze" and due to async I cant debug where.
        /// </summary>
        public static bool async_
        {
            get => SunamoExceptions.ThisApp.async_;
            set => SunamoExceptions.ThisApp.async_ = value;
        }
        public static string EventLogName = null;

        public static event SetStatusDelegate StatusSetted;

        public static void SetStatus(TypeOfMessage st, string status, params object[] args)
        {
            var format = SH.Format2(status, args);
            if (format.Trim() != string.Empty)
            {
                if (StatusSetted == null)
                {
                    // For unit tests
                    //////////DebugLogger.Instance.WriteLine(st + ": " + format);
                }
                else
                {
                    StatusSetted(st, format);
                }
            }
        }

        public static void StatusFromText(string v)
        {
            if (!string.IsNullOrEmpty(v))
            {
                var tom = StatusHelperSunamo.IsStatusMessage(ref v);
                SetStatus(tom, v);
            }
        }

        /// <summary>
        /// Strings which is on lines calling this method is not translate
        /// Debug method when I running app on release and app is behave extraordinary
        /// </summary>
        /// <param name="v"></param>
        /// <param name="o"></param>
        public static void a(string v, params object[] o)
        {

            ThisApp.SetStatus(TypeOfMessage.Appeal, v, o);
        }
    }
}