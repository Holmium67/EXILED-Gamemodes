using System.Collections.Generic;
using EXILED.Extensions;
using MEC;
using UnityEngine;

namespace Zombieland
{
	public class Methods
	{
		private readonly Plugin plugin;
		public Methods(Plugin plugin) => this.plugin = plugin;

		public void EnableGamemode()
		{
			plugin.GamemodeEnabled = true;
			PlayerManager.localPlayer.GetComponent<Broadcast>().RpcAddElement("<color=lime>Zombieland gamemode is starting next round..</color>", 5, false);
		}

		public void DisableGamemode()
		{
			plugin.GamemodeEnabled = false;
		}

		public IEnumerator<float> SpawnMtf(IEnumerable<ReferenceHub> hubs)
		{
			foreach (ReferenceHub hub in hubs)
			{
				yield return Timing.WaitForSeconds(2f);
				hub.characterClassManager.SetPlayersClass(RoleType.NtfCommander, hub.gameObject);
				yield return Timing.WaitForSeconds(1f);
				
				hub.inventory.Clear();
				List<ItemType> items = new List<ItemType>
				{
					ItemType.Adrenaline,
					ItemType.Medkit,
					ItemType.MicroHID,
					ItemType.GunE11SR,
					ItemType.KeycardO5,
					ItemType.Painkillers,
					ItemType.Radio
				};

				foreach (ItemType item in items)
					hub.inventory.AddNewItem(item, 60);
				
				hub.plyMovementSync.OverridePosition(Plugin.GetRandomSpawnPoint(RoleType.Scp93953), 0f);
				hub.playerStats.maxHP = plugin.NtfHealth;
				hub.playerStats.health = plugin.NtfHealth;
			}
		}

		public IEnumerator<float> SpawnZombies(IEnumerable<ReferenceHub> hubs)
		{
			foreach (ReferenceHub hub in hubs)
			{
				yield return Timing.WaitForSeconds(2f);
				hub.characterClassManager.SetPlayersClass(RoleType.Scp0492, hub.gameObject);
				yield return Timing.WaitForSeconds(1f);

				hub.plyMovementSync.OverridePosition(Plugin.GetRandomSpawnPoint(RoleType.ClassD), 0f);
			}
		}

		public IEnumerator<float> Countdown(IEnumerable<ReferenceHub> hubs)
		{
			yield return Timing.WaitForSeconds(600f);

			foreach (ReferenceHub hub in hubs)
			{
				if (hub.characterClassManager.IsAnyScp())
					hub.characterClassManager.SetPlayersClass(RoleType.Spectator, hub.gameObject);
			}
		}

		public IEnumerator<float> CarePackage()
		{
			List<ItemType> packageItems = new List<ItemType>
			{
				ItemType.Adrenaline,
				ItemType.Adrenaline,
				ItemType.Ammo9mm,
				ItemType.Ammo556,
				ItemType.Ammo762,
				ItemType.Medkit,
				ItemType.Medkit,
				ItemType.Painkillers,
				ItemType.GrenadeFrag,
				ItemType.GunLogicer,
				ItemType.GunProject90,
				ItemType.KeycardO5,
				ItemType.GunMP7,
				ItemType.SCP018,
				ItemType.GunCOM15,
				ItemType.GunUSP,
				ItemType.GunE11SR,
				ItemType.GunProject90,
				ItemType.Ammo9mm,
				ItemType.Ammo556,
				ItemType.Ammo762
			};
			
			List<Vector3> spawnPoints = new List<Vector3>
			{
				Plugin.GetRandomSpawnPoint(RoleType.Scp049),
				Plugin.GetRandomSpawnPoint(RoleType.Scp173),
				Plugin.GetRandomSpawnPoint(RoleType.Scp93953),
				Plugin.GetRandomSpawnPoint(RoleType.NtfCadet)
			};
			
			while (RoundSummary.RoundInProgress())
			{
				yield return Timing.WaitForSeconds(120f);
				
				List<int> rands = new List<int>
				{
					plugin.Gen.Next(packageItems.Count),
					plugin.Gen.Next(packageItems.Count),
					plugin.Gen.Next(packageItems.Count),
					plugin.Gen.Next(packageItems.Count),
					plugin.Gen.Next(packageItems.Count),
					plugin.Gen.Next(packageItems.Count)
				};

				foreach (int r in rands)
				{
					ItemType item = packageItems[r];
					Vector3 spawn = spawnPoints[plugin.Gen.Next(spawnPoints.Count)];

					if (item == ItemType.Ammo9mm || item == ItemType.Ammo556 || item == ItemType.Ammo762)
						Map.SpawnItem(item, 300f, spawn);
					else
						Map.SpawnItem(item, 45f, spawn);
					PlayerManager.localPlayer.gameObject.GetComponent<Broadcast>()
						.RpcAddElement("A care package has arrived!", 10, false);
				}
			}
		}
	}
}