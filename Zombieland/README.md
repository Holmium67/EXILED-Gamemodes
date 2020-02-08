Zombieland
======
## Description
A configurable number of NTF will spawn in SCP-939's area (default 3). They will be given an O5 keycard, Radio, Medkit, MicroHID, Painkillers and Adrenaline. Their job is to survive the rest of the server being zombies (who spawn in D-Class cells) for 10 minutes. If successful, all remaining zombies kill be killed and the round will end. If they all die, the Zombies win.

Players who are killed (both zombies and humans) will respawn as more SCP-049-2's at standard Respawn itnervals.

Every 2 minutes there will be a supply drop, that will contain 6 items from the care package pool, which can include:
Adrenaline, Medkits, Painkillers, Frag Grenades, Logicers, P90s, MP7s, Com15s, USPs, E11s, SCP-018, SCP-207 or ammo of any type (each sammo will grant 200 ammo of that type).

### Features


### Config Settings
Config option | Config Type | Default Value | Description
:---: | :---: | :---: | :------
Zombieland_enabled | bool | true | If the gamemode should be loaded.
Zombieland_max_ntf | int | 3 | How many NTF, at most, should be spawned. It will always prefer to spawn another NTF over another zombie, but will always ensure there is at least 1 zombie.
Zombieland_ntf_health | int | 500 | The starting health of the NTF.

### Commands
  Command |  |  | Description
:---: | :---: | :---: | :------
**Aliases** | **zombieland** | **zl** | ****
zombieland enable | ~~ | ~~ | Enables the gamemode
zombieland disable | ~~ | ~~ | Disables the gamemode
