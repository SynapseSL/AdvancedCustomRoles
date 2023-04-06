# AdvancedCustomRoles
This is a SCP:SL plugin for [Synapse 3](http://synapsesl.xyz) with which you can easily create new Roles with no coding knowledge.

***

## Installation
Download the latest version of this Plugin and unzip the file inside your SL Server Files

***

# Create Custom Roles

It is very easy to create new Roles. A Example file will be generated in `~/Synapse/CustomRoles` that you can use as base to create a new Role.

You can read our docs [here](https://docs3.synapsesl.xyz/resources) for Information about some Id's you need for configuring roles

## Example Role
```yml
[Role]
name: ExampleRole
#A unique id for this Role for plugins and commands to spawn this role
roleId: 101
#The team wh
teamId: 2
#A list of all enemies that must be defeated before the round can end
enemies:
- 0
#A list of all teams that this role can't damage (ff configuration)
friends:
- 0
#The Role which the Player will actually be set and will be used for internal server calculation
role: ClassD
#The Role which other Players will see. When None it uses role
visibleRole: None
#The Role which the Player itself will see. When None it uses role
ownRole: None
#The role the player will be assigned when he escapes
escapeRole: 4294967295
health: 100
maxHealth: 150
artificialHealth: 0
maxArtificialHealth: 75
#The spawn locations for this Role. Use the Roompoint command in game to get the values
possibleSpawns:
- roomName: Shelter
  position:
    x: 0
    y: 2
    z: 0
  rotation:
    x: 80
    y: 180
    z: 0
#The Inventories the player can get
possibleInventories:
- items:
  - chance: 100
    provideFully: true
    iD: 1
    durability: 0
    weaponAttachments: 0
    xSize: 1
    ySize: 1
    zSize: 1
  ammo:
    ammo5: 0
    ammo7: 0
    ammo9: 10
    ammo12: 0
    ammo44: 0
#When enabled will a Custom Display generated that shows the Custom Role Name instead of the vanilla one
customDisplay: true
#When enabled the Role will display it's unit in the custom display
hierarchy: false
#When enabled will the unit in the Custom Display be a custom one
useCustomUnitName: false
#See above
customUnitName: ''
#A list of teams that can see the unit
teamsThatCanSeeUnit: []
scale:
  x: 1
  y: 1
  z: 1
godMode: false
#Additional Info that will be added to the Display
displayInfo: Test
spawnMessageTime: 5
spawnBroadcast: ''
spawnHint: Hello
spawnWindow: ''
#The chance for each player that he will spawn as this role
setPlayerAtRoundStartChance: 0
#When enabled will one Scp spawn less upon this role being spawned
spawnOneScpLessOnSpawn: false
# left side the Role Id and Right side the chance
# this will replace the role on the left upon Respawn
respawnReplace:
  12: 0
maxSpawnAmount: 10
maxRespawnAmount: 10
maxAmount: 5

[Scp]
scpAttackDamage: 200
scp106TakeIntoPocket: true

[Advanced]
commandToExecuteAtSpawn:
- pbc %player% 5 Hello
commandToExecuteAtDeSpawn:
- pbc %player% %playername% you are now dead
```
