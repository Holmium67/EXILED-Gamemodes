using EXILED;
using EXILED.Extensions;
using MEC;

namespace GangWar
{
	public class EventHandlers
	{
		private readonly GangWar plugin;
		public EventHandlers(GangWar plugin) => this.plugin = plugin;

		public void OnWaitingForPlayers()
		{
			if (plugin.GamemodeEnabled)
				plugin.RoundStarted = false;
		}

		public void OnRoundStart()
		{
			if (plugin.GamemodeEnabled)
			{
				plugin.RoundStarted = true;
				Timing.RunCoroutine(plugin.Functions.SpawnPlayers());
			}
		}

		public void OnRoundEnd()
		{
			if (plugin.GamemodeEnabled)
				plugin.RoundStarted = false;
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (plugin.GamemodeEnabled)
			{
				PlayerManager.localPlayer.GetComponent<Broadcast>().RpcClearElements();
				if (plugin.RoundStarted)
					ev.Player.Broadcast(5, "<color=green>Currently playing Gangwar Gamemode!</color>");
				else
					PlayerManager.localPlayer.GetComponent<Broadcast>()
						.RpcAddElement("<color=green>Gangwar is starting..</color>", 5, false);
			}
		}

		public void OnRespawn(ref TeamRespawnEvent ev)
		{
			if (plugin.GamemodeEnabled && plugin.RoundStarted)
			{
				ev.IsChaos = RoundSummary.singleton.CountTeam(Team.MTF) > RoundSummary.singleton.CountTeam(Team.CHI);

				foreach (ReferenceHub hub in ev.ToRespawn)
					Timing.RunCoroutine(plugin.Functions.SetInventory(hub, 2f));
			}
		}
	}
}