using System.Collections.Generic;
using System.Linq;
using EXILED;
using EXILED.Extensions;
using MEC;

namespace Survival
{
	public class EventHandlers
	{
		private readonly Plugin plugin;
		public EventHandlers(Plugin plugin) => this.plugin = plugin;

		public void OnWaitingForPlayers()
		{
			plugin.RoundStarted = false;
			Timing.KillCoroutines("blackout");
		}

		public void OnRoundStart()
		{
			if (!plugin.GamemodeEnabled)
				return;
			
			plugin.RoundStarted = true;
			plugin.Functions.DoSetup();

			List<ReferenceHub> hubs = Plugin.GetHubs();
			List<ReferenceHub> nuts = new List<ReferenceHub>();
			
			for (int i = 0; i < plugin.MaxNuts && hubs.Count > 2; i++)
			{
				int r = plugin.Gen.Next(hubs.Count);
				nuts.Add(hubs[r]);
				hubs.Remove(hubs[r]);
			}

			Timing.RunCoroutine(plugin.Functions.SpawnDbois(hubs));
			Timing.RunCoroutine(plugin.Functions.SpawnNuts(nuts));
		}

		public void OnRoundEnd()
		{
			plugin.RoundStarted = false;
			Timing.KillCoroutines("blackout");
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (!plugin.GamemodeEnabled)
				return;
			
			if (plugin.RoundStarted)
				ev.Player.Broadcast(10, "<color=yellow>Now playing: Survival of the Fittest gamemode</color");
			else
			{
				Broadcast broadcast = PlayerManager.localPlayer.GetComponent<Broadcast>();
				broadcast.CallRpcClearElements();
				broadcast.RpcAddElement("<color=yellow>Survival of the Fittest gamemode is starting..</color>", 10, false);
			}
		}

		public void OnTeamRespawn(ref TeamRespawnEvent ev)
		{
			if (!plugin.RoundStarted)
				return;
			
			ev.ToRespawn = new List<ReferenceHub>();
			ev.MaxRespawnAmt = 0;
		}
	}
}