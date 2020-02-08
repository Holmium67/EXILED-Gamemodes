using EXILED;
using EXILED.Extensions;

namespace Outbreak
{
	public class Commands
	{
		private readonly Plugin plugin;
		public Commands(Plugin plugin) => this.plugin = plugin;

		public void OnRaCommand(ref RACommandEvent ev)
		{
			if (!ev.Command.StartsWith("outbreak"))
				return;
			
			string[] args = ev.Command.Split(' ');

			switch (args[0].ToLower())
			{
				case "enable":
					ev.Allow = false;
					plugin.Functions.EnableGamemode();
					ev.Sender.RAMessage("Gamemode enabled for the next round.");
					return;
				case "disable":
					ev.Allow = false;
					plugin.Functions.DisableGamemode();
					ev.Sender.RAMessage("Gamemode disabled.");
					return;
			}
		}
	}
}