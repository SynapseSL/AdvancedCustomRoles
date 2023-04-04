using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Text;
using Neuron.Core;
using Neuron.Core.Meta;
using PlayerRoles;
using Syml;
using Synapse3.SynapseModule.Config;
using Synapse3.SynapseModule.Map.Rooms;
using Synapse3.SynapseModule.Role;

namespace AdvancedCustomRoles;

public class CustomRoleHandler : InjectedLoggerBase
{
    private readonly RoleService _role;
    private readonly NeuronBase _neuronBase;

    public CustomRoleHandler(RoleService roleService, NeuronBase neuronBase)
    {
        _role = roleService;
        _neuronBase = neuronBase;
    }
    
    public List<CustomRole> CustomRoles { get; } = new();

    public CustomRole GetCustomRole(uint id) => CustomRoles.First(x => x.RoleId == id);

    public void Reload()
    {
        foreach (var role in CustomRoles)
            _role.UnRegisterRole(role.RoleId);
        
        CustomRoles.Clear();
        Load();
    }

    public void Load()
    {
        var path = _neuronBase.PrepareRelativeDirectory("CustomRoles");
        var files = Directory.GetFiles(path, "*.syml").ToList();
        if (files.Count == 0) CreateExampleFile();
        foreach (var file in files)
        {
            try
            {
                var syml = new SymlDocument();
                syml.Load(File.ReadAllText(file, Encoding.UTF8));
                if (syml.Sections.Count == 0)
                {
                    Logger.Warn($"No Section found in {file}");
                    continue;
                }

                CustomRole role = null;
                ScpConfig scp = null;
                AdvancedConfig advanced = null;
                foreach (var documentSection in syml.Sections)
                {
                    switch (documentSection.Key.ToLower())
                    {
                        case "role" when role == null:
                            role = documentSection.Value.Export<CustomRole>();
                            if (_role.IsIdRegistered(role.RoleId))
                            {
                                Logger.Warn(
                                    $"CustomRole: {role.Name} uses an Id that is already used. Please choose another one\n" +
                                    file);
                                role = null;
                            }
                            break;

                        case "scp" when scp == null:
                            scp = documentSection.Value.Export<ScpConfig>();
                            break;
                        
                        case "advanced" when advanced == null:
                            advanced = documentSection.Value.Export<AdvancedConfig>();
                            break;
                    }
                }
                if (role == null) continue;
                role.Scp = scp;
                role.Advanced = advanced;
                
                CustomRoles.Add(role);
                _role.RegisterRole(new RoleAttribute(role.Name, role.RoleId, role.TeamId, typeof(AdvancedRoleScript)));
            }
            catch (Exception ex)
            {
                Logger.Error("Error while Loading File:\n" + ex);
            }
        }
    }

    public void CreateExampleFile()
    {
        Logger.Warn("No Custom Role was found. Generating default File that will not be active until the next Reload");

        var syml = new SymlDocument();
        syml.Set("Role", new CustomRole()
        {
            Name = "ExampleRole",
            DisplayInfo = "Test",
            Enemies = new() { 0 },
            Friends = new() { 0 },
            GodMode = false,
            MaxAmount = 5,
            MaxHealth = 150,
            RoleId = 101,
            TeamId = 2,
            SetPlayerAtRoundStartChance = 100,
            RespawnReplace = new()
            {
                { (uint)RoleTypeId.NtfCaptain, 100 }
            },
            Health = 100,
            Role = RoleTypeId.ClassD,
            VisibleRole = RoleTypeId.None,
            ArtificialHealth = 0,
            MaxArtificialHealth = 75,
            EscapeRole = uint.MaxValue,
            PossibleInventories = new[]
            {
                new SerializedPlayerInventory()
                {
                    Ammo = new SerializedAmmo()
                    {
                        Ammo9 = 10,
                    },
                    Items = new List<SerializedPlayerItem>()
                    {
                        new()
                        {
                            ID = 1
                        }
                    }
                }
            },
            PossibleSpawns = new[]
            {
                new RoomPoint()
                {
                    roomName = "Shelter",
                    position = new SerializedVector3(0f, 2f, 0f),
                    rotation = new SerializedVector3(80f, 180f, 0f)
                }
            },
            MaxRespawnAmount = 10,
            MaxSpawnAmount = 10,
            SpawnHint = "Hello"
        });
        syml.Set("Scp",new ScpConfig()
        {
            ScpAttackDamage = 200
        });
        syml.Set("Advanced",new AdvancedConfig()
        {
            CommandToExecuteAtSpawn = new List<string>()
            {
                "pbc %player% 5 Hello"
            },
            CommandToExecuteAtDeSpawn = new List<string>()
            {
                "pbc %player% %playername% you are now dead"
            }
        });
        var file = _neuronBase.RelativePath("CustomRoles", "ExampleRole.syml");
        if (!File.Exists(file)) File.Create(file).Close();
        File.WriteAllText(file, syml.Dump(), Encoding.UTF8);
    }
}
