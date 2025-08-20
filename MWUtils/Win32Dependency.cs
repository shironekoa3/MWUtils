using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MWUtils
{
    public partial class Win32
    {
        // 内存相关 API
        [DllImportAttribute("kernel32.dll", EntryPoint = "ReadProcessMemory")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesRead);

        [DllImportAttribute("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int flNewProtect, out int lpflOldProtect);

        [DllImportAttribute("kernel32.dll", EntryPoint = "OpenProcess")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll")]
        private static extern void CloseHandle(IntPtr hObject);


        // 剪贴板 API
        [DllImport("User32")]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("User32")]
        public static extern bool CloseClipboard();

        [DllImport("User32")]
        public static extern bool EmptyClipboard();

        [DllImport("User32")]
        public static extern bool IsClipboardFormatAvailable(int format);

        [DllImport("User32")]
        public static extern IntPtr GetClipboardData(int uFormat);

        [DllImport("User32", CharSet = CharSet.Unicode)]
        public static extern IntPtr SetClipboardData(int uFormat, IntPtr hMem);

        // 鼠标操作
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32")]
        private static extern int keybd_event(int bVk, int bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern short GetAsyncKeyState(int nVirtKey);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public int type;
            public InputUnion U;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }



        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        /// <summary>
        /// 会截取到阴影部分，，需要使用 GetClientRect + ClientToScreen
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpRect"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT lpPoint);

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width => Right - Left;
            public int Height => Bottom - Top;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }




        public static readonly int VK_LSHIFT = 0xA0;
        public static readonly int VK_RSHIFT = 0xA1;
        public static readonly int VK_LCONTROL = 0xA2;
        public static readonly int VK_RCONTROL = 0xA3;
        public static readonly int VK_LMENU = 0xA4;
        public static readonly int VK_RMENU = 0xA5;

        public static readonly int INPUT_MOUSE = 0x0000;                 //移动鼠标 
        public static readonly int MOUSEEVENTF_MOVE = 0x0001;            //移动鼠标 
        public static readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;        //模拟鼠标左键按下 
        public static readonly int MOUSEEVENTF_LEFTUP = 0x0004;          //模拟鼠标左键抬起 
        public static readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008;       //模拟鼠标右键按下 
        public static readonly int MOUSEEVENTF_RIGHTUP = 0x0010;         //模拟鼠标右键抬起 
        public static readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020;      //模拟鼠标中键按下 
        public static readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;        //模拟鼠标中键抬起 
        public static readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;        //标示是否采用绝对坐标 
        public static readonly int MOUSEEVENTF_WHEEL = 0x0800;           //模拟鼠标滚轮滚动操作，必须配合dwData参数


    }
}
