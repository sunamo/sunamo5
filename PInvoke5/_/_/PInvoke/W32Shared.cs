using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public partial class W32
{
    #region ClipboardMonitor
    /// <summary>
    /// Places the given window in the system-maintained clipboard format listener list.
    /// </summary>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool AddClipboardFormatListener(IntPtr hwnd);


    /// <summary>
    /// Removes the given window from the system-maintained clipboard format listener list.
    /// </summary>
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);
    #endregion

    /// <summary>
    /// Sent when the contents of the clipboard have changed.
    /// </summary>
    public const int WM_CLIPBOARDUPDATE = 0x031D;

    /// <summary>
    /// To find message-only windows, specify HWND_MESSAGE in the hwndParent parameter of the FindWindowEx function.
    /// </summary>
    public static IntPtr HWND_MESSAGE = new IntPtr(-3);


    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetForegroundWindow(IntPtr hWnd);


    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
    public static extern short GetKeyState(int keyCode);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GlobalLock(IntPtr hMem);

    [DllImport("Kernel32.Dll", EntryPoint = "Wow64EnableWow64FsRedirection")]
    public static extern bool EnableWow64FSRedirection(bool enable);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool CloseClipboard();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetClipboardData(uint uFormat, IntPtr data);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    static extern IntPtr GetOpenClipboardWindow();

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GlobalUnlock(IntPtr hMem);

    /// <summary>
    /// Retrieves the current size of the specified global memory object, in bytes.
    /// </summary>
    /// <param name="hMem"></param>
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern UIntPtr GlobalSize(IntPtr hMem);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetClipboardData(uint uFormat);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool IsClipboardFormatAvailable(uint format);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool OpenClipboard(IntPtr hWndNewOwner);


}