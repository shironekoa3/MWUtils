using MWUtils;
using System;
using System.Drawing;

namespace MWUtilsTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //MemReadTest();
            //MemWriteTest();
        }

        public static void MemReadTest()
        {
            string processName = "AsteriskPassword";
            long addr = 0x00BD0000;

            int pid = Win32.GetPidByProcessName(processName);

            byte byteValue = Win32.ReadByte(pid, addr);
            int intValue = Win32.ReadInt32(pid, addr);
            long longValue = Win32.ReadInt64(pid, addr);
            float floatValue = Win32.ReadFloat(pid, addr);
            double doubleValue = Win32.ReadDouble(pid, addr);
            byte[] bytesValue = Win32.ReadBytes(pid, addr, 6);

            Console.WriteLine($"Byte: {byteValue}");
            Console.WriteLine($"Int32: {intValue}");
            Console.WriteLine($"Int64: {longValue}");
            Console.WriteLine($"Float: {floatValue}");
            Console.WriteLine($"Double: {doubleValue}");
            Console.WriteLine($"Bytes: {bytesValue[0]} {bytesValue[1]} {bytesValue[2]} {bytesValue[3]} {bytesValue[4]} {bytesValue[5]}");
        }

        public static void MemWriteTest()
        {
            string processName = "AsteriskPassword";
            long addr = 0x00BD0000;

            int pid = Win32.GetPidByProcessName(processName);

            Win32.WriteByte(pid, addr, 78);
            Console.WriteLine($"Byte: {Win32.ReadByte(pid, addr)}");

            Win32.WriteInt32(pid, addr, 10000004);
            Console.WriteLine($"Int32: {Win32.ReadInt32(pid, addr)}");

            Win32.WriteInt64(pid, addr, 100000040000003);
            Console.WriteLine($"Int64: {Win32.ReadInt64(pid, addr)}");

            Win32.WriteFloat(pid, addr, 2.3445f);
            Console.WriteLine($"Float: {Win32.ReadFloat(pid, addr)}");

            Win32.WriteDouble(pid, addr, 8.1111122223333);
            Console.WriteLine($"Double: {Win32.ReadDouble(pid, addr)}");

            Win32.WriteBytes(pid, addr, new byte[] { 1, 2, 3, 4, 5, 6 });
            byte[] bytesValue = Win32.ReadBytes(pid, addr, 6);
            Console.WriteLine($"Bytes: {bytesValue[0]} {bytesValue[1]} {bytesValue[2]} {bytesValue[3]} {bytesValue[4]} {bytesValue[5]}");
        }

    }
}
