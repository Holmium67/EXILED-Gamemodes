using EXILED;
using EXILED.Extensions;

namespace HandStand
{
	public class Commands
	{
		private readonly Plugin plugin;
		public Commands(Plugin plugin) => this.plugin = plugin;

		public void OnRaCommand(ref RACommandEvent ev)
		{
			if (!ev.Command.StartsWith("hands"))
				return;
			string[] args = ev.Command.Split(' ');

			switch (args[1].ToLower())
			{
				case "enable":
					plugin.Functions.EnableGamemode();
					ev.Sender.RAMessage("Gamemode enabled.");
					break;
				case "disable":
					plugin.Functions.DisableGamemode();
					ev.Sender.RAMessage("Gamemode disabled.");
					break;
			}
		}
	}
}