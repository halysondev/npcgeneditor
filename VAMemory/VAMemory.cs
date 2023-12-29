using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

public class VAMemory
{
	[Flags]
	private enum ProcessAccessFlags : uint
	{
		All = 0x1F0FFFu,
		Terminate = 1u,
		CreateThread = 2u,
		VMOperation = 8u,
		VMRead = 0x10u,
		VMWrite = 0x20u,
		DupHandle = 0x40u,
		SetInformation = 0x200u,
		QueryInformation = 0x400u,
		Synchronize = 0x100000u
	}

	private enum VirtualMemoryProtection : uint
	{
		PAGE_NOACCESS = 1u,
		PAGE_READONLY = 2u,
		PAGE_READWRITE = 4u,
		PAGE_WRITECOPY = 8u,
		PAGE_EXECUTE = 16u,
		PAGE_EXECUTE_READ = 32u,
		PAGE_EXECUTE_READWRITE = 64u,
		PAGE_EXECUTE_WRITECOPY = 128u,
		PAGE_GUARD = 256u,
		PAGE_NOCACHE = 512u,
		PROCESS_ALL_ACCESS = 2035711u
	}

	public static bool debugMode;

	private IntPtr baseAddress;

	private ProcessModule processModule;

	private Process[] mainProcess;

	private IntPtr processHandle;

	public string processName { get; set; }

