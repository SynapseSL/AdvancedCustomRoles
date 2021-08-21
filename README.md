# AdvancedCustomRoles
This is a SCP:SL plugin for [Synapse](http://synapsesl.xyz) with which you can easily create new Roles with no coding knowledge.

***

## Installation
1. [Install Synapse](https://docs.synapsesl.xyz/setup/setup)
2. Place the AdvancedCustomRoles.dll file that you can download [here](https://github.com/SynapseSL/AdvancedCustomRoles/releases) in your plugin directory
3. Restart/Start your server.

***

# Create Custom Roles

It is very easy to create new Roles. A Example file will be generated in `~/Synapse/configs/server-shared/customroles` that you can use as base to create a new Role.

If you need more Examples you can see some Example Roles [here](https://github.com/SynapseSL/AdvancedCustomRoles/tree/master/Example%20Roles)

`Note: Every Role must have his own file inside the customroles folder.`

### Inventory
```yml
inventory:
  items:
    # The chance that the player gets the Items
  - chance: 100
    # If the prefered weapon attachments should be used
    usePreferences: false
    # The Id of the Item
    iD: 0
    durabillity: 0
    weaponAttachments: 0
    xSize: 1
    ySize: 1
    zSize: 1
  ammo:
    ammo5: 50
    ammo7: 50
    ammo9: 50
    ammo12: 0
    ammo44: 0
```

### Role Information
```yml
# The Name of the Role that other plugins use
name: ExampleRole
# The ID of the Role (you can set your class with setclass {playerid} {Roleid})
roleID: 25
# The Team of the Role (see https://docs.synapsesl.xyz/resources#teams). You can also use your own IDs to create a own Team
teamID: 0
# The Team IDs of the enemies that must be defeated or else the round can't end
enemies:
- 0
# The Team IDs that the role can't hurt
friends:
- 0
```

### Display
```yml
# The skin that the role use (https://docs.synapsesl.xyz/resources#roles)
spawnrole: ClassD
# The Role the Player will become after Escaping
# -1 is None
escapeRole: -1
# The amount of Health the Role spawns with
spawnHealth: 100
# The max amount of Health the Role can have
maxHealth: 100
# The Text that should be displayed when looking at the player of this Role
displayInfo: <color=green>Example</color>
# If the Role name should be removed. For Example Class-D, which can be usefull in combination with displaInfo
removeRoleName: true
```

### SpawnMessage
```yml
# The amount of Time the Broadcast/Hint should be displayed
spawnMessageTime: 5
# The Broadcast that should be displayed
spawnBroadcast: ''
# The Hint that should be displayed
spawnHint: ''
# The content of the Window that appear when spawning
spawnWindow: ''
```

## Spawn
```yml
# The locations where the role can spawn (remove it for default spawns)
spawns:
- room: EZ_Shelter
  x: 0
  y: 2
  z: 0
- room: EZ_Shelter
  x: 2
  y: 2
  z: 0
# This replace a Role at the start of the Round with the Custom Role
# The first value (here 0 which would be SCP-173) is the Role that is going to be replaced and the second value is the chance that the role will be replaced with the custom one
roundStartReplace:
  0: 100
# The same than roundStartReplace except with a Team respawn.(Chaos / MTF Respawn)
respawnReplace: ::lsb::::rsb::
# The max amount of this custom role that can spawn at the beginning of the round
maxSpawnAmount: -1
# The max amount of this custom role that can respawn in one respawn
maxRespawnAmount: -1
# The max Amount of this role that can ever exist (you can still forceclass yourself to the role)
maxAmount: -1
```
