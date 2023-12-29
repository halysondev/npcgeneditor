using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NpcGenDataEditor;

public static class KBDHook
{
	public delegate void HookKeyPress(LLKHEventArgs e);

	private struct KBDLLHOOKSTRUCT
	{
		public uint vkCode;

		public uint scanCode;

		public KBDLLHOOKSTRUCTFlags flags;

		public uint time;

		public IntPtr dwExtraInfo;
	}

	[Flags]
	private enum KBDLLHOOKSTRUCTFlags
	{
		LLKHF_EXTENDED = 1,
		LLKHF_INJECTED = 0x10,
		LLKHF_ALTDOWN = 0x20,
		LLKHF_UP = 0x80
	}

	private static IntPtr hHook = IntPtr.Zero;

	private static IntPtr hModule = IntPtr.Zero;

	private static bool hookInstall = false;

	private static bool localHook = true;

	private static API.HookProc hookDel;

	public static bool IsHookInstalled => hookInstall && hHook != IntPtr.Zero;

	public static IntPtr ModuleHandle => hModule;

	public static bool LocalHook
	{
		get
		{
			return localHook;
		}
		set
		{
			if (value != localHook)
			{
				if (IsHookInstalled)
				{
					throw new Win32Exception("Can't change type of hook than it install!");
				}
				localHook = value;
			}
		}
	}

	public static event HookKeyPress KeyUp;

	public static event HookKeyPress KeyDown;

	public static void InstallHook()
	{
		if (!IsHookInstalled)
		{
			hModule = Marshal.GetHINSTANCE(AppDomain.CurrentDomain.GetAssemblies()[0].GetModules()[0]);
			hookDel = HookProcFunction;
			if (localHook)
			{
				hHook = API.SetWindowsHookEx(API.HookType.WH_KEYBOARD, hookDel, IntPtr.Zero, AppDomain.CurrentDomain.Id);
			}
			else
			{
				hHook = API.SetWindowsHookEx(API.HookType.WH_KEYBOARD_LL, hookDel, hModule, 0);
			}
			if (hHook != IntPtr.Zero)
			{
				hookInstall = true;
			}
		}
	}

	public static void UnInstallHook()
	{
		if (IsHookInstalled)
		{
			if (!API.UnhookWindowsHookEx(hHook))
			{
				throw new Win32Exception("Can't uninstall low level keyboard hook!");
			}
			hHook = IntPtr.Zero;
			hModule = IntPtr.Zero;
			hookInstall = false;
		}
	}

	private static IntPtr HookProcFunction(int nCode, IntPtr wParam, [In] IntPtr lParam)
	{
		if (nCode == 0)
		{
			LLKHEventArgs lLKHEventArgs = null;
			if (localHook)
			{
				bool flag = false;
				if (lParam.ToInt32() >> 31 == 0)
				{
					flag = true;
				}
				Keys keys = (Keys)wParam.ToInt32();
				lLKHEventArgs = new LLKHEventArgs(keys, flag, 0u, 0u);
				if (flag)
				{
					KBDHook.KeyDown?.Invoke(lLKHEventArgs);
				}
				else
				{
					KBDHook.KeyUp?.Invoke(lLKHEventArgs);
				}
			}
			else
			{
				KBDLLHOOKSTRUCT kBDLLHOOKSTRUCT = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
				bool flag2 = false;
				if (wParam.ToInt32() == 256 || wParam.ToInt32() == 260)
				{
					flag2 = true;
				}
				Keys vkCode = (Keys)kBDLLHOOKSTRUCT.vkCode;
				lLKHEventArgs = new LLKHEventArgs(vkCode, flag2, kBDLLHOOKSTRUCT.time, kBDLLHOOKSTRUCT.scanCode);
				if (flag2)
				{
					KBDHook.KeyDown?.Invoke(lLKHEventArgs);
				}
				else
				{
					KBDHook.KeyUp?.Invoke(lLKHEventArgs);
				}
			}
			if (lLKHEventArgs != null && lLKHEventArgs.Hooked)
			{
				return (IntPtr)1;
			}
		}
		return API.CallNextHookEx(hHook, nCode, wParam, lParam);
	}
}
