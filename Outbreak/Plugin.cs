using System;
using EXILED;

namespace Outbreak
{
	public class Plugin : EXILED.Plugin
	{
		public Methods Functions { get; private set; }
		public EventHandlers EventHandlers { get; private set; }
		public Commands Commands { get; private set; }

		public bool Enabled;
		public int MaxZombies;
		public int ZombieHealth;
		public bool AlphasBreakDoors;

		public bool RoundStarted;
		public bool GamemodeEnabled;
		public Random Gen = new Random();

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
			Events.PlayerJoinEvent += EventHandlers.OnPlayerJoin;
			Events.CheckRoundEndEvent += EventHandlers.OnCheckRoundEnd;
			Events.PlayerDeathEvent += EventHandlers.OnPlayerDeath;
		}

		public override void OnDisable()
		{
			Events.WaitingForPlayersEvent -= EventHandlers.OnWaitingForPlayers;
			Events.RoundStartEvent -= EventHandlers.OnRoundStart;
			Events.RoundEndEvent -= EventHandlers.OnRoundEnd;
			Events.RemoteAdminCommandEvent -= Commands.OnRaCommand;

			EventHandlers = null;
			Functions = null;
			Commands = null;
		}

		public override void OnReload()
		{
		}

		public override string getName { get; } = "Outbreak";

		public void ReloadConfig()
		{
			Enabled = Config.GetBool("Outbreak_enabled", true);
			MaxZombies = Config.GetInt("Outbreak_max_alphas", 3);
			ZombieHealth = Config.GetInt("Outbreak_alpha_health", 3000);
		}
	}
}