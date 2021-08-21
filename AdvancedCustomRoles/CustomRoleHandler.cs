using System.Collections.Generic;
using System.Linq;
using Synapse;
using Synapse.Api;
using Synapse.Config;
using System.IO;
using Synapse.Api.Roles;
using System;

namespace AdvancedCustomRoles
{
    public class CustomRoleHandler
    {
        public List<CustomRole> CustomRoles { get; } = new List<CustomRole>();

        public CustomRole GetCustomRole(int id) => CustomRoles.First(x => x.RoleID == id);

        public void Reload()
        {
            foreach (var role in CustomRoles)
                Server.Get.RoleManager.UnRegisterCustomRole(role.RoleID);
            CustomRoles.Clear();
            Load();
        }

        public void Load()
        {
            var shared = Path.Combine(Server.Get.Files.SharedConfigDirectory, "customroles");
            var local = Path.Combine(Server.Get.Files.ConfigDirectory, "customroles");

            if (!Directory.Exists(shared)) Directory.CreateDirectory(shared);
            if (!Directory.Exists(local)) Directory.CreateDirectory(local);

            var files = Directory.GetFiles(shared, "*.syml").ToList();
            files.AddRange(Directory.GetFiles(local, "*.syml"));

            if (files.Count == 0) CreateExampleFile(shared);

            foreach(var file in files)
            {
                try
                {
                    var syml = new SYML(file);
                    syml.Load();
                    if (syml.Sections.Count == 0)
                    {
                        Logger.Get.Warn($"[AdvancedCustomRoles] No Section found in {file}");
                        continue;
                    }
                    var section = syml.Sections.FirstOrDefault().Value;
                    var role = section.LoadAs<CustomRole>();
                    if (Server.Get.RoleManager.IsIDRegistered(role.RoleID))
                    {
                        Logger.Get.Warn($"[AdvancedCustomRoles] CustomRole: {role.Name} is invalid since it ID is already registered please use a different one");
                        continue;
                    }
                    CustomRoles.Add(role);
                    Server.Get.RoleManager.RegisterCustomRole(new RoleInformation(role.Name, role.RoleID, typeof(AdvancedRoleScript)));
                }
                catch(Exception e)
                {
                    Logger.Get.Error($"Error while loading CustomRole: {file}\n\n{e}");
                }
            }
        }

        public void CreateExampleFile(string path)
        {
            Logger.Get.Warn("[AdvancedCustomRoles] No Roles was found. Example File will be generated but not loaded until the next reload");

            var syml = new SYML(Path.Combine(path, "example.syml"));
            var section = new ConfigSection
            {
                Section = "Example"
            };
            var role = new CustomRole
            {
                Name = "ExampleRole",
                DisplayInfo = "<color=green>Example</color>",
                Enemies = new List<int> { 0 },
                Friends = new List<int> { 0 },
                Inventory = new SerializedPlayerInventory
                {
                    Ammo = new SerializedAmmo
                    {
                        Ammo5 = 50,
                        Ammo7 = 50,
                        Ammo9 = 50,
                    },
                    Items = new List<SerializedPlayerItem>
                    {
                        new SerializedPlayerItem(0, 0, 0, UnityEngine.Vector3.one, 100, false)
                    }
                },
                RemoveRoleName = true,
                RoleID = 25,
                Spawns = new List<SerializedMapPoint> { new SerializedMapPoint("EZ_Shelter", 0f, 2f, 0f), new SerializedMapPoint("EZ_Shelter", 2f, 2f, 0f) },
                RoundStartReplace = new Dictionary<int, float>(),
            };
            role.RoundStartReplace.Add(0, 100);
            section.Import(role);
            syml.Sections.Add("Example", section);
            syml.Store();
        }
    }
}
