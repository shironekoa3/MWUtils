using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace MWUtils
{
    public partial class Win32
    {
        /// <summary>
        /// 根据进程名获取PID，无需后缀名
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public static int GetPidByProcessName(string processName)
        {
            processName = processName.Replace(".exe", "");
            Process[] arrayProcess = Process.GetProcessesByName(processName);
            foreach (Process p in arrayProcess)
            {
                return p.Id;
            }
            return 0;
        }

        /// <summary>
        /// Dump 模块内存
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DumpModule(int pid, string moduleName, string path)
        {
            if (pid <= 0)
            {
                return false;
            }

            Process p = Process.GetProcessById(pid);
            for (int i = 0; i < p.Modules.Count; i++)
            {
                if (p.Modules[i].ModuleName.Replace(".dll", "").Replace(".exe", "") == moduleName.Replace(".dll", "").Replace(".exe", ""))
                {
                    ProcessModule pm = p.Modules[i];
                    //pm.BaseAddress
                    FileStream fs = new FileStream(path, FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);

                    int curr = 0;
                    int size = 1024;
                    for (int j = 0; curr < pm.ModuleMemorySize; j++)
                    {
                        int tempSize = (pm.ModuleMemorySize - curr) < size ? pm.ModuleMemorySize - curr : size;
                        bw.Write(ReadBytes(pid, (long)pm.BaseAddress + curr, tempSize));
                        curr += tempSize;
                    }
                    bw.Close();
                    fs.Close();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 取模块基址，自动删除后缀名
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public static long GetModuleAddr(int pid, string moduleName)
        {
            if (pid <= 0)
            {
                return 0;
            }

            Process p = Process.GetProcessById(pid);
            for (int i = 0; i < p.Modules.Count; i++)
            {
                if (p.Modules[i].ModuleName.Replace(".dll", "") == moduleName.Replace(".dll", ""))
                {
                    return (long)p.Modules[i].BaseAddress;
                }
            }
            return 0;
        }

        /// <summary>
        /// 读内存 1 字节
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        public static byte ReadByte(int pid, long baseAddr)
        {
            if (pid <= 0)
            {
                return 0;
            }

            try
            {
                byte[] buffer = new byte[4];
                //获取缓冲区地址
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                //打开一个已存在的进程对象  0x1F0FFF 最高权限
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                //将制定内存中的值读入缓冲区
                ReadProcessMemory(hProcess, (IntPtr)baseAddr, byteAddress, 1, IntPtr.Zero);
                //关闭操作
                CloseHandle(hProcess);
                //从非托管内存中读取一个 32 位带符号整数。
                return Marshal.ReadByte(byteAddress);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 读内存 4 字节
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        public static int ReadInt32(int pid, long baseAddr)
        {
            if (pid <= 0)
            {
                return 0;
            }

            try
            {
                byte[] buffer = new byte[4];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                ReadProcessMemory(hProcess, (IntPtr)baseAddr, byteAddress, 4, IntPtr.Zero);
                CloseHandle(hProcess);
                return Marshal.ReadInt32(byteAddress);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 读内存 8 字节
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        public static long ReadInt64(int pid, long baseAddr)
        {
            if (pid <= 0)
            {
                return 0;
            }

            try
            {
                byte[] buffer = new byte[8];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                ReadProcessMemory(hProcess, (IntPtr)baseAddr, byteAddress, 8, IntPtr.Zero);
                CloseHandle(hProcess);
                return Marshal.ReadInt64(byteAddress);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 读内存字节数组
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] ReadBytes(int pid, long baseAddr, int length)
        {
            byte[] buffer = new byte[length];
            if (pid <= 0)
            {
                return buffer;
            }

            try
            {
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                ReadProcessMemory(hProcess, (IntPtr)baseAddr, byteAddress, length, IntPtr.Zero);
                CloseHandle(hProcess);
            }
            catch { }
            return buffer;
        }

        /// <summary>
        /// 读内存浮点数
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        public static float ReadFloat(int pid, long baseAddr)
        {
            if (pid <= 0)
            {
                return 0;
            }

            try
            {
                byte[] buffer = new byte[4];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                ReadProcessMemory(hProcess, (IntPtr)baseAddr, byteAddress, 4, IntPtr.Zero);
                CloseHandle(hProcess);
                return BitConverter.ToSingle(buffer, 0);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 读内存双浮点数
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <returns></returns>
        public static double ReadDouble(int pid, long baseAddr)
        {
            if (pid <= 0)
            {
                return 0;
            }

            try
            {
                byte[] buffer = new byte[8];
                IntPtr byteAddress = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                ReadProcessMemory(hProcess, (IntPtr)baseAddr, byteAddress, 8, IntPtr.Zero);
                CloseHandle(hProcess);
                return BitConverter.ToDouble(buffer, 0);
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// 写内存 1 字节
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteByte(int pid, long baseAddr, byte value)
        {
            if (pid <= 0) 
                return false;

            bool flag = false;

            byte[] bytes = new byte[1] { value };
            // 固定数组，防止垃圾回收器移动它
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                IntPtr pValue = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                flag = WriteProcessMemory(hProcess, (IntPtr)baseAddr, pValue, 1, IntPtr.Zero);
                CloseHandle(hProcess);
            }
            catch { }
            finally { handle.Free(); }
            return flag;
        }

        /// <summary>
        /// 写内存 4 字节
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteInt32(int pid, long baseAddr, int value)
        {
            if (pid <= 0)
                return false;

            bool flag = false;

            int[] bytes = new int[1] { value };
            // 固定数组，防止垃圾回收器移动它
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                IntPtr pValue = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                flag = WriteProcessMemory(hProcess, (IntPtr)baseAddr, pValue, 4, IntPtr.Zero);
                CloseHandle(hProcess);
            }
            catch { }
            finally { handle.Free(); }
            return flag;
        }

        /// <summary>
        /// 写内存 8 字节
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteInt64(int pid, long baseAddr, long value)
        {
            if (pid <= 0)
                return false;

            bool flag = false;

            long[] bytes = new long[1] { value };
            // 固定数组，防止垃圾回收器移动它
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                IntPtr pValue = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                flag = WriteProcessMemory(hProcess, (IntPtr)baseAddr, pValue, 8, IntPtr.Zero);
                CloseHandle(hProcess);
            }
            catch { }
            finally { handle.Free(); }
            return flag;
        }

        /// <summary>
        /// 写内存字节数组
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteBytes(int pid, long baseAddr, byte[] value)
        {
            if (pid <= 0)
                return false;

            bool flag = false;

            // 固定数组，防止垃圾回收器移动它
            GCHandle handle = GCHandle.Alloc(value, GCHandleType.Pinned);
            try
            {
                IntPtr pValue = Marshal.UnsafeAddrOfPinnedArrayElement(value, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                flag = WriteProcessMemory(hProcess, (IntPtr)baseAddr, pValue, value.Length, IntPtr.Zero);
                CloseHandle(hProcess);
            }
            catch { }
            finally { handle.Free(); }
            return flag;
        }

        /// <summary>
        /// 写内存 4 字节
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteFloat(int pid, long baseAddr, float value)
        {
            if (pid <= 0)
                return false;

            bool flag = false;

            byte[] bytes = BitConverter.GetBytes(value);
            // 固定数组，防止垃圾回收器移动它
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                IntPtr pValue = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                flag = WriteProcessMemory(hProcess, (IntPtr)baseAddr, pValue, 4, IntPtr.Zero);
                CloseHandle(hProcess);
            }
            catch { }
            finally { handle.Free(); }
            return flag;
        }

        /// <summary>
        /// 写内存 8 字节
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool WriteDouble(int pid, long baseAddr, double value)
        {
            if (pid <= 0)
                return false;

            bool flag = false;

            byte[] bytes = BitConverter.GetBytes(value);
            // 固定数组，防止垃圾回收器移动它
            GCHandle handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                IntPtr pValue = Marshal.UnsafeAddrOfPinnedArrayElement(bytes, 0);
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                flag = WriteProcessMemory(hProcess, (IntPtr)baseAddr, pValue, 8, IntPtr.Zero);
                CloseHandle(hProcess);
            }
            catch { }
            finally { handle.Free(); }
            return flag;
        }

        /// <summary>
        /// 修改指定地址和大小的内存页属性
        /// </summary>
        /// <param name="baseAddr"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static bool MemVirtualProtectEx(int pid, long baseAddr, int size)
        {
            if (pid <= 0) return false;
            bool flag = false;
            try
            {
                int oldProtect = 0;
                IntPtr hProcess = OpenProcess(0x1F0FFF, false, pid);
                // PAGE_EXECUTE_READWRITE 0x40
                flag = VirtualProtectEx(hProcess, (IntPtr)baseAddr, size, 0x40, out oldProtect);
                CloseHandle(hProcess);
            }
            catch { }
            return flag;
        }

        /// <summary>
        /// 整数转字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// 长整数转字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetBytes(long value)
        {
            return BitConverter.GetBytes((ulong)value);
        }
    }
}