	public long getBaseAddress
	{
		get
		{
			baseAddress = (IntPtr)0;
			processModule = mainProcess[0].MainModule;
			baseAddress = processModule.BaseAddress;
			return (long)baseAddress;
		}
	}

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint dwSize, uint lpNumberOfBytesRead);

	[DllImport("kernel32.dll")]
	private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, uint lpNumberOfBytesWritten);

	[DllImport("kernel32.dll")]
	private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

	[DllImport("kernel32.dll")]
	private static extern bool CloseHandle(IntPtr hObject);

	[DllImport("kernel32.dll")]
	private static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

	[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

	public VAMemory()
	{
	}

	public VAMemory(string pProcessName)
	{
		processName = pProcessName;
	}

	public bool CheckProcess()
	{
		if (processName != null)
		{
			mainProcess = Process.GetProcessesByName(processName);
			if (mainProcess.Length == 0)
			{
				ErrorProcessNotFound(processName);
				return false;
			}
			processHandle = OpenProcess(2035711u, bInheritHandle: false, mainProcess[0].Id);
			if (processHandle == IntPtr.Zero)
			{
				ErrorProcessNotFound(processName);
				return false;
			}
			return true;
		}
		MessageBox.Show("Programmer, define process name first!");
		return false;
	}

	public byte[] ReadByteArray(IntPtr pOffset, uint pSize)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			VirtualProtectEx(processHandle, pOffset, (UIntPtr)pSize, 4u, out var lpflOldProtect);
			byte[] array = new byte[pSize];
			ReadProcessMemory(processHandle, pOffset, array, pSize, 0u);
			VirtualProtectEx(processHandle, pOffset, (UIntPtr)pSize, lpflOldProtect, out lpflOldProtect);
			return array;
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadByteArray" + ex.ToString());
			}
			return new byte[1];
		}
	}

	public string ReadStringUnicode(IntPtr pOffset, uint pSize)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return Encoding.Unicode.GetString(ReadByteArray(pOffset, pSize), 0, (int)pSize);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadStringUnicode" + ex.ToString());
			}
			return "";
		}
	}

	public string ReadStringASCII(IntPtr pOffset, uint pSize)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return Encoding.ASCII.GetString(ReadByteArray(pOffset, pSize), 0, (int)pSize);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadStringASCII" + ex.ToString());
			}
			return "";
		}
	}

	public char ReadChar(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToChar(ReadByteArray(pOffset, 1u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadChar" + ex.ToString());
			}
			return ' ';
		}
	}

	public bool ReadBoolean(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToBoolean(ReadByteArray(pOffset, 1u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadByte" + ex.ToString());
			}
			return false;
		}
	}

	public byte ReadByte(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return ReadByteArray(pOffset, 1u)[0];
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadByte" + ex.ToString());
			}
			return 0;
		}
	}

	public short ReadInt16(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToInt16(ReadByteArray(pOffset, 2u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadInt16" + ex.ToString());
			}
			return 0;
		}
	}

	public short ReadShort(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToInt16(ReadByteArray(pOffset, 2u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadInt16" + ex.ToString());
			}
			return 0;
		}
	}

	public int ReadInt32(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToInt32(ReadByteArray(pOffset, 4u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadInt32" + ex.ToString());
			}
			return 0;
		}
	}

	public int ReadInteger(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToInt32(ReadByteArray(pOffset, 4u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadInteger" + ex.ToString());
			}
			return 0;
		}
	}

	public long ReadInt64(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToInt64(ReadByteArray(pOffset, 8u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadInt64" + ex.ToString());
			}
			return 0L;
		}
	}

	public long ReadLong(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToInt64(ReadByteArray(pOffset, 8u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadLong" + ex.ToString());
			}
			return 0L;
		}
	}

	public ushort ReadUInt16(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToUInt16(ReadByteArray(pOffset, 2u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadUInt16" + ex.ToString());
			}
			return 0;
		}
	}

	public ushort ReadUShort(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToUInt16(ReadByteArray(pOffset, 2u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadUShort" + ex.ToString());
			}
			return 0;
		}
	}

	public uint ReadUInt32(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToUInt32(ReadByteArray(pOffset, 4u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadUInt32" + ex.ToString());
			}
			return 0u;
		}
	}

	public uint ReadUInteger(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToUInt32(ReadByteArray(pOffset, 4u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadUInteger" + ex.ToString());
			}
			return 0u;
		}
	}

	public ulong ReadUInt64(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToUInt64(ReadByteArray(pOffset, 8u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadUInt64" + ex.ToString());
			}
			return 0uL;
		}
	}

	public long ReadULong(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return (long)BitConverter.ToUInt64(ReadByteArray(pOffset, 8u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadULong" + ex.ToString());
			}
			return 0L;
		}
	}

	public float ReadFloat(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToSingle(ReadByteArray(pOffset, 4u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadFloat" + ex.ToString());
			}
			return 0f;
		}
	}

	public double ReadDouble(IntPtr pOffset)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return BitConverter.ToDouble(ReadByteArray(pOffset, 8u), 0);
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: ReadDouble" + ex.ToString());
			}
			return 0.0;
		}
	}

	public bool WriteByteArray(IntPtr pOffset, byte[] pBytes)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			VirtualProtectEx(processHandle, pOffset, (UIntPtr)(ulong)pBytes.Length, 4u, out var lpflOldProtect);
			bool result = WriteProcessMemory(processHandle, pOffset, pBytes, (uint)pBytes.Length, 0u);
			VirtualProtectEx(processHandle, pOffset, (UIntPtr)(ulong)pBytes.Length, lpflOldProtect, out lpflOldProtect);
			return result;
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteByteArray" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteStringUnicode(IntPtr pOffset, string pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, Encoding.Unicode.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteStringUnicode" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteStringASCII(IntPtr pOffset, string pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, Encoding.ASCII.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteStringASCII" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteBoolean(IntPtr pOffset, bool pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteBoolean" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteChar(IntPtr pOffset, char pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteChar" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteByte(IntPtr pOffset, byte pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteByte" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteInt16(IntPtr pOffset, short pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteInt16" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteShort(IntPtr pOffset, short pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteShort" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteInt32(IntPtr pOffset, int pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteInt32" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteInteger(IntPtr pOffset, int pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteInt" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteInt64(IntPtr pOffset, long pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteInt64" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteLong(IntPtr pOffset, long pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteLong" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteUInt16(IntPtr pOffset, ushort pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteUInt16" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteUShort(IntPtr pOffset, ushort pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteShort" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteUInt32(IntPtr pOffset, uint pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteUInt32" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteUInteger(IntPtr pOffset, uint pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteUInt" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteUInt64(IntPtr pOffset, ulong pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteUInt64" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteULong(IntPtr pOffset, ulong pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteULong" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteFloat(IntPtr pOffset, float pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteFloat" + ex.ToString());
			}
			return false;
		}
	}

	public bool WriteDouble(IntPtr pOffset, double pData)
	{
		if (processHandle == IntPtr.Zero)
		{
			CheckProcess();
		}
		try
		{
			return WriteByteArray(pOffset, BitConverter.GetBytes(pData));
		}
		catch (Exception ex)
		{
			if (debugMode)
			{
				Console.WriteLine("Error: WriteDouble" + ex.ToString());
			}
			return false;
		}
	}

	private void ErrorProcessNotFound(string pProcessName)
	{
		MessageBox.Show(processName + " is not running or has not been found. Please check and try again", "Process Not Found", MessageBoxButtons.OK, MessageBoxIcon.Hand);
	}
}
