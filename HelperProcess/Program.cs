using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("kernel32.dll")]
    static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

    const uint PROCESS_VM_READ = 0x0010;
    const uint PROCESS_QUERY_INFORMATION = 0x0400;

    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: HelperProcess.exe <ProcessId> <MemoryAddress>");
            return;
        }

        int processId = int.Parse(args[0]);
        long memoryAddress = long.Parse(args[1], System.Globalization.NumberStyles.HexNumber);

        IntPtr hProcess = OpenProcess(PROCESS_VM_READ | PROCESS_QUERY_INFORMATION, false, processId);
        if (hProcess == IntPtr.Zero)
        {
            Console.WriteLine("Failed to open process.");
            return;
        }

        byte[] buffer = new byte[8];
        if (ReadProcessMemory(hProcess, new IntPtr(memoryAddress), buffer, buffer.Length, out int bytesRead))
        {
            Console.WriteLine(BitConverter.ToInt64(buffer, 0));
        }
        else
        {
            Console.WriteLine("Failed to read memory.");
        }
    }
}
