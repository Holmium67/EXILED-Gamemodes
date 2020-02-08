using System.Collections.Generic;
using MEC;

namespace Outbreak
{
	public class Methods
	{
		private readonly Plugin plugin;
		public Methods(Plugin plugin) => this.plugin = plugin;

		public void EnableGamemode()
		{
			plugin.GamemodeEnabled = true;
			PlayerManager.localPlayer.GetComponent<Broadcast>().RpcAddElement("<color=green>Outbreak gamemode is starting next round..</color>", 5, false);
		}

		public void DisableGamemode()
		{
			plugin.GamemodeEnabled = false;
		}
		public IEnumerator<float> SpawnAlphas()
		{
			yield return Timing.WaitForSeconds(1f);

			foreach (ReferenceHub player in Plugin.GetHubs())
			{
				if (!player.characterClassManager.IsAnyScp())
					continue;
				
				player.characterClassManager.SetPlayersClass(RoleType.Scp0492, player.gameObject);
				yield return Timing.WaitForSeconds(1.5f);

				player.plyMovementSync.OverridePosition(Plugin.GetRandomSpawnPoint(RoleType.Scp049),
					player.gameObject.transform.rotation.y);
				player.playerStats.maxHP = plugin.ZombieHealth;
				player.playerStats.health = plugin.ZombieHealth;
			}
		}

		public IEnumerator<float> RespawnZombie(ReferenceHub hub)
		{
			yield return Timing.WaitForSeconds(1f);

			hub.characterClassManager.SetPlayersClass(RoleType.Scp0492, hub.gameObject);
		}
	}
}