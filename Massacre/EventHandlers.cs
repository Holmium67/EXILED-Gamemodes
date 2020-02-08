using EXILED;
using EXILED.Extensions;
using MEC;

namespace Massacre
{
	public class EventHandlers
	{
		private readonly Massacre plugin;
		public EventHandlers(Massacre plugin) => this.plugin = plugin;
		public void OnWaitingForPlayers()
		{
			plugin.RoundStarted = false;
		}

		public void OnRoundStart()
		{
			if (!plugin.GamemodeEnabled) 
				return;
			
			plugin.RoundStarted = true;
			Timing.RunCoroutine(plugin.Functions.SpawnPeople());
		}

		public void OnRoundEnd()
		{
			plugin.RoundStarted = false;
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (!plugin.GamemodeEnabled) 
				return;
			
			PlayerManager.localPlayer.GetComponent<Broadcast>().RpcClearElements();
			if (plugin.RoundStarted)
				ev.Player.Broadcast(5, "<color=red>Currently playing: Massacre of the Dbois Gamemode.</color>");
			PlayerManager.localPlayer.GetComponent<Broadcast>()
				.RpcAddElement("<color=red>Massacre of the Dbois is starting..</color>", 5, false);
		}
	}
}