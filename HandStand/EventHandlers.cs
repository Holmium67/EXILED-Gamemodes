using System.Collections.Generic;
using EXILED;
using EXILED.Extensions;

namespace HandStand
{
	public class EventHandlers
	{
		private readonly Plugin plugin;
		public EventHandlers(Plugin plugin) => this.plugin = plugin;

		public void OnWaitingForPlayers()
		{
			plugin.RoundStarted = false;
			plugin.Functions.InvertedPlayers.Clear();
		}

		public void OnRoundStart()
		{
			if (!plugin.GamemodeEnabled) 
				return;
			
			plugin.RoundStarted = true;
		}

		public void OnRoundEnd()
		{
			plugin.RoundStarted = false;
			plugin.Functions.InvertedPlayers.Clear();
		}

		public void OnPlayerSpawn(PlayerSpawnEvent ev)
		{
			if (!plugin.RoundStarted)
				return;
			
			plugin.Functions.InvertPlayer(ev.Player);
		}
		
		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (!plugin.GamemodeEnabled) 
				return;
			
			PlayerManager.localPlayer.GetComponent<Broadcast>().RpcClearElements();
			if (plugin.RoundStarted)
				ev.Player.Broadcast(5, "<color=purple>Currently playing: Handstand Gamemode.</color>");
			PlayerManager.localPlayer.GetComponent<Broadcast>()
				.RpcAddElement("<color=purple>Handstand is starting..</color>", 5, false);
		}
	}
}