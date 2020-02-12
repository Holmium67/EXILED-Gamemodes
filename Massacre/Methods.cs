using System;
using System.Collections.Generic;
using EXILED;
using MEC;
using UnityEngine;

namespace Massacre
{
	public class Methods
	{
		private readonly Massacre plugin;
		public Methods(Massacre plugin) => this.plugin = plugin;

		public void EnableGamemode()
		{
			plugin.GamemodeEnabled = true;
			PlayerManager.localPlayer.GetComponent<Broadcast>().RpcAddElement("<color=red>Massacre of the D-Bois gamemode is enabled for the next round!</color>", 10, false);
		}

		public void DisableGamemode()
		{
			plugin.GamemodeEnabled = false;
		}

		public IEnumerator<float> SpawnPeople()
		{
			yield return Timing.WaitForSeconds(2f);

			try
			{
				List<ReferenceHub> players = Massacre.GetHubs();
				List<ReferenceHub> nuts = new List<ReferenceHub>();
				for (int i = 0; i < plugin.MaxPeanuts; i++)
				{
					int r = plugin.Gen.Next(players.Count);
					players[r].characterClassManager.SetPlayersClass(RoleType.Scp173, players[r].gameObject);
					players.Remove(players[r]);
					nuts.Add(players[r]);
				}

				foreach (ReferenceHub player in players)
					player.characterClassManager.SetPlayersClass(RoleType.ClassD, player.gameObject);

				Timing.RunCoroutine(TeleportPlayers());
			}
			catch (Exception e)
			{
				Massacre.Error(e.ToString());
			}
		}

		public IEnumerator<float> TeleportPlayers()
		{
			yield return Timing.WaitForSeconds(0.5f);
			Massacre.Debug("Teleporting humans..");

			List<ReferenceHub> players = Massacre.GetHubs();
			foreach (ReferenceHub hub in players)
			{
				if (hub.characterClassManager.IsHuman())
				{
					Plugin.Debug($"Teleporting {hub.nicknameSync.MyNick} - {hub.characterClassManager.CurClass}");
					hub.plyMovementSync.OverridePosition(new Vector3(53, 1020, -44), 0f);
				}
			}

			yield return Timing.WaitForSeconds(3f);
			
			Massacre.Debug("Teleporting peanut bois");
			foreach (ReferenceHub nut in players)
			{
				if (nut.characterClassManager.IsAnyScp())
				{
					Massacre.Debug($"Teleporting {nut.nicknameSync.MyNick} - {nut.characterClassManager.CurClass}");
					nut.plyMovementSync.OverridePosition(new Vector3(53, 1020, -44), 0f);
				}
			}
		}
	}
}