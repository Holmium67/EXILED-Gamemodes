using System.Collections.Generic;
using EXILED.Extensions;
using MEC;
using UnityEngine;

namespace Survival
{
	public class Methods
	{
		private readonly Plugin plugin;
		public Methods(Plugin plugin) => this.plugin = plugin;

		public void EnableGamemode()
		{
			plugin.GamemodeEnabled = true;
			PlayerManager.localPlayer.GetComponent<Broadcast>().RpcAddElement("<color=yellow>Survival of the Fittest gamemode is enabled for the next round!</color>", 10, false);
		}

		public void DisableGamemode()
		{
			plugin.GamemodeEnabled = false;
		}

		public IEnumerator<float> SpawnDbois(List<ReferenceHub> hubs)
		{
			foreach (ReferenceHub hub in hubs)
			{
				hub.characterClassManager.NetworkCurClass = RoleType.ClassD;
				hub.Broadcast(30, "<color=orange>You are a Dboi, you need to find a hiding place and pray you are the last person alive.</color>");
				yield return Timing.WaitForOneFrame * 30;
				
				hub.inventory.Clear();
				hub.inventory.AddNewItem(ItemType.Flashlight);
				hub.plyMovementSync.OverridePosition(Plugin.GetRandomSpawnPoint(RoleType.Scp096), 0f);
			}
		}

		public IEnumerator<float> SpawnNuts(List<ReferenceHub> hubs)
		{
			foreach (ReferenceHub hub in hubs)
			{
				hub.characterClassManager.NetworkCurClass = RoleType.Scp173;
				hub.Broadcast(30, "<color=red>You are a Nut, once this notice dissapears, you will be set loose to kill the Dbois!</color>");
			}

			yield return Timing.WaitForSeconds(30f);
			
			foreach (ReferenceHub hub in hubs)
				hub.plyMovementSync.OverridePosition(Plugin.GetRandomSpawnPoint(RoleType.Scp93953), 0f);

			Timing.RunCoroutine(DoBlackout(), "blackout");
		}

		public IEnumerator<float> DoBlackout()
		{
			while (plugin.RoundStarted)
			{
				Generator079.generators[0].RpcCustomOverchargeForOurBeautifulModCreators(30f, false);
				yield return Timing.WaitForSeconds(28f);
			}
		}

		public void DoSetup()
		{
			foreach (Door door in Object.FindObjectsOfType<Door>())
				if (door.DoorName.Contains("CHK") || door.DoorName.Contains("ARMORY"))
				{
					door.Networklocked = true;
					door.NetworkisOpen = false;
				}
			foreach (Pickup item in Object.FindObjectsOfType<Pickup>())
				Object.Destroy(item);
		}
	}
}