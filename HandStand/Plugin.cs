using EXILED;

namespace HandStand
{
	public class Plugin : EXILED.Plugin
	{
		public Methods Functions { get; private set; }
		public EventHandlers EventHandlers { get; private set; }
		public Commands Commands { get; private set; }
		public bool RoundStarted;
		public bool GamemodeEnabled;

		public bool Enabled;

		public override void OnEnable()
		{
			ReloadConfig();
			if (!Enabled)
				return;

			EventHandlers = new EventHandlers(this);
			Functions = new Methods(this);
			Commands = new Commands(this);

			Events.WaitingForPlayersEvent += EventHandlers.OnWaitingForPlayers;
			Events.RoundStartEvent += EventHandlers.OnRoundStart;
			Events.RoundEndEvent += EventHandlers.OnRoundEnd;
			Events.RemoteAdminCommandEvent += Commands.OnRaCommand;
			Events.PlayerSpawnEvent += EventHandlers.OnPlayerSpawn;
			Events.PlayerJoinEvent += EventHandlers.OnPlayerJoin;
		}

		public override void OnDisable()
		{
			Events.WaitingForPlayersEvent -= EventHandlers.OnWaitingForPlayers;
			Events.RoundStartEvent -= EventHandlers.OnRoundStart;
			Events.RoundEndEvent -= EventHandlers.OnRoundEnd;
			Events.RemoteAdminCommandEvent -= Commands.OnRaCommand;
			Events.PlayerSpawnEvent -= EventHandlers.OnPlayerSpawn;
			Events.PlayerJoinEvent -= EventHandlers.OnPlayerJoin;

			EventHandlers = null;
			Functions = null;
			Commands = null;
		}

		public override void OnReload()
		{
		}

		public override string getName { get; } = "HandStand";

		public void ReloadConfig()
		{
			Enabled = Config.GetBool("HandStand_enabled", true);
		}
	}
}