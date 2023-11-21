using HarmonyLib;
using ChronoArkMod.Plugin;
using UnityEngine;
using ChronoArkMod.ModData;
using ChronoArkMod;
using ChronoArkMod.ModData.Settings;
using System.IO;
using ArkReplay.Json;
using System.Reflection;

namespace ArkReplay
{
    [PluginConfig(GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : ChronoArkPlugin
    {
        public const string GUID = "net.frostu8.ca.arkreplay";

        /// <summary>
        /// The directory to store saved runs.
        /// </summary>
        public static string ReplayDir { get; set; }

        private GameObject monoBehaviorHooks;

        private Harmony harmony;

        public override void Dispose()
        {
            // destroy run recorder and replayer
            Object.Destroy(monoBehaviorHooks);

            // undo patches
            harmony.UnpatchSelf();
        }

        public override void OnModSettingUpdate()
        {
            ModInfo info = ModManager.getModInfo(GUID);

            // get out dir
            var setting = info.GetSetting<InputFieldSetting>("OutDir");

            if (setting.Value.IsNullOrEmpty())
            {
                // set setting
                setting.Value = Path.Combine(Application.persistentDataPath, "Replays");
                info.SaveSetting();
            }

            ReplayDir = setting.Value;
        }

        public override void Initialize()
        {
            harmony = new Harmony(GUID);
            harmony.PatchAll();

            monoBehaviorHooks = new GameObject("ArkReplay");
            Object.DontDestroyOnLoad(monoBehaviorHooks);

            // create run recorder
            monoBehaviorHooks.AddComponent<RunRecorder>();
            // create run replayer
            monoBehaviorHooks.AddComponent<RunReplayer>();

            // register json types
            JsonTaggedConverter.RegisterAll(Assembly.GetExecutingAssembly());

            OnModSettingUpdate();
        }
    }
}