using System.Collections.Generic;
using System.Linq;
using EXILED;
using EXILED.Extensions;
using MEC;

namespace Zombieland
{
	public class EventHandlers
	{
		private readonly Plugin plugin;
		public EventHandlers(Plugin plugin) => this.plugin = plugin;

		public void OnWaitingForPlayers()
		{
			plugin.RoundStarted = false;
		}

		public void OnRoundStart()
		{
			if (!plugin.GamemodeEnabled)
				return;

			plugin.RoundStarted = true;
			List<ReferenceHub> players = Plugin.GetHubs();
			List<ReferenceHub> ntf = new List<ReferenceHub>();

			for (int i = 0; i < 3 && players.Count > 2; i++)
			{
				int r = plugin.Gen.Next(players.Count);
				ntf.Add(players[r]);
				players.Remove(players[r]);
			}

			Timing.RunCoroutine(plugin.Functions.SpawnMtf(ntf));
			Timing.RunCoroutine(plugin.Functions.SpawnZombies(players));
			Timing.RunCoroutine(plugin.Functions.Countdown(players), "Countdown");
			Timing.RunCoroutine(plugin.Functions.CarePackage(), "CarePackage");
		}

		public void OnRoundEnd()
		{
			plugin.RoundStarted = false;
			Timing.KillCoroutines("Countdown");
			Timing.KillCoroutines("CarePackage");
		}

		public void OnTeamRespawn(ref TeamRespawnEvent ev)
		{
			foreach (ReferenceHub hub in ev.ToRespawn.Take(ev.MaxRespawnAmt))
				hub.characterClassManager.SetPlayersClass(RoleType.Scp0492, hub.gameObject);
			ev.ToRespawn = new List<ReferenceHub>();
			ev.MaxRespawnAmt = 0;
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (!plugin.GamemodeEnabled)
				return;
			if (!plugin.RoundStarted)
			{
				PlayerManager.localPlayer.gameObject.GetComponent<Broadcast>().RpcClearElements();
				PlayerManager.localPlayer.gameObject.GetComponent<Broadcast>().RpcAddElement("<color=lime>Zombieland Gamemode is starting..</color>", 5, false);
			}
			else
			{
				ev.Player.GetComponent<Broadcast>().TargetClearElements(ev.Player.scp079PlayerScript.connectionToClient);
				ev.Player.Broadcast(5, "<color=lime>Currently playing: Zombieland Gamemode.</color>");
			}
		}
	}
}