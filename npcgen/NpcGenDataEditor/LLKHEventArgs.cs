using System.Windows.Forms;

namespace NpcGenDataEditor;

public class LLKHEventArgs
{
	private Keys keys;

	private bool pressed;

	private uint time;

	private uint scCode;

	public Keys Keys => keys;

	public bool IsPressed => pressed;

	public uint Time => time;

	public uint ScanCode => scCode;

	public bool Hooked { get; set; }

	public LLKHEventArgs(Keys keys, bool pressed, uint time, uint scanCode)
	{
		this.keys = keys;
		this.pressed = pressed;
		this.time = time;
		scCode = scanCode;
	}
}
