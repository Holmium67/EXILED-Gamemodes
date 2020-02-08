using EXILED;
using EXILED.Extensions;

namespace Massacre
{
	public class Commands
	{
		private readonly Massacre plugin;
		public Commands(Massacre plugin) => this.plugin = plugin;

		public void OnRaCommand(ref RACommandEvent ev)
		{
			if (!ev.Command.StartsWith("mass"))
				return;
			string[] args = ev.Command.Split(' ');

			switch (args[1].ToLower())
			{
				case "enable":
					ev.Allow = false;
					ev.Sender.RAMessage("Gamemode enabled. c:");
					plugin.Functions.EnableGamemode();
					break;
				case "disable":
					ev.Allow = false;
					ev.Sender.RAMessage("Gamemode disabled. :c");
					plugin.Functions.DisableGamemode();
					break;
			}
		}
	}
}