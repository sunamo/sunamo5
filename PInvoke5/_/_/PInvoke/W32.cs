using System.Runtime.InteropServices;
using System;
using sunamo.PInvoke;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// 
/// SetLastError = true should be specified always, then I can get error value from Marshal.GetLastWin32Error. 
/// https://stackoverflow.com/a/17918729/9327173
/// </summary>
public partial class W32 : W32Base
{
    [DllImport("kernel32.dll")]
        public  static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /// <summary>
        /// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
        /// You would install a hook procedure to monitor the system for certain types of events. These events are
        /// associated either with a specific thread or with all threads in the same desktop as the calling thread.
        /// </summary>
        /// <param name="idHook">hook type</param>
        /// <param name="lpfn">hook procedure</param>
        /// <param name="hMod">handle to application instance</param>
        /// <param name="dwThreadId">thread identifier</param>
        /// <returns>If the function succeeds, the return value is the handle to the hook procedure.</returns>
        [DllImport("USER32", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        /// <summary>
        /// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        /// </summary>
        /// <param name="hhk">handle to hook procedure</param>
        /// <returns>If the function succeeds, the return value is true.</returns>
        [DllImport("USER32", SetLastError = true)]
        public static extern bool UnhookWindowsHookEx(IntPtr hHook);

        /// <summary>
        /// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain.
        /// A hook procedure can call this function either before or after processing the hook information.
        /// </summary>
        /// <param name="hHook">handle to current hook</param>
        /// <param name="code">hook code passed to hook procedure</param>
        /// <param name="wParam">value passed to hook procedure</param>
        /// <param name="lParam">value passed to hook procedure</param>
        /// <returns>If the function succeeds, the return value is true.</returns>
        [DllImport("USER32", SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hHook, int code, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

    [DllImport("psapi.dll")]
    public static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In][MarshalAs(UnmanagedType.U4)] int nSize);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    public static System.Drawing.Icon GetFileIcon(string name, IconSize size,
                                              bool linkOverlay = false)
    {
        SHFILEINFO shfi = new SHFILEINFO();
        uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;

        if (true == linkOverlay) flags += SHGFI_LINKOVERLAY;


        /* Check the size specified for return. */
        if (IconSize.Small == size)
        {
            flags += SHGFI_SMALLICON; // include the small icon flag
        }
        else
        {
            flags += SHGFI_LARGEICON;  // include the large icon flag
        }

        SHGetFileInfo(name,
            FILE_ATTRIBUTE_NORMAL,
            out shfi,
            (uint)System.Runtime.InteropServices.Marshal.SizeOf(shfi),
            flags);


        // Copy (clone) the returned icon to a new object, thus allowing us 
        // to call DestroyIcon immediately
        System.Drawing.Icon icon = (System.Drawing.Icon)
                             System.Drawing.Icon.FromHandle(shfi.hIcon).Clone();
        User32.DestroyIcon(shfi.hIcon); // Cleanup
        return icon;
    }

    public const uint SHGFI_ICON = 0x000000100;     // get icon
    public const uint SHGFI_DISPLAYNAME = 0x000000200;     // get display name
    public const uint SHGFI_TYPENAME = 0x000000400;     // get type name
    public const uint SHGFI_ATTRIBUTES = 0x000000800;     // get attributes
    public const uint SHGFI_ICONLOCATION = 0x000001000;     // get icon location
    public const uint SHGFI_EXETYPE = 0x000002000;     // return exe type
    public const uint SHGFI_SYSICONINDEX = 0x000004000;     // get system icon index
    public const uint SHGFI_LINKOVERLAY = 0x000008000;     // put a link overlay on icon
    public const uint SHGFI_SELECTED = 0x000010000;     // show icon in selected state
    public const uint SHGFI_ATTR_SPECIFIED = 0x000020000;     // get only specified attributes
    public const uint SHGFI_LARGEICON = 0x000000000;     // get large icon
    public const uint SHGFI_SMALLICON = 0x000000001;     // get small icon
    public const uint SHGFI_OPENICON = 0x000000002;     // get open icon
    public const uint SHGFI_SHELLICONSIZE = 0x000000004;     // get shell size icon
    public const uint SHGFI_PIDL = 0x000000008;     // pszPath is a pidl
    public const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;     // use passed dwFileAttribute
    public const uint SHGFI_ADDOVERLAYS = 0x000000020;     // apply the appropriate overlays
    public const uint SHGFI_OVERLAYINDEX = 0x000000040;     // Get the index of the overlay

    public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
    public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;



    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, IntPtr lpSECURITY_ATTRIBUTES, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

    public static uint GetFileInformationByHandleWorker(string file, out int lastError)
    {
        uint nNumberOfLinks = uint.MaxValue;
        lastError = 0;

        BY_HANDLE_FILE_INFORMATION hfi = new BY_HANDLE_FILE_INFORMATION { };

        IntPtr handle = CreateFile(file, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, IntPtr.Zero);
        if (handle.ToInt32() != INVALID_HANDLE_VALUE)
        {
            if (GetFileInformationByHandle(handle, ref hfi))
                nNumberOfLinks = hfi.nNumberOfLinks;
            else
                lastError = Marshal.GetLastWin32Error();

            CloseHandle(handle);
        }
        else
            lastError = Marshal.GetLastWin32Error();

        return nNumberOfLinks;
    }

    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool GetFileInformationByHandle(IntPtr handle, ref BY_HANDLE_FILE_INFORMATION hfi);

    public const int INVALID_HANDLE_VALUE = -1;

    public const uint GENERIC_READ = 0x80000000;
    public const int ERROR_INSUFFICIENT_BUFFER = 122;

    public const int FILE_SHARE_READ = 1;
    public const int FILE_SHARE_WRITE = 2;
    public const int FILE_SHARE_DELETE = 4;


    public const int CREATE_NEW = 1;
    public const int CREATE_ALWAYS = 2;
    public const int OPEN_EXISTING = 3;
    public const int OPEN_ALWAYS = 4;
    public const int TRUNCATE_EXISTING = 5;

    [StructLayout(LayoutKind.Sequential)]
    public struct BY_HANDLE_FILE_INFORMATION
    {
        public uint dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public uint dwVolumeSerialNumber;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint nNumberOfLinks;
        public uint nFileIndexHigh;
        public uint nFileIndexLow;
    };

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GlobalFree(IntPtr hMem);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint EnumClipboardFormats(uint format);

    /// <summary>
    /// Use Marshal.GetLastWin32Error instead. https://stackoverflow.com/a/17918729/9327173
    /// </summary>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern uint GetLastError();


















    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

    [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

    [DllImport("shell32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string pszPath);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetConsoleIcon(IntPtr hIcon);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern bool DeleteObject(IntPtr hObject);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);





    [DllImport(@"urlmon.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    public extern static System.UInt32 FindMimeFromData(
        System.UInt32 pBC,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
        [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
        System.UInt32 cbSize,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
        System.UInt32 dwMimeFlags,
        out System.UInt32 ppwzMimeOut,
        System.UInt32 dwReserverd
    );

    [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, out SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool DestroyIcon(IntPtr hIcon);


    [DllImport("kernel32.dll", ExactSpelling = true)]
    public static extern IntPtr GetCurrentProcess();

    [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

    [DllImport("advapi32.dll", SetLastError = true)]
    public static extern bool LookupPrivilegeValue(string host, string name, ref LUID pluid);

    [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
    public static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TOKEN_PRIVILEGES newst, int len, IntPtr prev, IntPtr relen);




    [DllImport("user32.dll")]
    public static extern IntPtr GetShellWindow();


    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, uint processId);

    [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, IntPtr lpTokenAttributes, SECURITY_IMPERSONATION_LEVEL impersonationLevel, TOKEN_TYPE tokenType, out IntPtr phNewToken);

    [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern bool CreateProcessWithTokenW(IntPtr hToken, int dwLogonFlags, string lpApplicationName, string lpCommandLine, int dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

}