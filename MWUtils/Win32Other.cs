
namespace MWUtils
{
    public partial class Win32
    {
        public static RECT GetGameWindow(string lpClassName, string lpWindowName)
        {
            RECT result = new RECT();
            GetWindowRect(FindWindow(lpClassName, lpWindowName), ref result);
            return result;
        }


    }

}
