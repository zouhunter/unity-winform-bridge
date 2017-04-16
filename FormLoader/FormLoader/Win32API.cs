using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

public class Win32API
{
    #region Win32 API
    [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true,
         CharSet = CharSet.Unicode, ExactSpelling = true,
         CallingConvention = CallingConvention.StdCall)]
    public static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

    [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
    public static extern long GetWindowLong(IntPtr hwnd, int nIndex);

    public static IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong)
    {
        if (IntPtr.Size == 4)
        {
            return SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
        }
        return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
    }
    [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
    public static extern IntPtr SetWindowLongPtr32(HandleRef hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
    public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

    [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
    public static extern bool PostMessage(IntPtr hwnd, uint Msg, uint wParam, uint lParam);

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    public delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
    [DllImport("user32.dll")]
    public static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

    public const int WM_ACTIVATE = 0x0006;
    public static readonly IntPtr WA_ACTIVE = new IntPtr(1);
    public static readonly IntPtr WA_INACTIVE = new IntPtr(0);
    /// <summary>
    /// 获取系统错误信息描述
    /// </summary>
    /// <param name="errCode">系统错误码</param>
    /// <returns></returns>
    public static string GetLastError()
    {
        var errCode = Marshal.GetLastWin32Error();
        IntPtr tempptr = IntPtr.Zero;
        string msg = null;
        FormatMessage(0x1300, ref tempptr, errCode, 0, ref msg, 255, ref tempptr);
        return msg;
    }
    /// <summary>
    /// 获取系统错误信息描述
    /// </summary>
    /// <param name="errCode">系统错误码</param>
    /// <returns></returns>
    public static string GetLastErrorString(int errCode)
    {
        IntPtr tempptr = IntPtr.Zero;
        string msg = null;
        FormatMessage(0x1300, ref tempptr, errCode, 0, ref msg, 255, ref tempptr);
        return msg;
    }

    [System.Runtime.InteropServices.DllImport("Kernel32.dll")]
    public extern static int FormatMessage(int flag, ref IntPtr source, int msgid, int langid, ref string buf, int size, ref IntPtr args);


    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetParent(IntPtr hwnd);
    ///// <summary>
    ///// ShellExecute(IntPtr.Zero, "Open", "C:/Program Files/TTPlayer/TTPlayer.exe", "", "", 1);
    ///// </summary>
    ///// <param name="hwnd"></param>
    ///// <param name="lpOperation"></param>
    ///// <param name="lpFile"></param>
    ///// <param name="lpParameters"></param>
    ///// <param name="lpDirectory"></param>
    ///// <param name="nShowCmd"></param>
    ///// <returns></returns>
    //[DllImport("shell32.dll", EntryPoint = "ShellExecute")]
    //public static extern int ShellExecute(
    //IntPtr hwnd,
    //string lpOperation,
    //string lpFile,
    //string lpParameters,
    //string lpDirectory,
    //int nShowCmd
    //);
    //[DllImport("kernel32.dll")]
    //public static extern int OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId); 
    [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    public const int SWP_NOOWNERZORDER = 0x200;
    public const int SWP_NOREDRAW = 0x8;
    public const int SWP_NOZORDER = 0x4;
    public const int SWP_SHOWWINDOW = 0x0040;
    public const int WS_EX_MDICHILD = 0x40;
    public const int SWP_FRAMECHANGED = 0x20;
    public const int SWP_NOACTIVATE = 0x10;
    public const int SWP_ASYNCWINDOWPOS = 0x4000;
    public const int SWP_NOMOVE = 0x2;
    public const int SWP_NOSIZE = 0x1;
    public const int GWL_STYLE = (-16);
    public const int WS_VISIBLE = 0x10000000;
    public const int WM_CLOSE = 0x10;
    public const int WS_CHILD = 0x40000000;

    public const int SW_HIDE = 0; //{隐藏, 并且任务栏也没有最小化图标}
    public const int SW_SHOWNORMAL = 1; //{用最近的大小和位置显示, 激活}
    public const int SW_NORMAL = 1; //{同 SW_SHOWNORMAL}
    public const int SW_SHOWMINIMIZED = 2; //{最小化, 激活}
    public const int SW_SHOWMAXIMIZED = 3; //{最大化, 激活}
    public const int SW_MAXIMIZE = 3; //{同 SW_SHOWMAXIMIZED}
    public const int SW_SHOWNOACTIVATE = 4; //{用最近的大小和位置显示, 不激活}
    public const int SW_SHOW = 5; //{同 SW_SHOWNORMAL}
    public const int SW_MINIMIZE = 6; //{最小化, 不激活}
    public const int SW_SHOWMINNOACTIVE = 7; //{同 SW_MINIMIZE}
    public const int SW_SHOWNA = 8; //{同 SW_SHOWNOACTIVATE}
    public const int SW_RESTORE = 9; //{同 SW_SHOWNORMAL}
    public const int SW_SHOWDEFAULT = 10; //{同 SW_SHOWNORMAL}
    public const int SW_MAX = 10; //{同 SW_SHOWNORMAL}

    //const int PROCESS_ALL_ACCESS = 0x1F0FFF;
    //const int PROCESS_VM_READ = 0x0010;
    //const int PROCESS_VM_WRITE = 0x0020;     
    [Flags()]
    public enum SetWindowPosFlags : uint
    {
        /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
        /// the system posts the request to the thread that owns the window. This prevents the calling thread from
        /// blocking its execution while other threads process the request.</summary>
        /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
        AsynchronousWindowPosition = 0x4000,
        /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
        /// <remarks>SWP_DEFERERASE</remarks>
        DeferErase = 0x2000,
        /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
        /// <remarks>SWP_DRAWFRAME</remarks>
        DrawFrame = 0x0020,
        /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
        /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
        /// is sent only when the window's size is being changed.</summary>
        /// <remarks>SWP_FRAMECHANGED</remarks>
        FrameChanged = 0x0020,
        /// <summary>Hides the window.</summary>
        /// <remarks>SWP_HIDEWINDOW</remarks>
        HideWindow = 0x0080,
        /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
        /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
        /// parameter).</summary>
        /// <remarks>SWP_NOACTIVATE</remarks>
        DoNotActivate = 0x0010,
        /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
        /// contents of the client area are saved and copied back into the client area after the window is sized or
        /// repositioned.</summary>
        /// <remarks>SWP_NOCOPYBITS</remarks>
        DoNotCopyBits = 0x0100,
        /// <summary>Retains the current position (ignores X and Y parameters).</summary>
        /// <remarks>SWP_NOMOVE</remarks>
        IgnoreMove = 0x0002,
        /// <summary>Does not change the owner window's position in the Z order.</summary>
        /// <remarks>SWP_NOOWNERZORDER</remarks>
        DoNotChangeOwnerZOrder = 0x0200,
        /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
        /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
        /// window uncovered as a result of the window being moved. When this flag is set, the application must
        /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
        /// <remarks>SWP_NOREDRAW</remarks>
        DoNotRedraw = 0x0008,
        /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
        /// <remarks>SWP_NOREPOSITION</remarks>
        DoNotReposition = 0x0200,
        /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
        /// <remarks>SWP_NOSENDCHANGING</remarks>
        DoNotSendChangingEvent = 0x0400,
        /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
        /// <remarks>SWP_NOSIZE</remarks>
        IgnoreResize = 0x0001,
        /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
        /// <remarks>SWP_NOZORDER</remarks>
        IgnoreZOrder = 0x0004,
        /// <summary>Displays the window.</summary>
        /// <remarks>SWP_SHOWWINDOW</remarks>
        ShowWindow = 0x0040,
    }
    #endregion Win32 API


}