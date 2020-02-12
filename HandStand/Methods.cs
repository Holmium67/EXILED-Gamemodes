using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace HandStand
{
	public class Methods
	{
		private readonly Plugin plugin;
		public Methods(Plugin plugin) => this.plugin = plugin;
		public List<ReferenceHub> InvertedPlayers = new List<ReferenceHub>();
		
		public void EnableGamemode()
		{
			plugin.GamemodeEnabled = true;
			PlayerManager.localPlayer.GetComponent<Broadcast>().RpcAddElement("<color=purple>Handstand gamemode is enabled for the next round!</color>", 10, false);
		}

		public void DisableGamemode()
		{
			plugin.GamemodeEnabled = false;
		}

		public void InvertPlayer(ReferenceHub hub)
		{
			if (InvertedPlayers.Contains(hub))
				return;
			
			GameObject gameObject = hub.gameObject;
			NetworkIdentity ident = gameObject.GetComponent<NetworkIdentity>();
			ObjectDestroyMessage destroyMessage = new ObjectDestroyMessage { netId = ident.netId };

			gameObject.transform.localScale = new Vector3(1f, -1f, 1f);

			foreach (GameObject player in PlayerManager.players)
			{
				if (gameObject == player)
					continue;

				NetworkConnection conn = player.GetComponent<NetworkIdentity>().connectionToClient;

				conn.Send(destroyMessage);
				object[] parameters = new object[] { ident, conn };
				typeof(NetworkServer).InvokeStaticMethod("SendSpawnMessage", parameters);
			}

			InvertedPlayers.Add(hub);
		}
	}
}