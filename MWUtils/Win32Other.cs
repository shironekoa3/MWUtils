
using System;
using System.Drawing;

namespace MWUtils
{
    public partial class Win32
    {
        public static RECT GetGameWindow(string lpClassName, string lpWindowName)
        {
            RECT result = new RECT();
            var hWnd = FindWindow(lpClassName, lpWindowName);
            //GetWindowRect(hwnd, ref result);

            if (!GetClientRect(hWnd, out RECT clientRect))
            {
                Console.WriteLine("GetClientRect 调用失败");
                return result;
            }

            POINT point = new POINT { X = clientRect.Left, Y = clientRect.Top };
            if (!ClientToScreen(hWnd, ref point))
            {
                Console.WriteLine("ClientToScreen 调用失败");
                return result;
            }

            result = new RECT
            {
                Left = point.X,
                Top = point.Y,
                Right = point.X + clientRect.Width,
                Bottom = point.Y + clientRect.Height
            };

            return result;
        }


    }

}
