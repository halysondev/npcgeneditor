using System;
using System.Windows.Forms;
using UIExceptionHandlerWinForms;

namespace NpcGenDataEditor;

internal static class Program
{
	[STAThread]
	private static void Main()
	{
		UIException.Start("SmtpServer", 26, "Password", "User", "bf", "user@gmail.com", "Exception", "Npcgen Editor");
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		Application.Run(new Form1());
	}
}
